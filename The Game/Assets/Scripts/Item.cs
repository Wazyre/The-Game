﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int id;

    public string name;
    public string desc;

    public Sprite icon;
    //public Dictionary<string, int> stats = new Dictionary<string, int>();

    public Item(int id, string name, string desc)
    {
        this.id = id;
        this.name = name;
        this.desc = desc;
        this.icon = Resources.Load<Sprite>("Sprites/Items/" + name);
        //this.stats = stats;
    }

    public Item(Item item)
    {
        this.id = item.id;
        this.name = item.name;
        this.desc = item.desc;
        this.icon = Resources.Load<Sprite>("Sprites/Items/" + item.name);
        //this.stats = item.stats;
    }
}