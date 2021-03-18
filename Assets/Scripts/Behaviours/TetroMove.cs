using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class TetroMove : MonoBehaviour
{
    //Pertinent only to Tetrominoes
    [SerializeField] private Vector3 rotationPoint;
    private Transform[] tetrominoes;
    private float lastTime;
    private float timeToFall = 0.5f;

    // Start is called before the first frame update
    void Awake()
    {
        tetrominoes = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!GridController.instance.isValidMove(tetrominoes))
            {
                Debug.Log("Wasn't a valid move when going left! | " + transform.position);
                transform.position += new Vector3(1, 0, 0);
            }
        } else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!GridController.instance.isValidMove(tetrominoes))
            {
                Debug.Log("Wasn't a valid move when going right! | " + transform.position);
                transform.position += new Vector3(-1, 0, 0);
            }
        } else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //This is the tetromino rotation
            transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),90);
            if (!GridController.instance.isValidMove(tetrominoes))
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1), -90);
            }
        }
        
        if(Time.time - lastTime > (Input.GetKey(KeyCode.DownArrow) ? timeToFall / 10 : timeToFall))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!GridController.instance.isValidMove(tetrominoes))
            {
                transform.position += new Vector3(0, 1, 0);
                // Once its touched the ground add it to the array
                GridController.instance.addToGrid(tetrominoes);
                GridController.instance.CheckLines();
                //Then spawn the next tetro
                this.enabled = false;
                FindObjectOfType<GenerateTetromino>().SpawnTetro();
            }
            lastTime = Time.time;
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

}
