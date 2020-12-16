using System.Collections;
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

    //textMesseging
    public GameObject msgHolder;
    public GameObject textMsg;
    public TextMeshProUGUI currentTextBox;
    public bool textMsgEnabled;

    public List<string> characterNames = new List<string>();
    public List<string> sentancesWONames = new List<string>();

    // Start is called before the first frame update
    private void Awake()
    {
        if (autoStart && !textMsgEnabled)
        {
            if (gameData.Day <= choicesBaseOnDays.Length)
            {
                startConvo(choicesBaseOnDays[gameData.Day - 1]);
            }
            else
            {
                startConvo(choicesBaseOnDays[Random.Range(0, choicesBaseOnDays.Length)]);
            }
        }
        else if(textMsgEnabled)
        {
            sentances = HandleText();
            SpawsnNewTextBox();
            isTalking = true;
        }
        //sentances = currentTextFile.text.Split('\n');
        //sentances = currentChoice.currentChoice.text.Split('\n');
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && isTalking) //Input.GetButtonDown("Interact") ||  || Input.GetTouch(0).phase == TouchPhase.Began
        {
            if (!textMsgEnabled)
            {
                if (currentSentance != textBox.text)
                {
                    fillText = true;
                }
                else
                {
                    Continue();
                }
            }
            else
            {
                if (currentSentance != currentTextBox.text)
                {
                    fillText = true;
                }
                else
                {
                    ContinueText();
                }
            }
        }
    }

    public void startConvo(popup Talking)
    {
        dialogueParent.SetActive(true);
        currentChoice = Talking;
        sentances = HandleText();
        sentancesWONames = HandleSentences();
        characterNames = HandleNames();
        textBox.text = "";
        nameBox.text = characterNames[index];
        isTalking = true;
        StartCoroutine("Type");
    }

    public void SpawsnNewTextBox()
    {
        GameObject currentMsg = Instantiate(textMsg, msgHolder.transform);
        currentTextBox = currentMsg.GetComponentInChildren<TextMeshProUGUI>();
        currentTextBox.text = string.Empty;
        StartCoroutine("TypeMessage");
    }

    public void startConvoBaseOnDay()
    {
        if (gameData.Day <= choicesBaseOnDays.Length)
        {
            startConvo(choicesBaseOnDays[gameData.Day - 1]);
        }
        else
        {
            startConvo(choicesBaseOnDays[Random.Range(0, choicesBaseOnDays.Length)]);
        }
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
        currentSentance = sentancesWONames[index];
        nameBox.text = characterNames[index];
        foreach (char letter in currentSentance.ToCharArray())
        {
            if (fillText)
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

    IEnumerator TypeMessage()
    {
        currentSentance = sentancesWONames[index];
        nameBox.text = characterNames[index];
        foreach (char letter in currentSentance.ToCharArray())
        {
            if (fillText)
            {
                currentTextBox.text = currentSentance;
                fillText = false;
                break;
            }
            else
            {
                currentTextBox.text += letter;
                audioSource.Play();
                yield return new WaitForSeconds(textSpeed);
            }
        }
    }

    public void Continue()
    {
        if (index < sentancesWONames.Count - 1)
        {
            index++;
            StopCoroutine("Type");
            audioSource.Stop();
            textBox.text = "";
            StartCoroutine("Type");
        }
        else if (currentChoice.isChoice)
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

    public void ContinueText()
    {
        if (index < sentancesWONames.Count - 1)
        {
            index++;
            SpawsnNewTextBox();
        }
        else if (currentChoice.isChoice)
        {
            choice.SetActive(true);
            choiceOneText.text = currentChoice.choiceOneName;
            choiceTwoText.text = currentChoice.choiceTwoName;
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
        index = 0;
        textBox.text = "";
        startConvo(currentChoice);
    }

    public void ChoiceTwoClick()
    {
        currentChoice = currentChoice.choiceTwo;
        choice.SetActive(false);
        sentances = HandleText(); 
        index = 0;
        textBox.text = "";
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

    private List<string> HandleNames()
    {
        List<string> names = new List<string>();
        for(int i =0; i< sentances.Length;i++)
        {
            int charLocation = sentances[i].IndexOf(':');
            if (charLocation> 0)
            {
                names.Add(sentances[i].Substring(0, charLocation));
            }
        }
        return names;
    }
    private List<string> HandleSentences()
    {
        List<string> sentancesWithOutNames = new List<string>();
        for(int i = 0; i < sentances.Length;i++)
        {
            sentancesWithOutNames.Add(sentances[i].Split(':').Last());
        }
        return sentancesWithOutNames;
    }
}
