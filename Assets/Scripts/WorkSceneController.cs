using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkSceneController : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] QTEs;
    public float waitTime;
    private bool gameInProgress;

    public int currentPoints = 0;
    public int requiredPoints = 10;

    // Start is called before the first frame update
    void Start()
    {
        gameInProgress = true;
        StartCoroutine("SpawnQTEs");
    }

    // Update is called once per frame
    void Update()
    {
        if(currentPoints >= requiredPoints)
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
    }

    IEnumerator SpawnQTEs()
    {
        while (gameInProgress)
        {
            int RandSpawn = Random.Range(0, spawnPoints.Length - 1);
            int RandQTE = Random.Range(0, QTEs.Length);
            Instantiate(QTEs[RandQTE], spawnPoints[RandSpawn].position, Quaternion.identity,spawnPoints[RandSpawn]);
            yield return new WaitForSeconds(waitTime);
        }
        yield return null;
    }
}
