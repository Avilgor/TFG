using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuItem
{
    MENU_OPTIONS = 0,
    MENU_ADVENTURE,
    MENU_CHALLENGE,
    MENU_TUTORIAL,
    MENU_SHOP
}

public class MenuMarker : MonoBehaviour
{
    public MenuItem menu;

    [SerializeField]
    MenuButtons buttons;

    private void Start()
    {
        if (menu == MenuItem.MENU_CHALLENGE)
        {
            if (GLOBALS.player.CheckDailyChallenge()) GetComponent<SelectableMarker>().SetSelectable(true);
            else GetComponent<SelectableMarker>().SetSelectable(false);
        }
    }

    public void OnExecute()
    {
        buttons.OnButtonToggle(menu);
    }
}
