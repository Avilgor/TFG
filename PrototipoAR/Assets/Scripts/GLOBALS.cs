using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GLOBALS 
{
    public static SelectionFill selectionFillUI = null;
    public static GameController gameController = null;
    public static GameMarkerManager gameMarkerManager = null;

    public static bool dataLoaded = false;
    public static Dictionary<int, NodeInfo> infoNodes = new Dictionary<int, NodeInfo>();

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
            dataLoaded = true;
        }
    }

    public static void LoadSavedNodeData()
    {
        if (dataLoaded)
        {
            for (int i = 0;i < infoNodes.Count;i++)
            {
                infoNodes[0].UpdateNode(MissionState.MISSION_COMPLETED,true,true,false);
            }
        }
    }
}
