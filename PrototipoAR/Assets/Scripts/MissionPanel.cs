using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionPanel : MonoBehaviour
{
    [SerializeField]
    AdventureModeManager manager;
    [SerializeField]
    Color starFill, starEmpty;
    [SerializeField]
    Image star1, star2, star3;
    [SerializeField]
    TextMeshProUGUI levelTxt;
    [SerializeField]
    Button playBtn;

    int currentLevel;
    MissionState currentState;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void SetUpPanel(int level,bool star_1, bool star_2, bool star_3, MissionState state )
    {
        currentLevel = level;
        levelTxt.text = "Level "+level.ToString();
        if (star_1) star1.color = starFill;
        else star1.color = starEmpty;

        if (star_2) star2.color = starFill;
        else star2.color = starEmpty;

        if (star_3) star3.color = starFill;
        else star3.color = starEmpty;

        currentState = state;
        if(state == MissionState.MISSION_LOCKED) playBtn.interactable = false;
        else playBtn.interactable = true;       
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    public void StartMission()
    {
        if (currentState == MissionState.MISSION_COMPLETED) Debug.Log("Mission completed");//replay
        else Debug.Log("Mission to do");//to complete

        manager.StartMision(currentLevel);
    }
}