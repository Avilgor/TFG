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
        markerManager.StartMarkers();
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
            GenerateNew(challengeNode.operations[operationIndex].operators,challengeNode.difficulties[operationIndex]);
            timer = challengeNode.time;
        }
        timerTxt.text = ((int)timer).ToString();
    }

    private void OnDestroy()
    {
        GLOBALS.gameController = null;
        operationGenerator = null;
    }

    private void Update()
    {
        if (!results)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                timerTxt.text = ((int)timer).ToString();
            }
            else
            {
                timerTxt.text = "0";
                GLOBALS.gameSoundManager.PlayTimeOutFX();
                ProcessResults();
                results = true;
            }
        }
    }

    private void GenerateNew(List<OperatorType> list, Difficulty diff)
    {
        List<int> numList = new List<int>();
        currentNodeDifficulty = diff;
        operationGenerator.Generate(new List<OperatorType>(list), diff);
        text.text = operationGenerator.currentOperation + " = ?";
        numList.Add(operationGenerator.currentSolution);

        int aux,sol = operationGenerator.currentSolution;
        for (int i = 0; i < 5; i++)
        {
            aux = Random.Range(sol - 5, sol + 6);
            if (numList.Contains(aux)) i--;
            else numList.Add(aux);
        }
      
        markerManager.SetMarkers(numList);
        if(GLOBALS.currentGameMode == GameMode.MODE_ADVENTURE) AlterCube();
    }

    public bool CheckNumber(int num)
    {
        if (num == operationGenerator.currentSolution)
        {
            //Debug.Log("Correct number");
            totalOperations--;
            operationIndex++;
            StartCoroutine(NextOperation());
            if (currentAlteration == GameAlteration.ALT_GOLD)
            {
                GLOBALS.player.stars += 1;
            }
            GLOBALS.gameSoundManager.PlayNumberCorrect();
            return true;
        }
        else
        {
            //Debug.Log("Incorrect number");
            failed = true;
            if (GLOBALS.currentGameMode == GameMode.MODE_CHALLENGE) ProcessResults();
            GLOBALS.gameSoundManager.PlayNumberWrong();
            return false;
        }
    }

    private void AlterCube()
    {
        int roll = Random.Range(1, 101);
        //Testing
        if (GLOBALS.currentNode == 2)
        {
            text.color = defaultColor;
            currentAlteration = GameAlteration.ALT_CUBETRAP;
            markerManager.AlterCube(currentAlteration);
            gameOptions.ToogleCalculatorButton(true);
        }
        else if (GLOBALS.currentNode == 3)
        {
            text.color = defaultColor;
            currentAlteration = GameAlteration.ALT_CUBEHOLED;
            markerManager.AlterCube(currentAlteration);
            gameOptions.ToogleCalculatorButton(false);
            GLOBALS.gameSoundManager.PlayVariationHole();
        }
        else if (GLOBALS.currentNode == 4)
        {
            currentAlteration = GameAlteration.ALT_GOLD;
            text.color = goldAlterColor;
            gameOptions.ToogleCalculatorButton(true);
            markerManager.AlterCube(currentAlteration);
            GLOBALS.gameSoundManager.PlayVariationGold();
        }
        else if (GLOBALS.currentNode == 5)
        {
            text.color = defaultColor;
            currentAlteration = GameAlteration.ALT_CUBECRAZY;
            markerManager.AlterCube(currentAlteration);
            gameOptions.ToogleCalculatorButton(true);
        } //Testing
        else if (roll <= 5)
        {
            currentAlteration = GameAlteration.ALT_GOLD;
            text.color = goldAlterColor;          
            gameOptions.ToogleCalculatorButton(true);
            markerManager.AlterCube(currentAlteration);
            GLOBALS.gameSoundManager.PlayVariationGold();
        }
        else if (roll <= 10)
        {
            text.color = defaultColor;
            roll = Random.Range(0, 3);
            switch (roll)
            {
                case 0:
                    currentAlteration = GameAlteration.ALT_CUBEHOLED;
                    markerManager.AlterCube(currentAlteration);
                    gameOptions.ToogleCalculatorButton(false);
                    GLOBALS.gameSoundManager.PlayVariationHole();
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
            text.color = defaultColor;
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

    private void ShowResultsAdventure(int stars)
    {
        panel.gameObject.SetActive(true);
        panel.SetUpEndPanel(GLOBALS.currentNode, stars);
        if(stars > 0) LevelRewards.OnLevelEnd(GLOBALS.currentNode);
    }

    private void ShowResultsChallenge(bool s1,bool s2,bool s3)
    {
        panel.gameObject.SetActive(true);
        panel.SetUpDailyChallengePanel(s1, s2, s3);
    }

    private void ProcessResults()
    {
        results = true;
        markerManager.EndMarkers();
        int totalStars = 0;
        if (GLOBALS.currentGameMode == GameMode.MODE_ADVENTURE)
        {
            if (GLOBALS.replayMission)
            {
                totalStars++;

                if (GLOBALS.infoNodes[GLOBALS.currentNode].starTime) totalStars++;
                else
                {
                    if (timer > (GLOBALS.infoNodes[GLOBALS.currentNode].time / 2))
                    {
                        GLOBALS.infoNodes[GLOBALS.currentNode].starTime = true;
                        GLOBALS.player.stars += 1;
                        totalStars++;
                    }
                    else GLOBALS.infoNodes[GLOBALS.currentNode].starTime = false;
                }

                if (GLOBALS.infoNodes[GLOBALS.currentNode].starError) totalStars++;               
                else
                {
                    if (failed) GLOBALS.infoNodes[GLOBALS.currentNode].starError = false;
                    else
                    {
                        GLOBALS.infoNodes[GLOBALS.currentNode].starError = true;
                        GLOBALS.player.stars += 1;
                        totalStars++;
                    }
                }
            }
            else
            {
                if (timer <= 0)
                {
                    GLOBALS.infoNodes[GLOBALS.currentNode].starCompleted = false;
                    GLOBALS.infoNodes[GLOBALS.currentNode].starTime = false;
                    GLOBALS.infoNodes[GLOBALS.currentNode].starError = false;
                }
                else
                {
                    GLOBALS.infoNodes[GLOBALS.currentNode].state = MissionState.MISSION_COMPLETED;
                    if (GLOBALS.infoNodes.ContainsKey(GLOBALS.currentNode + 1))
                    {
                        GLOBALS.infoNodes[GLOBALS.currentNode + 1].state = MissionState.MISSION_UNLOCKED;
                    }
                    //Debug.Log("Remaining time: " + timer.ToString());
                    //Debug.Log("Star time: " + GLOBALS.infoNodes[GLOBALS.currentNode].time / 2);
                    GLOBALS.infoNodes[GLOBALS.currentNode].starCompleted = true;
                    if (failed)
                    {
                        GLOBALS.infoNodes[GLOBALS.currentNode].starError = false;
                        if (timer > (GLOBALS.infoNodes[GLOBALS.currentNode].time / 2))
                        {
                            GLOBALS.infoNodes[GLOBALS.currentNode].starTime = true;
                            GLOBALS.player.stars += 2;
                            totalStars = 2;
                        }
                        else
                        {
                            GLOBALS.infoNodes[GLOBALS.currentNode].starTime = false;
                            GLOBALS.player.stars += 1;
                            totalStars = 1;
                        }
                    }
                    else
                    {
                        GLOBALS.infoNodes[GLOBALS.currentNode].starTime = true;
                        if (timer > (GLOBALS.infoNodes[GLOBALS.currentNode].time / 2))
                        {
                            GLOBALS.infoNodes[GLOBALS.currentNode].starError = true;
                            GLOBALS.player.stars += 3;
                            totalStars = 3;
                        }
                        else
                        {
                            GLOBALS.infoNodes[GLOBALS.currentNode].starError = false;
                            GLOBALS.player.stars += 2;
                            totalStars = 2;
                        }
                    }
                }
            }
            ShowResultsAdventure(totalStars);
        }
        else
        {
            GLOBALS.player.challengeDone = true;
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
        GLOBALS.SaveGame();
    }

    public void PowerUpCalculator()
    {
        if (currentAlteration != GameAlteration.ALT_CUBEHOLED)
        {
            markerManager.AlterCube(GameAlteration.ALT_CUBEHOLED);
            GLOBALS.SaveGame();
            GLOBALS.gameSoundManager.PlayCalculatorFX();
        }
    }

    public void PowerUpCrono()
    {
        timer += 30;
        GLOBALS.SaveGame();
        GLOBALS.gameSoundManager.PlayCronoFX();
    }

    IEnumerator NextOperation()
    {
        float duration = 1.0f;
        if (totalOperations > 0) StartCoroutine(OperationChangeFX(duration));
        else text.text = "";
        yield return new WaitForSeconds(duration);
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

    IEnumerator OperationChangeFX(float duration)
    {
        float timer = 0;
        do
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            string txt = "";
            txt += Random.Range(0, 50).ToString();
            switch (Random.Range(0, 4))
            {
                case 0:
                    txt += " + ";
                    break;
                case 1:
                    txt += " - ";
                    break;
                case 2:
                    txt += " * ";
                    break;
                case 3:
                    txt += " / ";
                    break;
            }
            txt += Random.Range(0, 50).ToString();
            switch (Random.Range(0, 4))
            {
                case 0:
                    txt += " + ";
                    break;
                case 1:
                    txt += " - ";
                    break;
                case 2:
                    txt += " * ";
                    break;
                case 3:
                    txt += " / ";
                    break;
            }
            txt += Random.Range(0, 50).ToString();
            txt += " = ?";
            text.text = txt;
        } while (timer < duration);
    }
}