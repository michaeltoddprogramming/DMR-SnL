using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Color playerOneColor;
    public Color playerTwoColor;

    // THIS AWAKE FUNCTION GUARDS THE SINGLETON INSTANCE OF THE GAME MANAGER
    // THIS IS TO MAKE SURE THAT THERE IS ONLY ONE INSTANCE OF THE GAME MANAGER
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
}