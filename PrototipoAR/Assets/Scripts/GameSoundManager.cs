using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundManager : MonoBehaviour
{
    [SerializeField]
    AudioSource source;

    [SerializeField]
    AudioClip btnClick,cronoFX,numberCorrect,numberWrong,variatinoGold,variationCrazy,variationHole,variationLockHit,variationLockBreak,timeOut;

    private void Awake()
    {
        GLOBALS.gameSoundManager = this;
    }

    public void PlayButtonFX()
    {
        if (GLOBALS.soundOn) source.PlayOneShot(btnClick);
    }

    public void PlayTimeOutFX()
    {
        if (GLOBALS.soundOn) source.PlayOneShot(timeOut);
    }

    public void PlayCronoFX()
    {
        if (GLOBALS.soundOn) source.PlayOneShot(cronoFX);
    }

    public void PlayCalculatorFX()
    {
        if (GLOBALS.soundOn) source.PlayOneShot(variationHole);
    }

    public void PlayNumberCorrect()
    {
        if (GLOBALS.soundOn) source.PlayOneShot(numberCorrect);
    }

    public void PlayNumberWrong()
    {
        if (GLOBALS.soundOn) source.PlayOneShot(numberWrong);
    }

    public void PlayVaritationGold()
    {
        if (GLOBALS.soundOn) source.PlayOneShot(variatinoGold);
    }

    public void PlayVaritationCrazy()
    {
        if (GLOBALS.soundOn) source.PlayOneShot(variationCrazy);
    }

    public void PlayVaritationHole()
    {
        if (GLOBALS.soundOn) source.PlayOneShot(variationHole);
    }

    public void PlayVaritationLockHit()
    {
        if (GLOBALS.soundOn) source.PlayOneShot(variationLockHit);
    }

    public void PlayVaritationLockBreak()
    {
        if (GLOBALS.soundOn) source.PlayOneShot(variationLockBreak);
    }
}