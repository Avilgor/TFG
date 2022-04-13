using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GameAlteration
{
    ALT_NONE = 0,
    ALT_CUBEHOLED,
    ALT_CUBETRAP,
    ALT_CUBECRAZY,
    ALT_GOLD
}

public class GameController : MonoBehaviour
{
    public GameMarkerManager markerManager;
    public GameOptions gameOptions;

    [SerializeField]
    MissionPanel panel;
    [SerializeField]
    TextMeshProUGUI text,timerTxt;
    [SerializeField]
    Color goldAlterColor,defaultColor;

    OperationGenerator operationGenerator;
    int totalOperations,operationIndex;
    bool failed;
    float timer;
    bool results;
    ChallengeNode challengeNode;
    Difficulty currentNodeDifficulty;
    public GameAlteration currentAlteration;

    private void Awake()
    {
        GLOBALS.gameController = this;
        operationGenerator = new OperationGenerator();
    }

    void Start()
    {
        currentAlteration = GameAlteration.ALT_NONE;
        currentNodeDifficulty = Difficulty.DFF_EASY;
        operationIndex = 0;
        results = false;
        failed = false;
        panel.gameObject.SetActive(false);
        if (GLOBALS.currentGameMode == GameMode.MODE_ADVENTURE)
        {
            totalOperations = GLOBALS.infoNodes[GLOBALS.currentNode].operations.Count;
            timer = GLOBALS.infoNodes[GLOBALS.currentNode].time;
            GenerateNew(GLOBALS.infoNodes[GLOBALS.currentNode].operations[operationIndex].operators, GLOBALS.infoNodes[GLOBALS.currentNode].nodeDifficulty);
        }
        else
        {
            challengeNode = new ChallengeNode(Difficulty.DFF_MED,Difficulty.DFF_MED,Difficulty.DFF_HARD);
            totalOperations = 3;
        }
        timerTxt.text = ((int)timer).ToString();
    }

    private void Update()
    {
        if (timer > 0 && !results)
        {
            timer -= Time.deltaTime;
            timerTxt.text = ((int)timer).ToString();
        }
        else if(!results)
        {
            timerTxt.text = "0";
            ProcessResults();
            results = true;
        }
    }

    private void GenerateNew(List<OperatorType> list, Difficulty diff)
    {
        List<int> numList = new List<int>();
        currentNodeDifficulty = diff;
        operationGenerator.Generate(list, diff);
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
        AlterCube();
    }

    public bool CheckNumber(int num)
    {
        if (num == operationGenerator.currentSolution)
        {
            Debug.Log("Correct number");
            totalOperations--;
            operationIndex++;
            StartCoroutine(NextOperation());
            if (currentAlteration == GameAlteration.ALT_GOLD)
            {
                GLOBALS.player.stars += 1;
            }
            return true;
        }
        else
        {
            Debug.Log("Incorrect number");
            failed = true;
            if (GLOBALS.currentGameMode == GameMode.MODE_CHALLENGE) ProcessResults();

            return false;
        }
    }

    private void AlterCube()
    {
        int roll = Random.Range(1,101);

        if (roll <= 5)
        {
            string aux = text.text;
            text.text = "<color=#" + ColorUtility.ToHtmlStringRGB(goldAlterColor) + ">" + aux + "</color>";
            gameOptions.ToogleCalculatorButton(true);
            markerManager.AlterCube(currentAlteration);
        }
        else if (roll <= 10)
        {
            roll = Random.Range(0, 3);
            switch (roll)
            {
                case 0:
                    currentAlteration = GameAlteration.ALT_CUBEHOLED;
                    markerManager.AlterCube(currentAlteration);
                    gameOptions.ToogleCalculatorButton(false);
                    break;
                case 1:
                    currentAlteration = GameAlteration.ALT_CUBETRAP;
                    markerManager.AlterCube(currentAlteration);
                    gameOptions.ToogleCalculatorButton(true);
                    break;
                case 2:
                    currentAlteration = GameAlteration.ALT_CUBECRAZY;
                    markerManager.AlterCube(currentAlteration);
                    gameOptions.ToogleCalculatorButton(true);
                    break;
            }
        }
        else
        {            
            string aux = text.text;
            text.text = "<color=#" + ColorUtility.ToHtmlStringRGB(defaultColor) + ">" + aux + "</color>";
            currentAlteration = GameAlteration.ALT_NONE;
            gameOptions.ToogleCalculatorButton(true);
            markerManager.AlterCube(currentAlteration);
        }
    }

