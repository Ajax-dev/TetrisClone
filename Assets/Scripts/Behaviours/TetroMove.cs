using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class TetroMove : MonoBehaviour
{
    //Pertinent only to tetromino
    [SerializeField] private Vector3 rotationPoint;
    [SerializeField] private bool isGhost;
    private Transform[] tetromino;
    private TetroMove GhostPair;
    private float lastTime;
    private float timeToFall = 0.5f;
    private bool isPlaced = false;
    
    public int tetroEnum;
    
    //Replay commands
    public static string replayDir;

    // Start is called before the first frame update
    void Awake()
    {
        tetromino = GetComponentsInChildren<Transform>();
    }

    private void Start()
    {
        if (GameMasterController.isReplay)
        {
            Debug.Log("Starting replay of " + Time.time);
        }
        else
        {
            Debug.Log("Tetromove " + Time.time);
        }
        if (isGhost)
        {
            transform.position += new Vector3(0, 0, -5);
            do
            {
                transform.position += new Vector3(0, -1, 0);
            } while (GridController.instance.isValidMove(tetromino));
            transform.position += new Vector3(0, +1, 0);
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameMasterController.isReplay)
        {
            if (!isPlaced && !isGhost)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    MoveLeft();
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    MoveRight();
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    Rotate();
                }

                if (Time.time - lastTime > (Input.GetKey(KeyCode.DownArrow) ? timeToFall / 10 : timeToFall))
                {
                    MoveDown();
                }
            }

            if (transform.childCount == 0)
            {
                Destroy(this.gameObject);
                Debug.Log(this.name + " has no more children");
            }
        }
        else
        {
            // ReplayUpdate();
            PlayReplay();
        }
    }

    private void PlayReplay()
    {
        print("Size of old commands " + GameMasterController.oldCommands.Count);
        print("Size of time " + GameMasterController.timeOfCommand.Count);
        print("Size of tetronum " + GameMasterController.tetroNum.Count);
        if (Time.time == GameMasterController.timeOfCommand[0])
        {
            replayDir = GameMasterController.oldCommands[0];
            GameMasterController.timeOfCommand.RemoveAt(0);
            GameMasterController.oldCommands.RemoveAt(0);
            GameMasterController.tetroNum.RemoveAt(0);
            switch (replayDir)
            {
                case "Spawn":
                    FindObjectOfType<GenerateTetromino>().SpawnTetro();
                    break;
                case "L":
                    ReplayLeft();
                    break;
                case "R":
                    ReplayRight();
                    break;
                case "T":
                    ReplayRotate();
                    break;
                case "D":
                    ReplayDown();
                    break;
                default:
                    Debug.Log("No commands");
                    break;
            }
        }
    }
    private void ReplayUpdate()
    {
        if (!isPlaced && !isGhost)
        {
            if (replayDir == "L")
            {
                MoveLeft();
            }
            else if (replayDir == "R")
            {
                MoveRight();
            }
            else if (replayDir == "T")
            {
                Rotate();
            }

            if (replayDir == "D")
            {
                MoveDown();
            }
            else
            {
                if (Time.time - lastTime > (Input.GetKey(KeyCode.DownArrow) ? timeToFall / 10 : timeToFall))
                {
                    MoveDown();
                }
            }
        }

        if (transform.childCount == 0)
        {
            Destroy(this.gameObject);
            Debug.Log(this.name + " has no more children");
        }
    }

    
    public void MoveRight()
    {
        GameMasterController.instance.updateCommandPattern("R", tetroEnum);
        transform.position += new Vector3(1, 0, 0);
        GhostPair.GhostMove(Vector3.right);
        if (!GridController.instance.isValidMove(tetromino))
        {
            // Debug.Log("Wasn't a valid move when going right! | " + transform.position);
            transform.position += new Vector3(-1, 0, 0);
            GhostPair.GhostMove(Vector3.left);
        }
        // GameMasterController.instance.updateCommandPattern(transform.position, tetroEnum);
    }

    public void MoveLeft()
    {
        GameMasterController.instance.updateCommandPattern("L", tetroEnum);
        transform.position += new Vector3(-1, 0, 0);
        GhostPair.GhostMove(Vector3.left);
        if (!GridController.instance.isValidMove(tetromino))
        {
            // Debug.Log("Wasn't a valid move when going left! | " + transform.position);
            transform.position += new Vector3(1, 0, 0);
            GhostPair.GhostMove(Vector3.right);
        }
    }

    public void Rotate()
    {
        //This is the tetromino rotation
        GameMasterController.instance.updateCommandPattern("T", tetroEnum);
        transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),90);
        GhostPair.GhostRotate(new Vector3(0,0,1),90);
        if (!GridController.instance.isValidMove(tetromino))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1), -90);
            GhostPair.GhostRotate(new Vector3(0,0,1),-90);
        }
    }
    public void MoveDown()
    {
        GhostPair.GhostToBottom();
        GameMasterController.instance.updateCommandPattern("D", tetroEnum);
        transform.position += new Vector3(0, -1, 0);
        if (!GridController.instance.isValidMove(tetromino))
        {
            transform.position += new Vector3(0, 1, 0);
            // Once its touched the ground add it to the array
            GridController.instance.addToGrid(tetromino);
            GridController.instance.CheckLines();
            //Then spawn the next tetro
            // this.enabled = false;
            this.isPlaced = true;
            Destroy(GhostPair.gameObject);
            FindObjectOfType<GenerateTetromino>().SpawnTetro();
            GameMasterController.instance.updateCommandPattern("Spawn", tetroEnum);
        }
        lastTime = Time.time;
    }
    
    /**
     * Functions for ghost block
     */
    public void SetGhostPair(TetroMove g)
    {
        this.GhostPair = g;
    }
    private void GhostToBottom()
    {
        
        transform.position += new Vector3(0, -1, 0);
        if (GridController.instance.isValidMove(tetromino))
        {
            GhostToBottom();
        }
        if (!GridController.instance.isValidMove(tetromino))
        {
            transform.position += new Vector3(0, 1, 0);
            GhostUp();
        }
    }

    private void GhostUp()
    {
        if (!GridController.instance.isValidMove(tetromino))
        {
            transform.position += Vector3.up;
            GhostUp();
        }
    }

    private void GhostRotate(Vector3 rotation, float angle)
    {
        transform.RotateAround(transform.TransformPoint(rotationPoint),rotation, angle);   
    }
    private void GhostMove(Vector3 movement)
    {
        Debug.Log("GhostMove called on: " + this.gameObject.name);
        transform.position += movement;
        if (!GridController.instance.isValidMove(tetromino))
        {
            Debug.Log("move not complete " + " because trying to get past " + transform.position.normalized);

        }
    }
    
    // Functions for replay
    private void ReplayLeft()
    {
        transform.position += new Vector3(-1, 0, 0);
        if (!GridController.instance.isValidMove(tetromino))
        {
            // Debug.Log("Wasn't a valid move when going left! | " + transform.position);
            transform.position += new Vector3(1, 0, 0);
        }
    }
    
    private void ReplayRight()
    {
        transform.position += new Vector3(1, 0, 0);
        if (!GridController.instance.isValidMove(tetromino))
        {
            // Debug.Log("Wasn't a valid move when going left! | " + transform.position);
            transform.position += new Vector3(-1, 0, 0);
        }
    }

    private void ReplayDown()
    {
        transform.position += new Vector3(0, -1, 0);
        if (!GridController.instance.isValidMove(tetromino))
        {
            transform.position += new Vector3(0, 1, 0);
            // Once its touched the ground add it to the array
            GridController.instance.addToGrid(tetromino);
            GridController.instance.CheckLines();
            //Then spawn the next tetro
            // this.enabled = false;
            this.isPlaced = true;
            FindObjectOfType<GenerateTetromino>().SpawnTetro();
        }
        lastTime = Time.time;
    }

    private void ReplayRotate()
    {
        transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),90);
        if (!GridController.instance.isValidMove(tetromino))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1), -90);
        }
    }
}
