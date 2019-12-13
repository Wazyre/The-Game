using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    //An item's identification parameters
    int id;
    string name;
    string desc;

    Sprite icon;
    //public Dictionary<string, int> stats = new Dictionary<string, int>();

    //New item initialization
    public Item(int id, string name, string desc)
    {
        this.id = id;
        this.name = name;
        this.desc = desc;
        this.icon = Resources.Load<Sprite>("Sprites/Items/" + name);
        //this.stats = stats;
    }

    //Copy item parameters
    public Item(Item item)
    {
        this.id = item.id;
        this.name = item.name;
        this.desc = item.desc;
        this.icon = Resources.Load<Sprite>("Sprites/Items/" + item.name);
        //this.stats = item.stats;
    }
}
