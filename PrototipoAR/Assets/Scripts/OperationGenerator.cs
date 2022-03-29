using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OperatorType
{
    OP_SUM = 0,
    OP_RES,
    OP_DIV,
    OP_MUL
}

public enum Difficulty
{
    DFF_EASY = 0, //All 1 digit
    DFF_EASY2, //From 1 to 20
    DFF_MED, //From 1 to 50
    DFF_HARD //All 2 digits up to 99
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

    public void Generate(List<string> list, Difficulty diff)
    {
        string txt = "";
        List<int> numbers = new List<int>();

        switch (diff)
        {
            case Difficulty.DFF_EASY:
                Debug.Log("Difficulty easy");
                int aux;
                for (int i = 0;i < list.Count;i++)
                {
                    aux = Random.Range(1, 10);
                    txt += aux.ToString();
                    txt += list[i];
                    numbers.Add(aux);
                }
                aux = Random.Range(1, 10);
                txt += aux.ToString();
                numbers.Add(aux);
                break;

            case Difficulty.DFF_EASY2:
                Debug.Log("Difficulty easy2");
                int aux2;
                for (int i = 0; i < list.Count; i++)
                {
                    aux2 = Random.Range(1, 21);
                    txt += aux2.ToString();
                    txt += list[i];
                    numbers.Add(aux2);
                }
                aux2 = Random.Range(1, 21);
                txt += aux2.ToString();
                numbers.Add(aux2);
                break;

            case Difficulty.DFF_MED:
                Debug.Log("Difficulty medium");
                int aux3;
                for (int i = 0; i < list.Count; i++)
                {
                    aux3 = Random.Range(1, 51);
                    txt += aux3.ToString() + " ";
                    txt += list[i] + " ";
                    numbers.Add(aux3);
                }
                aux3 = Random.Range(1, 51);
                txt += aux3.ToString();
                numbers.Add(aux3);
                break;

            case Difficulty.DFF_HARD:
                Debug.Log("Difficulty hard");
                int aux4;
                for (int i = 0; i < list.Count; i++)
                {
                    aux4 = Random.Range(1, 100);
                    txt += aux4.ToString();
                    txt += list[i];
                    numbers.Add(aux4);
                }
                aux4 = Random.Range(1, 100);
                txt += aux4.ToString();
                numbers.Add(aux4);
                break;

            default:
                Debug.Log("Difficulty clause not found.");
                break;
        }
        currentOperation = txt;
        GenerateSolution(numbers,list);
    }

    private void GenerateSolution(List<int> numbers,List<string> operators)
    {
        int prior = 0;

        //Check how many priority operations are
        for (int i = 0; i < operators.Count; i++)
        {
            if (operators[i].Equals("*") || operators[i].Equals("/")) prior++;
        }

        if (prior > 0)
        {
            Debug.Log("Calculating * and /...");
            for (int i = 0; i < prior; i++)
            {
                for (int a = 0; a < operators.Count; a++)
                {
                    if (operators[a].Equals("*"))
                    {
                        int aux = numbers[a] * numbers[a + 1];
                        numbers[a] = aux;
                        numbers.RemoveAt(a + 1);
                        operators.RemoveAt(a);
                        break;
                    }
                    else if (operators[a].Equals("/"))
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
                if (operators[0].Equals("+"))
                {
                    int aux = numbers[0] + numbers[1];
                    numbers[0] = aux;
                    numbers.RemoveAt(1);
                    operators.RemoveAt(0);
                }
                else if (operators[0].Equals("-"))
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
