using UnityEngine;
using UnityEngine.UI;

public class PlayerCountManager : MonoBehaviour
{
    public Image playerCountImage;  // Assign the PlayerCount Image component
    public Sprite[] numberSprites;  // Drag & drop sprites (2-6) in the Inspector

    private int currentPlayerCount = 2;  // Default to 2

    public AudioSource audioSource;
    public AudioClip clickSound;

    void Start()
    {
        UpdatePlayerCount();  // Ensure correct sprite on start
    }

    void Update()
    {
        Debug.Log("THis is the player count: " + currentPlayerCount);

        if(Input.GetKeyDown(KeyCode.RightArrow) && currentPlayerCount < 6)
        {
            IncreasePlayerCount();
            PlaySound();
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow) && currentPlayerCount > 2)
        {
            DecreasePlayerCount();
            PlaySound();
        }
    }

    private void PlaySound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound); 
        }
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

        PlayerPrefs.SetInt("PlayerCount", currentPlayerCount);
    }
}