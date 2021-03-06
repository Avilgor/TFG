using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class CameraRayCast : MonoBehaviour
{
    [SerializeField] Camera camera;
    public int rayLength;

    int layerMask = 1 << 3;
    GameObject lastHit = null;
    Vector2 screenCenter;

    void Start()
    {
        screenCenter.x = Screen.width / 2;
        screenCenter.y = Screen.height / 2;
    }

    void Update()
    {       
        if (GLOBALS.selectionFill)
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(screenCenter);
            if (Physics.Raycast(ray, out hit, layerMask))
            {
                GameObject go = hit.collider.gameObject;

                if (lastHit == null)
                {
                    GLOBALS.gameMarkerManager.HitMarker(go);
                    lastHit = go;
                }
                else if (lastHit != go)
                {
                    GLOBALS.gameMarkerManager.StopHitMarker(lastHit);
                    GLOBALS.gameMarkerManager.HitMarker(go);
                    lastHit = go;
                }
            }
            else if (lastHit != null)
            {
                GLOBALS.gameMarkerManager.StopHitMarker(lastHit);
                lastHit = null;
            }
        }
        else if (Input.touchCount > 0)
        {
            Touch currentTouch = Input.GetTouch(0);
            if (currentTouch.phase == TouchPhase.Began)
            {
                RaycastHit hit;
                Ray ray = camera.ScreenPointToRay(currentTouch.position);
                if (Physics.Raycast(ray, out hit, layerMask))
                {              
                    GLOBALS.gameMarkerManager.HitMarker(hit.collider.gameObject);                      
                }
            }
        }
    }
}
