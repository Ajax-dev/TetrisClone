using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTetromino : MonoBehaviour
{
    [SerializeField] private TetroMove[] tetrominoes;
    [SerializeField] private TetroMove[] ghosts;

    [SerializeField] private GridController gc;
    // Start is called before the first frame update
    void Start()
    {
        SpawnTetro();
    }

    // Update is called once per frame
    public void SpawnTetro()
    {
        int spawnNumber = Random.Range(0, tetrominoes.Length);
        TetroMove t = Instantiate(tetrominoes[spawnNumber], transform.position,
            Quaternion.identity);
        TetroMove g = Instantiate(ghosts[spawnNumber], transform.position, Quaternion.identity);
        t.SetGhostPair(g);
    }
}
