using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class AdventureModeManager : MonoBehaviour
{
    [SerializeField]
    Sprite SPcurrentMision, SPlockedMission, SPcompletedMission;

    [SerializeField]
    TextMeshProUGUI lifesTxt;

    [SerializeField]
    List<AdventureNode> nodes = new List<AdventureNode>();

    float lifeCd;
    bool lifeRecovery;

    private void Awake()
    {
        GLOBALS.LoadDefaultNodeData();
       
    }

    void Start()
    {
        lifeRecovery = GLOBALS.player.LifeCD();
        lifeCd = GLOBALS.player.activeCd;
        lifesTxt.text = GLOBALS.player.lifes.ToString();
        LoadNodes();
    }

    private void OnDestroy()
    {
        GLOBALS.player.activeCd = lifeCd;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene(0);
        if (Input.GetKeyDown(KeyCode.A))
        {
            GLOBALS.player.UpdateLife(-1);
            lifeRecovery = GLOBALS.player.LifeCD();
            lifeCd = GLOBALS.player.activeCd;
            lifesTxt.text = GLOBALS.player.lifes.ToString();
        }

        if (lifeRecovery)
        {
            lifeCd += Time.deltaTime;
            Debug.Log(lifeCd.ToString());
            if (lifeCd >= GLOBALS.LIFERECOVERYTIME)
            {
                Debug.Log("Life CD finished");
                lifeCd = 0;
                GLOBALS.player.UpdateLife(1);
                lifesTxt.text = GLOBALS.player.lifes.ToString();
                lifeRecovery = GLOBALS.player.LifeCD();
            }
        }
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
        if (GLOBALS.player.lifes > 0)
        {
            GLOBALS.player.UpdateLife(-1);
            GLOBALS.currentGameMode = GameMode.MODE_ADVENTURE;
            GLOBALS.currentNode = level;
            SceneManager.LoadScene(2);
        }
    }
}
