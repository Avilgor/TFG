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
    public static SelectionFill selectionFillUI = null;
    public static GameController gameController = null;
    public static GameMarkerManager gameMarkerManager = null;
    public static PlayerData player = null;

    public static bool dataLoaded = false;
    public static Dictionary<int, NodeInfo> infoNodes = new Dictionary<int, NodeInfo>();
    public static int currentNode = 0;
    public static GameMode currentGameMode = GameMode.MODE_ADVENTURE;

    private static bool startUp = false;

    public static void StartData()
    {
        if (!startUp)
        {
            player = new PlayerData();
            LoadDefaultNodeData();
            if (Encription.DecryptFile())
            {
                //Load save
                XMLSerialization.LoadXMLData();
                player.LifeCD();
                Encription.EncryptFile();
            }
            else Debug.Log("Error loading save data: Save file not found.");
            startUp = true;
            Debug.Log("App start up done.");
        }
    }

    public static void SaveGame()
    {
        player.LifeCD();
        XMLSerialization.SaveXMLData();
        Encription.EncryptFile();
    }

    public static void LoadDefaultNodeData()
    {
        if (!dataLoaded)
        {
            infoNodes.Add(1, new NodeInfo(1, MissionState.MISSION_UNLOCKED, false, false, false,Difficulty.DFF_EASY, new List<NodeOperations>() { 
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM})
            }));
            infoNodes.Add(2, new NodeInfo(2, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY, new List<NodeOperations>() {                    
                    new NodeOperations(new List<OperatorType>() { 
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM})
            }));
            infoNodes.Add(3, new NodeInfo(3, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM})
            }));
            infoNodes.Add(4, new NodeInfo(4, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY, new List<NodeOperations>() {
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
            infoNodes.Add(5, new NodeInfo(5, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY2, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM})
            }));
            infoNodes.Add(6, new NodeInfo(6, MissionState.MISSION_LOCKED, false, false, false,Difficulty.DFF_EASY2, new List<NodeOperations>() {
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
            infoNodes.Add(7, new NodeInfo(7, MissionState.MISSION_LOCKED, false, false, false,Difficulty.DFF_EASY2, new List<NodeOperations>() {
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
            infoNodes.Add(8, new NodeInfo(8, MissionState.MISSION_LOCKED, false, false, false,Difficulty.DFF_MED, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM})
            }));
            infoNodes.Add(9, new NodeInfo(9, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_MED, new List<NodeOperations>() {
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
            infoNodes.Add(10, new NodeInfo(10, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY, new List<NodeOperations>() {
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
            infoNodes.Add(11, new NodeInfo(11, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB})
            }));
            infoNodes.Add(12, new NodeInfo(12, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY, new List<NodeOperations>() {
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
            infoNodes.Add(13, new NodeInfo(13, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB, OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB, OperatorType.OP_SUB})
            }));
            infoNodes.Add(14, new NodeInfo(14, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB, OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB, OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB, OperatorType.OP_SUB})
            }));
            infoNodes.Add(15, new NodeInfo(15, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY2, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB})
            }));
            infoNodes.Add(16, new NodeInfo(16, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY2, new List<NodeOperations>() {
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
            infoNodes.Add(17, new NodeInfo(17, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM, OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB, OperatorType.OP_SUM})
            }));
            infoNodes.Add(18, new NodeInfo(18, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY2, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM, OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM, OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB, OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB, OperatorType.OP_SUM})
            }));
            infoNodes.Add(19, new NodeInfo(19, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY2, new List<NodeOperations>() {
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB, OperatorType.OP_SUM}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUB, OperatorType.OP_SUB}),
                    new NodeOperations(new List<OperatorType>() {
                        OperatorType.OP_SUM, OperatorType.OP_SUB, OperatorType.OP_SUM})
            }));
            infoNodes.Add(20, new NodeInfo(20, MissionState.MISSION_LOCKED, false, false, false, Difficulty.DFF_EASY2, new List<NodeOperations>() {
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
