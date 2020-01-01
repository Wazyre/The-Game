using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System;

public class PlayerCollisions : MonoBehaviour
{
  [Header("Offsets of Player")]
  [SerializeField] Vector2 bottomOffset = new Vector2(0.11f, 0);
  [SerializeField] Vector2 rightOffset = new Vector2(0.11f, 0);
  [SerializeField] Vector2 leftOffset = new Vector2(0.11f, 0);
  [SerializeField] float collisionRadius = 0.25f;
  [SerializeField] float interactRadius = 15f;

  [Space]

  [Header("Collision Checks")]
  [SerializeField] bool onGround;
  [SerializeField] bool nearInteracted;
  [SerializeField] bool onWall;
  [SerializeField] bool onRightWall;
  [SerializeField] bool onLeftWall;
  [SerializeField] int wallSide;
  [SerializeField] Transform groundTransform;

  [Space]

  [Header("Layers")]
  [SerializeField] LayerMask enemyLayer;
  [SerializeField] LayerMask groundLayer;
  [SerializeField] LayerMask interactableLayer;

  Rigidbody2D rb;

  void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
  }
  
  void Start()
  {
      groundTransform = transform.Find("groundTransform");
      groundLayer = LayerMask.GetMask("Ground");
      enemyLayer = LayerMask.GetMask("Enemy");
      interactableLayer = LayerMask.GetMask("Interactable");
  }

  void Update()
  {
      onGround = Physics2D.OverlapCircle((Vector2)transform.position+bottomOffset, collisionRadius, groundLayer);

      onRightWall = Physics2D.OverlapCircle((Vector2)transform.position+rightOffset, collisionRadius, groundLayer);
      onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position+leftOffset, collisionRadius, groundLayer);

      wallSide = onRightWall ? 1 : -1;

      nearInteracted = Physics2D.OverlapCircle(transform.position, interactRadius, interactableLayer);
      if(onGround) //If the player is standing on ground
      {
          //grounded = true;
          //inAir = false;
          //anim.SetBool("isJumping", false);
          //anim.SetBool("isIdle", true);
      }
  }

  void OnWall()
  {

  }

  void OnTriggerEnter2D(Collider2D other)
  {
      if(other.gameObject.tag == "Spikes")
      {
          //Adds a knockback to player if steps on spikes
          rb.AddForce(new Vector2(rb.velocity.x*-100, rb.velocity.y*-100));
          //rb.AddForce(new Vector2(, rb.velocity.y));
      }
  }

}
