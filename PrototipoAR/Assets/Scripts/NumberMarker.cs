using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberMarker : MonoBehaviour
{
    public TextMeshProUGUI text;
    
    bool isSolution;
    int number;

    void Start()
    {
        isSolution = false;
    }

    public void SetNumber(int num,bool sol)
    {
        number = num;
        text.text = num.ToString();
        isSolution = sol;
        gameObject.SendMessage("SetSelectable", true);
    }

    public void OnExecute()
    {
        if (isSolution)
        {
            Debug.Log("Correct number");
        }
        else
        {
            Debug.Log("Incorrect number");
            gameObject.SendMessage("SetSelectable",false);
            text.enabled = false;
        }
    }
}