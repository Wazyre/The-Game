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
            new Item(0, "Healing Potion", "A potion that heals the player"),
            new Item(1, "", ""),
            new Item(2, "", ""),
            new Item(3, "", ""),
            new Item(4, "", ""),
            new Item(5, "", ""),
            new Item(6, "", ""),
            new Item(7, "", ""),
            new Item(8, "", ""),
            new Item(9, "", "")
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
