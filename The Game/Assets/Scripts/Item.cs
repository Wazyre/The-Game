using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    //An item's identification parameters
    int m_id;
    string m_name;
    string m_desc;

    Sprite icon;
    //public Dictionary<string, int> stats = new Dictionary<string, int>();

    //New item initialization
    public Item(int id, string name, string desc)
    {
        this.m_id = id;
        this.m_name = name;
        this.m_desc = desc;
        this.icon = Resources.Load<Sprite>("Sprites/Items/" + name);
        //this.stats = stats;
    }

    //Copy item parameters
    public Item(Item item)
    {
        this.m_id = item.id;
        this.m_name = item.name;
        this.m_desc = item.desc;
        this.icon = Resources.Load<Sprite>("Sprites/Items/" + item.name);
        //this.stats = item.stats;
    }

    public int id
    {
      get{return m_id;}
    }
    public string name
    {
      get{return m_name;}
    }
    public string desc
    {
      get{return m_desc;}
    }
}
