using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public Texture2D cubeImg;

    [SerializeField]
    SelectableMarker optionsMarker;
    [SerializeField]
    GameObject optionsScreen;

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        GLOBALS.StartData();
    }

    void Start()
    {
        if(GLOBALS.player.CheckDailyChallenge())
        optionsScreen.SetActive(false);
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
                SceneManager.LoadScene(1);
                break;
            case MenuItem.MENU_CHALLENGE:
                GLOBALS.currentGameMode = GameMode.MODE_CHALLENGE;
                SceneManager.LoadScene(2);
                break;
            case MenuItem.MENU_SHOP:
                SceneManager.LoadScene(3);
                break;
            case MenuItem.MENU_OPTIONS:
                OpenOptions();
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
    }

    public void CloseOptions()
    {
        optionsMarker.SetSelectable(true);
        optionsScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void DownloadCube()
    {
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