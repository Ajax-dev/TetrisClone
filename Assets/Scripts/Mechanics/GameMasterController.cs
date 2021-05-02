using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

//Using observer pattern

public class GameMasterController : MonoBehaviour
{
    private GameData saveData = new GameData();
    [SerializeField] public TMP_Text level;
    [SerializeField] public TMP_Text score;
    [SerializeField] public TMP_Text highscore;
    [SerializeField] public TMP_Text linesCleared;
    public static bool isReplay = false;
    
    private TetroMove tm;
    private GameData gm = new GameData();
    
    public static GameMasterController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        tm = FindObjectOfType<TetroMove>();
        gm.Init();
        score.text = gm.GetScore().ToString();
        level.text = gm.GetLevel().ToString();
        // NEEDS CHANGING
        highscore.text = 0.ToString();
        linesCleared.text = gm.GetLinesCleared().ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        GridController.updateScore += UpdateScore;
        GridController.lineCleared += UpdateLineCleared;
    }

    private void OnDisable()
    {
        GridController.updateScore -= UpdateScore;
        GridController.lineCleared -= UpdateLineCleared;
    }
    
    private void UpdateScore()
    {
        gm.AddScore(20 * gm.GetLevel() * gm.GetLevel());
        score.text = gm.GetScore().ToString();
    }

    private void UpdateLineCleared()
    {
        gm.SetLineCleared();
        level.text = gm.checkLevel(gm.GetLinesCleared()).ToString();
        linesCleared.text = gm.GetLinesCleared().ToString();
    }


    void PrintScore()
    {
        Debug.Log("The current score is: " + saveData.GetScore());
        Debug.Log("The current level is: " + saveData.GetLevel());
        Debug.Log("The current lines cleared are: " + saveData.GetLinesCleared());
        
    }
    
    // Command pattern
    //coordinate of tetro - command pattern
    public static List<String> oldCommands = new List<String>();
    // public static List<Vector3> oldCommands = new List<Vector3>();
    public static List<float> timeOfCommand = new List<float>();
    public static List<int> tetroNum = new List<int>();
    public void updateCommandPattern(String command, int tetroEnum)
    {
        // Debug.Log("transform is " + coords.ToString() + " with a time of " + Time.timeSinceLevelLoad + " tetro num is " + tetroEnum);
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Game") && !isReplay)
        {
            oldCommands.Add(command);
            timeOfCommand.Add(Time.timeSinceLevelLoad);
            tetroNum.Add(tetroEnum);
            print(oldCommands[oldCommands.Count - 1]);
        }
        print(oldCommands.Count);
    }
}
