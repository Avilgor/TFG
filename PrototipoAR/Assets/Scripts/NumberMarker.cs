using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(SelectableMarker))]
public class NumberMarker : MonoBehaviour
{
    public TextMeshProUGUI text;
    [SerializeField]
    Animator textAnim, holeAnim;

    [SerializeField]
    Image holeSprite,lockSprite;

    [SerializeField]
    GameObject holeGo, lockGo;

    SelectableMarker selectable;
    public int number;
    bool faceClosed;
    int lockBreaks,lockSteps,lockIndex;

    private void Awake()
    {
        selectable = GetComponent<SelectableMarker>();
    }

    public void StartMarker()
    {
        lockBreaks = 0;
        faceClosed = false;
        holeGo.SetActive(false);
        lockGo.SetActive(false);
    }

    public void SetNumber(int num)
    {
        number = num;
        holeGo.SetActive(false);
        holeAnim.SetTrigger("PopOut");
        lockGo.SetActive(false);
        text.text = num.ToString();
        selectable.SetSelectable(true);
        text.enabled = true;
        textAnim.SetTrigger("PopIn");
        faceClosed = false;
    }

    public void EndMarker()
    {
        //text.enabled = false;
        textAnim.SetTrigger("PopOut");
        faceClosed = true;
        selectable.SetSelectable(false);
    }

    /*public void SetButtonSelection()
    {
        selectionButton.enabled = true;
        fillSelection.enabled = false;
    }

    public void SetSelectionFill()
    {
        selectionButton.enabled = false; 
        fillSelection.enabled = true;
    }*/

    public void OnExecute()
    {
        Debug.Log("Called execute");
        if (!GLOBALS.gameController.CheckNumber(number))
        {
            selectable.SetSelectable(false);
            //text.enabled = false;
            textAnim.SetTrigger("PopOut");
            faceClosed = true;
        }
    }

    public void HoledFace(Sprite sp)
    {
        holeGo.SetActive(true);
        holeSprite.sprite = sp;      
        text.enabled = false;
        textAnim.SetTrigger("PopOut");
        holeAnim.SetTrigger("PopIn");
        selectable.SetSelectable(false);
        faceClosed = true;
    }

    public void LockFace(Sprite sp,int taps,int steps)
    {
        lockIndex = 1;
        lockSteps = steps;
        lockBreaks = taps;
        lockSprite.sprite = sp;
        lockGo.SetActive(true);
        textAnim.SetTrigger("PopOut");
        text.enabled = false;
        selectable.SetSelectable(false);
        faceClosed = true;
    }

    public void DisableAlterations()
    {
        //holeGo.SetActive(false);
        holeAnim.SetTrigger("PopOut");
        lockGo.SetActive(false);
        text.enabled = true;
    }

    public void ToggleSelectable(bool value)
    {
        selectable.SetSelectable(value);
    }

    public void ToggleText(bool value)
    {
        text.enabled = value;
        if(value) textAnim.SetTrigger("PopIn");
        else textAnim.SetTrigger("PopOut");
    }

    public bool IsOpen()
    {
        return !faceClosed;
    }

    public void LockTap()
    {
        lockBreaks--;
        GLOBALS.gameSoundManager.PlayVariationLockHit();
        if (lockBreaks <= 0)
        {
            faceClosed = false;
            selectable.SetSelectable(true);
            text.enabled = true;
            textAnim.SetTrigger("PopIn");
            lockGo.SetActive(false);
            GLOBALS.gameSoundManager.PlayVariationLockBreak();
        }
        else if ((lockBreaks % lockSteps) == 0)
        {
            lockIndex++;
            lockSprite.sprite = GLOBALS.gameMarkerManager.GetLockSprite(lockIndex);
        }
    }
}