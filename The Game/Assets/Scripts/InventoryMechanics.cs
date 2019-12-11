using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMechanics : MonoBehaviour
{
    //Toggles inventory menu visibility
    bool showInvMenu = false;

    //Inventory of player
    public List<Item> playerItems = new List<Item>();

    //Holds item database
    public ItemDatabase itemDatabase;

    //Holds gamePbjects of menus
    public GameObject inventoryMenu;
    GameObject pauseMenu;

    // Start is called before the first frame update
    void Awake()
    {
        inventoryMenu = GameObject.FindGameObjectWithTag("InventoryMenu");
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        inventoryMenu.SetActive(false); //Begin as not visible
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pauseMenu.GetComponent<PausingScript>().pauseMenu.activeSelf == false)
        {
            if(showInvMenu)
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
        showInvMenu = !showInvMenu;
    }

    void DisplayItems()
    {
        //FILL IN
        Debug.Log("hy");
    }

    //Registers and adds item to player's inventory
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
