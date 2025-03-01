using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // public static PlayerController Instance;
    public GameManager gameManager;
    public Rigidbody2D player;
    
    private bool canMove = false;
    private int moveAmount;
    private int count = 0;
    public float moveSpeed = 100f;

    private float pos1;
    private float pos2;

    void Start()
    {

        // if(gameManager == null)
        // {
        //     gameManager = GameManager.Instance;
        // }

    }

    void Update()
    {

        if(canMove == true && Input.GetKeyDown(KeyCode.RightArrow) && count < moveAmount)
        {
            Debug.Log("The player will move now");
            pos1 = player.position.x;
            count ++;
            movePlayer();
        }
        
        if(count > moveAmount)
        {
            count = 0;
            canMove = false;
            stopPlayer();
        }

        if(player.position.x >= (pos1 + 147))
        {
            stopPlayer();
        }
    }

    public void allowMove(int amount)
    {
        canMove = true;
        moveAmount = amount;

        Debug.Log("The player is allowed to move now!");

        // if(Input.GetKeyDown(KeyCode.RightArrow))
        // {
        //     player.linearVelocity = Vector2.right * 10;

        // }

    }

    public void movePlayer()
    {
        
        player.linearVelocity = Vector2.right * moveSpeed;

        Debug.Log("Player moved: " + count + "steps. was allowed to move: " + moveAmount);
    }

    public void stopPlayer()
    {
        player.linearVelocity = Vector2.zero;
    }
}
