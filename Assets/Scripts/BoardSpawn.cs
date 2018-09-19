using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSpawn : MonoBehaviour {
    

    BoardController boardController;

    [SerializeField] GameObject tilePrefab;
    GameObject newObj;
    

    void Start () {

        boardController = FindObjectOfType<BoardController>();

        for (int i = 0; i < boardController.GetRows; i++)
        {
            for (int j = 0; j < boardController.GetComponent<BoardController>().GetColumns; j++)
            {
                if(i%2 == 0)
                {
                    newObj = Instantiate(tilePrefab, new Vector3(0.865f +j * 1.73f, 0, i * 1.5f), transform.rotation);
                }
                else
                {
                    newObj = Instantiate(tilePrefab, new Vector3(j * 1.73f, 0, i * 1.5f), transform.rotation);
                }
                newObj.GetComponent<Tile>().row = i;
                newObj.GetComponent<Tile>().column = j;
                //tilldela tileobjektet värden för dess egen position i matrisen
                boardController.Matris[i, j] = newObj.GetComponent<Tile>();
            }
        }
	}
}
