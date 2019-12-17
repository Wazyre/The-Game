using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Puddle : Interactable
{
  public override void Activate()
  {
    Game.current.currentPlayerData.disease -= Game.current.currentPlayerData.maxDisease*0.25f;
  }
}
