using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Using observer pattern

public class GameMasterController : MonoBehaviour
{
    private GameData saveData = new GameData();
    private int linesCleared;
    [SerializeField] public TMP_Text level;
    [SerializeField] public TMP_Text lives;
    [SerializeField] public TMP_Text score;
    [SerializeField] public TMP_Text highscore;
    private TetroMove tm;
    private GameData gm = new GameData();

    private void Awake()
    {
        tm = FindObjectOfType<TetroMove>();
        gm.Init();
        score.text = gm.score.ToString();
        lives.text = gm.lives.ToString();
        level.text = gm.level.ToString();
        linesCleared = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        TetroMove.updateScore += UpdateScore;
        TetroMove.lineCleared += UpdateLineCleared;
        TetroMove.removeLife += UpdateLives;
    }

    private void OnDisable()
    {
        TetroMove.updateScore -= UpdateScore;
        TetroMove.lineCleared -= UpdateLineCleared;
    }

    private void UpdateScore()
    {
        gm.AddScore(20 * gm.GetLevel() * gm.GetLevel());
        score.text = gm.GetScore().ToString();
    }

    private void UpdateLineCleared()
    {
        linesCleared++;
        level.text = gm.checkLevel(linesCleared).ToString();
    }

    private void UpdateLives()
    {
        gm.lives--;
        lives.text = gm.lives.ToString();
    }


    void PrintScore()
    {
        Debug.Log("The current score is: " + saveData.score);
        Debug.Log("The current lives are: " + saveData.lives);
        Debug.Log("The current level is: " + saveData.level);
    }
}
