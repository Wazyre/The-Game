using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    bool grounded = true;
    bool inAir = false;
    bool crouching = false;
    bool flipCol = true;

    GameObject player;
    Transform groundCheck;

    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer playerSprite;
    LayerMask groundLayer;
    BoxCollider2D pCol;

    PlayerControlMapping control;

    void Awake()
    {
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
        player = this.gameObject;
        groundCheck = transform.Find("GroundCheck");
        currentSpeed = normalSpeed;
        control.inputting = true;
        groundLayer = LayerMask.GetMask("Ground");
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

        if(control.run && dashDelay == 0)
        {
            dashDelay = 1.0f;
            anim.SetBool("isDashing", true);
            rb.AddForce(new Vector2(rb.velocity.x*300, 0));
        }
        else{
            anim.SetBool("isDashing", false);
        }

        Collider2D col = Physics2D.OverlapCircle(groundCheck.position, .06f, groundLayer);

        if(col != null)
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

        if(control.attack1)
        {
            anim.SetBool("isAttacking", true);
            Attack();
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }

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
        Vector2 direction = new Vector2(1, 0);
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, direction, hitRange);
        Debug.Log(hit.transform.gameObject.tag);
        if(hit.transform.gameObject.tag == "Enemy")
        {
            Debug.Log("Here");
            hit.transform.gameObject.SendMessage("TakeDamage", 30);
            hit.transform.gameObject.SendMessage("Knockback", 200);
        }
    }

}
