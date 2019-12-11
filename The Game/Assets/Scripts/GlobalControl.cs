using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GlobalControl : MonoBehaviour
{
    public bool isSceneBeingLoaded = false;

    public static GlobalControl Instance;

    public PlayerStatistics savedPlayerData = new PlayerStatistics();

    //A temp holder for player data
    public PlayerStatistics LocalCopyOfData;

    void Awake ()
    {
        //Prevents script, and player data, from being destroyed with change of scenes
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        //If the game shuts down or exits, then destroy the script
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    //Responsible for saving data to files
    public void SaveData()
    {
        //Creates save data directory if not created yet.
        if(!Directory.Exists("Saves"))
        {
            Directory.CreateDirectory("Saves");
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create("Saves/save.binary");

        LocalCopyOfData = PlayerState.Instance.localPlayerData;
        formatter.Serialize(saveFile, LocalCopyOfData);

        saveFile.Close();

    }

    //Responsible for loading data from files
    public void LoadData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Open("Saves/save.binary", FileMode.Open);

        LocalCopyOfData = (PlayerStatistics)formatter.Deserialize(saveFile);

        saveFile.Close();
    }
}
