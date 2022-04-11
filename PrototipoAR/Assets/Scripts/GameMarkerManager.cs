using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CubeAlteration
{
    CUBE_NONE = 0,
    CUBE_HOLED,
    CUBE_TRAP,
    CUBE_CRAZY
}

public class GameMarkerManager : MonoBehaviour
{
    public List<NumberMarker> markers = new List<NumberMarker>();

    List<GameObject> markersGo = new List<GameObject>();
    public CubeAlteration currentAlteration;

    private void Awake()
    {
        GLOBALS.gameMarkerManager = this;
    }

    void Start()
    {
        currentAlteration = CubeAlteration.CUBE_NONE;
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

    public void AlterCube(CubeAlteration alter)
    {
        switch (alter)
        {
            case CubeAlteration.CUBE_HOLED:
                currentAlteration = alter;
                break;
            case CubeAlteration.CUBE_CRAZY:
                StartCoroutine(CrazyCube());
                currentAlteration = alter;
                break;
            case CubeAlteration.CUBE_TRAP:
                currentAlteration = alter;
                break;
            default:
                break;
        }
    }

    IEnumerator CrazyCube()
    {
        yield return new WaitForSeconds(5);

        if (currentAlteration == CubeAlteration.CUBE_CRAZY) StartCoroutine(CrazyCube());
    }
}