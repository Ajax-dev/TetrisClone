using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//Use service locator and observer
public class GameData
{
    public int score;
    public int level;
    public int lives;

    // To be called first on the game being started
    public void Init()
    {
        score = 0;
        level = 1;
        lives = 3;
    }
    
    public void AddScore(int points)
    {
        score += points;
    }

    public int checkLevel(int numberCleared)
    {
        switch (level)
        {
            case 1:
                if (numberCleared % 2 == 0)
                {
                    LevelUp();
                }

                break;
            case 2:
                if (numberCleared % 6 == 0)
                {
                    LevelUp();
                    
                }
                break;
            case 3: 
                if (numberCleared % 12 == 0)
                {
                    LevelUp();
                    
                }
                break;
            case 4:
                if (numberCleared % 20 == 0)
                {
                    LevelUp();
                }

                break;
            case 5:
                if (numberCleared % 30 == 0)
                {
                    LevelUp();
                }

                break;
            default:
                if (numberCleared % (32 + level) == 0)
                {
                    LevelUp();
                }

                break;
        }

        return level;
    }
    
    public void LevelUp()
    {
        level++;
    }

    public void ResetData()
    {
        score = 0;
        lives = 3;
        level = 1;
    }

    public int GetLives()
    {
        return lives;
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetScore()
    {
        return score;
    }
}
