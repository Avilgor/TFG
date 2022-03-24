using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameMarkerManager markerManager;
    
    [SerializeField]
    TextMeshProUGUI text;

    GenerateOperation operationGenerator;

    private void Awake()
    {
        operationGenerator = new GenerateOperation();
    }

    void Start()
    {
        GenerateNew();
    }

    void Update()
    {
        
    }

    private void GenerateNew()
    {
        List<string> testList = new List<string>();
        List<int> numList = new List<int>();
        testList.Add("+");
        testList.Add("*");
        testList.Add("-");
        testList.Add("-");

        operationGenerator.Generate(testList,Difficulty.DFF_EASY);
        text.text = operationGenerator.currentOperation + "= ?";
        numList.Add(operationGenerator.currentSolution);

        int aux,sol = operationGenerator.currentSolution;
        for (int i = 0; i < 5; i++)
        {
            aux = Random.Range(sol - 5, sol + 6);
            if (numList.Contains(aux)) i--;
            else numList.Add(aux);
        }

        for (int i = 0; i < numList.Count; i++)
        {
            Debug.Log("Number: "+numList[i]);
        }
        markerManager.SetMarkers(numList);
    }
}
