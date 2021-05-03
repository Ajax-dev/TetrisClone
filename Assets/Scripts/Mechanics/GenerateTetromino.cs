using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateTetromino : MonoBehaviour
{
    [SerializeField] private TetroMove[] tetrominoes;
    [SerializeField] private TetroMove[] ghosts;

    public static GenerateTetromino instance;
    // [SerializeField] private GridController gc;
    // Start is called before the first frame update
    void Start()
    {
        SpawnTetro();
    }

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
    }

    // Update is called once per frame
    public void SpawnTetro()
    {
        string isReplaying = GameMasterController.isReplay.ToString();
        int spawnNumber;
        if(GameMasterController.isReplay == false)
        {
            Debug.Log("The spawner is spawning replay tetros" + isReplaying);
            spawnNumber = Random.Range(0, tetrominoes.Length);
            TetroMove t = Instantiate(tetrominoes[spawnNumber], transform.position,
                Quaternion.identity);
            TetroMove g = Instantiate(ghosts[spawnNumber], transform.position, Quaternion.identity);
            t.SetGhostPair(g);
            t.tetroEnum = spawnNumber;
        }
        else
        {
            Debug.Log("The spawner is spawning replay tetros" + isReplaying);
            Debug.Log("The spawner is spawning" + GameMasterController.tetroNum[0]);
            
            spawnNumber = GameMasterController.tetroNum[0];
            TetroMove t = Instantiate(tetrominoes[spawnNumber], transform.position,
                Quaternion.identity);
            t.tetroEnum = spawnNumber;
            // TetroMove g = Instantiate(ghosts[spawnNumber], transform.position, Quaternion.identity);
            // t.SetGhostPair(g);
            // t.tetroEnum = spawnNumber;

        }
    }
}
