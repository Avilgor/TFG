using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMarkerManager : MonoBehaviour
{
    public List<NumberMarker> markers = new List<NumberMarker>();

    List<GameObject> markersGo = new List<GameObject>();

    private void Awake()
    {
        GLOBALS.gameMarkerManager = this;
    }

    void Start()
    {
        for (int i = 0; i < markers.Count; i++) markersGo.Add(markers[i].gameObject);
    }

    public void SetMarkers(List<int> list)
    {
        List<NumberMarker> toSet = new List<NumberMarker>(markers);
        markers.Clear();        

        //Set  numbers
        int max = toSet.Count;
        int rand;
        for (int i = 0; i < max; i++)
        {
            if (toSet.Count > 1)
            {
                rand = Random.Range(0, toSet.Count);
                toSet[rand].SetNumber(list[0]);
                markers.Add(toSet[rand]);
                toSet.RemoveAt(rand);
                list.RemoveAt(0);
            }
            else
            {
                toSet[0].SetNumber(list[0]);
                markers.Add(toSet[0]);
                toSet.Clear();
                list.Clear();
            }
        }
    }

    public void EndMarkers()
    {
        for (int i = 0; i < markers.Count; i++)
        {
            markers[i].EndMarker();
        }
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