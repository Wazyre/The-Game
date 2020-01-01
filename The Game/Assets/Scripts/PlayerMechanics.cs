using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System;

[System.Serializable]
public class PlayerMechanics : MonoBehaviour
{
    //Speeds and jump strengths of player
    [Header("Vertical Movement")]
    [SerializeField] float bigJumpSpeed = 200f;
    [SerializeField] float smallJumpMod = 2f;
    [SerializeField] float fallingMod = 2.5f;
    [SerializeField] float wallJumpLerp = 10f;
    [SerializeField] bool inAir = false;

    [Space]

    [Header("Horizontal Movement")]
    [SerializeField] float normalSpeed = 15f;
    [SerializeField] float dashingSpeed = 25f;
    [SerializeField] float crouchingMod = 0.5f;
    [SerializeField] float dashDelay = 0.5f; //Delay between each dash
    [SerializeField] float currentSpeed;
    [SerializeField] bool crouching = false;

    [Space]

    [Header("Booleans")]
    bool onGround;
    bool wallGrab;
    bool wallJumped;
    bool isDashing;
    bool isAttacking;
    bool isWalking;
    bool isJumping;
    bool isFalling;
    bool isCrouching;
    bool isIdle;
    bool hasDashed;

    [Space]

    //float hitRange = 2f;
    float tempVelocity;

    bool flipCol = true; //True if facing right, false if facing left

    [Header("Attacking")]
    [SerializeField] string power1;
    [SerializeField] string power2;
    [SerializeField] string currentPower; //Power used at moment of attack

    //Dictionary of "discovered" and enabled powers
    [SerializeField] Dictionary<string, bool> powers = new Dictionary<string, bool>
    {
        {"Claw", false}, {"Shotgun", false}, {"Scythe", false}, {"Spring", false}
    };

    //For power stats, first int is damage followed by range, knockback...
    [SerializeField] Dictionary<string, List<int>> powerStats = new Dictionary<string, List<int>>
    {
        {"Claw", new List<int>(){10, 2, 0}}, {"Shotgun", new List<int>(){30, 5, 5}},
        {"Scythe", new List<int>(){20, 4, 2}}, {"Spring", new List<int>(){0, 0, 0}}
    };
    [SerializeField] float timeSinceAttack = 0f;
    [SerializeField] float attackDelay = 1f;

    GameObject cam;
    GameObject inventoryMenu;

    Rigidbody2D rb; //Player's rigidbody
    SpriteRenderer playerSprite;

    Text interactText;

    BoxCollider2D pCol; //The collider of the player

    PlayerControlMapping control;
    CameraFollow camFol;
    PlayerCollisions collisions;
    public Interactable focus;
    LayerMask interactableLayer;
    float interactRadius = 15f;

    LayerMask enemyLayer;


