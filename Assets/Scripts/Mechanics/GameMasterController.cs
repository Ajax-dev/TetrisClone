using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Using observer pattern

public class GameMasterController : MonoBehaviour
{
    private GameData saveData = new GameData();
    [SerializeField] public TMP_Text level;
    [SerializeField] public TMP_Text lives;
    [SerializeField] public TMP_Text score;
    [SerializeField] public TMP_Text highscore;
    [SerializeField] public TMP_Text linesCleared;
    
    private TetroMove tm;
    private GameData gm = new GameData();

    private void Awake()
    {
        tm = FindObjectOfType<TetroMove>();
        gm.Init();
        score.text = gm.GetScore().ToString();
        lives.text = gm.GetLives().ToString();
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
        GridController.removeLife += UpdateLives;
        GridController.updateGame += UpdateGameState;
    }

    private void OnDisable()
    {
        GridController.updateScore -= UpdateScore;
        GridController.lineCleared -= UpdateLineCleared;
    }

    private void UpdateGameState()
    {
        tm.UpdateGameState();
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

    private void UpdateLives()
    {
        gm.TakeLife();
        lives.text = gm.GetLives().ToString();
    }


    void PrintScore()
    {
        Debug.Log("The current score is: " + saveData.GetScore());
        Debug.Log("The current lives are: " + saveData.GetLives());
        Debug.Log("The current level is: " + saveData.GetLevel());
        Debug.Log("The current lines cleared are: " + saveData.GetLinesCleared());
        
    }
}
