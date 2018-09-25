using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSpawn : MonoBehaviour {
    

    BoardController boardController;

    [SerializeField] GameObject tilePrefab;
    GameObject newObj;

    [SerializeField] int numberOfPlayers;

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

    

    void Start ()
    {
        boardController = FindObjectOfType<BoardController>();

        SpawnBoard();
        SpawnPlayers(numberOfPlayers);
        
	}

    void SpawnPlayers(int numberOfplayers)
    {
        switch (numberOfplayers)
        {
            case 2:
                PlayerOneSpawn(true);
                PlayerFourSpawn(true);
                break;
            case 3:
                PlayerOneSpawn(false);
                PlayerThreeSpawn();
                PlayerFiveSpawn();
                break;
            case 4:
                PlayerOneSpawn(false);
                PlayerThreeSpawn();
                PlayerFourSpawn(false);
                PlayerSixSpawn();
                break;
            case 6:
                PlayerOneSpawn(false);
                PlayerTwoSpawn();
                PlayerThreeSpawn();
                PlayerFourSpawn(false);
                PlayerFiveSpawn();
                PlayerSixSpawn();
                break;
            default:
                print("Number of players invalid");
                break;
        }
    }


    void PlayerOneSpawn(bool twoPlayerGame)
    {
        boardController.playerActive[0] = true;
        boardController.Matris[0, 6].SetState(2);
        boardController.Matris[1, 6].SetState(2);
        boardController.Matris[1, 7].SetState(2);
        boardController.Matris[2, 5].SetState(2);
        boardController.Matris[2, 6].SetState(2);
        boardController.Matris[2, 7].SetState(2);
        boardController.Matris[3, 5].SetState(2);
        boardController.Matris[3, 6].SetState(2);
        boardController.Matris[3, 7].SetState(2);
        boardController.Matris[3, 8].SetState(2);

        if (twoPlayerGame)
         {
             boardController.Matris[4, 4].SetState(2);
             boardController.Matris[4, 5].SetState(2);
             boardController.Matris[4, 6].SetState(2);
             boardController.Matris[4, 7].SetState(2);
             boardController.Matris[4, 8].SetState(2);
         }

    }
    void PlayerTwoSpawn()
    {
        boardController.playerActive[1] = true;
        boardController.Matris[4, 0].SetState(3);
        boardController.Matris[4, 1].SetState(3);
        boardController.Matris[4, 2].SetState(3);
        boardController.Matris[4, 3].SetState(3);
        boardController.Matris[5, 1].SetState(3);
        boardController.Matris[5, 2].SetState(3);
        boardController.Matris[5, 3].SetState(3);
        boardController.Matris[6, 1].SetState(3);
        boardController.Matris[6, 2].SetState(3);
        boardController.Matris[7, 2].SetState(3);
    }
    void PlayerThreeSpawn()
    {
        boardController.playerActive[2] = true;
        boardController.Matris[9, 2].SetState(4);
        boardController.Matris[10, 2].SetState(4);
        boardController.Matris[10, 1].SetState(4);
        boardController.Matris[11, 3].SetState(4);
        boardController.Matris[11, 2].SetState(4);
        boardController.Matris[11, 1].SetState(4);
        boardController.Matris[12, 3].SetState(4);
        boardController.Matris[12, 2].SetState(4);
        boardController.Matris[12, 1].SetState(4);
        boardController.Matris[12, 0].SetState(4);
    }
    void PlayerFourSpawn(bool twoPlayerGame)
    {
        boardController.playerActive[3] = true;
        boardController.Matris[16, 6].SetState(5);
        boardController.Matris[15, 6].SetState(5);
        boardController.Matris[15, 7].SetState(5);
        boardController.Matris[14, 5].SetState(5);
        boardController.Matris[14, 6].SetState(5);
        boardController.Matris[14, 7].SetState(5);
        boardController.Matris[13, 5].SetState(5);
        boardController.Matris[13, 6].SetState(5);
        boardController.Matris[13, 7].SetState(5);
        boardController.Matris[13, 8].SetState(5);

        if (twoPlayerGame)
         {
             boardController.Matris[12, 4].SetState(5);
             boardController.Matris[12, 5].SetState(5);
             boardController.Matris[12, 6].SetState(5);
             boardController.Matris[12, 7].SetState(5);
             boardController.Matris[12, 8].SetState(5);
         }
    }
    void PlayerFiveSpawn()
    {
        boardController.playerActive[4] = true;
        boardController.Matris[9, 11].SetState(6);
        boardController.Matris[10, 10].SetState(6);
        boardController.Matris[10, 11].SetState(6);
        boardController.Matris[11, 10].SetState(6);
        boardController.Matris[11, 11].SetState(6);
        boardController.Matris[11, 12].SetState(6);
        boardController.Matris[12, 9].SetState(6);
        boardController.Matris[12, 10].SetState(6);
        boardController.Matris[12, 11].SetState(6);
        boardController.Matris[12, 12].SetState(6);
    }
    void PlayerSixSpawn()
    {
        boardController.playerActive[5] = true;
        boardController.Matris[4, 12].SetState(7);
        boardController.Matris[4, 11].SetState(7);
        boardController.Matris[4, 10].SetState(7);
        boardController.Matris[4, 9].SetState(7);
        boardController.Matris[5, 12].SetState(7);
        boardController.Matris[5, 11].SetState(7);
        boardController.Matris[5, 10].SetState(7);
        boardController.Matris[6, 11].SetState(7);
        boardController.Matris[6, 10].SetState(7);
        boardController.Matris[7, 11].SetState(7);
    }

    void SpawnBoard()
    {
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
                    newObj.GetComponent<Tile>().state = Tile.TileState.open;

                    //tilldela tileobjektet värden för dess egen position i matrisen
                    newObj.GetComponent<Tile>().row = i;
                    newObj.GetComponent<Tile>().column = j;
                    //lägg in på den positionen i matrisen
                    boardController.Matris[i, j] = newObj.GetComponent<Tile>();
                }
                counter++;
            }
        }
    }
}
