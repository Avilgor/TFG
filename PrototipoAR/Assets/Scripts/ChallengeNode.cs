using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeNode
{
    public List<NodeOperations> operations;
    public List<Difficulty> difficulties;
    public float time;

    public ChallengeNode(Difficulty diff1, Difficulty diff2, Difficulty diff3)
    {
        operations = new List<NodeOperations>();
        difficulties = new List<Difficulty>();
        difficulties.Add(diff1);
        difficulties.Add(diff2);
        difficulties.Add(diff3);
        Generate(diff1);
        Generate(diff2);
        Generate(diff3);
        GenerateTime();
    }

    private void Generate(Difficulty diff)
    {
        List<OperatorType> operators = new List<OperatorType>();
        NodeOperations node;
        int rand;
        switch (diff)
        {
            case Difficulty.DFF_EASY:
                rand = Random.Range(0,4);
                operators.Add((OperatorType)rand);
                break;

            case Difficulty.DFF_EASY2:
                rand = Random.Range(0, 4);
                operators.Add((OperatorType)rand);
                rand = Random.Range(0, 4);
                operators.Add((OperatorType)rand);
                break;

            case Difficulty.DFF_MED:
                rand = Random.Range(0, 4);
                operators.Add((OperatorType)rand);
                rand = Random.Range(0, 4);  
                operators.Add((OperatorType)rand);
                rand = Random.Range(0, 4);  
                operators.Add((OperatorType)rand);
                break;

            case Difficulty.DFF_HARD:
                rand = Random.Range(0, 4);
                operators.Add((OperatorType)rand);
                rand = Random.Range(0, 4);  
                operators.Add((OperatorType)rand);
                rand = Random.Range(0, 4);  
                operators.Add((OperatorType)rand);
                rand = Random.Range(0, 4);  
                operators.Add((OperatorType)rand);
                break;

            default:
                Debug.LogError("Challenge difficulty not found");
                break;
        }

        node = new NodeOperations(operators);
        operations.Add(node);
    }

    private void GenerateTime()
    {
        time = 10;
        time += (operations[0].GetOperationTime() * 2.5f) + (operations[1].GetOperationTime() * 2.0f) + (operations[2].GetOperationTime() * 1.5f);         
    }
}
