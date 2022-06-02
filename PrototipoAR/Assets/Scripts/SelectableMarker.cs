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
        GLOBALS.markerRecognition.UpdateTracked(1);
    }

    public void OnDisable()
    {
        isDetected = false;
        GLOBALS.markerRecognition.UpdateTracked(-1);
    }

    public void InSight()
    {     
        if (!isDetected) isDetected = true;        
    }

    public void OutSight()
    {
        if (isDetected) isDetected = false;      
        if (gameObject.activeSelf) gameObject.SetActive(false);
    }

    public void OnHit()
    {
        if (isSelectable)
        {
            if (GLOBALS.selectionFill)
            {
                isHit = true;
                GLOBALS.selectionFillUI.StartFill(gameObject);
            }
            else gameObject.SendMessage("OnExecute");
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
        //Debug.Log("Selection filled");
        gameObject.SendMessage("OnExecute");
    }

    public void SetSelectable(bool sel)
    {
        isSelectable = sel;
    }
}