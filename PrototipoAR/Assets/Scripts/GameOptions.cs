using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOptions : MonoBehaviour
{
    [SerializeField]
    GameObject optionsPanel;
    bool open;  

    void Start()
    {
        optionsPanel.SetActive(false);
        open = false;    
    }

    public void ToggleOptions()
    {
        if (open)
        {
            optionsPanel.SetActive(false);
            open = false;
        }
        else
        {
            optionsPanel.SetActive(true);
            open = true;
        }
    }

    public void ExitGame()
    {
        if (GLOBALS.currentGameMode == GameMode.MODE_CHALLENGE) SceneManager.LoadScene(0);
        else SceneManager.LoadScene(1);
    }
}
