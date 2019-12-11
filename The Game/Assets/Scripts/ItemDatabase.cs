using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    void Awake()
    {
        BuildDatabase();
    }

    //Begins new inventory with healing potion
    void BuildDatabase()
    {
        items = new List<Item>{
            new Item(0, "Healing Potion", "A potion that heals the player")
        };
    }

    //Find items using id or name
    public Item GetItem(int id)
    {
        return items.Find(item => item.id == id);
    }

    public Item GetItem(string name)
    {
        return items.Find(item => item.name == name);
    }
}
