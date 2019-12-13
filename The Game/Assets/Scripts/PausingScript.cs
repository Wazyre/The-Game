using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausingScript : MonoBehaviour
{
    GameObject pauseMenu;
    GameObject inventoryMenu;

    void Awake()
    {
        inventoryMenu = GameObject.FindGameObjectWithTag("InventoryMenu");
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
    }

    void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        //Toggle pause menu when pressing Escape
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 0) //If pause menu is on
            {
                Time.timeScale = 1; //Resume time
                ResumeGame();
            }
            else if(Time.timeScale == 1) //If pause menu is off
            {
                Time.timeScale = 0; //Stop time
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {  
        pauseMenu.SetActive(true);
        inventoryMenu.GetComponent<InventoryMechanics>().inventoryMenu.SetActive(false); //Prevent inventory from being used
    }

    public void ResumeGame()
     {
        pauseMenu.SetActive(false);
        inventoryMenu.GetComponent<InventoryMechanics>().inventoryMenu.SetActive(true); //Allow inventory to be used again
    }

    //Allows pausing game using external scripts
    public void PauseControl()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
            ResumeGame();
        }
        else if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
            PauseGame();
        }
    }
}
