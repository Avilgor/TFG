using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AdventureModeManager : MonoBehaviour
{
    [SerializeField]
    Sprite SPcurrentMision, SPlockedMission, SPcompletedMission;

    [SerializeField]
    List<AdventureNode> nodes = new List<AdventureNode>();
    

    void Start()
    {
        
    }

    private void LoadNodes()
    {
        foreach (KeyValuePair<int,NodeInfo> node in GLOBALS.infoNodes)
        {
            
        }
    }

    public void BackToMainManu()
    {
        SceneManager.LoadScene(0);
    }

    public void StartMision(int level)
    {
        SceneManager.LoadScene(4);
    }
}
