using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameMarkerManager : MonoBehaviour
{
    [SerializeField]
    Sprite cubeHole, cubeLock1, cubeLock2, cubeLock3, cubeLock4, cubeLock5;
    public List<NumberMarker> markers = new List<NumberMarker>();

    List<GameObject> markersGo = new List<GameObject>();

    private void Awake()
    {
        GLOBALS.gameMarkerManager = this;        
    }

    public void StartMarkers()
    { 
        for (int i = 0; i < markers.Count; i++)
        {
            markersGo.Add(markers[i].gameObject);
            markers[i].StartMarker();
        }
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

    public void AlterCube(GameAlteration alter)
    {
        int aux;
        switch (alter)
        {
            case GameAlteration.ALT_CUBEHOLED:
                int total = 0;
                int sol = GLOBALS.gameController.GetOperationResult();
                do
                {
                    aux = Random.Range(0, markers.Count);
                    if (markers[aux].number != sol && markers[aux].IsOpen())
                    {
                        markers[aux].HoledFace(cubeHole);
                        total++;
                    }
                } while (total < 3);
                break;

            case GameAlteration.ALT_CUBECRAZY:
                StartCoroutine(CrazyCube());
                break;

            case GameAlteration.ALT_CUBETRAP:
                List<NumberMarker> toSet = new List<NumberMarker>(markers);
                switch (GLOBALS.gameController.GetNodeDifficulty())
                {
                    case Difficulty.DFF_EASY:
                        for (int i = 0; i < 2; i++)
                        {
                            aux = Random.Range(0, toSet.Count);
                            toSet[aux].LockFace(cubeLock1, 5, 1);
                            toSet.RemoveAt(aux);
                        }
                        break;
                    case Difficulty.DFF_EASY2:
                        for (int i = 0; i < 3; i++)
                        {
                            aux = Random.Range(0, toSet.Count);
                            toSet[aux].LockFace(cubeLock1, 10, 2);
                            toSet.RemoveAt(aux);
                        }
                        break;
                    case Difficulty.DFF_MED:
                        for (int i = 0; i < 4; i++)
                        {
                            aux = Random.Range(0, toSet.Count);
                            toSet[aux].LockFace(cubeLock1, 15, 3);
                            toSet.RemoveAt(aux);
                        }
                        break;
                    case Difficulty.DFF_HARD:
                        for (int i = 0; i < 4; i++)
                        {
                            aux = Random.Range(0, toSet.Count);
                            toSet[aux].LockFace(cubeLock1, 20, 4);
                            toSet.RemoveAt(aux);
                        }
                        break;
                }
                break;

            case GameAlteration.ALT_GOLD:
            case GameAlteration.ALT_NONE:
                for (int i = 0; i < markers.Count; i++)
                {
                    markers[i].DisableAlterations();
                    markers[i].ToggleSelectable(true);
                    markers[i].ToggleText(true);
                }
                break;
            default:
                Debug.Log("Cube alteration " + alter + " not found.");
                break;
        }
    }

    public Sprite GetLockSprite(int index)
    {
        switch(index)
        {
            case 1:
                return cubeLock1;
            case 2:
                return cubeLock2;
            case 3:
                return cubeLock3;
            case 4:
                return cubeLock4;
            case 5:
                return cubeLock5;
            default:
                return cubeLock1;
        }
    }

    private List<T> ShuffleList<T>(List<T> list)
    {
        int count = list.Count;
        int last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            int r = Random.Range(i, count);
            var tmp = list[i];
            list[i] = list[r];
            list[r] = tmp;
        }
        return list;
    }

    IEnumerator CrazyCube()
    {
        yield return new WaitForSeconds(7);
        List<NumberMarker> openList = new List<NumberMarker>(); 
        List<int> numberList = new List<int>();

        for (int i = 0; i < markers.Count; i++)
        {
            if (markers[i].IsOpen())
            {
                openList.Add(markers[i]);
                numberList.Add(markers[i].number);
            }
        }
      
        if (openList.Count > 1)
        {
            openList = ShuffleList(openList);
            numberList = ShuffleList(numberList);
           
            for (int i = 0; i < openList.Count; i++)
            {
                openList[i].SetNumber(numberList[i]);
            }
        }
        GLOBALS.gameSoundManager.PlayVariationCrazy();
        if (GLOBALS.gameController.currentAlteration == GameAlteration.ALT_CUBECRAZY) StartCoroutine(CrazyCube());
    }
}