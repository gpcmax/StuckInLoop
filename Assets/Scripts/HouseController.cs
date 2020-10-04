using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : MonoBehaviour
{
    public GameData gameData;
    public bool TalkedOnPC;
    public bool inCollision = false;

    public GameObject interactionKey;

    private void Awake()
    {
        gameData.reset();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            interactionKey.SetActive(true);
            inCollision = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactionKey.SetActive(false);
            inCollision = false;
        }
    }

    private void Update()
    {
        if(TalkedOnPC && inCollision)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                ChangeLevel();
            }
        }
    }
    public void ChangeLevel()
    {
        if(TalkedOnPC)
        {
            gameData.ChangeScene();
        }
    }
}
