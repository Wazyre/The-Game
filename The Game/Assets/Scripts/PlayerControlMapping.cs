using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerControlMapping : MonoBehaviour
{
    float m_xMove; //Horizontal movement
    float m_yMove; //Vertical movement

    bool m_jumpOn;
    bool m_jumpOff;
    bool m_run;
    bool m_useItem;
    bool m_attack1;
    bool m_attack2;
    bool m_heal;
    bool m_crouch;
    bool m_save;
    bool m_load;
    bool m_inventory;
    bool m_inputting = true; //Player can input or not

    // Start is called before the first frame update
    void Awake()
    {
        m_xMove = Input.GetAxisRaw("Horizontal");
        m_yMove = Input.GetAxis("Vertical");
        m_jumpOn = Input.GetKeyDown(KeyCode.Space);
        m_jumpOff = Input.GetKeyUp(KeyCode.Space);
        m_run = Input.GetKeyDown(KeyCode.LeftShift);
        m_useItem = Input.GetKeyDown(KeyCode.E);
        m_attack1 = Input.GetKeyDown(KeyCode.Mouse0);
        m_attack2 = Input.GetKeyDown(KeyCode.Mouse1);
        m_heal = Input.GetKeyDown(KeyCode.Q);
        m_crouch = Input.GetKey(KeyCode.LeftControl);
        m_save = Input.GetKeyDown(KeyCode.F5);
        m_load = Input.GetKeyDown(KeyCode.F9);
        m_inventory = Input.GetKeyDown(KeyCode.I);
    }

    // Update is called once per frame
    void Update()
    {
      if(m_inputting) //If player input is on
      {
          m_xMove = Input.GetAxisRaw("Horizontal");
          m_yMove = Input.GetAxis("Vertical");
          m_jumpOn = Input.GetKeyDown(KeyCode.Space);
          m_jumpOff = Input.GetKeyUp(KeyCode.Space);
          m_run = Input.GetKeyDown(KeyCode.LeftShift);
          m_useItem = Input.GetKeyDown(KeyCode.E);
          m_attack1 = Input.GetKeyDown(KeyCode.Mouse0);
          m_attack2 = Input.GetKeyDown(KeyCode.Mouse1);
          m_heal = Input.GetKeyDown(KeyCode.Q);
          m_crouch = Input.GetKey(KeyCode.LeftControl);
          m_save = Input.GetKeyDown(KeyCode.F5);
          m_load = Input.GetKeyDown(KeyCode.F9);
          m_inventory = Input.GetKeyDown(KeyCode.I);
      }
    }

    public float xMove
    {
      get{return m_xMove;}
    }

    public float yMove
    {
      get{return m_yMove;}
    }

    public bool jumpOn
    {
      get{return m_jumpOn;}
    }

    public bool jumpOff
    {
      get{return m_jumpOff;}
    }

    public bool run
    {
      get{return m_run;}
    }

    public bool useItem
    {
      get{return m_useItem;}
    }

    public bool attack1
    {
      get{return m_attack1;}
    }

    public bool attack2
    {
      get{return m_attack2;}
    }

    public bool heal
    {
      get{return m_heal;}
    }

    public bool crouch
    {
      get{return m_crouch;}
    }

    public bool save
    {
      get{return m_save;}
    }

    public bool load
    {
      get{return m_load;}
    }

    public bool inventory
    {
      get{return m_inventory;}
    }

    public bool inputting
    {
      get{return m_inputting;}
    }



    public void NoInput()
    {
        m_inputting = false; //Shuts off input for player
    }
    public void StartInput()
    {
        m_inputting = true; //Turns on input for player
    }

    public IEnumerator ToggleInput(float delay)
    {
        NoInput();
        yield return new WaitForSeconds(delay);
        StartInput();
    }
}
