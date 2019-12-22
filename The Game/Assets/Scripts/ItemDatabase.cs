using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    List<Item> items = new List<Item>();

    void Awake()
    {
        BuildDatabase();
    }

    //Begins new inventory with healing potion
    void BuildDatabase()
    {
        items = new List<Item>{
            new Item(0, "Healing Potion", "A potion that heals the player."),
            new Item(1, "Health Shard", "Adds a heart when three have been gathered."),
            new Item(2, "Disease Potion", "A potion that reduces the amount of disease a player has."),
            new Item(3, "Coin", "A coin that is worth 1 money."),
            new Item(4, "Double Coin", "A coin that is worth 5 money."),
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
