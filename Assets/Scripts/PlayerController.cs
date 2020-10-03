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

    public float maxHealth = 1f;
    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    private void FixedUpdate()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
            EndGame();
        }
        if(!talking)
        {
            moveInputX = Input.GetAxis("Horizontal");
            moveInputY = Input.GetAxis("Vertical");
            Vector2 inputVector = new Vector2(moveInputX * speed, moveInputY * speed);
            inputVector = Vector2.ClampMagnitude(inputVector, speed);

            rb.velocity = inputVector;
        }
    }

    private void EndGame()
    {

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            currentHealth = currentHealth - .25f;
        }
    }
}
