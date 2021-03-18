using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTetromino : MonoBehaviour
{
    [SerializeField] private TetroMove[] tetrominoes;

    [SerializeField] private GridController gc;
    // Start is called before the first frame update
    void Start()
    {
        SpawnTetro();
    }

    // Update is called once per frame
    public void SpawnTetro()
    {
        TetroMove t = Instantiate(tetrominoes[Random.Range(0, tetrominoes.Length)], transform.position,
            Quaternion.identity);
    }
}
