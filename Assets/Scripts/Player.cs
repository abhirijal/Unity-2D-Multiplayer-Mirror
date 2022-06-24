using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    private Rigidbody2D rb;// Start is called before the first frame update

    //Jumping required attribuets
    private bool canJump = false;
    private int numOfJumps;
    [SerializeField] private LayerMask touchableGround;
    // [SerializeField] private LayerMask JumpablePlayer;
    private BoxCollider2D boxColl;
    private SpriteRenderer playerSpriteRenderer;
    GameObject platCollid;
    BoxCollider2D platCollider;
    Rigidbody2D platRB;

    private GameObject blockCollider;

    private GameObject blockCollider2;

    private GameObject blockCollid;
    private GameObject blockCollid2;

    bool isTouchingBlock;
    public static NetworkConnection lastConnectionID;

    // public float lastXPos = -99f;

    public float Xvelocity;

    // private Rigidbody2D playerBlockCollid;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxColl = GetComponent<BoxCollider2D>();

        if (!isServer)
        {
            blockCollider = GameObject.Find("Block");
            blockCollider2 = GameObject.Find("Block2");
            // playerBlockCollid = blockCollider.GetComponent<Rigidbody2D>();
            // playerBlockCollid.bodyType = RigidbodyType2D.Kinematic;
        }

        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        // LVL TWO CODES
        // platCollid = GameObject.Find("MovingPlatform");
        // platCollider = platCollid.GetComponent<BoxCollider2D>();
        // platRB = platCollid.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Xvelocity = rb.velocity.x;

        if (Xvelocity > 4f)
        {
            playerSpriteRenderer.flipX = false;
        }
        else if (Xvelocity < -4f)
        {
            playerSpriteRenderer.flipX = true;
        }
        // lastXPos = transform.position.x;
        // runOnServer();
        // checkBlockPosition();
        // if (isServer) {
        //     checkServerBlockPos();
        // }
        // Checks if client has authority 
        if (!hasAuthority) { return; }
        isTouchingBlock = false;
        if (!isServer)
        {

            isTouchingBlock = (rb.IsTouching(blockCollider.GetComponent<CompositeCollider2D>()) || (rb.IsTouching(blockCollider2.GetComponent<CompositeCollider2D>())));
            // Debug.Log(isTouchingBlock);
            sendBlockPosToServer(blockCollider.transform.position.x, blockCollider.transform.position.y, blockCollider2.transform.position.x, blockCollider2.transform.position.y, isTouchingBlock);

        }

        // LEFT AND RIGHT MOVEMENT
        float valueOfAxis = Input.GetAxisRaw("Horizontal"); // returns 1 or -1 depending on where the player is facing
        rb.velocity = new Vector2(valueOfAxis * 5.05f, rb.velocity.y);
        // if ( valueOfAxis > 0 ) { playerSpriteRenderer.flipX = false; }
        // if ( valueOfAxis < 0 )  { playerSpriteRenderer.flipX = true; }

        // JUMP MOVEMENT
        if (Input.GetButtonDown("Jump"))
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

                rb.velocity = new Vector2(rb.velocity.x, 10f);
                numOfJumps++;
                if (numOfJumps == 2)  // enable double jumping
                {
                    canJump = false;
                }

            }
            // if (isOnTopOfPLayer()) {

            //     canJump = true;
            //     numOfJumps = 0;  
            // }        
        }

        // if (rb.IsTouching(platCollider))
        // { 
        //         Debug.Log(rb.IsTouching(platCollider));
        //         if (rb.velocity == new Vector2(0,0)) {
        //            rb.velocity = new Vector2(platRB.velocity.x,rb.velocity.y);
        //         } 
        // }
    }

    private bool isGrounded()
    {
        // if (!hasAuthority) {return false;}
        // Vector2 tempPosition = new Vector2(this.gameObject.transform.position.x + .01f, this.gameObject.transform.position.y + .01f);
        // function for all unless a player is below.
        return (Physics2D.BoxCast(boxColl.bounds.center, boxColl.bounds.size, 0f, Vector2.down, .1f, touchableGround) || bhaluJump());
    }

    private bool bhaluJump()
    {
        // function only if a player is below
        // CODE NOT OPTIMIZED - 2/2/22
        float tempx = transform.position.x;
        float tempy = transform.position.y;
        // fatfuck mote obese
        Vector2 playerLeftTuntun = new Vector2(tempx - .7f, tempy);
        Vector2 playerRightTuntun = new Vector2(tempx + .7f, tempy);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 50f);
        RaycastHit2D hit2 = Physics2D.Raycast(playerLeftTuntun, -Vector2.up, 50f);
        RaycastHit2D hit3 = Physics2D.Raycast(playerRightTuntun, -Vector2.up, 50f);
        float distance = 0;
        float distance2 = 0;
        float distance3 = 0;
        //If the collider of the object hit is not NUll
        if ((hit.collider != null) || (hit2.collider != null) || (hit3.collider != null))
        {
            if (hit2.collider == null)
            {
                if ((hit3.collider.tag == "Player"))
                {
                    distance3 = hit3.distance - 1f;
                    float finalDistance = Mathf.Min(distance3);
                    //Hit something, print the tag of the object
                    Debug.Log("Hitting: " + hit.collider.tag);
                    Debug.Log(finalDistance);
                    Debug.Log(distance + distance2 + distance3);
                    if ((finalDistance > .2))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else if (hit3.collider == null)
            {
                if ((hit2.collider.tag == "Player"))
                {
                    distance2 = hit2.distance - 1f;
                    float finalDistance = Mathf.Min(distance2);
                    //Hit something, print the tag of the object
                    Debug.Log("Hitting: " + hit.collider.tag);
                    Debug.Log(finalDistance);
                    Debug.Log(distance + distance2 + distance3);
                    if ((finalDistance > .2))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else if ((hit3.collider != null) && (hit2.collider != null))
            {
                if ((hit2.collider.tag == "Player") || (hit3.collider.tag == "Player"))
                {
                    distance2 = hit2.distance - 1f;
                    distance3 = hit3.distance - 1f;
                    float finalDistance = Mathf.Min(distance2, distance3);
                    //Hit something, print the tag of the object
                    Debug.Log("Hitting: " + hit.collider.tag);
                    Debug.Log(finalDistance);
                    Debug.Log(distance + distance2 + distance3);
                    if ((finalDistance > .2))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }

        }
        return false;
    }
    // private bool isOnTopOfPLayer() 
    // {
    //     return Physics2D.BoxCast(boxColl.bounds.center, boxColl.bounds.size, 0f, Vector2.down, .1f, JumpablePlayer);
    // }

    [ClientRpc]
    public void checkBlockPosition(float px)
    {
        GameObject newCollider;
        newCollider = GameObject.Find("Block");
        Debug.Log(newCollider.transform.position);
        newCollider.transform.position = new Vector2(px, newCollider.transform.position.y);
        // Debug.Log(blockCollider.transform.position);
        // Debug.Log(px);
    }

    [ServerCallback]
    public void checkServerBlockPos()
    {
        blockCollid = GameObject.Find("Block");
        // Debug.Log();
        checkBlockPosition(blockCollid.transform.position.x + 0.05f);
    }

    [Command]
    public void sendBlockPosToServer(float blockX, float blockY, float block2X, float block2Y, bool ifClientTouching)
    {

        if (ifClientTouching) { lastConnectionID = connectionToClient; }
        Debug.Log(lastConnectionID);
        if (connectionToClient == lastConnectionID)
        {
            blockCollid = GameObject.Find("Block");
            blockCollid2 = GameObject.Find("Block2");
            // Debug.Log();
            // blockCollid.transform.position = new Vector2(blockX,blockY);
            // blockCollid2.transform.position = new Vector2(block2X,block2Y);
            getBlockPosFromClients(blockX, blockY, block2X, block2Y);
        }
    }

    [ClientRpc(includeOwner = false)]
    public void getBlockPosFromClients(float blockX, float blockY, float block2X, float block2Y)
    {
        Debug.Log("received block co-ordinates from the server.");
        GameObject newCollider;
        GameObject newCollider2;
        newCollider = GameObject.Find("Block");
        newCollider2 = GameObject.Find("Block2");
        // Debug.Log();
        newCollider.transform.position = new Vector2(blockX, blockY);
        newCollider2.transform.position = new Vector2(block2X, block2Y);

    }
}
