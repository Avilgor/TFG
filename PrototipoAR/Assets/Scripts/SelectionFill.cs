using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionFill : MonoBehaviour
{
    public Image fillImg;
    public int fillTime;

    bool fill;
    float timer;
    GameObject fillRequester;
    float fillAmount;

    private void Awake()
    {
        GLOBALS.selectionFillUI = this;        
    }

    // Start is called before the first frame update
    void Start()
    {
        fill = false;
        timer = 0.0f;
        fillImg.enabled = false;
    }

    void Update()
    {
        //Test
        if (Input.GetKeyDown(KeyCode.Space)) StartFill(gameObject);
        if (Input.GetKeyUp(KeyCode.Space)) StopFill();
        //
        if (fill)
        {
            timer += Time.deltaTime;
            fillImg.fillAmount = timer / fillTime;

            if (timer >= fillTime)
            {
                timer = 0;
                fillImg.fillAmount = 1;
                fill = false;
                fillImg.enabled = false;
                //Call completion
                if(fillRequester != null) fillRequester.SendMessage("OnSelectionFill");
            }
        }
    }

    public void StartFill(GameObject requester)
    {
        fill = true;
        fillImg.enabled = true;
        timer = 0;
        fillRequester = requester;
        Debug.Log("Start selection fill");
    }

    public void StopFill()
    {
        fill = false;
        fillImg.enabled = false;
        fillRequester = null;
        Debug.Log("Stop selection fill");
    }
}
