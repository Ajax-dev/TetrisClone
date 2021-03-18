using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public static event Action updateScore;
    public static event Action lineCleared;

    public static event Action removeLife;

    public static event Action updateGame;
    
    public static GridController instance;
    
    private static int gridHeight = 21;
    private static int gridWidth = 10;
    
    private static Transform[,] grid = new Transform[gridWidth, gridHeight];

    private TetroMove[] tetroBlocks;

    private void Awake()
    {
        //Check there are no other copies of this class in the scene
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void addToGrid(Transform[] tetros)
    {
        foreach (Transform children in tetros)
        {
            int xRound = Mathf.RoundToInt(children.transform.position.x);
            int yRound = Mathf.RoundToInt(children.transform.position.y);
            Debug.Log(xRound + " is x and y is " + yRound);
            grid[xRound, yRound] = children;
        }
    }
    
    public bool isValidMove(Transform[] tetros)
    {
        // Debug.Log(tetros.Length + " this is tetros string");
        foreach (Transform children in tetros)
        {
            int xRound = Mathf.RoundToInt(children.transform.position.x);
            int yRound = Mathf.RoundToInt(children.transform.position.y);

            // Check if tetromino is out of bounds
            if (xRound < 0 || xRound >= gridWidth || yRound < 0 || yRound >= gridHeight)
            {
                return false;
            }
            
            // Check if tetromino is already in that position
            if (grid[xRound, yRound] != null)
            {
                return false;
            }

        }

        return true;
    }

    public void CheckLines()
    {
        for (int i = gridHeight - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
                lineCleared.Invoke();
                updateScore.Invoke();
            }
        }
    }

    bool HasLine(int line)
    {
        for (int i = 0; i < gridWidth; i++)
        {
            if (grid[i, line] == null)
            {
                return false;
            }
        }

        return true;
    }

    void DeleteLine(int line)
    {
        for (int i = 0; i < gridWidth; i++)
        {
            Destroy(grid[i, line].gameObject);
            grid[i, line] = null;
            
        }
    }

    void RowDown(int line)
    {
        for (int i = line; i < gridHeight; i++)
        {
            for (int j = 0; j < gridWidth; j++)
            {
                if (grid[j, i] != null)
                {
                    grid[j, i - 1] = grid[j, i];
                    grid[j, i] = null;
                    grid[j, i - 1].transform.position += new Vector3(0, -1, 0);
                }
            }
        }
    }
}
