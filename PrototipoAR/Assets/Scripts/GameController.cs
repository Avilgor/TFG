using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameMarkerManager markerManager;
    
    [SerializeField]
    TextMeshProUGUI text;

    OperationGenerator operationGenerator;

    private void Awake()
    {
        GLOBALS.gameController = this;
        operationGenerator = new OperationGenerator();
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
        List<OperatorType> testList = new List<OperatorType>();
        List<int> numList = new List<int>();
        testList.Add(OperatorType.OP_SUM);
  
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

    public bool CheckNumber(int num)
    {
        if (num == operationGenerator.currentSolution)
        {
            Debug.Log("Correct number");
            StartCoroutine(NextOperation());
            return true;
        }
        else
        {
            Debug.Log("Incorrect number");
            return false;
        }
    }

    IEnumerator NextOperation()
    {
        yield return new WaitForSeconds(1);
        GenerateNew();
    }
}
