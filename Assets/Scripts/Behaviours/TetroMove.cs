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
    private Transform[] tetromino;
    private float lastTime;
    private float timeToFall = 0.5f;
    private bool placed = false;

    // Start is called before the first frame update
    void Awake()
    {
        tetromino = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!placed)
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
        Debug.Log(this.name + " has " + transform.childCount + " children");
        if (transform.childCount == 0)
        {
            Destroy(this.gameObject);
            Debug.Log(this.name + " has no more children");
        }
    }
    public void UpdateGameState ()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    public void MoveRight()
    {
        transform.position += new Vector3(1, 0, 0);
        if (!GridController.instance.isValidMove(tetromino))
        {
            // Debug.Log("Wasn't a valid move when going right! | " + transform.position);
            transform.position += new Vector3(-1, 0, 0);
        }
    }

    public void MoveLeft()
    {
        transform.position += new Vector3(-1, 0, 0);
        if (!GridController.instance.isValidMove(tetromino))
        {
            // Debug.Log("Wasn't a valid move when going left! | " + transform.position);
            transform.position += new Vector3(1, 0, 0);
        }
    }

    public void Rotate()
    {
        //This is the tetromino rotation
        transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),90);
        if (!GridController.instance.isValidMove(tetromino))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1), -90);
        }
    }

    public void MoveDown()
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
            this.placed = true;
            FindObjectOfType<GenerateTetromino>().SpawnTetro();
        }
        lastTime = Time.time;
    }
}
