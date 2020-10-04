using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuickTime : MonoBehaviour
{
    public KeyCode inputBTN;
    public TextMeshProUGUI textInput;
    public Image background;
    public Color passColor;
    private WorkSceneController sceneController;

    // Start is called before the first frame update
    void Start()
    {
        sceneController = GameObject.FindGameObjectWithTag("GameController").GetComponent<WorkSceneController>();
        textInput.text = inputBTN.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(inputBTN))
        {
            background.color = passColor;
            sceneController.AddPoint();
            Destroy(gameObject, 0.1f);
        }
    }
}
