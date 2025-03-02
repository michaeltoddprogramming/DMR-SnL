using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro; // Add this directive for TMP_Text
using UnityEngine.SceneManagement;

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
    public Transform[] sidebarPositions; // Array of positions for sidebar player pieces
    public PlayerColor[] playerColors; // Array of player colors
    public DiceRoller diceRoller; // Reference to the DiceRoller
    public GridManager gridManager; // Reference to the GridManager
    public Transform sidebarParent; // Parent transform for sidebar player pieces
    public float sidebarPlayerScale = 3f; // How much larger the sidebar pieces should be
    public float sidebarSpacing = 2.0f; // Vertical spacing between sidebar players
    public Material haloMaterial; // Material for highlighting the current player
    private int numberOfPlayers; // Default to 2 players
    public string winSceneName = "Main Menu"; // Name of your win scene
    private bool gameOver = false;
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public AudioClip ladderSound1;
    public AudioClip weeSound;

    private List<GameObject> players;
    private List<GameObject> sidebarPlayers = new List<GameObject>(); // For sidebar player pieces
    private int currentPlayerIndex = 0;
    private Dictionary<int, int> snakesAndLadders = new Dictionary<int, int> {
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
    }; 

    void Start()
    {
        Debug.Log("GameManager Start() called.");
        numberOfPlayers = PlayerPrefs.GetInt("PlayerCount", 2);
        
        // Create sidebar parent if not assigned
        if (sidebarParent == null)
        {
            GameObject sidebar = new GameObject("SidebarParent");
            sidebarParent = sidebar.transform;
            sidebarParent.position = new Vector3(-5, 0, 0); // Position on left side
        }
        
        ValidateSetup();
        InitializePlayers();
        StartTurn();
    }

    void PlayLadderSound()
    {
        if (audioSource1 != null && ladderSound1 != null)
        {
            audioSource1.PlayOneShot(ladderSound1);
        }
        else
        {
            Debug.LogWarning("audioSource1 or ladderSound1 not assigned.");
        }

    }

    // All your existing methods remain the same

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
        sidebarPlayers.Clear(); // Clear any existing sidebar players

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

            // Create regular player piece (your existing code)
            GameObject player = Instantiate(playerPrefab, startPos.position, Quaternion.identity);
            players.Add(player);
            Debug.Log($"Player {i + 1} instantiated at position {player.transform.position}");

            // Assign color to player (your existing code)
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

            // Initialize player's current position (your existing code)
            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.SetCurrentPosition(0); // Assuming starting position is 0

            // NEW: Create sidebar player piece using either predefined positions or calculation
            Vector3 sidebarPos;
            
            // Use predefined positions if available
            if (sidebarPositions != null && i < sidebarPositions.Length && sidebarPositions[i] != null)
            {
                sidebarPos = sidebarPositions[i].position;
            }
            else
            {
                // Fall back to calculated position
                sidebarPos = sidebarParent.position + new Vector3(0, i * sidebarSpacing, 0);
            }
            
            GameObject sidebarPlayer = Instantiate(playerPrefab, sidebarPos, Quaternion.identity);
            sidebarPlayer.name = $"SidebarPlayer_{i+1}";
            sidebarPlayer.transform.parent = sidebarParent;
            
            // Scale up the sidebar piece
            sidebarPlayer.transform.localScale *= sidebarPlayerScale;
            
            // Apply the same color
            Renderer sidebarRenderer = sidebarPlayer.GetComponentInChildren<Renderer>();
            if (sidebarRenderer != null)
            {
                sidebarRenderer.material.color = GetColor(color);
            }
            
            // Add number label
            GameObject numberObj = new GameObject($"Number_{i+1}");
            numberObj.transform.parent = sidebarPlayer.transform;
            numberObj.transform.localPosition = new Vector3(1.0f, 0, 0); // Position to the right of piece
            TextMeshPro numberText = numberObj.AddComponent<TextMeshPro>();
            numberText.text = $"{i+1}";
            numberText.fontSize = 5;
            numberText.alignment = TextAlignmentOptions.Center;
            numberText.color = Color.white;
            
            // Keep track of sidebar player
            sidebarPlayers.Add(sidebarPlayer);
        }
    }

    // Rest of your existing methods remain unchanged

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
        
        // Optional: Highlight current player's sidebar piece
        HighlightCurrentSidebarPlayer();
    }
    
    // NEW: Method to highlight the current player's sidebar piece
    void HighlightCurrentSidebarPlayer()
{
    for (int i = 0; i < sidebarPlayers.Count; i++)
    {
        Transform halo = sidebarPlayers[i].transform.Find("Halo");
        
        // For the current player
        if (i == currentPlayerIndex)
        {
            // Create a halo if it doesn't exist
            if (halo == null)
            {
                GameObject haloObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                haloObj.name = "Halo";
                haloObj.transform.parent = sidebarPlayers[i].transform;
                haloObj.transform.localPosition = Vector3.zero;
                haloObj.transform.localScale = Vector3.one * 1.5f; // Larger for visibility
                
                // Make it glow with player's color
                Renderer haloRenderer = haloObj.GetComponent<Renderer>();
                if (haloRenderer != null)
                {
                    // Get player's color but make it much brighter
                    Color playerColor = GetColor(playerColors[i]);
                    Color glowColor = new Color(
                        Mathf.Clamp01(playerColor.r * 2.5f),
                        Mathf.Clamp01(playerColor.g * 2.5f),
                        Mathf.Clamp01(playerColor.b * 2.5f),
                        0.6f // More opaque for stronger effect
                    );
                    
                    // Use custom material if provided
                    if (haloMaterial != null)
                    {
                        haloRenderer.material = new Material(haloMaterial);
                        haloRenderer.material.color = glowColor;
                    }
                    else
                    {
                        // Create Standard transparent material with emission
                        haloRenderer.material = new Material(Shader.Find("Standard"));
                        haloRenderer.material.color = glowColor;
                        
                        // Make it glow with emission
                        haloRenderer.material.EnableKeyword("_EMISSION");
                        haloRenderer.material.SetColor("_EmissionColor", glowColor * 1.5f);
                        
                        // Make it transparent
                        haloRenderer.material.SetFloat("_Mode", 3); // Transparent mode
                        haloRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                        haloRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                        haloRenderer.material.SetInt("_ZWrite", 0);
                        haloRenderer.material.DisableKeyword("_ALPHATEST_ON");
                        haloRenderer.material.DisableKeyword("_ALPHABLEND_ON");
                        haloRenderer.material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                        haloRenderer.material.renderQueue = 3000;
                    }
                }
                
                // Disable collider
                Collider haloCollider = haloObj.GetComponent<Collider>();
                if (haloCollider != null)
                {
                    haloCollider.enabled = false;
                }
                
                // Start pulsing animation
                StartCoroutine(PulseHalo(haloObj));
            }
            else
            {
                halo.gameObject.SetActive(true);
            }
        }
        // For other players, hide the halo if it exists
        else if (halo != null)
        {
            halo.gameObject.SetActive(false);
        }
    }
}

