using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameData GameData;
    public string SceneToLoad = string.Empty;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneToLoad);
    }

    public void ResetData()
    {
        GameData.resetData();
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
