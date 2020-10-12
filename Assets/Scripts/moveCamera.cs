using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class moveCamera : MonoBehaviour
{
    public float speed = 1;

    void FixedUpdate()
    {
        transform.Translate((transform.right * speed) * Time.deltaTime);
    }
}
