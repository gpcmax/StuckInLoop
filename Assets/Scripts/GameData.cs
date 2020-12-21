using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/New Game Data")]
public class GameData : ScriptableObject
{
    public int Day = 1;
    public string[] Scenes;
    public int currentScene = 0;

    public void reset()
    {
        Debug.Log("Something triggered a reset");
        currentScene = 0;
    }
    public void resetData()
    {
        Debug.Log("Something triggered a reset");
        currentScene = 0;
        Day = 1;
    }

    public void ChangeScene()
    {
        currentScene++;
        //Debug.Log(Scenes[currentScene]);
        Debug.Log("Current Scene: " + currentScene);
        if(currentScene >= Scenes.Length)
        {
            Debug.Log("Scene Was Reset");
            currentScene = 0;
            Day++;
        }
        //Debug.Log(Scenes[currentScene]);
        SceneManager.LoadScene(Scenes[currentScene]);
    }
}
