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
        currentScene = 0;
    }

    public void ChangeScene()
    {
        //Debug.Log(Scenes[currentScene]);
        currentScene++;
        if(currentScene > Scenes.Length)
        {
            currentScene = 0;
            Day++;
        }
        //Debug.Log(Scenes[currentScene]);
        SceneManager.LoadScene(Scenes[currentScene]);
    }
}
