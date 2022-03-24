using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMarkerManager : MonoBehaviour
{
    public List<NumberMarker> markers = new List<NumberMarker>();

    void Start()
    {
        
    }

    public void SetMarkers(List<int> list)
    {
        List<NumberMarker> toSet = new List<NumberMarker>(markers);
        markers.Clear();        

        //Set the correct number
        int rand = Random.Range(0, toSet.Count);
        toSet[rand].SetNumber(list[0],true);
        markers.Add(toSet[rand]);
        toSet.RemoveAt(rand);
        list.RemoveAt(0);

        //Set remaining numbers
        int max = toSet.Count;
        for (int i = 0; i < max; i++)
        {
            if (toSet.Count > 1)
            {
                rand = Random.Range(0, toSet.Count);
                toSet[rand].SetNumber(list[0], false);
                markers.Add(toSet[rand]);
                toSet.RemoveAt(rand);
                list.RemoveAt(0);
            }
            else
            {
                toSet[0].SetNumber(list[0], false);
                markers.Add(toSet[0]);
                toSet.Clear();
                list.Clear();
            }
        }
    }

}
