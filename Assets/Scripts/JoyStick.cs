using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour//,IDragHandler,IPointerUpHandler,IPointerDownHandler
{
    public Transform player;
    public float speed = 1.5f;
    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;
    public Camera mainCamera;

    private void Update()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetMouseButtonDown(0))
            {
                pointA = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));

            }
            if (Input.GetMouseButton(0))
            {
                touchStart = true;
                pointB = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            }
            else
            {
                touchStart = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if(touchStart)
        {
            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
            moveCharacter(direction);
        }
    }

    void moveCharacter(Vector2 directiopn)
    {
        player.Translate(directiopn * speed * Time.deltaTime);
    }
}
