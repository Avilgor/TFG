using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOptions : MonoBehaviour
{
    [SerializeField]
    GameObject optionsPanel;
    [SerializeField]
    TextMeshProUGUI calculatorTxt, cronoTxt;
    [SerializeField]
    Button calculatorBtn, cronoBtn;
    bool open;

    void Start()
    {
        optionsPanel.SetActive(false);
        open = false;
        calculatorTxt.text = GLOBALS.player.calculatorPower.ToString();
        cronoTxt.text = GLOBALS.player.cronoPower.ToString();
        if (GLOBALS.currentGameMode == GameMode.MODE_ADVENTURE)
        {
            if (GLOBALS.player.cronoPower > 0) cronoBtn.interactable = true;
            else cronoBtn.interactable = false;
            if (GLOBALS.player.calculatorPower > 0) calculatorBtn.interactable = true;
            else calculatorBtn.interactable = false;
        }
        else
        {
            cronoBtn.interactable = false;
            calculatorBtn.interactable = false;
        }
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

    public void UseCalculator()
    {
        if (GLOBALS.player.calculatorPower > 0)
        {
            GLOBALS.player.calculatorPower--;
            calculatorTxt.text = GLOBALS.player.calculatorPower.ToString();
            GLOBALS.gameController.PowerUpCalculator();
            if (GLOBALS.player.calculatorPower <= 0) calculatorBtn.interactable = false;            
        }
    }

    public void UseCrono()
    {
        if (GLOBALS.player.cronoPower > 0)
        {
            GLOBALS.player.cronoPower--;
            cronoTxt.text = GLOBALS.player.cronoPower.ToString();
            GLOBALS.gameController.PowerUpCrono();
            if (GLOBALS.player.cronoPower <= 0) cronoBtn.interactable = false;
        }
    }

    public void ToogleCronoButton(bool val)
    {
        cronoBtn.interactable = val;
    }

    public void ToogleCalculatorButton(bool val)
    {
        calculatorBtn.interactable = val;
    }

    public void ExitGame()
    {
        if (GLOBALS.currentGameMode == GameMode.MODE_CHALLENGE) SceneManager.LoadScene(0);
        else SceneManager.LoadScene(1);
    }
}
