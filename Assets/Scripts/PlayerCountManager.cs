using UnityEngine;
using UnityEngine.UI;

public class PlayerCountManager : MonoBehaviour
{
    public Image playerCountImage;  // Assign the PlayerCount Image component
    public Sprite[] numberSprites;  // Drag & drop sprites (2-6) in the Inspector

    private int currentPlayerCount = 2;  // Default to 2

    void Start()
    {
        UpdatePlayerCount();  // Ensure correct sprite on start
    }

    public void IncreasePlayerCount()
    {
        if (currentPlayerCount < 6)
        {
            currentPlayerCount++;
            UpdatePlayerCount();
        }
    }

    public void DecreasePlayerCount()
    {
        if (currentPlayerCount > 2)
        {
            currentPlayerCount--;
            UpdatePlayerCount();
        }
    }

    private void UpdatePlayerCount()
    {
        playerCountImage.sprite = numberSprites[currentPlayerCount - 2];  
    }
}
