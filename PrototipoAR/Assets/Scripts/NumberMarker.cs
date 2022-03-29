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

    public void OnExecute()
    {
        if(GLOBALS.gameController.CheckNumber(number))
        { 
        
        }
        else
        {            
            gameObject.SendMessage("SetSelectable",false);
            text.enabled = false;
        }
    }
}