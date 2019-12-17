using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerStatistics
{
    float m_hp = 100;
    float m_disease = 0;
    float m_maxHealth = 100;
    float m_maxDisease = 0;

    float m_playerPosX = 0;
    float m_playerPosY = 0;
    float m_playerPosZ = 0;

    int m_money = 0;
    int m_sceneID = 1; //Current scene

    string m_power1 = "";
    string m_power2 = "";
    List<Item> m_inventory = new List<Item>{};

    Dictionary<string, bool> m_powers = new Dictionary<string, bool>
    {
        {"Claw", false}, {"Shotgun", false}, {"Scythe", false}, {"Spring", false}
    };

    public float hp
    {
      get{return m_hp;}
      set{m_hp = value;}
    }
    public float disease
    {
      get{return m_disease;}
      set{m_disease = value;}
    }
    public float maxHealth
    {
      get{return m_maxHealth;}
      set{m_maxHealth = value;}
    }
    public float maxDisease
    {
      get{return m_maxDisease;}
      set{m_maxDisease = value;}
    }
    public float playerPosX
    {
      get{return m_playerPosX;}
      set{m_playerPosX = value;}
    }
    public float playerPosY
    {
      get{return m_playerPosY;}
      set{m_playerPosY = value;}
    }
    public float playerPosZ
    {
      get{return m_playerPosZ;}
      set{m_playerPosZ = value;}
    }
    public int money
    {
      get{return m_money;}
      set{m_money = value;}
    }
    public int sceneID
    {
      get{return m_sceneID;}
      set{m_sceneID = value;}
    }
    public string power1
    {
      get{return m_power1;}
      set{m_power1 = value;}
    }
    public string power2
    {
      get{return m_power2;}
      set{m_power2 = value;}
    }
    public List<Item> inventory
    {
      get{return m_inventory;}
      set{m_inventory = value;}
    }
    public Dictionary<string, bool> powers
    {
      get{return m_powers;}
      set{m_powers = value;}
    }
}
