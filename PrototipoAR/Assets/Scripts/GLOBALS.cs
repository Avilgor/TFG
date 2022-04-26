using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    MODE_ADVENTURE = 0,
    MODE_CHALLENGE
}

public static class GLOBALS 
{
    public const int LIFERECOVERYTIME = 5;
    public const int LIFEPRICE = 40;
    public const int CRONOPRICE = 5; 
    public const int CALCULATORPRICE = 10;
    public static SelectionFill selectionFillUI = null;
    public static GameController gameController = null;
    public static GameMarkerManager gameMarkerManager = null;
    public static PlayerData player = null;
    public static GameSoundManager gameSoundManager = null;

    public static bool soundOn;
    public static bool dataLoaded = false;
    public static bool replayMission = false;
    public static Dictionary<int, NodeInfo> infoNodes = new Dictionary<int, NodeInfo>();
    public static int currentNode = 0;
    public static GameMode currentGameMode = GameMode.MODE_ADVENTURE;

    private static bool startUp = false;

    public static void StartData()
    {
        if (!startUp)
        {
            soundOn = true;
            player = new PlayerData();
            LoadDefaultNodeData();
            if (Encription.DecryptFile())
            {
                //Load save
                if (XMLSerialization.LoadXMLData())
                {
                    player.LifeCD();
                    Encription.EncryptFile();
                }
                else SaveGame();
            }
            else
            {
                Debug.Log("Error loading save data: Save file not found.");
                SaveGame();
            }
            startUp = true;
            Debug.Log("App start-up done.");
        }
    }

    public static void ResetSaveFile()
    {
        soundOn = true;
        player = new PlayerData();
        LoadDefaultNodeData();
        SaveGame();
    }

    public static void SaveGame()
    {
        player.LifeCD();
        XMLSerialization.SaveXMLData();
        Encription.EncryptFile();
    }

    public static void ShowAndroidToast(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }
    }

    public static void LoadDefaultNodeData()
    {
        if (!dataLoaded)
        {
            infoNodes.Add(1, new NodeInfo(1, MissionState.MISSION_COMPLETED, true, false, false, Difficulty.DFF_EASY, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM})
            }));
            infoNodes.Add(2, new NodeInfo(2, MissionState.MISSION_UNLOCKED, false, false, false, Difficulty.DFF_EASY, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM,OperatorType.OP_SUM})
            }));
            infoNodes.Add(3, new NodeInfo(3, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY2, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM,OperatorType.OP_SUM})
            }));
            infoNodes.Add(4, new NodeInfo(4, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_MED, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM,OperatorType.OP_SUM})
            }));
            infoNodes.Add(5, new NodeInfo(5, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_HARD, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM,OperatorType.OP_SUM})
            }));
            infoNodes.Add(6, new NodeInfo(6, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB})
            }));
            infoNodes.Add(7, new NodeInfo(7, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB,OperatorType.OP_SUB})
            }));
            infoNodes.Add(8, new NodeInfo(8, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY2, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB,OperatorType.OP_SUB})
            }));
            infoNodes.Add(9, new NodeInfo(9, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_MED, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB,OperatorType.OP_SUB})
            }));
            infoNodes.Add(10, new NodeInfo(10, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_HARD, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB,OperatorType.OP_SUB})
            }));
            infoNodes.Add(11, new NodeInfo(11, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_MUL})
            }));
            infoNodes.Add(12, new NodeInfo(12, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_MUL}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_MUL}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_MUL,OperatorType.OP_MUL})
            }));
            infoNodes.Add(13, new NodeInfo(13, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY2, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_MUL}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_MUL}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_MUL,OperatorType.OP_MUL})
            }));
            infoNodes.Add(14, new NodeInfo(14, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_MED, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_MUL}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_MUL}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_MUL,OperatorType.OP_MUL})
            }));
            infoNodes.Add(15, new NodeInfo(15, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_HARD, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_MUL}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_MUL}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_MUL,OperatorType.OP_MUL})
            }));
            infoNodes.Add(16, new NodeInfo(16, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_DIV})
            }));
            infoNodes.Add(17, new NodeInfo(17, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_DIV}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_DIV}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_DIV,OperatorType.OP_DIV})
            }));
            infoNodes.Add(18, new NodeInfo(18, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY2, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_DIV}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_DIV}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_DIV,OperatorType.OP_DIV})
            }));
            infoNodes.Add(19, new NodeInfo(19, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_MED, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_DIV}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_DIV}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_DIV,OperatorType.OP_DIV})
            }));
            infoNodes.Add(20, new NodeInfo(20, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_HARD, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_DIV}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_DIV}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_DIV,OperatorType.OP_DIV})
            }));
            infoNodes.Add(21, new NodeInfo(21, MissionState.MISSION_LOCKED, false, false, false,Difficulty.DFF_EASY, new List<NodeOperations>() { 
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM})
            }));
            infoNodes.Add(22, new NodeInfo(22, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY, new List<NodeOperations>() {                    
                    new NodeOperations(new List<OperatorType>() { 
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM})
            }));
            infoNodes.Add(23, new NodeInfo(23, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM})
            }));
            infoNodes.Add(24, new NodeInfo(24, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM})
            }));
            infoNodes.Add(25, new NodeInfo(25, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY2, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM})
            }));
            infoNodes.Add(26, new NodeInfo(26, MissionState.MISSION_LOCKED, false, false, false,Difficulty.DFF_EASY2, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM})
            }));
            infoNodes.Add(27, new NodeInfo(27, MissionState.MISSION_LOCKED, false, false, false,Difficulty.DFF_EASY2, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM})
            }));
            infoNodes.Add(28, new NodeInfo(28, MissionState.MISSION_LOCKED, false, false, false,Difficulty.DFF_MED, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM})
            }));
            infoNodes.Add(29, new NodeInfo(29, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_MED, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM})
            }));
            infoNodes.Add(30, new NodeInfo(30, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM, OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM, OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM, OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM, OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM, OperatorType.OP_SUM})
            }));
            infoNodes.Add(31, new NodeInfo(31, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB})
            }));
            infoNodes.Add(32, new NodeInfo(32, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                         OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB})
            }));
            infoNodes.Add(33, new NodeInfo(33, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB, OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB, OperatorType.OP_SUB})
            }));
            infoNodes.Add(34, new NodeInfo(34, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB, OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB, OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB, OperatorType.OP_SUB})
            }));
            infoNodes.Add(35, new NodeInfo(35, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY2, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB})
            }));
            infoNodes.Add(36, new NodeInfo(36, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY2, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB, OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB, OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB, OperatorType.OP_SUB})
            }));
            infoNodes.Add(37, new NodeInfo(37, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM, OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB, OperatorType.OP_SUM})
            }));
            infoNodes.Add(38, new NodeInfo(38, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY2, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM, OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM, OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB, OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB, OperatorType.OP_SUM})
            }));
            infoNodes.Add(39, new NodeInfo(39, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY2, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB, OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB, OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM, OperatorType.OP_SUB, OperatorType.OP_SUM})
            }));
            infoNodes.Add(40, new NodeInfo(40, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY2, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB, OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM, OperatorType.OP_SUM, OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM, OperatorType.OP_SUB, OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB, OperatorType.OP_SUB, OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB, OperatorType.OP_SUM, OperatorType.OP_SUM})
            }));

            dataLoaded = true;
            Debug.Log("Loaded default data");
        }
    }
}