    public int GetOperationResult()
    {
        return operationGenerator.currentSolution;
    }

    public Difficulty GetNodeDifficulty()
    {
        return currentNodeDifficulty;
    }

    private void ShowResultsAdventure()
    {
        panel.gameObject.SetActive(true);
        panel.SetUpEndPanel(GLOBALS.currentNode, 
            GLOBALS.infoNodes[GLOBALS.currentNode].star1, 
            GLOBALS.infoNodes[GLOBALS.currentNode].star2, 
            GLOBALS.infoNodes[GLOBALS.currentNode].star3);
    }

    private void ShowResultsChallenge(bool s1,bool s2,bool s3)
    {
        panel.gameObject.SetActive(true);
        panel.SetUpDailyChallengePanel(s1, s2, s3);
    }

    private void ProcessResults()
    {
        markerManager.EndMarkers();
        if (GLOBALS.currentGameMode == GameMode.MODE_ADVENTURE)
        {
            if (timer <= 0)
            {
                GLOBALS.infoNodes[GLOBALS.currentNode].star1 = false;
                GLOBALS.infoNodes[GLOBALS.currentNode].star2 = false;
                GLOBALS.infoNodes[GLOBALS.currentNode].star3 = false;
            }
            else
            {
                GLOBALS.infoNodes[GLOBALS.currentNode].state = MissionState.MISSION_COMPLETED;
                if (GLOBALS.infoNodes.ContainsKey(GLOBALS.currentNode + 1))
                {
                    GLOBALS.infoNodes[GLOBALS.currentNode +1].state = MissionState.MISSION_UNLOCKED;
                }

                GLOBALS.infoNodes[GLOBALS.currentNode].star1 = true;
                if (failed)
                {
                    GLOBALS.infoNodes[GLOBALS.currentNode].star3 = false;
                    if (timer > (GLOBALS.infoNodes[GLOBALS.currentNode].time / 2))
                    {
                        GLOBALS.infoNodes[GLOBALS.currentNode].star2 = true;
                        GLOBALS.player.stars += 2;
                    }
                    else
                    { 
                        GLOBALS.infoNodes[GLOBALS.currentNode].star2 = false;
                        GLOBALS.player.stars += 1;
                    }
                }
                else
                {
                    GLOBALS.infoNodes[GLOBALS.currentNode].star2 = true;
                    if (timer > (GLOBALS.infoNodes[GLOBALS.currentNode].time / 2))
                    {
                        GLOBALS.infoNodes[GLOBALS.currentNode].star3 = true;
                        GLOBALS.player.stars += 3;
                    }
                    else
                    {
                        GLOBALS.infoNodes[GLOBALS.currentNode].star3 = false;
                        GLOBALS.player.stars += 2;
                    }
                }
            }
            ShowResultsAdventure();
        }
        else
        {
            if (totalOperations == 0)
            {
                GLOBALS.player.stars += 3;
                ShowResultsChallenge(true,true,true);
            }
            else if (totalOperations == 1)
            {
                GLOBALS.player.stars += 2;
                ShowResultsChallenge(true, true, false);
            }
            else if (totalOperations == 2)
            {
                GLOBALS.player.stars += 1;
                ShowResultsChallenge(true, false, false);
            }
            else ShowResultsChallenge(false, false, false);
        }
    }

    public void PowerUpCalculator()
    {
        if (currentAlteration != GameAlteration.ALT_CUBEHOLED)
        {
            markerManager.AlterCube(GameAlteration.ALT_CUBEHOLED);
        }
    }

    public void PowerUpCrono()
    {
        timer += 30;
    }

    IEnumerator NextOperation()
    {
        yield return new WaitForSeconds(1);
        if (totalOperations <= 0) ProcessResults();
        else
        {
            if (GLOBALS.currentGameMode == GameMode.MODE_ADVENTURE)
            {
                GenerateNew(GLOBALS.infoNodes[GLOBALS.currentNode].operations[operationIndex].operators, GLOBALS.infoNodes[GLOBALS.currentNode].nodeDifficulty);
            }
            else
            {
                GenerateNew(challengeNode.operations[operationIndex].operators,challengeNode.difficulties[operationIndex]);
            }
        }
    }
}