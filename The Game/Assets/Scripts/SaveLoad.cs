using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{
    public static List<Game> savedGames = new List<Game>();

    public static void Save()
    {
      //Creates save data directory if not created yet.
      if(!Directory.Exists("Saves"))
      {
          Directory.CreateDirectory("Saves");
      }
      
      SaveLoad.savedGames.Add(Game.current);
      BinaryFormatter bf = new BinaryFormatter();
      FileStream file = File.Create (Application.persistentDataPath + "Saves/savedGames.gd");
      bf.Serialize(file, SaveLoad.savedGames);
      file.Close();
    }

    public static void Load()
    {
      if(File.Exists(Application.persistentDataPath + "Saves/savedGames.gd"))
      {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "Saves/savedGames.gd", FileMode.Open);
        SaveLoad.savedGames = (List<Game>)bf.Deserialize(file);
        Game.current = savedGames[-1];
        file.Close();
      }
    }

}
