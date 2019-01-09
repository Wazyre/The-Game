using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerStatistics
{
    public float hp;
    public float disease;

    public float playerPosX;
    public float playerPosY;
    public float playerPosZ;

    public int money;
    public int sceneID;

    public string power1;
    public string power2;
    public List<Item> inventory;

    public Dictionary<string, bool> powers;

}
