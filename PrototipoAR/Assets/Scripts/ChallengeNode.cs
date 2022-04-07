using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeNode
{
    public List<NodeOperations> operations;
    public List<Difficulty> difficulties;

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
    }

    private void Generate(Difficulty diff)
    {
        List<OperatorType> operators = new List<OperatorType>();
        NodeOperations node;

        switch (diff)
        {
            case Difficulty.DFF_EASY:
                operators.Add((OperatorType)Random.Range(0, 4));
                break;

            case Difficulty.DFF_EASY2:
                operators.Add((OperatorType)Random.Range(0, 4));
                operators.Add((OperatorType)Random.Range(0, 4));
                break;

            case Difficulty.DFF_MED:
                operators.Add((OperatorType)Random.Range(0, 4));
                operators.Add((OperatorType)Random.Range(0, 4));
                operators.Add((OperatorType)Random.Range(0, 4));
                break;

            case Difficulty.DFF_HARD:
                operators.Add((OperatorType)Random.Range(0, 4));
                operators.Add((OperatorType)Random.Range(0, 4));
                operators.Add((OperatorType)Random.Range(0, 4));
                operators.Add((OperatorType)Random.Range(0, 4));
                break;

            default:
                operators.Add((OperatorType)Random.Range(0, 4));
                break;
        }
        node = new NodeOperations(operators);
        operations.Add(node);
    }
}
