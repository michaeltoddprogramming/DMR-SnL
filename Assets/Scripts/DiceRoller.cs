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
    // public AudioSource audioSource2;
    // public AudioSource audioSource3;
    // public AudioSource audioSource4;
    // public AudioSource audioSource5;
    // public AudioSource audioSource6;
    // public AudioSource audioSource7;
    // public AudioSource audioSource8;
    // public AudioSource audioSource9;
    // public AudioSource audioSource10;
    // public AudioSource audioSource11;
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
        // audioSource = GetComponent<AudioSource>();
        // audioSource2 = GetComponent<AudioSource>();
        // audioSource3 = GetComponent<AudioSource>();
        // audioSource4 = GetComponent<AudioSource>();
        // audioSource5 = GetComponent<AudioSource>();
        // audioSource6 = GetComponent<AudioSource>();
        // audioSource7 = GetComponent<AudioSource>();
        // audioSource8 = GetComponent<AudioSource>();
        // audioSource9 = GetComponent<AudioSource>();
        // audioSource10 = GetComponent<AudioSource>();
        // audioSource11 = GetComponent<AudioSource>();

        GetComponent<SpriteRenderer>().gameObject.AddComponent<BoxCollider2D>();

        // audioSource = GetComponent<AudioSource>();

         if (audioSource == null)
    {
        Debug.LogError("AudioSource component is missing on the DiceRoller GameObject.");
    }
    }

    public void PlayRandomDiceRollSound()
    {
            // audioSource.PlayOneShot(diceRollSound);  // Play the single dice roll sound
        if (audioSource != null && diceRollSound != null)
        {
            audioSource.PlayOneShot(diceRollSound);  // Play the single dice roll sound
        }
        // if (diceRollSounds.Length > 0)
        // {
        //     int randomIndex = Random.Range(0, diceRollSounds.Length);  // Get a random index
        //     AudioClip clipToPlay = diceRollSounds[randomIndex]; // Select the clip
            
        //     // Play the sound at the position of the dice object
        //     AudioSource.PlayClipAtPoint(clipToPlay, transform.position);
        // }
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


        // int randomIndex = Random.Range(0, diceRollSounds.Length);

        // audioSource.PlayOneShot(diceRollSounds[randomIndex]);




        roll = temp;
        Animator animator = GetComponent<Animator>();
        
        
        PlayRandomDiceRollSound();

        
        // Start animation - will call AnimationStarted() via event
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

            PlayRandomDiceRollSound();


            // Start animation - should call AnimationStarted() via event
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