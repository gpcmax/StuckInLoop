using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillBar : MonoBehaviour
{
    public Slider progressBar;
    public float speedOfBar;
    public CarSceneController controller;

    private float currentValue = 0f;
    public float CurrentValue
    {
        get { return currentValue; }
        set
        {
            currentValue = value;
            progressBar.value = currentValue;
        }
    }

    private void Start()
    {
        CurrentValue = 0f;
    }

    private void Update()
    {
        CurrentValue += speedOfBar;
        if(currentValue >= 1f)
        {
            controller.LevelDone();
        }

    }
}
