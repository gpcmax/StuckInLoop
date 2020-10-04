using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCars : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 10f;
    private CarSceneController gameController;

    // Update is called once per frame
    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<CarSceneController>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (gameController.gameInProgress)
        {
            rb.velocity = transform.right * speed;
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
}
