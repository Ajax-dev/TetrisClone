using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetroMove : MonoBehaviour
{
    public static int gridHeight = 22;
    public static int gridWidth = 10;
    
    //Pertinent only to Tetrominoes
    private Vector3 rotationPoint;
    private float lastTime;
    private float timeToFall = 0.9f;
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
            }
            lastTime = Time.time;
        }
    }

    bool isValidMove()
    {
        foreach (Transform children in transform)
        {
            int xRound = Mathf.RoundToInt(children.transform.position.x);
            int yRound = Mathf.RoundToInt(children.transform.position.y);

            if (xRound < 0 || xRound >= gridWidth || yRound < 0 || yRound >= gridHeight)
            {
                return false;
            }
            
        }

        return true;
    }
}
