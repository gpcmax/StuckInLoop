using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class WorkSceneController : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] QTEs;
    public float waitTime;
    private bool gameInProgress;

    public int currentPoints = 0;
    public int requiredPoints = 10;

    public GameData gameData;
    public TextMeshProUGUI dayText;
    const string day = "Day: ";

    private void Awake()
    {
        dayText.text = day + gameData.Day;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameInProgress = true;
        StartCoroutine("StartLevel");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPoints >= requiredPoints)
        {
            EndGame();
        }
    }

    public void AddPoint()
    {
        currentPoints++;
    }

    public void EndGame()
    {
        //load next scene
        StopAllCoroutines();
        SaveGame();
        gameData.ChangeScene();
    }

    IEnumerator SpawnQTEs()
    {
        while (gameInProgress)
        {
            int RandSpawn = Random.Range(0, spawnPoints.Length - 1);
            int RandQTE = Random.Range(0, QTEs.Length);
            Instantiate(QTEs[RandQTE], spawnPoints[RandSpawn].position, Quaternion.identity, spawnPoints[RandSpawn]);
            yield return new WaitForSeconds(waitTime);
        }
        yield return null;
    }

    IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(4f);
        StartCoroutine("SpawnQTEs");
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
