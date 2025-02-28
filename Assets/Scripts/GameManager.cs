using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerController playerController;

    public Color playerOneColor;
    public Color playerTwoColor;
    public int rollAmount;

    public bool rolled = false;


    // THIS AWAKE FUNCTION GUARDS THE SINGLETON INSTANCE OF THE GAME MANAGER
    // THIS IS TO MAKE SURE THAT THERE IS ONLY ONE INSTANCE OF THE GAME MANAGER
    private void Awake()
    {

        if(gameManager == null)
        {
            Debug.LogError("GameManager reference is misssing!");
        }


        // if (Instance == null)
        // {
        //     Instance = this;
        //     DontDestroyOnLoad(gameObject);
        // }
        // else
        // {
        //     Destroy(gameObject);
        // }

        // if(playerController == null)
        // {
        //     playerController = playerController.Instance;
        // }
    }

    public Color GetCurrentPlayerColor(bool isPlayerOneTurn)
    {
        return isPlayerOneTurn ? playerOneColor : playerTwoColor;
    }

    public void UpdateButtonColor(Button button, bool isPlayerOneTurn)
    {
        Color targetColor = GetCurrentPlayerColor(isPlayerOneTurn);
        targetColor.a = 1f;
        Debug.Log("UPDATE BUTTON COLOUR -> " + targetColor);

        button.GetComponent<Image>().color = targetColor;
    }

    public void setDiceRoll(int amount)
    {
        rollAmount = amount;

        playerController.allowMove(amount);
    }
}