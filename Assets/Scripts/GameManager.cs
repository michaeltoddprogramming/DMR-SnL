using UnityEngine;
using System.Collections.Generic;
using TMPro; // Add this directive for TMP_Text

public class GameManager : MonoBehaviour
{
    public enum PlayerColor
    {
        Red,
        Blue,
        Green,
        Yellow,
        Purple,
        Orange
    }

    public GameObject playerPrefab; // Prefab for player pieces
    public Transform[] playerStartPositions; // Array of start positions for players
    public PlayerColor[] playerColors; // Array of player colors
    public DiceRoller diceRoller; // Reference to the DiceRoller
    public GridManager gridManager; // Reference to the GridManager
    private int numberOfPlayers; // Default to 2 players

    private List<GameObject> players;
    private int currentPlayerIndex = 0;
    private Dictionary<int, int> snakesAndLadders = new Dictionary<int, int> {
            { 1, 22 },
            { 3, 22 },
            { 4, 22 },
            { 5, 22 },
            { 6, 22 },
            { 7, 33 },
            { 19, 76 },
            { 28, 8 },
            { 31, 67 },
            { 37, 14 },
            { 40, 78 },
            { 46, 4 },
            { 52, 32 },
            { 61, 36 },
            { 73, 97 },
            { 81, 99 },
            { 84, 94 },
            { 85, 53 },
            { 91, 69 },
            { 96, 24 },
            { 99, 69 }
    };

    void Start()
    {
        Debug.Log("GameManager Start() called.");
        numberOfPlayers = PlayerPrefs.GetInt("PlayerCount", 2);
        
        ValidateSetup();
        InitializePlayers();
        StartTurn();
    }

    void ValidateSetup()
    {
        Debug.Log($"Number of Players: {numberOfPlayers}");
        Debug.Log($"Number of Start Positions: {playerStartPositions.Length}");
        Debug.Log($"Number of Colors: {playerColors.Length}");

        if (playerStartPositions.Length < numberOfPlayers)
        {
            Debug.LogError("Not enough start positions assigned in the Inspector!");
        }

        if (playerColors.Length < numberOfPlayers)
        {
            Debug.LogError("Not enough colors assigned in the Inspector!");
        }

        for (int i = 0; i < playerStartPositions.Length; i++)
        {
            Debug.Log($"Start Position {i}: {playerStartPositions[i].position}");
        }
    }

    void InitializePlayers()
    {
        players = new List<GameObject>();

        for (int i = 0; i < numberOfPlayers; i++)
        {
            if (i >= playerStartPositions.Length)
            {
                Debug.LogError($"Not enough start positions! Tried to access index {i}, but only {playerStartPositions.Length} available.");
                return;
            }

            if (i >= playerColors.Length)
            {
                Debug.LogError($"Not enough colors! Tried to access index {i}, but only {playerColors.Length} available.");
                return;
            }

            Transform startPos = playerStartPositions[i];

            if (startPos == null)
            {
                Debug.LogError($"Start position at index {i} is NULL!");
                return;
            }

            GameObject player = Instantiate(playerPrefab, startPos.position, Quaternion.identity);
            players.Add(player);
            Debug.Log($"Player {i + 1} instantiated at position {player.transform.position}");

            // Assign color to player
            PlayerColor color = playerColors[i];
            Renderer renderer = player.GetComponentInChildren<Renderer>();

            if (renderer != null)
            {
                renderer.material.color = GetColor(color);
                Debug.Log($"Player {i + 1} color set to {color}");
            }
            else
            {
                Debug.LogError($"Renderer not found on player {i + 1}. Check if the playerPrefab has a Renderer component.");
            }

            // Initialize player's current position
            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.SetCurrentPosition(0); // Assuming starting position is 0
        }
    }

    Color GetColor(PlayerColor color)
    {
        switch (color)
        {
            case PlayerColor.Red:
                return Color.red;
            case PlayerColor.Blue:
                return Color.blue;
            case PlayerColor.Green:
                return Color.green;
            case PlayerColor.Yellow:
                return Color.yellow;
            case PlayerColor.Purple:
                return new Color(0.5f, 0f, 0.5f); // Purple
            case PlayerColor.Orange:
                return new Color(1f, 0.5f, 0f); // Orange
            default:
                return Color.white;
        }
    }

    void StartTurn()
    {
        Debug.Log($"Player {currentPlayerIndex + 1}'s turn.");
        diceRoller.SetDiceColor(GetColor(playerColors[currentPlayerIndex]));
        diceRoller.EnableDice();
    }

    public void OnDiceRollComplete(int roll)
    {
        Debug.Log($"Player {currentPlayerIndex + 1} rolled a {roll}.");
        
        // Move the player based on the roll
        MovePlayer(currentPlayerIndex, roll);
        
        // Move to the next player
        currentPlayerIndex = (currentPlayerIndex + 1) % numberOfPlayers;
        
        StartTurn();
    }

    void MovePlayer(int playerIndex, int roll)
    {
        // Get the current player's position
        PlayerController playerController = players[playerIndex].GetComponent<PlayerController>();
        int currentPosition = playerController.GetCurrentPosition();

        // Calculate the new position
        int newPosition = currentPosition + roll;

        // Find the tile with the new position
        Transform targetTile = FindTileWithNumber(newPosition);
        if (targetTile != null)
        {
            // Move the player to the new position
            playerController.MoveToPosition(targetTile.position, () =>
            {
                // Update the player's current position
                playerController.SetCurrentPosition(newPosition);

                // Check for snakes and ladders after reaching the new position
                if (snakesAndLadders.ContainsKey(newPosition))
                {
                    int finalPosition = snakesAndLadders[newPosition];
                    Transform finalTile = FindTileWithNumber(finalPosition);
                    if (finalTile != null)
                    {
                        playerController.MoveToPosition(finalTile.position, () =>
                        {
                            // Update the player's current position after moving to the final position
                            playerController.SetCurrentPosition(finalPosition);
                        });
                    }
                }
            });
        }
        else
        {
            Debug.LogError($"Tile with number {newPosition} not found!");
        }

        Debug.Log($"Moving player {playerIndex + 1} to position {newPosition}.");
    }

    Transform FindTileWithNumber(int number)
    {
        foreach (Transform tile in gridManager.transform)
        {
            Transform numberTextTransform = tile.Find("NumberText");
            if (numberTextTransform != null)
            {
                TMP_Text textComponent = numberTextTransform.GetComponent<TMP_Text>();
                if (textComponent != null && textComponent.text == number.ToString())
                {
                    return tile;
                }
            }
        }
        return null;
    }
}