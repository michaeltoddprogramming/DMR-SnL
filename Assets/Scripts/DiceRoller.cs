using UnityEngine;
using UnityEngine.UI;

// OKAY SO RN EVERYTHING IS BASED ON THIS BUTTON CLICK
// I'M NOT SURE IF YOU WANT TO KEEP IT LIKE THIS OR IF YOU WANT TO CHANGE IT

public class DiceRoller : MonoBehaviour
{
    public Button rollDiceButton;

    private bool isPlayerOneTurn = true;

    void Start()
    {
        rollDiceButton.onClick.AddListener(RollDice);
        GameManager.Instance.UpdateButtonColor(rollDiceButton, isPlayerOneTurn);
    }

    void RollDice()
    {
        int diceResult = Random.Range(1, 7);
        string currentPlayer = isPlayerOneTurn ? "Player Uno" : "Player Dos";
        Debug.Log(currentPlayer + " rolled a... " + diceResult + "what a legend!");

        // TODO: MOVE PLAYER BASED ON DICERESULT - @Daniel @Ruan
        // NOT SURE IF YOU WILL NEED A CONTROLLER PER PLAYER OR JUST ONE CONTROLLER FOR BOTH
        // I JUST MADE AN EMPTY PLAYERCONTROLLER

        isPlayerOneTurn = !isPlayerOneTurn;
        GameManager.Instance.UpdateButtonColor(rollDiceButton, isPlayerOneTurn);
    }
}