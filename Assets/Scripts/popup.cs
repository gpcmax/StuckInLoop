using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Choice", menuName ="Dialouge/NewChoice")]
public class popup : ScriptableObject
{
    public TextAsset currentChoiceFile;
    public string currentChoice;
    public string charName = string.Empty;
    public bool isChoice;
    public string choiceOneName = null;
    public popup choiceOne;
    public string choiceTwoName = null;
    public popup choiceTwo;
}
