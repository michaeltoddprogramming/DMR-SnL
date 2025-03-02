using UnityEngine;
using System.Collections.Generic;

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

    // public PlayerCount numberOfPlayers ; // Default number of players
    public GameObject playerPrefab; // Prefab for player pieces
    public Transform[] playerStartPositions; // Array of start positions for players
    public PlayerColor[] playerColors; // Array of player colors
    public DiceRoller diceRoller; // Reference to the DiceRoller

    private List<GameObject> players;
    private int currentPlayerIndex = 0;
    private int numberOfPlayers;


    void Start()
    {
        Debug.Log("GameManager Start() called.");
        numberOfPlayers = PlayerPrefs.GetInt("PlayerCount", 2);
        Debug.Log("This is the player count in scene 2: " + numberOfPlayers);
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
            Debug.Log("THis is spawning a polayer");
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
        
        // Debug: Print current player index before incrementing
        Debug.Log($"Current player index before increment: {currentPlayerIndex}");
        
        // Move to the next player
        currentPlayerIndex = (currentPlayerIndex + 1) % numberOfPlayers;
        
        // Debug: Print current player index after incrementing
        Debug.Log($"Current player index after increment: {currentPlayerIndex}");
        
        StartTurn();
    }
}