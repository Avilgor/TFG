using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberMarker : MonoBehaviour
{
    public TextMeshProUGUI text;
    
    int number;

    void Start()
    {
    }

    public void SetNumber(int num)
    {
        number = num;
        text.text = num.ToString();
        gameObject.SendMessage("SetSelectable", true);
        text.enabled = true;
    }

    public void EndMarker()
    {
        text.enabled = false;
        gameObject.SendMessage("SetSelectable", false);
    }

    public void OnExecute()
    {
        if(!GLOBALS.gameController.CheckNumber(number))
        {                    
            gameObject.SendMessage("SetSelectable",false);
            text.enabled = false;
        }
    }
}