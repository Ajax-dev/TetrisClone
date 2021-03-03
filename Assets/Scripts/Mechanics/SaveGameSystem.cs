using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using Mechanics.SaveData;

public static class SaveGameSystem
{
    public static bool SaveGame(SaveData saveGame, string name)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream stream = new FileStream(GetSavePath(name), FileMode.Create))
        {
            try
            {
                formatter.Serialize(stream, saveGame);
            }
            catch (Exception)
            {
                return false;
            }
        }

        return true;
    }

    public static SaveData LoadGame(string name)
    {
        
    }

    public static bool DeleteSaveGame(string name)
    {
        
    }

    public static bool DoesSaveGameExist(string name)
    {
        
    }

    private static string GetSavePath(string name)
    {
        
    }
}
