using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
            gameData.ChangeScene();
        }
    }
}
