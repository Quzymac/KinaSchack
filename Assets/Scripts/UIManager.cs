using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField] GameObject startGameMenu;
    BoardController boardController;
    BoardSpawn boardSpawn;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject resumeButton;


    private void Start()
    {
        boardController = FindObjectOfType<BoardController>();
        boardSpawn = FindObjectOfType<BoardSpawn>();
    }

    public void OpenMenu()
    {
        startGameMenu.SetActive(true);
        boardController.paused = true;
        winScreen.SetActive(false);
    }
    public void DisableResumeButton()
    {
        resumeButton.SetActive(false);
    }

    public void CloseMenu()
    {
        startGameMenu.SetActive(false);
        boardController.paused = false;
        resumeButton.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void TwoPlayerGame()
    {
        ClearBoard();
        boardSpawn.SpawnPlayers(2);
        boardController.currentPlayer = boardController.playerList[0];
        CloseMenu();
        boardController.paused = false;
    }
    public void ThreePlayerGame()
    {
        ClearBoard();
        boardSpawn.SpawnPlayers(3);
        boardController.currentPlayer = boardController.playerList[0];
        CloseMenu();
        boardController.paused = false;
    }
    public void FourPlayerGame()
    {
        ClearBoard();
        boardSpawn.SpawnPlayers(4);
        boardController.currentPlayer = boardController.playerList[0];
        CloseMenu();
        boardController.paused = false;
    }
    public void SixPlayerGame()
    {
        ClearBoard();
        boardSpawn.SpawnPlayers(6);
        boardController.currentPlayer = boardController.playerList[0];
        CloseMenu();
        boardController.paused = false;
    }

    void ClearBoard()
    {

        boardController.playerList.Clear();

        boardController.playerOneActive = false;
        boardController.playerTwoActive = false;
        boardController.playerThreeActive = false;
        boardController.playerFourActive = false;
        boardController.playerFiveActive = false;
        boardController.playerSixActive = false;

        for (int i = 0; i < boardController.GetRows; i++)
        {
            for (int j = 0; j < boardController.GetColumns; j++)
            {
                if(boardController.Matris[i,j] != null)
                {
                    //set entire board to open
                    boardController.Matris[i, j].SetState(1);
                }
            }
        }
    }


}
