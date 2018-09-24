using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSpawn : MonoBehaviour {
    

    BoardController boardController;

    [SerializeField] GameObject tilePrefab;
    GameObject newObj;

    int[] gameBoard = new int[] {     0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 
                                    0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 
                                      0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 
                                    0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0,
                                      1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
                                    0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
                                      0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 
                                    0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0,  
                                      0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 
                                    0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0,
                                      0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 
                                    0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
                                      1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 
                                    0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 
                                      0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0,
                                    0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 
                                      0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 };


    

    void Start () {

        boardController = FindObjectOfType<BoardController>();

        int counter = 0;

        for (int i = 0; i < boardController.GetRows; i++)
        {
            for (int j = 0; j < boardController.GetColumns; j++)
            {
                if (gameBoard[counter] != 0)
                {
                   
                    if (i % 2 == 0)
                    {
                        newObj = Instantiate(tilePrefab, new Vector3(0.865f + j * 1.73f, 0, i * 1.5f), transform.rotation);
                    }
                    else
                    {
                        newObj = Instantiate(tilePrefab, new Vector3(j * 1.73f, 0, i * 1.5f), transform.rotation);
                    }
                    newObj.GetComponent<Tile>().row = i;
                    newObj.GetComponent<Tile>().column = j;
                    newObj.GetComponent<Tile>().state = Tile.TileState.open;


                    //tilldela tileobjektet värden för dess egen position i matrisen
                    boardController.Matris[i, j] = newObj.GetComponent<Tile>();
                }
               
                counter++;
            }
        }
	}
}
