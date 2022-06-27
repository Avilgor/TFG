using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OperatorType
{
    OP_SUM = 0,
    OP_SUB,
    OP_MUL,
    OP_DIV
}

public enum Difficulty
{
    DFF_EASY = 0, //From 1 to 9
    DFF_EASY2 = 1, //From 5 to 20
    DFF_MED = 2, //From 10 to 30
    DFF_HARD = 3//From 20 to 50
}

public class OperationGenerator
{      
    public int currentSolution;
    public string currentOperation;

    public OperationGenerator()
    {
        currentOperation = "";
        currentSolution = 0;
    }

    public void Generate(List<OperatorType> list, Difficulty diff)
    {
        Random.InitState(System.Environment.TickCount);
        string txt = "";
        List<int> numbers = new List<int>();
        int aux;
        switch (diff)
        {
            case Difficulty.DFF_EASY:
                //Debug.Log("Difficulty easy");
                for (int i = 0;i < list.Count;i++)
                {
                    aux = GetNumber(list[i], Difficulty.DFF_EASY);
                    txt += aux.ToString() + " ";
                    txt += GetOperatorString(list[i]) + " ";
                    numbers.Add(aux);
                }
                aux = GetNumber(list[list.Count - 1], Difficulty.DFF_EASY);
                txt += aux.ToString();
                numbers.Add(aux);
                break;

            case Difficulty.DFF_EASY2:
                //Debug.Log("Difficulty easy2");
                for (int i = 0; i < list.Count; i++)
                {
                    aux = GetNumber(list[i], Difficulty.DFF_EASY2);
                    txt += aux.ToString() + " ";
                    txt += GetOperatorString(list[i]) + " ";
                    numbers.Add(aux);
                }
                aux = GetNumber(list[list.Count - 1], Difficulty.DFF_EASY2);
                txt += aux.ToString();
                numbers.Add(aux);
                break;

            case Difficulty.DFF_MED:
                //Debug.Log("Difficulty medium");
                for (int i = 0; i < list.Count; i++)
                {
                    aux = GetNumber(list[i], Difficulty.DFF_MED);
                    txt += aux.ToString() + " ";
                    txt += GetOperatorString(list[i]) + " ";
                    numbers.Add(aux);
                }
                aux = GetNumber(list[list.Count-1], Difficulty.DFF_MED);
                txt += aux.ToString();
                numbers.Add(aux);
                break;

            case Difficulty.DFF_HARD:
                //Debug.Log("Difficulty hard");
                for (int i = 0; i < list.Count; i++)
                {
                    aux = GetNumber(list[i], Difficulty.DFF_HARD);
                    txt += aux.ToString() + " ";
                    txt += GetOperatorString(list[i]) + " "; 
                    numbers.Add(aux);
                }
                aux = GetNumber(list[list.Count - 1], Difficulty.DFF_HARD);
                txt += aux.ToString();
                numbers.Add(aux);
                break;

            default:
                Debug.Log("Difficulty clause not found.");
                break;
        }
        currentOperation = txt;
        GenerateSolution(numbers,list);
    }

    private string GetOperatorString(OperatorType type)
    {
        switch (type)
        {
            case OperatorType.OP_SUM:
                return "+";
            case OperatorType.OP_SUB:
                return "-";
            case OperatorType.OP_MUL:
                return "*";
            case OperatorType.OP_DIV:
                return "/";
            default:
                Debug.Log("Operator type no found");
                return " ";
        }
    }

    private int GetNumber(OperatorType type, Difficulty diff)
    {
        switch (diff)
        {
            case Difficulty.DFF_EASY:
                switch (type)
                {
                    case OperatorType.OP_SUM:
                        return Random.Range(1, 11);
                    case OperatorType.OP_SUB:
                        return Random.Range(1, 11);
                    case OperatorType.OP_MUL:
                        return Random.Range(1, 3);
                    case OperatorType.OP_DIV:
                        return Random.Range(1, 3);
                }                                
                break;

            case Difficulty.DFF_EASY2:
                switch (type)
                {
                    case OperatorType.OP_SUM:
                        return Random.Range(5, 21);
                    case OperatorType.OP_SUB:
                        return Random.Range(5, 21);
                    case OperatorType.OP_MUL:
                        return Random.Range(1, 3);
                    case OperatorType.OP_DIV:
                        return Random.Range(1, 3);
                }
                break;

            case Difficulty.DFF_MED:
                switch (type)
                {
                    case OperatorType.OP_SUM:
                        return Random.Range(10, 31);
                    case OperatorType.OP_SUB:
                        return Random.Range(10, 31);
                    case OperatorType.OP_MUL:
                        return Random.Range(1, 4);
                    case OperatorType.OP_DIV:
                        return Random.Range(1, 4);
                }
                break;

            case Difficulty.DFF_HARD:
                switch (type)
                {
                    case OperatorType.OP_SUM:
                        return Random.Range(20, 51);
                    case OperatorType.OP_SUB:
                        return Random.Range(20, 51);
                    case OperatorType.OP_MUL:
                        return Random.Range(1, 4);
                    case OperatorType.OP_DIV:
                        return Random.Range(1, 4);
                }
                break;
        }
        return 1;
    }

    private void GenerateSolution(List<int> numbers,List<OperatorType> operators)
    {
        int prior = 0;

        //Check how many priority operations are
        for (int i = 0; i < operators.Count; i++)
        {
            if (operators[i] == OperatorType.OP_MUL || operators[i] == OperatorType.OP_DIV) prior++;
        }

        if (prior > 0)
        {
            Debug.Log("Calculating * and /...");
            for (int i = 0; i < prior; i++)
            {
                for (int a = 0; a < operators.Count; a++)
                {
                    if (operators[a] == OperatorType.OP_MUL)
                    {
                        int aux = numbers[a] * numbers[a + 1];
                        numbers[a] = aux;
                        numbers.RemoveAt(a + 1);
                        operators.RemoveAt(a);
                        break;
                    }
                    else if (operators[a] == OperatorType.OP_DIV)
                    {
                        int aux = numbers[a] / numbers[a + 1];
                        numbers[a] = aux;
                        numbers.RemoveAt(a + 1);
                        operators.RemoveAt(a);
                        break;
                    }                   
                }
            }
        }
        
        if (operators.Count > 0)
        {
            Debug.Log("Calculating + and -...");
            int index = operators.Count;
            for (int i = 0; i < index; i++)
            {
                if (operators[0] == OperatorType.OP_SUM)
                {
                    int aux = numbers[0] + numbers[1];
                    numbers[0] = aux;
                    numbers.RemoveAt(1);
                    operators.RemoveAt(0);
                }
                else if (operators[0] == OperatorType.OP_SUB)
                {
                    int aux = numbers[0] - numbers[1];
                    numbers[0] = aux;
                    numbers.RemoveAt(1);
                    operators.RemoveAt(0);
                } else
                {
                    Debug.Log("Operator not found");
                    operators.RemoveAt(0);
                }
            }
        }
        currentSolution = numbers[0];
        Debug.Log("Solution is: "+currentSolution.ToString());
    }
}
