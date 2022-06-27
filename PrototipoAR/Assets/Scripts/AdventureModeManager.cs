using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class AdventureModeManager : MonoBehaviour
{
    [SerializeField]
    AudioSource source;
    [SerializeField]
    AudioClip btnClick,startMission;

    [SerializeField]
    Sprite SPcurrentMision, SPlockedMission, SPcompletedMission;

    [SerializeField]
    TextMeshProUGUI lifesTxt;

    [SerializeField]
    List<AdventureNode> nodes = new List<AdventureNode>();

    float lifeCd;
    bool lifeRecovery;

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

        if (lifeRecovery)
        {
            lifeCd += Time.deltaTime;
            if (lifeCd >= GLOBALS.LIFERECOVERYTIME)
            {
                lifeCd = 0;
                GLOBALS.player.UpdateLife(1);
                lifesTxt.text = GLOBALS.player.lifes.ToString();
            }
        }
    }

    private void LoadNodes()
    {
        for (int i = 0;i < nodes.Count;i++)
        {
            if (GLOBALS.infoNodes.ContainsKey(i+1))
            {
                nodes[i].SetNode(i+1,GLOBALS.infoNodes[i+1].state, GLOBALS.infoNodes[i+1].starCompleted, GLOBALS.infoNodes[i+1].starTime, GLOBALS.infoNodes[i+1].starError);
            }
        }
    }

    public void BackToMainManu()
    {
        if (GLOBALS.soundOn) source.PlayOneShot(btnClick);
        SceneManager.LoadScene(0);
    }

    public void StartMision(int level)
    {
        if (GLOBALS.player.lifes > 0)
        {
            if (GLOBALS.soundOn) source.PlayOneShot(startMission);
            if (GLOBALS.infoNodes[level].state == MissionState.MISSION_COMPLETED) GLOBALS.replayMission = true;
            else GLOBALS.replayMission = false;

            GLOBALS.player.UpdateLife(-1);
            
            GLOBALS.currentGameMode = GameMode.MODE_ADVENTURE;
            GLOBALS.currentNode = level;
            SceneManager.LoadScene(2);
        }
    }
}
