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

    private void Awake()
    {
        GLOBALS.LoadDefaultNodeData();
    }

    void Start()
    {
        LoadNodes();
    }

    private void LoadNodes()
    {
        for (int i = 0;i < nodes.Count;i++)
        {
            if (GLOBALS.infoNodes.ContainsKey(i))
            {
                nodes[i].SetNode(i,GLOBALS.infoNodes[i].state, GLOBALS.infoNodes[i].star1, GLOBALS.infoNodes[i].star2, GLOBALS.infoNodes[i].star3);
            }
        }
    }

    public void BackToMainManu()
    {
        SceneManager.LoadScene(0);
    }

    public void StartMision(int level)
    {
        GLOBALS.currentGameMode = GameMode.MODE_ADVENTURE;
        GLOBALS.currentNode = level;
        SceneManager.LoadScene(2);
    }
}
