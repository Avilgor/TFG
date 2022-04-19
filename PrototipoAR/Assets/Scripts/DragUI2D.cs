using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class DragUI2D : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public RectTransform limitLeft, limitRight;
    public Camera camera;
    public bool lockX, lockY;
    public float dragMultiply = 1.0f;

    Vector2 lastPos;
    RectTransform rectTrans;

    private void Awake()
    {
        rectTrans = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        lastPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 aux = eventData.position - lastPos;
        Vector3 currentPos = rectTrans.localPosition;

        Vector3 leftToScreen;
        Vector3 rightToScreen;

        if (!lockX)
        {
            float newX = rectTrans.localPosition.x + (aux.x * dragMultiply);
            rectTrans.localPosition = new Vector3(newX, rectTrans.localPosition.y, rectTrans.localPosition.z);
            leftToScreen = camera.WorldToScreenPoint(limitLeft.position);
            rightToScreen = camera.WorldToScreenPoint(limitRight.position);
            if (leftToScreen.x > 0 || rightToScreen.x < Screen.width) rectTrans.localPosition = currentPos;           
        }

        if (!lockY)
        {
            float newY = rectTrans.localPosition.y + (aux.y * dragMultiply);
            rectTrans.localPosition = new Vector3(rectTrans.localPosition.x, newY, rectTrans.localPosition.z);
            leftToScreen = camera.WorldToScreenPoint(limitRight.position);
            rightToScreen = camera.WorldToScreenPoint(limitRight.position);
            if (leftToScreen.x > 0 || rightToScreen.x < Screen.height) rectTrans.localPosition = currentPos;
        }
        lastPos = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}