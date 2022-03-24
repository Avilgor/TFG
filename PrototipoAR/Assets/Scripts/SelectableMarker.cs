using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableMarker : MonoBehaviour,ARTrackable,RaycastObject
{
    public bool isDetected 
    { 
        get =>  isDetected; 
        set => isDetected = value;  
    }

    public bool isHit 
    { 
        get => isHit; 
        set => isHit = value; 
    }

    public bool isSelectable 
    { 
        get => isSelectable; 
        set => isSelectable = value;
    }   

    private void Awake()
    {
        isDetected = false;
        isSelectable = true;
    }

    void Start()
    {

    }

    private void OnEnable()
    {
        isDetected = true;
    }

    private void OnDisable()
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
            Debug.Log("Gameobject " + name + " hit!");
            isHit = true;
            GLOBALS.selectionFillUI.StartFill(gameObject);
        }
    }

    public void StopHit()
    {
        if (isSelectable)
        {
            Debug.Log("Gameobject " + name + " stop hit.");
            isHit = false;
            GLOBALS.selectionFillUI.StopFill();
        }
    }

    public void OnSelectionFill()
    {
        gameObject.SendMessage("OnExecute");
    }

    public void SetSelectable(bool sel)
    {
        isSelectable = sel;
    }
}