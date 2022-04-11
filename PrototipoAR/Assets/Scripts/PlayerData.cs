using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerData
{
    public int stars, calculatorPower, cronoPower;
    public int MaxLifes, lifes, lifeUpgrades;
    DateTime lastTime;
    public float activeCd;

    public PlayerData()
    {
        activeCd = 0;
        lifeUpgrades = 0;
        lifes = 2;
        MaxLifes = 5 + lifeUpgrades;
        stars = 0;
        calculatorPower = 0;
        cronoPower = 0;
        lastTime = DateTime.Now;
    }

    public bool CheckDailyChallenge()
    {
        DateTime now = DateTime.Now;
        if (now.Year > lastTime.Year) return true;
        else if (now.Month > lastTime.Month) return true;
        else if (now.Day > lastTime.Day) return true;
        else return false;
    }

    public bool LifeCD()
    {
        if (lifes < MaxLifes)
        {
            activeCd += (float)(DateTime.Now - lastTime).TotalSeconds;
            int lifeRecovered = (int)Math.Truncate(activeCd / GLOBALS.LIFERECOVERYTIME);
            UpdateLife(lifeRecovered);
            activeCd -= lifeRecovered * GLOBALS.LIFERECOVERYTIME;
            lastTime = DateTime.Now;
            if (lifes < MaxLifes) return true;
            else return false;
        }
        else
        {
            lastTime = DateTime.Now;
            return false;
        }
    }

    public DateTime GetLastTime()
    {
        return lastTime;
    }

    public void SetLastTime(int day, int month, int year, int hour, int min, int sec)
    {
        lastTime = new DateTime(day,month,year,hour,min,sec);
    }

    public void UpdateLife(int num)
    {
        lifes += num;
        if (lifes < 0) lifes = 0;
        else if (lifes > MaxLifes) lifes = MaxLifes;
        lastTime = DateTime.Now;
    }
}