IEnumerator PulseHalo(GameObject halo)
{
    if (halo == null) yield break;
    
    float minScale = 1.4f;
    float maxScale = 1.7f;
    float pulseSpeed = 2.0f;
    
    Vector3 originalScale = halo.transform.localScale;
    float baseSize = originalScale.x / 1.5f;
    
    while (halo != null && halo.activeInHierarchy)
    {
        // Pulsate between min and max scale
        float pulseFactor = Mathf.PingPong(Time.time * pulseSpeed, 1.0f);
        float currentScale = Mathf.Lerp(minScale, maxScale, pulseFactor);
        
        halo.transform.localScale = new Vector3(
            baseSize * currentScale,
            baseSize * currentScale,
            baseSize * currentScale
        );
        
        yield return null;
    }
}

        public void OnDiceRollComplete(int roll)
    {
        Debug.Log($"Player {currentPlayerIndex + 1} rolled a {roll}.");
        
        // Move the player based on the roll
        MovePlayer(currentPlayerIndex, roll);
        
        // Skip to next player only if game isn't over
        if (!gameOver)
        {
            // Move to the next player
            currentPlayerIndex = (currentPlayerIndex + 1) % numberOfPlayers;
            
            StartTurn();
        }
    }

       void MovePlayer(int playerIndex, int roll)
    {
        // Get the current player's position
        PlayerController playerController = players[playerIndex].GetComponent<PlayerController>();
        int currentPosition = playerController.GetCurrentPosition();
    
        // Calculate the new position
        int newPosition = currentPosition + roll;
    
        // Handle bouncing back from 100
        if (newPosition > 100)
        {
            int overshoot = newPosition - 100;
            newPosition = 100 - overshoot;
            Debug.Log($"Player {playerIndex + 1} overshot by {overshoot} and bounced back to {newPosition}");
        }
    
        // Check if this is a winning move (exactly position 100)
        bool isWin = newPosition == 100;
    
        // Find the tile with the new position
        Transform targetTile = FindTileWithNumber(newPosition);
        if (targetTile != null)
        {
            // Move the player to the new position
            playerController.MoveToPosition(targetTile.position, () =>
            {
                // Update the player's current position
                playerController.SetCurrentPosition(newPosition);
    
                // Check for win
                if (isWin)
                {
                    HandleWin(playerIndex);
                    return; // Skip snake/ladder check if player won
                }
    
                // Check for snakes and ladders after reaching the new position
                if (snakesAndLadders.ContainsKey(newPosition))
                {
                    int finalPosition = snakesAndLadders[newPosition];
                    Transform finalTile = FindTileWithNumber(finalPosition);
                    if (finalTile != null)
                    {
                        bool flag = false;
                        bool flag1 = false;
                        if(newPosition < finalPosition)
                        {
                            flag = true;

                            PlayLadderSound();
                        }

                        if (finalPosition < newPosition && audioSource2 != null && weeSound != null)
                        {
                            flag1 = true;
                            audioSource2.PlayOneShot(weeSound);
                            Debug.Log("_wee__");
                        }

                        playerController.MoveToPosition(finalTile.position, () =>
                        {
                            // Update the player's current position after moving to the final position
                            if(flag != true && newPosition < finalPosition)
                            {
                                PlayLadderSound();
                                flag = false;
                            }

                            if (finalPosition < newPosition && audioSource2 != null && weeSound != null && flag1 != true)
                            {
                                audioSource2.PlayOneShot(weeSound);
                                flag1 = false;
                            }
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

    void HandleWin(int playerIndex)
{
    gameOver = true;
    Debug.Log($"Player {playerIndex + 1} wins!");
    
    // Save the winner info to PlayerPrefs to access in the win scene
    PlayerPrefs.SetInt("WinningPlayerNumber", playerIndex + 1);
    PlayerPrefs.SetString("WinningPlayerColor", playerColors[playerIndex].ToString());
    
    // Create a celebration effect before transitioning
    StartCoroutine(CelebrateAndTransition(playerIndex));
}

IEnumerator CelebrateAndTransition(int playerIndex)
{
    // Get the winning player
    GameObject winningPlayer = players[playerIndex];
    
    // Do some celebration effects
    float celebrationTime = 3.0f;
    float startTime = Time.time;
    Vector3 originalScale = winningPlayer.transform.localScale;
    
    while (Time.time - startTime < celebrationTime)
    {
        // Make the player pulse
        float pulse = 1.0f + 0.2f * Mathf.Sin((Time.time - startTime) * 10f);
        winningPlayer.transform.localScale = originalScale * pulse;
        
        yield return null;
    }
    
    // Restore original scale
    winningPlayer.transform.localScale = originalScale;
    
    // Load the win scene
    SceneManager.LoadScene(winSceneName);
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