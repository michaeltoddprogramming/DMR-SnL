using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinSceneManager : MonoBehaviour
{
    public TextMeshProUGUI winnerText;
    public Image colorDisplay;
    public string mainMenuScene = "Main Menu"; // Name of your main menu/start scene
    
    void Start()
    {
        // Get winner data from PlayerPrefs
        int playerNumber = PlayerPrefs.GetInt("WinningPlayerNumber", 1);
        string colorName = PlayerPrefs.GetString("WinningPlayerColor", "Red");
        
        // Set the winner text
        if (winnerText != null)
        {
            winnerText.text = $"Congratulations Player {playerNumber}!\n{colorName} wins!";
        }
        
        // Set the color display if there is one
        if (colorDisplay != null)
        {
            // Convert the string color name to an actual color
            Color displayColor = GetColorFromName(colorName);
            colorDisplay.color = displayColor;
        }
    }
    
    // Helper function to convert color name to Color
    Color GetColorFromName(string colorName)
    {
        switch (colorName)
        {
            case "Red":
                return Color.red;
            case "Blue":
                return Color.blue;
            case "Green":
                return Color.green;
            case "Yellow":
                return Color.yellow;
            case "Purple":
                return new Color(0.5f, 0f, 0.5f);
            case "Orange":
                return new Color(1f, 0.5f, 0f);
            default:
                return Color.white;
        }
    }
    
    // Button handler for "Play Again" or "Main Menu" buttons
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
    
    // Button handler for "Play Again" button
    public void PlayAgain()
    {
        // Load the game scene
        SceneManager.LoadScene("DMR SnL"); // Change to your actual game scene name
    }
}