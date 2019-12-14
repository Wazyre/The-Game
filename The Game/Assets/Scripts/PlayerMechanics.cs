using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System;

public class PlayerMechanics : MonoBehaviour
{
    //float hMove;
    //Speeds and jump strengths of player
    float bigJumpForce = 200f;
    float smallJumpForce = 50f;
    float normalSpeed = 10f;
    //float runningSpeed = 15f;
    float crouchingSpeed = 0.5f;
    float currentSpeed;

    //float hitRange = 2f;
    float tempVelocity;
    float dashDelay = 0.0f;
    float interactRadius = 15f;
    float groundCheckDis = 0.11f; //Distane to check for ground

    //Check conditions of player
    bool grounded = true;
    bool inAir = false;
    bool crouching = false;
    bool flipCol = true; //True if facing right, false if facing left

    string power1;
    string power2;

    //Dictionary of "discovered" and enabled powers
    Dictionary<string, bool> powers = new Dictionary<string, bool>
    {
        {"Claw", false}, {"Shotgun", false}, {"Scythe", false}, {"Spring", false}
    };

    //For power stats, first int is damage followed by range, knockback...
    Dictionary<string, List<int>> powerStats = new Dictionary<string, List<int>>
    {
        {"Claw", new List<int>(){10, 2, 0}}, {"Shotgun", new List<int>(){30, 5, 5}},
        {"Scythe", new List<int>(){20, 4, 2}}, {"Spring", new List<int>(){0, 0, 0}}
    };

    //GameObject player;
    GameObject cam;
    GameObject inventory;
    Transform groundCheck;

    Animator anim;  //Player's animator
    Rigidbody2D rb; //Player's rigidbody
    SpriteRenderer playerSprite;
    LayerMask groundLayer;
    LayerMask enemyLayer;
    LayerMask interactableLayer;

    Text interactText;

    BoxCollider2D pCol; //The collider of the player

    PlayerControlMapping control;
    StopCamera stopCam;
    public Interactable focus;

    void Awake()
    {
      cam = GameObject.FindGameObjectWithTag("MainCamera");
      interactText = GameObject.Find("Interact").GetComponent<Text>();
      inventory = GameObject.FindGameObjectWithTag("InventoryMenu");
      stopCam = cam.GetComponent<StopCamera>();
      rb = GetComponent<Rigidbody2D>();
      anim = GetComponent<Animator>();
      pCol = GetComponent<BoxCollider2D>();
      playerSprite = GetComponent<SpriteRenderer>();
      control = GetComponent<PlayerControlMapping>();

      rb.freezeRotation = true; //Prevents sprite from flipping
    }

    // Start is called before the first frame update
    void Start()
    {
        //player = this.gameObject;
        groundCheck = transform.Find("GroundCheck");
        currentSpeed = normalSpeed;
        control.StartInput();
        groundLayer = LayerMask.GetMask("Ground");
        enemyLayer = LayerMask.GetMask("Enemy");
        interactableLayer = LayerMask.GetMask("Interactable");
        interactText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        FlipSprite();

        //Calculates velocity based on speed and direction faced
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

        //If running, the player gets a dash with a delay of 1 second
        if(control.run && dashDelay == 0)
        {
            dashDelay = 1.0f; //Amount of delay in seconds between dashes
            anim.SetBool("isDashing", true); //Dashing animation on
            rb.AddForce(new Vector2(rb.velocity.x*300, 0)); //Dashing force
        }
        else
        {
            anim.SetBool("isDashing", false); //Dashing animation off
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

//-------------------------------------------------------------

        //Signals if the player is on the ground or not
        RaycastHit2D ground = Physics2D.Raycast(groundCheck.position, -transform.up, groundCheckDis, groundLayer);

        if(ground != null) //If the player is standing on ground
        {
            grounded = true;
            inAir = false;
            anim.SetBool("isJumping", false);
            anim.SetBool("isIdle", true);
        }

        //Animation and forces for first jump
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

        //Animation and forces for a double jump if enabled
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
                anim.SetBool("isJumping", false); //CHANGE TO FALLING Animation
                anim.SetBool("isFalling", true);
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f); //Adds a feather fall effect
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

        //Carries velocity to compare vertical distnace when jumping
        tempVelocity = rb.velocity.y;

//----------------------------------------------------------------

        if(control.attack1 && !control.crouch)
        {
            anim.SetBool("isAttacking", true);
            Attack(power1);
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }
        if(control.attack2 && !control.crouch)
        {
            anim.SetBool("isAttacking", true);
            Attack(power2);
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }

//----------------------------------------------------------------

        //Animations and controls for crouching, with no crouching while in the air
        if(control.crouch && control.jumpOff && !inAir)
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
            rb.velocity *= crouchingSpeed; //Change speed to crouching speed
        }

//-----------------------------------------------------------------

