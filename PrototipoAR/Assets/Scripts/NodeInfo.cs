using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeOperations
{
    public List<OperatorType> operators;

    public NodeOperations(List<OperatorType> list)
    {
        operators = list;
    }

    public float GetOperationTime()
    {
        float time = 0;
        for (int i = 0; i < operators.Count; i++)
        {
            switch (operators[i])
            {
                case OperatorType.OP_SUM:
                    time += 5;
                    break;
                case OperatorType.OP_SUB:
                    time += 8;
                    break;
                case OperatorType.OP_MUL:
                    time += 15;
                    break;
                case OperatorType.OP_DIV:
                    time += 15;
                    break;
            }
        }
        return time;
    }
}

public class NodeInfo
{
    public int level;
    public MissionState state;
    public bool starCompleted, starError, starTime;
    public Difficulty nodeDifficulty;
    public List<NodeOperations> operations;
    public float time;

    public NodeInfo(int level,MissionState NodeState,bool completed, bool time, bool error, Difficulty diff, List<NodeOperations> list)
    {
        this.level = level;
        state = NodeState;
        starCompleted = completed;
        starError = error;
        starTime = time;
        nodeDifficulty = diff;
        operations = list;
        GenerateTime();
    }

    public void UpdateNode(MissionState NodeState, bool completed, bool time, bool error)
    {
        state = NodeState;
        starCompleted = completed;
        starError = error;
        starTime = time;
    }

    private void GenerateTime()
    {
        time = 10;
        for (int i = 0;i < operations.Count;i++)
        {
            time += operations[i].GetOperationTime();
        }

        switch (nodeDifficulty)
        {
            case Difficulty.DFF_EASY:
                time *= 3;
                break;
            case Difficulty.DFF_EASY2:
                time *= 2.5f;
                break;
            case Difficulty.DFF_MED:
                time *= 2.0f;
                break;
            case Difficulty.DFF_HARD:
                time *= 1.5f;
                break;
        }
    }
}
