using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlMapping : MonoBehaviour
{
    public float xMove;  //Horizontal movement
    public float yMove;

    public bool jumpOn;
    public bool jumpOff;
    public bool run;
    public bool useItem;
    public bool attack1;
    public bool attack2;
    public bool inputting = true;

    public Vector3 playerPosition;

    // Start is called before the first frame update
    void Awake()
    {
        playerPosition = transform.position;
        xMove = Input.GetAxisRaw("Horizontal");
        yMove = Input.GetAxis("Vertical");
        jumpOn = Input.GetKeyDown(KeyCode.Space);
        jumpOff = Input.GetKeyUp(KeyCode.Space);
        run = Input.GetKey(KeyCode.LeftShift);
        useItem = Input.GetKeyDown(KeyCode.E);
        attack1 = Input.GetKey(KeyCode.Mouse0);
        attack2 = Input.GetKey(KeyCode.Mouse1);
    }

    // Update is called once per frame
    void Update()
    {
      if(inputting)
      {
          playerPosition = transform.position;
          xMove = Input.GetAxisRaw("Horizontal");
          yMove = Input.GetAxis("Vertical");
          jumpOn = Input.GetKeyDown(KeyCode.Space);
          jumpOff = Input.GetKeyUp(KeyCode.Space);
          run = Input.GetKey(KeyCode.LeftShift);
          useItem = Input.GetKeyDown(KeyCode.E);
          attack1 = Input.GetKey(KeyCode.Mouse0);
          attack2 = Input.GetKey(KeyCode.Mouse1);
      }
    }

    public void noInput()
    {
        inputting = false;
    }
    public void startInput()
    {
        inputting = true;
    }
}
