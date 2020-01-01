using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator anim;
    PlayerMovement move;
    PlayerCollisions colls;
    SpriteRenderer pSprite;

    void Start()
    {
        anim = GetComponent<Animator>();
        move = GetComponent<PlayerMovement>();
        colls = GetComponent<PlayerCollisions>();
        pSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        anim.SetBool("onGround", colls.onGround);
        anim.SetBool("onWall", colls.onWall);
        anim.SetBool("wallGrab", move.wallGrab);
        anim.SetBool("wallSlide", move.wallSlide);
        anim.SetBool("isDashing", move.isDashing);
        anim.SetBool("isWalking", move.isWalking);
        anim.SetBool("isCrouching", move.isCrouching);
        anim.SetBool("isJumping", move.isJumping);
        anim.SetBool("isFalling", move.isFalling);
        anim.SetBool("isIdle", move.isFalling);
    }

    public void SetHorizontalMovement(float x, float y, float ySpeed)
    {
        anim.SetFloat("HorizontalAxis", x);
        anim.SetFloat("VerticalAxis", y);
        anim.SetFloat("VerticalVelocity", ySpeed);
    }

    public void SetTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }
}
