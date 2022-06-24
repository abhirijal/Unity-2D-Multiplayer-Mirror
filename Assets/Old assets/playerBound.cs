using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBound : MonoBehaviour
{
    public float x;
    public float y = 2.53f;
    //int frameCount = 0;
    bool canJump = true;
    public float playerSpeed = 1f;  //allows us to be able to change speed in Unity
    public Vector2 playerJumpHeight;
    int numOfJumps = 0;
    public Rigidbody2D rb;
    public bool grounded = true;
    //public bool canDoubleJump = true;
    public float maxXVelocity;
    public float maxYVelocity;
    public float currXVelocity;
    public float currYVelocity;
    public SpriteRenderer mySpriteRendererHead;
    public SpriteRenderer mySpriteRendererBody;
    public Animation anim;
    //public Collider2D playerCollider;
    //public Collider2D[] blockSurfaceCollider;
    private bool isOver = false;
    
    public BoxCollider2D boxColl;


    //finish point boolean
    public BoxCollider2D finishPoint;

    [SerializeField] private LayerMask touchableGround;

     
    
       void Awake()
    {
    }
    
    // Start is called before the first frame update
    void Start()
    {
        print("The game started now");
        //anim = gameObject.GetComponent<Animation>();
        //this.gameObject.transform.position = new Vector2(x,  y);
        
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if game is over

            if (this.gameObject.transform.position.y <= -14) {
                isOver = true;
            }
            if ( isOver == true) {
                this.gameObject.transform.position = new Vector2(-15f,-4f);
                isOver = false;
            }
            currXVelocity = rb.velocity.x;
            currYVelocity = rb.velocity.y;
            // for (int i = 0; i < blockSurfaceCollider.Length; i++)
            // {
            //     if (playerCollider.IsTouching(blockSurfaceCollider[i]))
            //     {
            //         grounded = true;
            //     }
            // }
            //current y and x positions
            float tempY = this.gameObject.transform.position.y ;
            float tempX = this.gameObject.transform.position.x;

            // left and right movement with arrow keys
            float X = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(7f * X, rb.velocity.y);
            if (  X > 0 ) { mySpriteRendererHead.flipX = false; }
            if ( X < 0 )  { mySpriteRendererHead.flipX = true; }
            
            
            
            /******************* OLD CODE ********************/
            // if (Input.GetKey(KeyCode.RightArrow) && currXVelocity <= maxXVelocity) // move right with right arrow key
            // {
            //     mySpriteRendererHead.flipX = false;
    

            //     rb.AddForce(new Vector2(playerSpeed, 0), ForceMode2D.Impulse);

            // }
            // if (Input.GetKey(KeyCode.LeftArrow) && -currXVelocity <= maxXVelocity) // move left
            // {
            //     mySpriteRendererHead.flipX = true;
        

            //     rb.AddForce(new Vector2(-playerSpeed, 0), ForceMode2D.Impulse);
            // }

    
    
    
            //jumps with UP key
            if (Input.GetButtonDown("Jump")) // jump
            {
                if (canJump == false && isGrounded())
                {
                    canJump = true;
                    numOfJumps = 0;

                }
                if (canJump == true && isGrounded())
                {
                    numOfJumps = 0;
                }
                if (canJump == true)
                {
                rb.velocity = new Vector2(rb.velocity.x, 25f);
                    
                    anim.Play("Jump");
                    
                    // if (rb.velocity.x > 10)
                    // {
                    //     rb.AddForce(new Vector2(-10,0), ForceMode2D.Impulse);
                    // }
                    // if (rb.velocity.x < -10)
                    // {
                    //     rb.AddForce(new Vector2(10, 0), ForceMode2D.Impulse);
                    // }
                    numOfJumps++;

                    if (numOfJumps == 2)  // enable double jumping
                    {
                        canJump = false;
                    }

                }
                
                // **********WORK DONE BEFORE FOR REFERENCE********


                /*if (this.gameObject.transform.position.y < y) { grounded = true; }
                if (grounded)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    rb.AddForce(playerJumpHeight, ForceMode2D.Impulse);
                    canDoubleJump = true;
                }
                else
                {
                    if (canDoubleJump)
                    {
                        canDoubleJump = false;
                        rb.velocity = new Vector2(rb.velocity.x, 0);
                        rb.AddForce(playerJumpHeight, ForceMode2D.Impulse);
                        grounded = false;
                    }
                }*/
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }


            /*float muji = this.gameObject.transform.position.y;
            float randi = this.gameObject.transform.position.x;


            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKey(KeyCode.RightArrow)) // move right with right arrow key
            {
                Vector2 position = transform.position;
                position.x+= 0.2f; 
                transform.position = position;
                //this.gameObject.transform.position.x = randi+1.5f;

            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKey(KeyCode.LeftArrow)) // move left with left arrow key
            {
                Vector2 position = transform.position;
                position.x-= 0.2f;
                transform.position = position;
                //this.gameObject.transform.position.x = randi+1.5f;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow)) // jump with up arrow key
            {
                print(canJump);
                print(muji);
                if (canJump == false && muji <= y )
                {
                    canJump = true;
                    numOfJumps = 0;
                
                }
                if (canJump == true && muji <= y)
                {
                    numOfJumps = 0;
                }
                if ( canJump == true)
                {
                    //this.gameObject.transform.position = new Vector2(position.x, muji + 3f);
                    Vector2 position = transform.position;
                    position.y += 2f;
                    transform.position = position;      
                    numOfJumps++;
                    
                    if ( numOfJumps == 2)  // enable double jumping
                    {
                        canJump = false;
                    }
                    
                }*/



            //grounded = false;
        
        //resets if touches the finish point
        if (rb.IsTouching(finishPoint)) { isOver = true;}

        
        
        
        
    }
    
    private bool isGrounded() 
    {
        return Physics2D.BoxCast(boxColl.bounds.center, boxColl.bounds.size, 0f, Vector2.down, .1f, touchableGround);
    }
    
    
    void FixedUpdate()
    {

    }


}
