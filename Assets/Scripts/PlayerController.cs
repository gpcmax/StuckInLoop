using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    private float moveInputX;
    private float moveInputY;

    private bool talking = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(!talking)
        {
            moveInputX = Input.GetAxis("Horizontal");
            moveInputY = Input.GetAxis("Vertical");
            Vector2 inputVector = new Vector2(moveInputX * speed, moveInputY * speed);
            inputVector = Vector2.ClampMagnitude(inputVector, speed);

            rb.velocity = inputVector;
        }
    }

    public void InDialouge()
    {
        if(talking)
        {
            talking = false;
        }
        else
        {
            talking = true;
        }

    }
}
