using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelRewards
{
    public static void OnLevelEnd(int level)
    {
        switch (level)
        {
            case 3:
                GLOBALS.player.cronoPower++;
                GLOBALS.ShowAndroidToast("You got 1 crono power up!");
                break;
            case 5:
                GLOBALS.player.calculatorPower++;
                GLOBALS.ShowAndroidToast("You got 1 calculator power up!");
                break;
            case 8:
                GLOBALS.player.cronoPower++;
                GLOBALS.ShowAndroidToast("You got 1 crono power up!");
                break;
            case 10:
                GLOBALS.player.lifeUpgrades++;
                GLOBALS.player.lifes++;
                GLOBALS.player.MaxLifes++;
                GLOBALS.ShowAndroidToast("You got 1 extra maximum life!");
                break;
            case 13:
                GLOBALS.player.cronoPower++;
                GLOBALS.ShowAndroidToast("You got 1 crono power up!");
                break;
            case 15:
                GLOBALS.player.calculatorPower++;
                GLOBALS.ShowAndroidToast("You got 1 calculator power up!");
                break;
            case 18:
                GLOBALS.player.cronoPower++;
                GLOBALS.ShowAndroidToast("You got 1 crono power up!");
                break;
            case 20:
                GLOBALS.player.lifeUpgrades++;
                GLOBALS.player.lifes++;
                GLOBALS.player.MaxLifes++;
                GLOBALS.ShowAndroidToast("You got 1 extra maximum life!");
                break;
            default:
                Debug.Log("Level "+level+" has no reward.");
                break;
        }
    }
}