using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameData GameData;
    public string SceneToLoad = string.Empty;

    public void StartGame()
    {
        LoadGame();
        SceneManager.LoadScene(SceneToLoad);
    }

    public void ResetData()
    {
        GameData.resetData();
        SaveGame();
    }

    public void CloseGame()
    {
        Application.Quit();
    }
    
    public void LoadGame()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/game_save/data"))
        {
            return;
        }
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/game_save/data/game_data.txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/data/game_data.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), GameData);
            file.Close();
        }
    }

    public bool IsSaveFile()
    {
        return Directory.Exists(Application.persistentDataPath + "/game_save");
    }

    public void SaveGame()
    {
        if (!IsSaveFile())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }
        if (!Directory.Exists(Application.persistentDataPath + "/game_save/data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/data");
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/game_save/data/game_data");
        var json = JsonUtility.ToJson(GameData);
        bf.Serialize(file, json);
        file.Close();
    }
}
