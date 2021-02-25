using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTetromino : MonoBehaviour
{
    [SerializeField] private GameObject[] tetrominoes;
    // Start is called before the first frame update
    void Start()
    {
        SpawnTetro();
    }

    // Update is called once per frame
    public void SpawnTetro()
    {
        Instantiate(tetrominoes[Random.Range(0, tetrominoes.Length)], transform.position, Quaternion.identity);
    }
}
