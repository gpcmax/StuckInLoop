using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour,IDragHandler,IPointerUpHandler,IPointerDownHandler
{
    public Image joyContainer;
    public Image joystick;

    public Vector3 InputDirection;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = Vector2.zero;

        //to gegt input direction
        RectTransformUtility.ScreenPointToLocalPointInRectangle(joyContainer.rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out position);

        position.x = (position.x / joyContainer.rectTransform.sizeDelta.x);
        position.y = (position.y / joyContainer.rectTransform.sizeDelta.y);

        float x = (joyContainer.rectTransform.pivot.x == 1f) ? position.x * 2 + 1 : position.x * 2 - 1;
        float y = (joyContainer.rectTransform.pivot.y == 1f) ? position.y * 2 + 1 : position.y * 2 - 1;

        InputDirection = new Vector3(x, y, 0);
        InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;

        joystick.rectTransform.anchoredPosition = new Vector3(InputDirection.x * (joyContainer.rectTransform.sizeDelta.x / 3), InputDirection.y * (joyContainer.rectTransform.sizeDelta.y) / 3);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputDirection = Vector3.zero;
        joystick.rectTransform.anchoredPosition = Vector3.zero;
    }

    void Start()
    {
        InputDirection = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
