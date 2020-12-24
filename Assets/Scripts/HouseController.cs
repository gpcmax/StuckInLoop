using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class HouseController : MonoBehaviour
{
    public GameData gameData;
    public bool TalkedOnPC;
    public bool inCollision = false;

    public GameObject interactionKey;

    public TextMeshProUGUI dayText;
    const string day = "Day: ";
    public KeyCode interactKey;
    public bool morningHome;

    private void Awake()
    {
        if(morningHome)
        {
            gameData.reset();
        }
        dayText.text = day + gameData.Day;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            interactionKey.SetActive(true);
            inCollision = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactionKey.SetActive(false);
            inCollision = false;
        }
    }

    private void Update()
    {
        if(TalkedOnPC && inCollision)
        {
            if(Input.GetKeyDown(interactKey))
            {
                ChangeLevel();
            }
        }
    }
    public void ChangeLevel()
    {
        if(TalkedOnPC)
        {
            SaveGame();
            gameData.ChangeScene();
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
        if(!Directory.Exists(Application.persistentDataPath + "/game_save/data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/data");
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/game_save/data/game_data");
        var json = JsonUtility.ToJson(gameData);
        bf.Serialize(file, json);
        file.Close();
    }
}
