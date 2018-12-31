using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMechanics : MonoBehaviour
{
    //float hMove;
    float bigJumpForce = 200f;
    float smallJumpForce = 50f;
    float normalSpeed = 10f;
    //float runningSpeed = 15f;
    float crouchingSpeed = 0.5f;
    float currentSpeed;
    float hitRange = 2f;
    float tempVelocity;
    float dashDelay = 0.0f;
    float interactRadius = 15f;
    float groundCheckDis = 0.11f; //Distane to check for ground
    bool grounded = true;
    bool inAir = false;
    bool crouching = false;
    bool flipCol = true;

    //GameObject player;
    GameObject cam;
    GameObject temp; //To hold a text gameobject
    Transform groundCheck;

    Animator anim;  //Player's animator
    Rigidbody2D rb; //Player's rigidbody
    SpriteRenderer playerSprite;
    LayerMask groundLayer;
    LayerMask enemyLayer;
    LayerMask interactableLayer;

    Text pressE;

    BoxCollider2D pCol; //The collider of the player

    PlayerControlMapping control;
    StopCamera stopCam;
    public Interactable focus;

    void Awake()
    {
      cam = GameObject.FindGameObjectWithTag("MainCamera");
      temp = GameObject.Find("Interact");
      pressE = temp.GetComponent<Text>();
      stopCam = cam.GetComponent<StopCamera>();
      rb = GetComponent<Rigidbody2D>();
      anim = GetComponent<Animator>();
      pCol = GetComponent<BoxCollider2D>();
      playerSprite = GetComponent<SpriteRenderer>();
      control = GetComponent<PlayerControlMapping>();

      rb.freezeRotation = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        //player = this.gameObject;
        groundCheck = transform.Find("GroundCheck");
        currentSpeed = normalSpeed;
        control.inputting = true;
        groundLayer = LayerMask.GetMask("Ground");
        enemyLayer = LayerMask.GetMask("Enemy");
        interactableLayer = LayerMask.GetMask("Interactable");
        pressE.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        FlipSprite();
        //hMove = Input.GetAxisRaw("Horizontal") * currentSpeed;
        Vector2 moveVelocity = new Vector2(control.xMove*currentSpeed, rb.velocity.y);

        rb.velocity = moveVelocity;

        if(control.xMove != 0 && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") == false && !crouching)
        {
          anim.SetBool("isRunning", true);
          anim.SetBool("isIdle", false);
        }
        else
        {
          anim.SetBool("isRunning", false);
          anim.SetBool("isIdle", true);
        }

//---------------------------------------------------------------

        if(control.run && dashDelay == 0)
        {
            dashDelay = 1.0f;
            anim.SetBool("isDashing", true);
            rb.AddForce(new Vector2(rb.velocity.x*300, 0));
        }
        else{
            anim.SetBool("isDashing", false);
        }

//-------------------------------------------------------------

        RaycastHit2D ground = Physics2D.Raycast(groundCheck.position, -transform.up, groundCheckDis, groundLayer);

        if(ground != null)
        {
            grounded = true;
            inAir = false;
            anim.SetBool("isJumping", false);
            anim.SetBool("isIdle", true);
        }

        if(control.jumpOn && grounded)
        {
            if(!inAir)
            {
                inAir = true;
                grounded = false;
                rb.AddForce(new Vector2(0, bigJumpForce));
                anim.SetBool("isJumping", true);
                anim.SetBool("isIdle", false);
                anim.SetBool("isRunning", false);
            }
        }
        else if(control.jumpOn && inAir)
        {
            inAir = false;
            rb.AddForce(new Vector2(0, smallJumpForce));
            anim.SetBool("isJumping", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isRunning", false);
        }
        else if (control.jumpOff)
        {
            if (rb.velocity.y > 0)
            {
                anim.SetBool("isJumping", false);
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }

        if(rb.velocity.y > tempVelocity)
        {
            anim.SetBool("JumpUp", true);
            anim.SetBool("JumpDown", false);
        }
        else if(rb.velocity.y < tempVelocity)
        {
            anim.SetBool("JumpUp", false);
            anim.SetBool("JumpDown", true);
        }

//----------------------------------------------------------------

        if(control.attack1)
        {
            anim.SetBool("isAttacking", true);
            Attack();
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }

//----------------------------------------------------------------

        if(control.crouch)
        {
            anim.SetBool("isCrouching", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isRunning", false);
            crouching = true;
        }
        else
        {
          anim.SetBool("isCrouching", false);
          crouching = false;
        }

        if(anim.GetBool("isCrouching"))
        {
            rb.velocity *= crouchingSpeed;
        }

//-----------------------------------------------------------------

        if(control.useItem)
        {
            //Interact();
        }

        Collider2D col2 = Physics2D.OverlapCircle(transform.position, interactRadius, interactableLayer);

        if(col2 != null)
        {
            if(!stopCam.stopFollow)
            {
                pressE.gameObject.SetActive(true);
                Interactable interactable = col2.gameObject.GetComponent<Interactable>();
                SetFocus(interactable);
                stopCam.StopFollow();
            }
        }
        else
        {
            if(stopCam.stopFollow)
            {
                stopCam.StopFollow();
                pressE.gameObject.SetActive(false);
                RemoveFocus();
            }
        }

//-----------------------------------------------------------------

        tempVelocity = rb.velocity.y;

        if(dashDelay > 0)
        {
            dashDelay -= Time.deltaTime;
        }
        else
        {
            dashDelay = 0.0f;
        }
    }

    /*void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Puddle")
        {
            groundCheck = true;
        }
    }*/

//-----------------------------------------------------------------

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Spikes")
        {

            rb.AddForce(new Vector2(rb.velocity.x*-100, rb.velocity.y*-100));
            //rb.AddForce(new Vector2(, rb.velocity.y));
        }
    }
    void FlipSprite(){
        if (control.xMove < 0){
            playerSprite.flipX = true;

            if(flipCol)
            {
                pCol.offset = new Vector2(-pCol.offset.x, pCol.offset.y);
                flipCol = false;
            }
        }
        else if (control.xMove > 0){
            playerSprite.flipX = false;
            if(!flipCol)
            {
                pCol.offset = new Vector2(-pCol.offset.x, pCol.offset.y);
                flipCol = true;
            }
        }
    }

    void Attack()
    {
        RaycastHit2D hit;

        if(flipCol)
        {
            hit = Physics2D.Raycast(transform.position, transform.right, hitRange, enemyLayer);
        }
        else
        {
            hit = Physics2D.Raycast(transform.position, -transform.right, hitRange, enemyLayer);
        }

        if(hit.collider != null)
        {
            hit.transform.gameObject.SendMessage("TakeDamage", 30);
            hit.transform.gameObject.SendMessage("Knockback", 200);
        }
    }

    void Interact()
    {
        Debug.Log("this");
    }

    void SetFocus(Interactable newFocus)
    {
        focus = newFocus;
    }

    void RemoveFocus()
    {
        focus = null;
    }
}
