using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // public static PlayerController Instance;
    public GameManager gameManager;
    public Rigidbody2D player1;
    public Rigidbody2D player2;

    
    private bool canMove = false;
    private int moveAmount;
    private int count1 = 0;
    private int count2 = 0;
    private int moveCount1 = 0;
    private int moveCount2 = 0;
    private int total1;
    private int total2 = 0;
    private int changeAmount1 = 0;
    private int changeAmount2 = 0;
    // private char direction1 = 'r';
    private char direction1;
    private char direction2;

    private int rolls = 0; 

    private bool first = false;




    public float moveSpeed = 100f;

    private float pos1;
    private float pos1Y;

    private float pos2;

    private int currPlayer;

    void Start()
    {

        // total1 = 0;
        // Debug.Log("2222222222222222222222222222222222222222222222222222222222222222222222222");
        

        // if(gameManager == null)
        // {
        //     gameManager = GameManager.Instance;
        // }

    }

    void Update()
    {

        // Debug.Log("1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111 + " + total1);
        // Debug.Log("1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111 + " + total1);

        //will set the direction to right for the first row!!!!        
        if(first)
        {
            direction1 = 'r';
            // Debug.Log("jjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjj");
        }
        // Debug.Log("_+_+_++__+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_++__+_++_+_: " + direction1);

        // if(total1 > 0)
        // {
        //     direction1 = 'u';
        //     Debug.Log("llllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllll");

        // }

        //will change direction to up when edge of board
        if((total1 + 1) % 10 == 0 && (direction1 == 'l' || direction1 == 'r'))
        {
            first = false;
            direction1 = 'u';
            Debug.Log("12121221221212212212121112122122121211212121221212121");
            // if(moveCount1 == moveAmount)
            // {
            //     // stopPlayer();
            //     direction1 = 'u';
            //     // movePlayer();
            // }
        }

        // if((total1 + 1) % 10 == 0)
        // {
        //     Debug.Log("lsiedrueghfsioedurhfvioueshgoiuwervnuoiehurhrnuevgoiunhrvioevnrhunurnruhenruenurvnurenuvrnuvreg");
        //     if(moveCount1 == moveAmount)
        //     {
        //         stopPlayer();
        //         direction1 = 'u';
        //         // movePlayer();
        //     }


        //     if(changeAmount1 % 2 == 0)
        //     {
        //         // direction1 = 'r';
        //     }
        //     else
        //     {
        //         // direction1 = 'l';
        //     }
        // }

        // if(canMove == true && Input.GetKeyDown(KeyCode.RightArrow) && count < moveAmount)
        if(canMove == true && Input.GetKeyDown(KeyCode.RightArrow))
        {
            // if(currPlayer == 1 && count1 < moveAmount)
            if(currPlayer == 1 && count1 != 1)
            // if(currPlayer == 1 && count1 < moveAmount)
            {
                Debug.Log("Player 1 will move now");
                pos1 = player1.position.x;
                pos1Y = player1.position.y;
                count1 ++;
                movePlayer();
                // int here = 0;
                // here ++;
                // Debug.Log("Here is here1: " + here);
            }
            // else if(currPlayer == 2 && count2 < moveAmount)
            else if(currPlayer == 2 && count2 != 1)
            {
                Debug.Log("Player 2 will move now");
                pos2 = player2.position.x;
                count2 ++;
                movePlayer();
            }
        }

        // if(moveCount1 != moveAmount)
        // {
        //     if((total1 + moveCount1) % 10 == 0)
        //     {
        //         if(moveAmount - moveCount1 >= 2)
        //         {
        //             stopPlayer();
        //             // direction1 = 'u'; sedliryedhugfosieyrgfiysehrigtuhseiurgh
        //             movePlayer();
        //             stopPlayer();

        //             if(changeAmount1 % 2 == 0)
        //             {
        //                 // direction1 = 'r';
        //             }
        //             else
        //             {
        //                 // direction1 = 'l';
        //             }
        //             movePlayer();
        //             stopPlayer();
        //         }
        //         else
        //         {
        //             direction1 = 'u';
        //             movePlayer();
        //             stopPlayer();

        //             if(changeAmount1 % 2 == 0)
        //             {
        //                 // direction1 = 'r';
        //             }
        //             else
        //             {
        //                 // direction1 = 'l';
        //             }
        //         }

        //     }
        // }

        
        
        // if(count1 == moveAmount)
        // {
        //     count1 = 0;
        //     canMove = false;
        //     stopPlayer();
        // }
        // if(count2 == moveAmount)
        // {
        //     count2 = 0;
        //     canMove = false;
        //     stopPlayer();
        // }

            // Debug.Log("000000000000000000000001: " + total1 + " " + count1);
        if((total1 + count1) % 10 == 0 && direction1 == 'l')
        {
            stopPlayer();
            direction1 = 'u';
            Debug.Log("00000000000000000000000: " + total1 + " " + count1);
        }

        if(currPlayer == 1 && canMove)
        {
            if((player1.position.x >= (pos1 + (moveAmount * 144))) && direction1 == 'r')
            {
                first = false;
                stopPlayer();
                // total1 += moveAmount;
                // Debug.Log("4444444444444444444444444444444444444444444444444444444444444444444444444444444444 + " + moveAmount);
            }

            // if((player1.position.y >= (pos1Y + (moveAmount * 144))) && direction1 == 'u')
            if((player1.position.y >= (pos1Y + 100)) && direction1 == 'u')
            {
                if(moveAmount - 1 == 0)
                {
                    // total1 += moveAmount;
                    stopPlayer();
                    Debug.Log("Moving the player up buy the set amount");
                    direction1 = 'l';
                }
                else
                {
                    direction1 = 'l';
                    Debug.Log("34343443434343434343434334: " + direction1 + " " + total1);
                    // stopPlayer();
                    Debug.Log("34343443434343434343434334: " + direction1 + " " + total1);
                    // total1 += moveAmount - (moveAmount - 1);
                    total1 += moveAmount;
                    Debug.Log("34343443434343434343434334: " + direction1 + " " + total1);
                    // total1 += moveAmount;
                    Debug.Log("thi is ehat it is: " + total1);
                    // moveAmount += moveAmount - 1;
                    Debug.Log("Moving the player up buy the set amount");
                    moveAmount -= 1;
                    canMove = true;
                    // pos1 = player1.position.x;
                    movePlayer();
                    // total1 -= moveAmount; 
                    // direction1 = 'l';
                }
            }

            



            Debug.Log("12123131332323232233233232323232323323: " + total1 + " " + count1 + " " +  (total1 + count1));
            
            if((player1.position.x <= (pos1 - (moveAmount * 144))) && direction1 == 'l')
            {
                Debug.Log("32323232233233232323232323323: " + total1 + " " + count1 + " " +  (total1 + count1));
                // if((total1 + count1 + 1) % 10 == 0)
                // {
                //     stopPlayer();
                //     direction1 = 'u';
                // }
                total1 -= moveAmount;
                stopPlayer();
                Debug.Log("Moving the player left buy the set amount--------------------------------------------------------------------------");
                // direction1 = 'l';
            }
            // count1 ++;
        }





        // else
        // {
        //     if(player2.position.x >= (pos2 + (moveAmount * 144)))
        //     {
        //         stopPlayer();
        //     }
        // }

    }

    // public void movePlayerUp()
    // {
    //     if(player == 1)
    //     {
    //         player1.linearVelocity = Vector2.up * moveSpeed;

    //         Debug.Log("Player 1 moved up.");

    //         stopPlayer();
    //     }
    //     else
    //     {
    //         player2.linearVelocity = Vector2.up * moveSpeed;

    //         Debug.Log("Player 2 moved up.");
    //         stopPlayer();
    //     }
    // }

    public void allowMove(int amount, int player)
    {
        Debug.Log("edsirugfhseiurhgiuserhgiuhertg: " + total1);
        canMove = true;
        moveAmount = amount;
        // count = 0;

        if(player == 1)
        {
            
            count1 = 0;
            // if(total1 == 0)
            if(rolls == 0)
            {
                rolls ++;
                first = true;
                Debug.Log("11111111111111111111111111111111111111111111111111111111111111111111111111111111");
            }
            // total1 += amount;
            Debug.Log("The total that player 1 has moved is: " + total1 + "--------------------------------------------");
        }
        else
        {
            total2 += amount;
            count2 = 0;
            Debug.Log("The total that player 2 has moved is: " + total2 + "---------------------------------------------");
        }

        currPlayer = player;

        Debug.Log("The player is allowed to move now! The player is: " + currPlayer);

        // if(Input.GetKeyDown(KeyCode.RightArrow))
        // {
        //     player.linearVelocity = Vector2.right * 10;

        // }

    }

    public void movePlayer()
    {
        // Debug.Log("The current direction is: " + direction1 + "+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
        if(currPlayer == 1)
        {
            count1 ++;
            // if(count1 < moveAmount)
            // {
                if(direction1 == 'r')
                // if(count1 == 1)
                {
                    // int here = 0;
                    player1.linearVelocity = Vector2.right * moveSpeed;
                    // here++;
                    Debug.Log("Player 1 moved right: " + count1 + "steps. was allowed to move: dsrf;oikugjhserdiuerdghipursehtgiuretgiuriuht----------------------------------------------------" + moveAmount);
                }
                else if(direction1 == 'l')
                {
                    player1.linearVelocity = Vector2.left * moveSpeed;
                    Debug.Log("Player 1 moved left: " + count1 + "steps. was allowed to move: " + moveAmount);
                }
                else if(direction1 == 'u')
                {
                    player1.linearVelocity = Vector2.up * moveSpeed;
                    Debug.Log("Player 1 moved up: " + count1 + "steps. was allowed to move: dlkiufbhgilusrebgiusdfiguhsdeiufehgisudrhfdrgiusrhtgiuhsrtiughsiruthgiuhrtgiuhriutghiurthgiuth" + moveAmount);
                }
                else if(direction1 == 'd')
                {
                    player1.linearVelocity = Vector2.down * moveSpeed;
                    Debug.Log("Player 1 moved down: " + count1 + "steps. was allowed to move: " + moveAmount);
                }

                // count1 ++;
            // }

            // if(count1 >= moveAmount)
            // {
            //     stopPlayer();
            // }
        }
        else
        {
            if(direction2 == 'r')
            {
                player2.linearVelocity = Vector2.right * moveSpeed;
                Debug.Log("Player 2 moved right: " + count2 + "steps. was allowed to move: " + moveAmount);
            }
            if(direction2 == 'l')
            {
                player2.linearVelocity = Vector2.left * moveSpeed;
                Debug.Log("Player 2 moved left: " + count2 + "steps. was allowed to move: " + moveAmount);
            }
            if(direction2 == 'u')
            {
                player2.linearVelocity = Vector2.up * moveSpeed;
                Debug.Log("Player 2 moved up: " + count2 + "steps. was allowed to move: " + moveAmount);
            }
            if(direction2 == 'd')
            {
                player2.linearVelocity = Vector2.down * moveSpeed;
                Debug.Log("Player 2 moved down: " + count2 + "steps. was allowed to move: " + moveAmount);
            }
            // player2.linearVelocity = Vector2.right * moveSpeed;

            // Debug.Log("Player 2 moved: " + count2 + "steps. was allowed to move: " + moveAmount);
        }        
    }

    public void stopPlayer()
    {
        if(currPlayer == 1)
        {
            player1.linearVelocity = Vector2.zero;

            if(direction1 == 'u')
            {
                Debug.Log("---------------------------------------------------------------------------+++++++++++++++++++++++++++++++");
                direction1 = 'l';
            }
            // else if(direction1 == 'l')
            // {
            //     total1 += moveAmount;
            //     Debug.Log("Total after move: " + total1);
            //     canMove = false;

            // }
            else
            {
                total1 += moveAmount;
                Debug.Log("Total after move: " + total1);
                canMove = false;
            }
        }
        else
        {
            player2.linearVelocity = Vector2.zero;
        }
    }
}