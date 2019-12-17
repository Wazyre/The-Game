using UnityEngine;
using System.Collections;

[System.Serializable]
public class Game
{

  public static Game current;
  public PlayerStatistics currentPlayerData;

  public bool isSceneBeingLoaded = false;

  public Game()
  {
    currentPlayerData = new PlayerStatistics();
  }

}