        //Checking for any nearby interactables
        Collider2D col2 = Physics2D.OverlapCircle(transform.position, interactRadius, interactableLayer);

        if(col2 != null) //If one is found
        {
            Interactable interactable = col2.gameObject.GetComponent<Interactable>();

            if(!stopCam.IsFollowing()) //Stop the camera and focus it on the interactable
            {
                SetFocus(interactable);
                stopCam.ChangeFollow();
            }

            if(interactable.IsFocus())
            {
                interactText.gameObject.SetActive(true); //Turn on press E text

                if(control.useItem) //If E is pressed
                {
                    Interact(col2.gameObject); //Let the interactable do its thing
                }
            }

            else
            {
                interactText.gameObject.SetActive(false);
            }
        }
        else //If there is no interactable nearby anymore
        {
            if(stopCam.IsFollowing()) //Resume camera movement
            {
                stopCam.ChangeFollow();
                RemoveFocus();
            }
        }

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

//-----------------------------------------------------------------

        //Toggles inventory screen when its button is pressed
        if(control.inventory)
        {
            inventory.GetComponent<InventoryMechanics>().ToggleInvMenu();
        }

//-----------------------------------------------------------------


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
            //Adds a knockback to player if steps on spikes
            rb.AddForce(new Vector2(rb.velocity.x*-100, rb.velocity.y*-100));
            //rb.AddForce(new Vector2(, rb.velocity.y));
        }
    }

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

    //Range of attack depends on power used
    void Attack(string power)
    {
        RaycastHit2D hit;

        if(flipCol) //If facing right
        {
            //hit = Physics2D.Raycast(transform.position, transform.right, hitRange, enemyLayer);
            hit = Physics2D.Raycast(transform.position, transform.right, powerStats[power][1], enemyLayer);
        }
        else //If facing left
        {
            //hit = Physics2D.Raycast(transform.position, -transform.right, hitRange, enemyLayer);
            hit = Physics2D.Raycast(transform.position, transform.right, powerStats[power][1], enemyLayer);
        }

        if(hit.collider != null)
        {
            hit.transform.gameObject.SendMessage("TakeDamage", powerStats[power][0]);
            //hit.transform.gameObject.SendMessage("Knockback", 200);
            hit.transform.gameObject.SendMessage("Knockback", powerStats[power][2]);
        }
    }

    //PLay out different stuff based on interactable interacted with
    void Interact(GameObject obj)
    {
        if(obj.tag == "Shrine")
        {
            //PLAY SCENE
            Activate(obj.GetComponent<ActivatePower>().GetPower());
        }
        else if(obj.tag == "Puddle")
        {
            GetComponent<PlayerState>().RemoveDisease();
        }
        else if(obj.tag == "Bench")
        {
            GetComponent<PlayerState>().Heal();
            anim.SetBool("isSitting", true);
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

    //Activate a new power for the player
    void Activate(string power)
    {
        foreach(KeyValuePair<string, bool> item in powers)
        {
            if(power == item.Key)
            {
                powers[item.Key] = true;
            }
        }
    }

}
