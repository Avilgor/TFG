using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class DragUI2D : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{   
    public Vector2 limitLeft, limitRight;
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
        if (!lockX)
        {
            float newX = rectTrans.localPosition.x + (aux.x * dragMultiply);
            if (newX > limitLeft.x && newX < limitRight.x) rectTrans.localPosition = new Vector3(newX, rectTrans.localPosition.y, rectTrans.localPosition.z);
        }

        if (!lockY)
        {
            float newY = rectTrans.localPosition.y + (aux.y * dragMultiply);
            if (newY > limitLeft.y && newY < limitRight.y) rectTrans.localPosition = new Vector3(rectTrans.localPosition.x, newY, rectTrans.localPosition.z);
        }
        lastPos = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}