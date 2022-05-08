using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuButtons : MonoBehaviour
{
    public Texture2D cubeImg;
    [SerializeField]
    AudioSource source;
    [SerializeField]
    AudioClip btnClick,selectionFX;
    [SerializeField]
    Sprite soundOn, soundOff;
    [SerializeField]
    Image soundImg;
    [SerializeField]
    SelectableMarker optionsMarker, shopMarker,challengeMarker,tutorialMarker;
    [SerializeField]
    GameObject optionsScreen, shopScreen,tutorialScreen;
    [SerializeField]
    Button btnLife, btnCrono, btnCalculator;
    [SerializeField]
    TextMeshProUGUI starsTxt,lifePriceTxt,cronoPriceTxt,calculatorPriceTxt;

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        GLOBALS.StartData();
    }

    void Start()
    {
        shopScreen.SetActive(false);
        optionsScreen.SetActive(false);
        tutorialScreen.SetActive(false);
        challengeMarker.SetSelectable(GLOBALS.player.CheckDailyChallenge());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) OnButtonToggle(MenuItem.MENU_ADVENTURE);
        if (Input.GetKeyDown(KeyCode.F2)) OnButtonToggle(MenuItem.MENU_CHALLENGE);
        if (Input.GetKeyDown(KeyCode.F3)) OnButtonToggle(MenuItem.MENU_SHOP);
    }

    public void OnButtonToggle(MenuItem item)
    {       
        switch (item)
        {
            case MenuItem.MENU_ADVENTURE:
                if (GLOBALS.soundOn) source.PlayOneShot(selectionFX);
                SceneManager.LoadScene(1);
                break;
            case MenuItem.MENU_CHALLENGE:
                if (GLOBALS.soundOn) source.PlayOneShot(selectionFX);
                GLOBALS.currentGameMode = GameMode.MODE_CHALLENGE;
                SceneManager.LoadScene(2);
                break;
            case MenuItem.MENU_SHOP:
                if (GLOBALS.soundOn) source.PlayOneShot(selectionFX);
                OpenShop();
                break;
            case MenuItem.MENU_OPTIONS:
                if (GLOBALS.soundOn) source.PlayOneShot(selectionFX);
                OpenOptions();
                break;
            case MenuItem.MENU_TUTORIAL:
                if (GLOBALS.soundOn) source.PlayOneShot(selectionFX);
                OpenTutorial();
                break;
            default:
                Debug.Log("Menu item not identified");
                break;
        }
    }

    public void OpenOptions()
    {
        optionsMarker.SetSelectable(false);
        optionsScreen.SetActive(true);
        if (GLOBALS.soundOn) soundImg.sprite = soundOn;
        else soundImg.sprite = soundOff;
        optionsScreen.GetComponent<Animator>().SetTrigger("PopIn");
    }

    public void CloseOptions()
    {
        if (GLOBALS.soundOn) source.PlayOneShot(btnClick);
        optionsMarker.SetSelectable(true);
        //optionsScreen.SetActive(false);
        optionsScreen.GetComponent<Animator>().SetTrigger("PopOut");
    }

    public void OpenShop()
    {
        shopMarker.SetSelectable(false);
        shopScreen.SetActive(true);
        lifePriceTxt.text = GLOBALS.LIFEPRICE.ToString();
        cronoPriceTxt.text = GLOBALS.CRONOPRICE.ToString();
        calculatorPriceTxt.text = GLOBALS.CALCULATORPRICE.ToString();
        shopScreen.GetComponent<Animator>().SetTrigger("PopIn");
        UpdateShop();
    }

    public void CloseShop()
    {
        if (GLOBALS.soundOn) source.PlayOneShot(btnClick);
        shopScreen.GetComponent<Animator>().SetTrigger("PopOut");
        shopMarker.SetSelectable(true);
        //shopScreen.SetActive(false);
    }

    public void OpenTutorial()
    {
        tutorialMarker.SetSelectable(false);
        tutorialScreen.SetActive(true);
        tutorialScreen.GetComponent<Animator>().SetTrigger("PopIn");
    }

    public void CloseTutorial()
    {
        if (GLOBALS.soundOn) source.PlayOneShot(btnClick);
        tutorialScreen.GetComponent<Animator>().SetTrigger("PopOut");
        tutorialMarker.SetSelectable(true);
    }

    private void UpdateShop()
    {
        if (GLOBALS.player.shopLifes < 2 && GLOBALS.player.stars >= GLOBALS.LIFEPRICE) btnLife.interactable = true;
        else btnLife.interactable = false;
        if (GLOBALS.player.stars >= GLOBALS.CRONOPRICE) btnCrono.interactable = true;
        else btnCrono.interactable = false;
        if (GLOBALS.player.stars >= GLOBALS.CALCULATORPRICE) btnCalculator.interactable = true;
        else btnCalculator.interactable = false;
        starsTxt.text = GLOBALS.player.stars.ToString();
    }

    public void BuyLife()
    {
        if (GLOBALS.soundOn) source.PlayOneShot(btnClick);
        GLOBALS.player.stars -= GLOBALS.LIFEPRICE;
        GLOBALS.player.lifeUpgrades += 1;
        GLOBALS.player.lifes += 1;
        GLOBALS.player.MaxLifes += 1;
        UpdateShop();
    }

    public void BuyCrono()
    {
        if (GLOBALS.soundOn) source.PlayOneShot(btnClick);
        GLOBALS.player.stars -= GLOBALS.CRONOPRICE;
        GLOBALS.player.cronoPower += 1;
        UpdateShop();
    }

    public void BuyCalculator()
    {
        if (GLOBALS.soundOn) source.PlayOneShot(btnClick);
        GLOBALS.player.stars -= GLOBALS.CALCULATORPRICE;
        GLOBALS.player.calculatorPower += 1;
        UpdateShop();
    }

    public void ToggleSound()
    {
        if (GLOBALS.soundOn) source.PlayOneShot(btnClick);
        GLOBALS.soundOn = !GLOBALS.soundOn;
        if (GLOBALS.soundOn) soundImg.sprite = soundOn;
        else soundImg.sprite = soundOff;
        GLOBALS.SaveGame();
    }

    public void QuitGame()
    {
        if (GLOBALS.soundOn) source.PlayOneShot(btnClick);
        Application.Quit();
    }

    public void DownloadCube()
    {
        if (GLOBALS.soundOn) source.PlayOneShot(btnClick);
        byte[] data = null;

        data = cubeImg.EncodeToJPG(100);
        Debug.Log("Converted to byte");
        if (data != null)
        {
            NativeGallery.SaveImageToGallery(data, "ProtoAR", "CubeAR.jpg", (success, path) => ProceesSaveResult(success, path));
            Debug.Log("Image saved");
        }
        else Debug.LogError("Image conversion to JPG byte array error.");
    }

    public void ProceesSaveResult(bool res, string path)
    {
        Debug.Log("Media save result: " + res + " " + path);
        if (res) ShowAndroidToast("Cube img saved to your galley.");       
        else ShowAndroidToast("Could not save image.");
    }

    public void ResetSave()
    {
        GLOBALS.ResetSaveFile();
    }

    private void ShowAndroidToast(string msg)
    {
        //create a Toast class object
        AndroidJavaClass toastClass =
                    new AndroidJavaClass("android.widget.Toast");

        //create an array and add params to be passed
        object[] toastParams = new object[3];
        AndroidJavaClass unityActivity =
          new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        toastParams[0] =
                     unityActivity.GetStatic<AndroidJavaObject>
                               ("currentActivity");
        toastParams[1] = msg;
        toastParams[2] = toastClass.GetStatic<int>
                               ("LENGTH_LONG");

        //call static function of Toast class, makeText
        AndroidJavaObject toastObject =
                        toastClass.CallStatic<AndroidJavaObject>
                                      ("makeText", toastParams);

        //show toast
        toastObject.Call("show");
    }
}