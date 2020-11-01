using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public GameObject interactionButton;
    public GameObject objectToSetActive;
    private bool isInTrigger;
    public HouseController controller;
    public KeyCode interactKey;

    private void Update()
    {
        if (Input.GetKeyDown(interactKey) && isInTrigger)
        {
            objectToSetActive.SetActive(true);
            controller.TalkedOnPC = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            interactionButton.SetActive(true);
            isInTrigger = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            interactionButton.SetActive(false);
            isInTrigger = false;
        }
    }
}
