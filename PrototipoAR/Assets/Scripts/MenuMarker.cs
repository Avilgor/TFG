using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMarker : MonoBehaviour,ARTrackable
{
    public bool isDetected { get { return isDetected; } set { isDetected = value; } }

    private void Awake()
    {
        isDetected = false;
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
        if(gameObject.activeSelf) gameObject.SetActive(false);           
    }
}
