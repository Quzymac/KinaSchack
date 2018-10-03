using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class SaveGame : MonoBehaviour {


    BoardController boardController;
    BoardSpawn boardSpawn;

    void Start () {
        boardController = FindObjectOfType<BoardController>();
        boardSpawn = FindObjectOfType<BoardSpawn>();
    }

 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();

        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();

        }
    }



    public void Save()
    {

        SaveFile saveFile = new SaveFile();


        for (int i = 0; i < boardController.GetRows; i++)
        {
            for (int j = 0; j < boardController.GetColumns; j++)
            {
                if(boardController.Matris[i,j] != null)
                {
                    saveFile.gameBoardState.Add((int)boardController.Matris[i,j].state);
                }
            }
        }

        saveFile.playersActive[0] = boardController.playerOneActive;
        saveFile.playersActive[1] = boardController.playerTwoActive;
        saveFile.playersActive[2] = boardController.playerThreeActive;
        saveFile.playersActive[3] = boardController.playerFourActive;
        saveFile.playersActive[4] = boardController.playerFiveActive;
        saveFile.playersActive[5] = boardController.playerSixActive;

        saveFile.playersTurn = boardController.currentPlayer.PlayerNumber;
        
        BinaryFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(Application.persistentDataPath + "/savefile.dat", FileMode.Create);
        formatter.Serialize(stream, saveFile);
        stream.Close();

    }

    public void Load()
    {
        SaveFile loadFile;
        BinaryFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(Application.persistentDataPath + "/savefile.dat", FileMode.Open);
        loadFile = (SaveFile)formatter.Deserialize(stream);
        stream.Close();

        boardController.playerList.Clear();

        boardController.playerOneActive = loadFile.playersActive[0];
        boardController.playerTwoActive = loadFile.playersActive[1];
        boardController.playerThreeActive = loadFile.playersActive[2];
        boardController.playerFourActive = loadFile.playersActive[3];
        boardController.playerFiveActive = loadFile.playersActive[4];
        boardController.playerSixActive = loadFile.playersActive[5];

        int counter = 0;
        for (int i = 0; i < boardController.GetRows; i++)
        {
            for (int j = 0; j < boardController.GetColumns; j++)
            {
                if (boardController.Matris[i, j] != null)
                {
                    boardController.Matris[i, j].SetState(loadFile.gameBoardState[counter]);

                    if(loadFile.gameBoardState[counter] > 1)
                    {
                        MakePlayerLists(loadFile.gameBoardState[counter], boardController.Matris[i, j]);
                    }
                    counter++;
                }
            }
        }
        boardController.currentPlayer = boardController.playerList[0];
    }

    void MakePlayerLists(int playerNum, Tile tile)
    {
        List<Player> playerList = boardController.playerList;
        Player player = playerList.Find(x => x.PlayerNumber == playerNum);
        if(player == null)
        {
            player = Player.CreatePlayer(playerNum);
            boardController.playerList.Add(player);

            if (boardController.playerOneActive && !boardController.playerTwoActive && !boardController.playerThreeActive && boardController.playerFourActive &&
                !boardController.playerFiveActive && !boardController.playerSixActive)
            {
                player.WinPositions.AddRange(boardSpawn.GetWinPositions(player, true));
            }
            else
            {
                player.WinPositions.AddRange(boardSpawn.GetWinPositions(player, false));
            }
        }
        player.PlayerPieces.Add(tile);
    }
}
