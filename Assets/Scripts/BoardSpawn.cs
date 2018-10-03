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
        //SpawnPlayers(numberOfPlayers);
        
	}

    public void SpawnPlayers(int numberOfplayers)
    {
        switch (numberOfplayers)
        {
            case 2:
                PlayerOneSpawn(true);
                PlayerFourSpawn(true);
                boardController.currentPlayer = boardController.playerList[0];
                break;
            case 3:
                PlayerOneSpawn(false);
                PlayerThreeSpawn();
                PlayerFiveSpawn();
                boardController.currentPlayer = boardController.playerList[0];
                break;
            case 4:
                PlayerOneSpawn(false);
                PlayerThreeSpawn();
                PlayerFourSpawn(false);
                PlayerSixSpawn();
                boardController.currentPlayer = boardController.playerList[0];
                break;
            case 6:
                PlayerOneSpawn(false);
                PlayerTwoSpawn();
                PlayerThreeSpawn();
                PlayerFourSpawn(false);
                PlayerFiveSpawn();
                PlayerSixSpawn();
                boardController.currentPlayer = boardController.playerList[0];
                break;
            default:
                print("Number of players invalid");
                break;
        }
    }

    public List<Tile> GetWinPositions(Player player, bool twoPlayers)
    {
        List<Tile> WinPositions = new List<Tile>();

        switch (player.PlayerNumber)
        {
            case 2:
                WinPositions.Add(boardController.Matris[16, 6]);
                WinPositions.Add(boardController.Matris[15, 6]);
                WinPositions.Add(boardController.Matris[15, 7]);
                WinPositions.Add(boardController.Matris[14, 5]);
                WinPositions.Add(boardController.Matris[14, 6]);
                WinPositions.Add(boardController.Matris[14, 7]);
                WinPositions.Add(boardController.Matris[13, 5]);
                WinPositions.Add(boardController.Matris[13, 6]);
                WinPositions.Add(boardController.Matris[13, 7]);
                WinPositions.Add(boardController.Matris[13, 8]);
                if (twoPlayers)
                {
                    WinPositions.Add(boardController.Matris[12, 4]);
                    WinPositions.Add(boardController.Matris[12, 5]);
                    WinPositions.Add(boardController.Matris[12, 6]);
                    WinPositions.Add(boardController.Matris[12, 7]);
                    WinPositions.Add(boardController.Matris[12, 8]);
                }
                return WinPositions;
            case 3:
                WinPositions.Add(boardController.Matris[9, 11]);
                WinPositions.Add(boardController.Matris[10, 10]);
                WinPositions.Add(boardController.Matris[10, 11]);
                WinPositions.Add(boardController.Matris[11, 10]);
                WinPositions.Add(boardController.Matris[11, 11]);
                WinPositions.Add(boardController.Matris[11, 12]);
                WinPositions.Add(boardController.Matris[12, 9]);
                WinPositions.Add(boardController.Matris[12, 10]);
                WinPositions.Add(boardController.Matris[12, 11]);
                WinPositions.Add(boardController.Matris[12, 12]);
                return WinPositions;
            case 4:
                WinPositions.Add(boardController.Matris[4, 12]);
                WinPositions.Add(boardController.Matris[4, 11]);
                WinPositions.Add(boardController.Matris[4, 10]);
                WinPositions.Add(boardController.Matris[4, 9]);
                WinPositions.Add(boardController.Matris[5, 12]);
                WinPositions.Add(boardController.Matris[5, 11]);
                WinPositions.Add(boardController.Matris[5, 10]);
                WinPositions.Add(boardController.Matris[6, 11]);
                WinPositions.Add(boardController.Matris[6, 10]);
                WinPositions.Add(boardController.Matris[7, 11]);
                return WinPositions;
            case 5:
                WinPositions.Add(boardController.Matris[0, 6]);
                WinPositions.Add(boardController.Matris[1, 6]);
                WinPositions.Add(boardController.Matris[1, 7]);
                WinPositions.Add(boardController.Matris[2, 5]);
                WinPositions.Add(boardController.Matris[2, 6]);
                WinPositions.Add(boardController.Matris[2, 7]);
                WinPositions.Add(boardController.Matris[3, 5]);
                WinPositions.Add(boardController.Matris[3, 6]);
                WinPositions.Add(boardController.Matris[3, 7]);
                WinPositions.Add(boardController.Matris[3, 8]);
                if (twoPlayers)
                {
                    WinPositions.Add(boardController.Matris[4, 4]);
                    WinPositions.Add(boardController.Matris[4, 5]);
                    WinPositions.Add(boardController.Matris[4, 6]);
                    WinPositions.Add(boardController.Matris[4, 7]);
                    WinPositions.Add(boardController.Matris[4, 8]);
                }
                return WinPositions;
            case 6:
                WinPositions.Add(boardController.Matris[4, 0]);
                WinPositions.Add(boardController.Matris[4, 1]);
                WinPositions.Add(boardController.Matris[4, 2]);
                WinPositions.Add(boardController.Matris[4, 3]);
                WinPositions.Add(boardController.Matris[5, 1]);
                WinPositions.Add(boardController.Matris[5, 2]);
                WinPositions.Add(boardController.Matris[5, 3]);
                WinPositions.Add(boardController.Matris[6, 1]);
                WinPositions.Add(boardController.Matris[6, 2]);
                WinPositions.Add(boardController.Matris[7, 2]);
                return WinPositions;
            case 7:
                WinPositions.Add(boardController.Matris[9, 2]);
                WinPositions.Add(boardController.Matris[10, 2]);
                WinPositions.Add(boardController.Matris[10, 1]);
                WinPositions.Add(boardController.Matris[11, 3]);
                WinPositions.Add(boardController.Matris[11, 2]);
                WinPositions.Add(boardController.Matris[11, 1]);
                WinPositions.Add(boardController.Matris[12, 3]);
                WinPositions.Add(boardController.Matris[12, 2]);
                WinPositions.Add(boardController.Matris[12, 1]);
                WinPositions.Add(boardController.Matris[12, 0]);
                return WinPositions;
            default:
                return null;
        }
    }

    void PlayerOneSpawn(bool twoPlayerGame)
    {
        Player player = Player.CreatePlayer(2);
        boardController.playerList.Add(player);
        player.WinPositions.AddRange(GetWinPositions(player, twoPlayerGame));

        boardController.playerOneActive = true;

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

        player.PlayerPieces.Add(boardController.Matris[0, 6]);
        player.PlayerPieces.Add(boardController.Matris[1, 6]);
        player.PlayerPieces.Add(boardController.Matris[1, 7]);
        player.PlayerPieces.Add(boardController.Matris[2, 5]);
        player.PlayerPieces.Add(boardController.Matris[2, 6]);
        player.PlayerPieces.Add(boardController.Matris[2, 7]);
        player.PlayerPieces.Add(boardController.Matris[3, 5]);
        player.PlayerPieces.Add(boardController.Matris[3, 6]);
        player.PlayerPieces.Add(boardController.Matris[3, 7]);
        player.PlayerPieces.Add(boardController.Matris[3, 8]);

        if (twoPlayerGame)
            {
             boardController.Matris[4, 4].SetState(2);
             boardController.Matris[4, 5].SetState(2);
             boardController.Matris[4, 6].SetState(2);
             boardController.Matris[4, 7].SetState(2);
             boardController.Matris[4, 8].SetState(2);

            player.PlayerPieces.Add(boardController.Matris[4, 4]);
            player.PlayerPieces.Add(boardController.Matris[4, 5]);
            player.PlayerPieces.Add(boardController.Matris[4, 6]);
            player.PlayerPieces.Add(boardController.Matris[4, 7]);
            player.PlayerPieces.Add(boardController.Matris[4, 8]);
            }

        }
    void PlayerTwoSpawn()
    {
        Player player = Player.CreatePlayer(3);
        boardController.playerList.Add(player);
        player.WinPositions.AddRange(GetWinPositions(player, false));

        boardController.playerTwoActive = true;
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

        player.PlayerPieces.Add(boardController.Matris[4, 0]);
        player.PlayerPieces.Add(boardController.Matris[4, 1]);
        player.PlayerPieces.Add(boardController.Matris[4, 2]);
        player.PlayerPieces.Add(boardController.Matris[4, 3]);
        player.PlayerPieces.Add(boardController.Matris[5, 1]);
        player.PlayerPieces.Add(boardController.Matris[5, 2]);
        player.PlayerPieces.Add(boardController.Matris[5, 3]);
        player.PlayerPieces.Add(boardController.Matris[6, 1]);
        player.PlayerPieces.Add(boardController.Matris[6, 2]);
        player.PlayerPieces.Add(boardController.Matris[7, 2]);

    }
    void PlayerThreeSpawn()
    {
        Player player = Player.CreatePlayer(4);
        boardController.playerList.Add(player);
        player.WinPositions.AddRange(GetWinPositions(player, false));

        boardController.playerThreeActive = true;
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

        player.PlayerPieces.Add(boardController.Matris[9, 2]);
        player.PlayerPieces.Add(boardController.Matris[10, 2]);
        player.PlayerPieces.Add(boardController.Matris[10, 1]);
        player.PlayerPieces.Add(boardController.Matris[11, 3]);
        player.PlayerPieces.Add(boardController.Matris[11, 2]);
        player.PlayerPieces.Add(boardController.Matris[11, 1]);
        player.PlayerPieces.Add(boardController.Matris[12, 3]);
        player.PlayerPieces.Add(boardController.Matris[12, 2]);
        player.PlayerPieces.Add(boardController.Matris[12, 1]);
        player.PlayerPieces.Add(boardController.Matris[12, 0]);

    }
    void PlayerFourSpawn(bool twoPlayerGame)
    {
        Player player = Player.CreatePlayer(5);
        boardController.playerList.Add(player);
        player.WinPositions.AddRange(GetWinPositions(player, twoPlayerGame));

        boardController.playerFourActive = true;
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

        player.PlayerPieces.Add(boardController.Matris[16, 6]);
        player.PlayerPieces.Add(boardController.Matris[15, 6]);
        player.PlayerPieces.Add(boardController.Matris[15, 7]);
        player.PlayerPieces.Add(boardController.Matris[14, 5]);
        player.PlayerPieces.Add(boardController.Matris[14, 6]);
        player.PlayerPieces.Add(boardController.Matris[14, 7]);
        player.PlayerPieces.Add(boardController.Matris[13, 5]);
        player.PlayerPieces.Add(boardController.Matris[13, 6]);
        player.PlayerPieces.Add(boardController.Matris[13, 7]);
        player.PlayerPieces.Add(boardController.Matris[13, 8]);

        
        if (twoPlayerGame)
         {
            boardController.Matris[12, 4].SetState(5);
            boardController.Matris[12, 5].SetState(5);
            boardController.Matris[12, 6].SetState(5);
            boardController.Matris[12, 7].SetState(5);
            boardController.Matris[12, 8].SetState(5);

            player.PlayerPieces.Add(boardController.Matris[12, 4]);
            player.PlayerPieces.Add(boardController.Matris[12, 5]);
            player.PlayerPieces.Add(boardController.Matris[12, 6]);
            player.PlayerPieces.Add(boardController.Matris[12, 7]);
            player.PlayerPieces.Add(boardController.Matris[12, 8]);
        }
    }
    void PlayerFiveSpawn()
    {
        Player player = Player.CreatePlayer(6);
        boardController.playerList.Add(player);
        player.WinPositions.AddRange(GetWinPositions(player, false));

        boardController.playerFiveActive = true;
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

        player.PlayerPieces.Add(boardController.Matris[9, 11]);
        player.PlayerPieces.Add(boardController.Matris[10, 10]);
        player.PlayerPieces.Add(boardController.Matris[10, 11]);
        player.PlayerPieces.Add(boardController.Matris[11, 10]);
        player.PlayerPieces.Add(boardController.Matris[11, 11]);
        player.PlayerPieces.Add(boardController.Matris[11, 12]);
        player.PlayerPieces.Add(boardController.Matris[12, 9]);
        player.PlayerPieces.Add(boardController.Matris[12, 10]);
        player.PlayerPieces.Add(boardController.Matris[12, 11]);
        player.PlayerPieces.Add(boardController.Matris[12, 12]);

    }
    void PlayerSixSpawn()
    {
        Player player = Player.CreatePlayer(7);
        boardController.playerList.Add(player);
        player.WinPositions.AddRange(GetWinPositions(player, false));

        boardController.playerSixActive = true;
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

        player.PlayerPieces.Add(boardController.Matris[4, 12]);
        player.PlayerPieces.Add(boardController.Matris[4, 11]);
        player.PlayerPieces.Add(boardController.Matris[4, 10]);
        player.PlayerPieces.Add(boardController.Matris[4, 9]);
        player.PlayerPieces.Add(boardController.Matris[5, 12]);
        player.PlayerPieces.Add(boardController.Matris[5, 11]);
        player.PlayerPieces.Add(boardController.Matris[5, 10]);
        player.PlayerPieces.Add(boardController.Matris[6, 11]);
        player.PlayerPieces.Add(boardController.Matris[6, 10]);
        player.PlayerPieces.Add(boardController.Matris[7, 11]);
    }

    void SpawnBoard()
    {
        int counter = 0;

        for (int i = 0; i < boardController.GetRows; i++)
        {
            int colPointCount = 0;
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

                    newObj.GetComponent<Tile>().Points = i + j*j; //------ test points for 1v1---------------------------------------------------
                    
                    //lägg in på den positionen i matrisen
                    newObj.name = string.Format("Tile {0},{1}", i, j);
                    boardController.Matris[i, j] = newObj.GetComponent<Tile>();
                }
                counter++;
            }
        }
    }
}
