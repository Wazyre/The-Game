using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shrine : Interactable
{
  string power;

  public override void Activate()
  {
    foreach(KeyValuePair<string, bool> item in Game.current.currentPlayerData.powers)
    {
        if(power == item.Key)
        {
            Game.current.currentPlayerData.powers[item.Key] = true;
        }
    }
  }
}
