using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileController : MonoBehaviour
{
    public GameObject MobileCanvas;
    private void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            MobileCanvas.SetActive(true);
        }
    }
}
