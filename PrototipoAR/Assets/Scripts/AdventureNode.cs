using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MissionState
{
    MISSION_COMPLETED = 0,
    MISSION_LOCKED,
    MISSION_UNLOCKED
}

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class AdventureNode : MonoBehaviour
{
    [SerializeField]
    MissionPanel panel;
    [SerializeField]
    Sprite sprite_locked, sprite_unlocked, sprite_completed;
    public MissionState state;
    public int missionIndex;
    public bool star1, star2, star3;

    Button btn;
    Image btnImg;

    private void Awake()
    {
        btn = GetComponent<Button>();
        btnImg = GetComponent<Image>();
    }

    private void Start()
    {
        SetButton();
    }

    //For loading progress
    public void SetNode(int index,MissionState nodeState,bool s1,bool s2,bool s3)
    {
        missionIndex = index;
        state = nodeState;
        star1 = s1;
        star2 = s2;
        star3 = s3;
        SetButton();
    }

    public void SetButton()
    {
        switch (state)
        {
            case MissionState.MISSION_LOCKED:
                btn.interactable = false;
                btnImg.sprite = sprite_locked;
                break;

            case MissionState.MISSION_COMPLETED:
                btn.interactable = true;
                btnImg.sprite = sprite_completed;
                break;

            case MissionState.MISSION_UNLOCKED:
                btn.interactable = true;
                btnImg.sprite = sprite_unlocked;
                break;

            default:
                Debug.Log("Mission state not found");
                break;
        }
    }

    public void OpenMission()
    {
        panel.gameObject.SetActive(true);
        panel.SetUpPanel(missionIndex, star1, star2, star3, state);
    }
}