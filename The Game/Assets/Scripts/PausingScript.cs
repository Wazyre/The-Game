using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausingScript : MonoBehaviour
{
    public GameObject pauseMenu;
    GameObject inventoryMenu;

    void Awake()
    {
        inventoryMenu = GameObject.FindGameObjectWithTag("InventoryMenu");
    }

    void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            inventoryMenu.GetComponent<InventoryMechanics>().inventoryMenu.SetActive(false);
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

    public void PauseGame()
    {
        pauseMenu = GameObject.FindWithTag("PauseMenu");
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
     {
        pauseMenu = GameObject.FindWithTag("PauseMenu");
        pauseMenu.SetActive(false);
    }

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
