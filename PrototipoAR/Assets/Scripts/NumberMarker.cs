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
    Image holeSprite,lockSprite;

    SelectableMarker selectable;
    public int number;
    bool faceClosed;
    int lockBreaks,lockSteps,lockIndex;

    private void Awake()
    {
        selectable = GetComponent<SelectableMarker>();
    }

    private void Start()
    {
        lockBreaks = 0;
        faceClosed = false;
        holeSprite.gameObject.SetActive(false);
        lockSprite.gameObject.SetActive(false);
    }

    public void SetNumber(int num)
    {
        number = num;
        text.text = num.ToString();
        selectable.SetSelectable(true);
        text.enabled = true;
        faceClosed = false;
    }

    public void EndMarker()
    {
        text.enabled = false;
        faceClosed = true;
        selectable.SetSelectable(false);
    }

    public void OnExecute()
    {
        if (!GLOBALS.gameController.CheckNumber(number))
        {
            selectable.SetSelectable(false);
            text.enabled = false;
            faceClosed = true;
        }
    }

    public void HoledFace(Sprite sp)
    {
        holeSprite.sprite = sp;
        holeSprite.gameObject.SetActive(true);
        text.enabled = false;
        selectable.SetSelectable(false);
        faceClosed = true;
    }

    public void LockFace(Sprite sp,int taps,int steps)
    {
        lockIndex = 1;
        lockSteps = steps;
        lockBreaks = taps;
        lockSprite.sprite = sp;
        lockSprite.gameObject.SetActive(true);
        text.enabled = false;
        selectable.SetSelectable(false);
        faceClosed = true;
    }

    public void DisableAlterations()
    {
        lockSprite.gameObject.SetActive(false);
        holeSprite.gameObject.SetActive(false);
    }

    public void ToggleSelectable(bool value)
    {
        selectable.SetSelectable(value);
    }

    public void ToggleText(bool value)
    {
        text.enabled = value;
    }

    public bool IsOpen()
    {
        return !faceClosed;
    }

    public void LockTap()
    {
        lockBreaks--;
        if (lockBreaks <= 0)
        {
            faceClosed = false;
            selectable.SetSelectable(true);
            text.enabled = true;
            lockSprite.gameObject.SetActive(false);
        }
        else if ((lockBreaks % lockSteps) == 0)
        {
            lockIndex++;
            lockSprite.sprite = GLOBALS.gameMarkerManager.GetLockSprite(lockIndex);
        }
    }
}