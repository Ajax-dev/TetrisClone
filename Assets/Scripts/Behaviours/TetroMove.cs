using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetroMove : MonoBehaviour
{
    public static int gridHeight = 22;
    public static int gridWidth = 10;
    
    //Pertinent only to Tetrominoes
    [SerializeField] private Vector3 rotationPoint;
    private float lastTime;
    private float timeToFall = 0.9f;

    private static Transform[,] grid = new Transform[gridWidth, gridHeight];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!isValidMove())
            {
                Debug.Log("Wasn't a valid move when going left! | " + transform.position);
                transform.position += new Vector3(1, 0, 0);
            }
        } else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!isValidMove())
            {
                Debug.Log("Wasn't a valid move when going right! | " + transform.position);
                transform.position += new Vector3(-1, 0, 0);
            }
        } else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //This is the tetromino rotation
            transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),90);
            if (!isValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1), -90);
            }
        }
        
        if(Time.time - lastTime > (Input.GetKey(KeyCode.DownArrow) ? timeToFall / 10 : timeToFall))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!isValidMove())
            {
                transform.position += new Vector3(0, 1, 0);
                // Once its touched the ground add it to the array
                addToGrid();
                CheckLines();
                //Then spawn the next tetro
                this.enabled = false;
                FindObjectOfType<GenerateTetromino>().SpawnTetro();
            }
            lastTime = Time.time;
        }
    }

    void addToGrid()
    {
        foreach (Transform children in transform)
        {
            int xRound = Mathf.RoundToInt(children.transform.position.x);
            int yRound = Mathf.RoundToInt(children.transform.position.y);

            grid[xRound, yRound] = children;

        }
    }
    bool isValidMove()
    {
        foreach (Transform children in transform)
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

    void CheckLines()
    {
        for (int i = gridHeight - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
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
