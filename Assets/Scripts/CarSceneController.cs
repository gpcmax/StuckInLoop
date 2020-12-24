using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class CarSceneController : MonoBehaviour
{
    public bool gameInProgress;

    public Transform[] spawnPoints;

    public GameObject[] cars;

    public float waitTime = 1f;
    public GameData gameData;
    
    public TextMeshProUGUI dayText;
    const string day = "Day: ";
    bool changingLevel = false;

    private void Awake()
    {
        dayText.text = day + gameData.Day;
        changingLevel = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameInProgress = true;
        StartCoroutine("startLevel");
        //StartCoroutine("SpawnCars");
    }

    IEnumerator SpawnCars()
    {
        while (gameInProgress)
        {
            int RandSpawn = Random.Range(0, spawnPoints.Length - 1);
            int RandCar = Random.Range(0, cars.Length - 1);
            Instantiate(cars[RandCar], spawnPoints[RandSpawn].position, Quaternion.identity);
            yield return new WaitForSeconds(waitTime);
        }
        yield return null;
    }

    IEnumerator startLevel()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine("SpawnCars");
    }

    public void LevelDone()
    {
        if(!changingLevel)
        {
            changingLevel = true;
            gameInProgress = false;
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
        if (!Directory.Exists(Application.persistentDataPath + "/game_save/data"))
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
