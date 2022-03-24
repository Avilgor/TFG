using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRayCast : MonoBehaviour
{
    public int rayLength = 10;

    int layerMask = 1 << 3;
    GameObject lastHit = null;

    void Start()
    {
        
    }

    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * 10, Color.green);
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength, layerMask))
        {
            GameObject go = hit.collider.gameObject;
            if (lastHit == null)
            {
                go.SendMessage("OnHit");
                lastHit = go;
            }
            else if (lastHit != go)
            {
                lastHit.SendMessage("StopHit");
                go.SendMessage("OnHit");
                lastHit = go;
            }
        }
        else if (lastHit != null) lastHit = null;
    }
}