    void Awake()
    {
      cam = GameObject.FindGameObjectWithTag("MainCamera");
      interactText = GameObject.Find("Interact").GetComponent<Text>();
      inventoryMenu = GameObject.FindGameObjectWithTag("InventoryMenu");
      camFol = cam.GetComponent<CameraFollow>();
      rb = GetComponent<Rigidbody2D>();
      pCol = GetComponent<BoxCollider2D>();
      playerSprite = GetComponent<SpriteRenderer>();
      control = GetComponent<PlayerControlMapping>();
      collisions = GetComponent<PlayerCollisions>();

      rb.freezeRotation = true; //Prevents sprite from flipping
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = normalSpeed;
        control.StartInput();
        interactText.gameObject.SetActive(false);
        interactableLayer = LayerMask.GetMask("Interactable");
        enemyLayer = LayerMask.GetMask("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        FlipSprite();
        Walk();
        Run();
        Jump();
        Crouch();
        Interact();
        Attack();
        Inventory();
        SavenLoad();
    }

//-----------------------------------------------------------------
    //Flips sprite depending on direction player is facing
    void FlipSprite()
    {
        if (control.xMove < 0) //If moving left
        {
            playerSprite.flipX = true;

            if(flipCol)
            {
                pCol.offset = new Vector2(-pCol.offset.x, pCol.offset.y);
                flipCol = false;
            }
        }
        else if (control.xMove > 0) //If moving right
        {
            playerSprite.flipX = false;

            if(!flipCol)
            {
                pCol.offset = new Vector2(-pCol.offset.x, pCol.offset.y);
                flipCol = true;
            }
        }
    }

    void Walk()
    {
        if(!control.inputting)
        {
          return;
        }

        if(!wallJumped)
        {
            //Calculates velocity based on speed and direction faced
            rb.velocity = new Vector2(control.xMove*normalSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(control.xMove * normalSpeed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        }


        //Play out appropraiate animations
        if(control.xMove != 0 && !isAttacking && !crouching && !inAir)
        {
          isWalking = true;
        }
        else
        {
          isWalking = false;
        }
    }

    void Run()
    {
        //If running, the player gets a dash with a delay of 1 second
        if(control.run && dashDelay == 0)
        {
            dashDelay = 1.0f; //Amount of delay in seconds between dashes
            isDashing = true; //Dashing animation on
            rb.AddForce(new Vector2(rb.velocity.x*300, 0)); //Dashing force
        }
        else
        {
            isDashing = false; //Dashing animation off
        }

        //Counts down time between dashes
        if(dashDelay > 0)
        {
            dashDelay -= Time.deltaTime;
        }
        else
        {
            dashDelay = 0.0f;
        }
    }

    void Jump()
    {
        if(control.jumpOn)
        {
          if(rb.velocity.y < 0)
          {
              rb.velocity += Vector2.up * Physics2D.gravity.y * (fallingMod - 1) * Time.deltaTime;
          }
          else if(rb.velocity.y > 0 && !control.jumpOn)
          {
              rb.velocity += Vector2.up * Physics2D.gravity.y * (smallJumpMod - 1) * Time.deltaTime;
          }
        }

        /*
        //Animation and forces for first jump
        if(control.jumpOn && grounded)
        {
            if(!inAir)
            {
                inAir = true;
                grounded = false;
                rb.velocity += new Vector2(0, bigJumpForce);
                anim.SetBool("isJumping", true);
                anim.SetBool("isIdle", false);
                anim.SetBool("isRunning", false);
            }
        }

        //Animation and forces for a double jump if enabled
        else if(control.jumpOn && inAir)
        {
            inAir = false;
            rb.velocity += new Vector2(0, smallJumpForce);
            anim.SetBool("isJumping", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isRunning", false);
        }
        else if (control.jumpOff)
        {
            if (rb.velocity.y > 0)
            {
                anim.SetBool("isJumping", false); //CHANGE TO FALLING Animation
                anim.SetBool("isFalling", true);
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f); //Adds a feather fall effect
            }
        }

        if(rb.velocity.y > tempVelocity)
        {
            anim.SetBool("JumpUp", true);
            anim.SetBool("isFalling", false);
        }
        else if(rb.velocity.y < tempVelocity && rb.velocity.y != 0)
        {
            anim.SetBool("JumpUp", false);
            anim.SetBool("isFalling", true);
        }

        //Carries velocity to compare vertical distnace when jumping
        tempVelocity = rb.velocity.y;*/
    }

    void Crouch()
    {
        //Animations and controls for crouching, with no crouching while in the air
        if(control.crouch && control.jumpOff && !inAir)
        {
            isCrouching = true;
            isWalking = false;
            crouching = true;
        }
        else
        {
          isCrouching = false;
          crouching = false;
        }

        if(isCrouching)
        {
            rb.velocity *= crouchingMod; //Change speed to crouching speed
        }
    }

    void Interact()
    {
        //Checking for any nearby interactables
        Collider2D col2 = Physics2D.OverlapCircle(transform.position, interactRadius, interactableLayer);

        if(col2 != null) //If one is found
        {
            Interactable interactable = col2.gameObject.GetComponent<Interactable>();

            if(!camFol.IsFollowing()) //Stop the camera and focus it on the interactable
            {
                SetFocus(interactable);
                camFol.ChangeFollow();
            }

            if(interactable.IsFocus())
            {
                interactText.gameObject.SetActive(true); //Turn on press E text

                if(control.useItem) //If E is pressed
                {
                    interactable.Activate(); //Activate the interactable
                }
            }

            else
            {
                interactText.gameObject.SetActive(false);
            }
        }
        else //If there is no interactable nearby anymore
        {
            if(camFol.IsFollowing()) //Resume camera movement
            {
                camFol.ChangeFollow();
                RemoveFocus();
            }
        }
    }

    //Range of attack depends on power used
    void Attack()
    {
        timeSinceAttack += Time.deltaTime;

        if(control.attack1 && !control.crouch && timeSinceAttack >= attackDelay)
        {
            currentPower = power1;
        }
        else if(control.attack2 && !control.crouch && timeSinceAttack >= attackDelay)
        {
            currentPower = power2;
        }
        else
        {
            isAttacking = false;
            return;
        }

        timeSinceAttack = 0;
        isAttacking = true;  //CHANGE TO SPECIFIC POWER ANIMATION
        RaycastHit2D[] hits;

        if(flipCol) //If facing right
        {
            //hit = Physics2D.Raycast(transform.position, transform.right, hitRange, enemyLayer);
            hits = Physics2D.RaycastAll(transform.position, transform.right, powerStats[currentPower][1], enemyLayer);
        }
        else //If facing left
        {
            //hit = Physics2D.Raycast(transform.position, -transform.right, hitRange, enemyLayer);
            hits = Physics2D.RaycastAll(transform.position, -transform.right, powerStats[currentPower][1], enemyLayer);
        }

        if(hits.Length > 0)
        {
          for(int i = 0; i < hits.Length; i++)
          {
              hits[i].transform.gameObject.SendMessage("TakeDamage", powerStats[currentPower][0]);
              //hit.transform.gameObject.SendMessage("Knockback", 200);
              hits[i].transform.gameObject.SendMessage("Knockback", powerStats[currentPower][2]);
          }
        }
    }

    void Inventory()
    {
        //Toggles inventory screen when its button is pressed
        if(control.inventory)
        {
            inventoryMenu.GetComponent<InventoryMechanics>().ToggleInvMenu();
        }
    }

    void SavenLoad()
    {
      //-----------------------------------------------------------------
      //ADD SAVE AND LOAD ICONS/TEXT
          if(control.save)
          {
              Scene scene = SceneManager.GetActiveScene();
              Game.current.currentPlayerData.sceneID = scene.buildIndex;
              Game.current.currentPlayerData.playerPosX = transform.position.x;
              Game.current.currentPlayerData.playerPosY = transform.position.y;
              Game.current.currentPlayerData.playerPosZ = transform.position.z;

              Game.current.currentPlayerData.powers = powers;
              Game.current.currentPlayerData.power1 = power1;
              Game.current.currentPlayerData.power2 = power2;

              SaveLoad.Save();
          }

          //-----------------------------------------------------------------

          if(control.load)
          {
              SaveLoad.Load();
              Game.current.isSceneBeingLoaded = true;
              int whichScene = Game.current.currentPlayerData.sceneID;
              SceneManager.LoadScene(whichScene);

              float t_x = Game.current.currentPlayerData.playerPosX;
              float t_y = Game.current.currentPlayerData.playerPosY;
              float t_z = Game.current.currentPlayerData.playerPosZ;

              transform.position = new Vector3(t_x, t_y, t_z);

              powers = Game.current.currentPlayerData.powers;
              power1 = Game.current.currentPlayerData.power1;
              power2 = Game.current.currentPlayerData.power2;
          }
    }

    //Change camera focus to interactable
    void SetFocus(Interactable newFocus)
    {
        focus = newFocus;
    }

    //Remove all focuses
    void RemoveFocus()
    {
        focus = null;
    }

}
