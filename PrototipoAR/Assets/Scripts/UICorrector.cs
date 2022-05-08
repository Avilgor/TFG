using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UICorrector : MonoBehaviour
{
    public Vector2 defaultScreenSize;
    Vector3 defaultSize;
    RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        defaultSize = rect.localScale;
    }

    void Start()
    {
        Debug.Log("Starting size: " + rect.localScale.x + "/" + rect.localScale.y);
        Correct();
    }

    public void Correct()
    {
        rect.localScale = new Vector3(
            (Screen.width * defaultSize.x) / defaultScreenSize.x, 
            (Screen.height * defaultSize.y) / defaultScreenSize.y, 
            rect.localScale.z);
        Debug.Log("New size: "+rect.localScale.x+"/"+rect.localScale.y);
    }
}