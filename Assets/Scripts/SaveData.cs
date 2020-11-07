using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class SaveData
{
    public bool[] finished;
    public int mathpoints;
    public int sachpoints;

    public SaveData(SaveData saveData)
    {
        finished = saveData.finished;
        mathpoints = saveData.mathpoints;
        sachpoints = saveData.sachpoints;
    }

    public SaveData(int mathp, int sachp, bool[] fin)
    {
        finished = fin;
        mathpoints = mathp;
        sachpoints = sachp;
    }
}

public static class SaveSystem
{
    public static void Save(SaveData save)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(save);
        Debug.Log("Game was saved");

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData Load()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            Debug.Log("Game was loaded");
            return data;
        }
        else
        {
            Debug.LogError("Save file not foun in " + path);
            return null;
        }
    }

    public static void ResetSaveData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        bool[] resetBool = new bool[Game.Instance.GetFinished().Length];
        SaveData data = new SaveData(0, 0, resetBool);

        Debug.Log("Save was reset");
        formatter.Serialize(stream, data);
        stream.Close();
    }

}
