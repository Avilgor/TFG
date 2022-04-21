using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class DragUI2D : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public RectTransform limitTopLeft, limitBotRight;
    public RectTransform canvasTopLeft, canvasBotRight;

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

        if (!lockX)
        {
            float newX = rectTrans.localPosition.x + (aux.x * dragMultiply);
            rectTrans.localPosition = new Vector3(newX, rectTrans.localPosition.y, rectTrans.localPosition.z);
            if (limitTopLeft.position.x > canvasTopLeft.position.x || limitBotRight.position.x < canvasBotRight.position.x)
            {
                rectTrans.localPosition = new Vector3(currentPos.x, rectTrans.localPosition.y, rectTrans.localPosition.z);
            }  
        }

        if (!lockY)
        {
            float newY = rectTrans.localPosition.y + (aux.y * dragMultiply);
            rectTrans.localPosition = new Vector3(rectTrans.localPosition.x, newY, rectTrans.localPosition.z);
            if (limitTopLeft.position.x < canvasTopLeft.position.y || limitBotRight.position.y > canvasBotRight.position.y)
            {
                rectTrans.localPosition = new Vector3(rectTrans.localPosition.x, currentPos.y, rectTrans.localPosition.z);
            }
        }
        lastPos = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}