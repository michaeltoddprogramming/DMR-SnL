using UnityEngine;
using UnityEngine.UI;

// OKAY SO RN EVERYTHING IS BASED ON THIS BUTTON CLICK
// I'M NOT SURE IF YOU WANT TO KEEP IT LIKE THIS OR IF YOU WANT TO CHANGE IT

public class DiceRoller : MonoBehaviour
{
    public Button RollDiceButton;
    public GameManager gameManager;

    private bool isPlayerOneTurn = true;

    void Start()
    {
        // if(gameManager == null)
        // {
        //     gameManager = GameManager.Instance;
        // }


        RollDiceButton.onClick.AddListener(RollDice);
        gameManager.UpdateButtonColor(RollDiceButton, isPlayerOneTurn);
    }

    public void RollDice()
    {
        // Debug.Log("The button was clicked");


        
        // int diceResult = Random.Range(1, 7);


        int diceResult = 9;
        gameManager.setDiceRoll(diceResult);

        string currentPlayer = isPlayerOneTurn ? "Player Uno" : "Player Dos";
        Debug.Log(currentPlayer + " rolled a... " + diceResult + " what a legend!");

        Debug.Log("The button was clicked");

     

        // TODO: MOVE PLAYER BASED ON DICERESULT - @Daniel @Ruan
        // NOT SURE IF YOU WILL NEED A CONTROLLER PER PLAYER OR JUST ONE CONTROLLER FOR BOTH
        // I JUST MADE AN EMPTY PLAYERCONTROLLER

        isPlayerOneTurn = !isPlayerOneTurn;
        gameManager.UpdateButtonColor(RollDiceButton, isPlayerOneTurn);
    }
}