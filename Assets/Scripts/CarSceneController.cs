using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSceneController : MonoBehaviour
{
    public bool gameInProgress;

    public Transform[] spawnPoints;

    public GameObject[] cars;

    public float waitTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        gameInProgress = true;
        StartCoroutine("SpawnCars");
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
}
