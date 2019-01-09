using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMechanics : MonoBehaviour
{
    bool showMenu = false;
    //string[] slots = new string[12];

    public List<Item> playerItems = new List<Item>();
    public ItemDatabase itemDatabase;

    public GameObject inventoryMenu;
    GameObject pauseMenu;

    // Start is called before the first frame update
    void Awake()
    {
        inventoryMenu = GameObject.FindGameObjectWithTag("InventoryMenu");
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        inventoryMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(pauseMenu.GetComponent<PausingScript>().pauseMenu.activeSelf == false)
        {
            if(showMenu)
            {
                inventoryMenu.SetActive(true);
            }
            else
            {
                inventoryMenu.SetActive(false);
            }
        }

    }

    public void ToggleInvMenu()
    {
        showMenu = !showMenu;
    }

    void DisplayItems()
    {
        Debug.Log("hy");
    }

    public void RegisterItem(string name)
    {
        Item itemToAdd = itemDatabase.GetItem(name);
        playerItems.Add(itemToAdd);
    }

    public void RegisterItem(int id)
    {
        Item itemToAdd = itemDatabase.GetItem(id);
        playerItems.Add(itemToAdd);
    }

    public Item CheckForItem(int id)
    {
        return playerItems.Find(item => item.id == id);
    }

    public void DeleteItem(int id)
    {
        if(CheckForItem(id) != null)
        {
            playerItems.Remove(CheckForItem(id));
        }
    }
}
