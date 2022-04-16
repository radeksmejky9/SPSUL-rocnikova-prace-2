
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static bool CheckIfFolderExists => Directory.Exists(Application.persistentDataPath + "/Saves");
    public static bool CheckIfFileExists(string fileName) => File.Exists(Application.persistentDataPath + $"/Saves/{fileName}.save");
    public static bool CheckIfFolderContainsFiles(IEnumerable<string> files)
    {
        bool contains = false;
        foreach (string file in files)
        {
            if (CheckIfFileExists(file))
            {
                contains = true;
                break;
            }
        }
        return contains;
    }
    public static void SavePlayer(string fileName)
    {
        if (!CheckIfFolderExists)
            CreateSaveFolder();

        BinaryFormatter formatter = new BinaryFormatter();
        File.Delete(Application.persistentDataPath + $"/Saves/{fileName}.save");
        FileStream file = new FileStream(Application.persistentDataPath + $"/Saves/{fileName}.save", FileMode.Create);
        PlayerData data = new PlayerData(Player.Instance);

        formatter.Serialize(file, data);
        file.Close();
    }

    public static void LoadPlayer(string fileName)
    {
        if (!CheckIfFileExists(fileName))
            return;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = new FileStream(Application.persistentDataPath + $"/Saves/{fileName}.save", FileMode.Open);
        PlayerData data = formatter.Deserialize(file) as PlayerData;
        Player.Instance.Load(data);
        file.Close();
    }

    public static PlayerData LoadData(string fileName)
    {
        if (!CheckIfFileExists(fileName))
            return null;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = new FileStream(Application.persistentDataPath + $"/Saves/{fileName}.save", FileMode.Open);
        PlayerData data = formatter.Deserialize(file) as PlayerData;
        file.Close();
        return data;
    }
    public static void CreateSaveFolder()
    {
        Directory.CreateDirectory(Application.persistentDataPath + "/Saves");
    }
}
}
}
