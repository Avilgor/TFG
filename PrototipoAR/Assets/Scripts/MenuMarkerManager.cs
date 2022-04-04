using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMarkerManager : MonoBehaviour
{
    public List<MenuMarker> markers = new List<MenuMarker>();

    List<GameObject> markersGo = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < markers.Count; i++) markersGo.Add(markers[i].gameObject);
    }

    public void HitMarker(GameObject go)
    {
        if (markersGo.Contains(go))
        {
            go.GetComponent<SelectableMarker>().OnHit();
        }
    }

    public void StopHitMarker(GameObject go)
    {
        if (markersGo.Contains(go))
        {
            go.GetComponent<SelectableMarker>().StopHit();
        }
    }
}
