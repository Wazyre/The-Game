using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bench : Interactable
{
  public override void Activate()
  {
    Game.current.currentPlayerData.hp -= Game.current.currentPlayerData.maxHealth*0.25f;
  }
}
