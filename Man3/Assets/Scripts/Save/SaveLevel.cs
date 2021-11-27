using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLevel 
{
    public static void SavePlayer(player p)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "player.fun";
       // string path = "D:/unity/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        LevelData data = new LevelData(p);
        formatter.Serialize(stream, data);
        stream.Close();

    }
    public static LevelData Load()
    {
        string path = Application.persistentDataPath + "player.fun";
        //string path = "D:/unity/player.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            LevelData data = formatter.Deserialize(stream) as LevelData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Không tìm thấy file" + path);
            return null;
        }
    }
}
