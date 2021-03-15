using System;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveGameSystem : MonoBehaviour
{
    private string FilePath;
    public static SaveGameSystem instance;

    //As filepath can't be accessed statically but we still need this class as a singleton, add this in Awake
    private void Awake()
    {
        //Check there are no other copies of this class in the scene
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        FilePath = Application.persistentDataPath + "/save.data";
        Debug.Log("Your save data path is: " + FilePath);
    }

    public bool SaveGame(GameData saveData)
    {
        BinaryFormatter converter = new BinaryFormatter();
        using (FileStream stream = new FileStream(FilePath, FileMode.Create))
        {
            try
            {
                converter.Serialize(stream, saveData);
            }
            catch (Exception e)
            {
                Debug.Log("Game failed to save with: \n" + e.Message);
                return false;
            }

            stream.Close();
        }

        Debug.Log("Game successfully saved");
        return true;
    }
    
    // Now a function for loading the game
    public GameData LoadGame()
    {
        if (!DoesSaveGameExist(FilePath))
        {
            return null;
        }

        BinaryFormatter converter = new BinaryFormatter();
        using (FileStream stream = new FileStream(FilePath, FileMode.Open))
        {
            try
            {
                GameData mySaveData = converter.Deserialize(stream) as GameData;
                stream.Close();
                return mySaveData;
            }
            catch (Exception)
            {
                Debug.Log("Failed loading game!");
                stream.Close();
                return null;
            }
        }
    }

    public bool DoesSaveGameExist(string path)
    {
        return File.Exists(path);
    }
}
