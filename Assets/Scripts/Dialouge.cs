﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Linq;
using UnityEngine.UI;

public class Dialouge : MonoBehaviour
{
    public GameObject dialogueParent;
    public popup currentChoice;
    public TextMeshProUGUI nameBox;
    public TextMeshProUGUI textBox;
    //public TextAsset currentTextFile;

    public bool autoStart;

    private string[] sentances;
    private string currentSentance;
    private int index;
    public string charName = string.Empty;
    public float textSpeed;

    private bool isTalking = false;

    public AudioSource audioSource;

    private bool fillText = false;

    public bool hasChoice;

    public GameObject choice;
    public TextMeshProUGUI choiceOneText;
    public TextMeshProUGUI choiceTwoText;

    public GameData gameData;
    public popup[] choicesBaseOnDays;


    // Start is called before the first frame update
    private void Awake()
    {
        if(autoStart)
        {
            if (gameData.Day <= choicesBaseOnDays.Length)
            {
                startConvo(choicesBaseOnDays[gameData.Day]);
            }
            else
            {
                startConvo(choicesBaseOnDays[Random.Range(0,choicesBaseOnDays.Length)]);
            }
        }
        //sentances = currentTextFile.text.Split('\n');
        //sentances = currentChoice.currentChoice.text.Split('\n');
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && isTalking) //Input.GetButtonDown("Interact") ||  || Input.GetTouch(0).phase == TouchPhase.Began
        {
            if(currentSentance != textBox.text)
            {
                fillText = true;
            }
        }
    }

    public void startConvo(popup Talking)
    {
        dialogueParent.SetActive(true);
        charName = Talking.charName;
        currentChoice = Talking;
        sentances = HandleText();
        textBox.text = "";
        nameBox.text = charName;
        isTalking = true;
        StartCoroutine("Type");
    }

    public void stopConvo()
    {
        StopCoroutine("Type");
        isTalking = false;
        audioSource.Stop();
        dialogueParent.SetActive(false);
    }

    public bool talking(bool startTalk)
    {
        isTalking = startTalk;
        return isTalking;
    }

    IEnumerator Type()
    {
        currentSentance = sentances[index];
        foreach(char letter in currentSentance.ToCharArray())
        {
            if(fillText)
            {
                textBox.text = currentSentance;
                fillText = false;
                break;
            }
            else
            {
                textBox.text += letter;
                audioSource.Play();
                yield return new WaitForSeconds(textSpeed);
            }
        }
    }

    public void Continue()
    {
        if(index < sentances.Length - 1)
        {
            index++;
            StopCoroutine("Type");
            audioSource.Stop();
            textBox.text = "";
            StartCoroutine("Type");
        }
        else if(currentChoice.isChoice)
        {
            choice.SetActive(true);
            choiceOneText.text = currentChoice.choiceOneName;
            choiceTwoText.text = currentChoice.choiceTwoName;
        }
        else
        {
            index = 0;
            textBox.text = "";
            stopConvo();
        }
    }

    public void choiceClick(popup choiceOne)
    {
        choice.SetActive(false);
        currentChoice = choiceOne;
        sentances = HandleText();
        startConvo(choiceOne);
    }

    public void ChoiceOneClick()
    {
        choice.SetActive(false);
        currentChoice = currentChoice.choiceOne;
        sentances = HandleText();
        startConvo(currentChoice);
    }

    public void ChoiceTwoClick()
    {
        currentChoice = currentChoice.choiceTwo;
        choice.SetActive(false);
        sentances = HandleText();
        startConvo(currentChoice);
    }

    private string[] HandleText()
    {
        if(currentChoice.currentChoiceFile != null)
        {
            return currentChoice.currentChoiceFile.text.Split('\n');
        }
        else
        {
            return currentChoice.currentChoice.Split('\n');
        }
    }
}
