using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    int roll;

    private bool isPlayerOneTurn = true;

    [SerializeField]
    List<Sprite> die;

    private void Start()
    {
        GetComponent<SpriteRenderer>().gameObject.AddComponent<BoxCollider2D>();

    }

    private void OnMouseDown()
    {
        Debug.Log("Dice clicked");
        RollDice();
    }

    public void RandomImage()
    {
        roll = Random.Range(1, 7);
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = die[roll - 1];
        Debug.Log("Random image set to " + roll);
    }

    public void SetImage()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = die[roll - 1];
    }

    public void Roll(int temp)
    {
        roll = temp;
        Animator animator = GetComponent<Animator>();
        animator.Play("Roll", -1, 0f);
        ShowRollResult();
    }

    public void RollDice()
    {
        roll = Random.Range(1, 7);
        Animator animator = GetComponent<Animator>();
        animator.Play("Roll", -1, 0f);
        ShowRollResult();
    }

    private void ShowRollResult()
    {
        SetImage();
        Debug.Log("Rolled a " + roll);
        
        string currentPlayer = isPlayerOneTurn ? "Player Uno" : "Player Dos";
        Debug.Log(currentPlayer + " rolled a... " + roll + " what a legend!");

        isPlayerOneTurn = !isPlayerOneTurn;
    }

    public int GetRoll()
    {
        Debug.Log("This function returns the final number: " + roll);
        return roll;
    }
}