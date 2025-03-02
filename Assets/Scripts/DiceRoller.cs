using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DiceRoller : MonoBehaviour
{
    int roll;
    private bool isDiceEnabled = false;
    private bool isAnimating = false;

    [SerializeField]
    List<Sprite> die;

    public GameManager gameManager; // Reference to the GameManager
    public AudioSource audioSource;
    public AudioClip diceRollSound;

    /// <summary>
    /// Called from animation event at the start of the dice roll animation
    /// </summary>
    public void AnimationStarted()
    {
        isAnimating = true;
        Debug.Log("Dice animation started");
    }

    /// <summary>
    /// Called from animation event at the end of the dice roll animation
    /// </summary>
    public void AnimationEnded()
    {
        isAnimating = false;
        Debug.Log("Dice animation ended");
        
        // Now that animation is complete, show result and notify GameManager
        ProcessRollResult();
    }

    private void Start()
    {
        GetComponent<SpriteRenderer>().gameObject.AddComponent<BoxCollider2D>();
    }

    public void PlayRandomDiceRollSound() {
        if (audioSource != null && diceRollSound != null)
        {
            audioSource.PlayOneShot(diceRollSound);
        }
    }

    private void OnMouseDown()
    {
        if (isDiceEnabled && !isAnimating)
        {
            Debug.Log("Dice clicked");
            RollDice();
        }
        else if (isAnimating)
        {
            Debug.Log("Dice is currently animating, please wait");
            
        }
        else if (!isDiceEnabled)
        {
            Debug.Log("Dice is not enabled, wait for your turn");
        }
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
        
        // Start animation - will call AnimationStarted() via event
        PlayRandomDiceRollSound();
        animator.Play("Roll", -1, 0f);

        
        // Don't call ShowRollResult() here - wait for AnimationEnded()

    }

    public void RollDice()
    {
        // Disable input during animation
        isDiceEnabled = false;
        
        roll = Random.Range(1, 7);
        Animator animator = GetComponent<Animator>();
        
        if (animator != null)
        {
            // Start animation - should call AnimationStarted() via event
            PlayRandomDiceRollSound();
            animator.Play("Roll", -1, 0f);
            
            // Fallback in case animation event isn't configured
            if (!animator.HasState(0, Animator.StringToHash("Roll")))
            {
                Debug.LogWarning("Roll animation state not found, using fallback");
                StartCoroutine(FallbackAnimation());
            }
        }
        else
        {
            Debug.LogWarning("No Animator found on dice, using fallback");
            StartCoroutine(FallbackAnimation());
        }
    }

    private IEnumerator FallbackAnimation()
    {
        AnimationStarted();
        
        // Simple fallback animation
        for (int i = 0; i < 10; i++)
        {
            RandomImage();
            yield return new WaitForSeconds(0.1f);
        }
        
        AnimationEnded();
    }

    private void ProcessRollResult()
    {
        SetImage();
        Debug.Log("Rolled a " + roll);
    
        // Check if gameManager reference is valid
        if (gameManager == null)
        {
            Debug.LogError("GameManager reference is lost or not set!");
            return;
        }
    
        // Notify the GameManager that the roll is complete
        gameManager.OnDiceRollComplete(roll);
    
        // Dice remains disabled until GameManager enables it for next turn
    }

    public void SetDiceColor(Color color)
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.color = color;
        }
    }

    public void EnableDice()
    {
        isDiceEnabled = true;
    }
}