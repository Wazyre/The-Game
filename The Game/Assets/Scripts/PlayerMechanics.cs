using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMechanics : MonoBehaviour
{
    //float hMove;
    float bigJumpForce = 200f;
    float smallJumpForce = 50f;
    float normalSpeed = 10f;
    float runningSpeed = 15f;
    float currentSpeed;
    float hitRange = 2f;

    bool grounded = true;
    bool inAir = false;

    GameObject player;
    Transform groundCheck;

    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer playerSprite;
    LayerMask groundLayer;

    PlayerControlMapping control;

    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        groundCheck = transform.Find("GroundCheck");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        currentSpeed = normalSpeed;
        control = GetComponent<PlayerControlMapping>();
        control.inputting = true;
        groundLayer = LayerMask.GetMask("Ground");
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        FlipSprite();
        //hMove = Input.GetAxisRaw("Horizontal") * currentSpeed;
        Vector2 moveVelocity = new Vector2(control.xMove*currentSpeed, rb.velocity.y);

        rb.velocity = moveVelocity;

        if(control.xMove != 0 && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") == false)
        {
          anim.SetBool("isRunning", true);
          anim.SetBool("isIdle", false);
        }
        else
        {
          anim.SetBool("isRunning", false);
          anim.SetBool("isIdle", true);
        }

        if(control.run)
        {
            currentSpeed = runningSpeed;
        }
        else{
            currentSpeed = normalSpeed;
        }

        Collider2D col = Physics2D.OverlapCircle(groundCheck.position, .06f, groundLayer);

        if(col != null)
        {
            grounded = true;
            inAir = false;
            anim.SetBool("isJumping", false);
            anim.SetBool("isIdle", true);
        }

        if(control.jump && grounded)
        {
            if(!inAir)
            {
                inAir = true;
                grounded = false;
                rb.AddForce(new Vector2(0, bigJumpForce));
                anim.SetBool("isJumping", true);
                anim.SetBool("isIdle", false);
            }
        }
        else if(control.jump && inAir)
        {
            inAir = false;
            rb.AddForce(new Vector2(0, smallJumpForce));
            anim.SetBool("isJumping", true);
            anim.SetBool("isIdle", false);
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
      }
      else if (control.xMove > 0){
        playerSprite.flipX = false;
      }
    }

    void Attack()
    {
        Debug.Log("There");
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
