using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableMarker : MonoBehaviour,ARTrackable,RaycastObject
{
    bool isSelectable,isHit,isDetected;

    void Awake()
    {
        isDetected = false;
        isSelectable = true;
    }

    void Start()
    {

    }

    public void OnEnable()
    {
        isDetected = true;
    }

    public void OnDisable()
    {
        isDetected = false;
    }

    public void InSight()
    {
        isDetected = true;
    }

    public void OutSight()
    {
        isDetected = false;
        if (gameObject.activeSelf) gameObject.SetActive(false);
    }

    public void OnHit()
    {
        if (isSelectable)
        {          
            isHit = true;
            GLOBALS.selectionFillUI.StartFill(gameObject);
        }
    }

    public void StopHit()
    {
        if (isSelectable)
        {
            isHit = false;
            GLOBALS.selectionFillUI.StopFill();
        }
    }

    public void OnSelectionFill()
    {
        Debug.Log("Selection filled");
        gameObject.SendMessage("OnExecute");
    }

    public void SetSelectable(bool sel)
    {
        isSelectable = sel;
    }
}