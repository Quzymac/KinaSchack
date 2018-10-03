using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Minimax;

public class BoardController : MonoBehaviour
{

    [SerializeField] Camera cam;
    RaycastHit hit;
    [SerializeField] LayerMask mask;

    public bool HolingBall { get; set; }
    [SerializeField] Tile ballTile;
    [SerializeField] Tile destinationTile;

    const int rows = 17;
    const int columns = 13;

    public bool paused { get; set; } = true;

    public bool playerOneActive { get; set; }
    public bool playerTwoActive { get; set; }
    public bool playerThreeActive { get; set; }
    public bool playerFourActive { get; set; }
    public bool playerFiveActive { get; set; }
    public bool playerSixActive { get; set; }
    
    Tile[,] matris = new Tile[rows, columns];
    
    List<Vector2Int> validJumpList = new List<Vector2Int>();

    public List<Player> playerList = new List<Player>();

    public Player currentPlayer;


    public Tile[,] Matris
    {
        get { return matris; }
        set { matris = value; }
    }
    public int GetRows
    {
        get { return rows; }
    }
    public int GetColumns
    {
        get { return columns; }
    }
    

    public List<Vector2Int> GetValidMovesList()
    {
        return validJumpList;
    }

    //Checks if move to a tile is possible
    public bool TileIsValid(int row, int column)
    {
        if (row >= rows || column >= columns || row < 0 || column < 0 || matris[row, column] == null || matris[row, column].state != Tile.TileState.open)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void NorthEastMove(int row, int column, bool hasJumped, Tile.TileState[,] board)
    {
        if (row % 2 == 0)
        {
            if (TileIsValid(row + 1, column + 1) && !matris[row, column].validMove && !hasJumped)
            {
                validJumpList.Add(new Vector2Int(row + 1, column + 1));
            }

            else
            {
                if (TileIsValid(row + 2, column + 1) && matris[row + 1, column + 1].state != Tile.TileState.open && !validJumpList.Contains(new Vector2Int(row + 2, column + 1)))
                {
                    validJumpList.Add(new Vector2Int(row + 2, column + 1));
                    CheckForValidMoves(matris[row + 2, column + 1].GetComponent<Tile>(), true, board);
                }
            }
        }
        else
        {
            if (TileIsValid(row + 1, column) && !matris[row, column].validMove && !hasJumped)
            {
                validJumpList.Add(new Vector2Int(row + 1, column));
            }
            else
            {
                if (TileIsValid(row + 2, column + 1) && matris[row + 1, column].state != Tile.TileState.open && !validJumpList.Contains(new Vector2Int(row + 2, column + 1)))
                {
                    validJumpList.Add(new Vector2Int(row + 2, column + 1));
                    CheckForValidMoves(matris[row + 2, column + 1].GetComponent<Tile>(), true, board);
                }
            }
        }
    }
    public void EastMove(int row, int column, bool hasJumped, Tile.TileState[,] board)
    {
        if (TileIsValid(row, column + 1) && !matris[row, column].validMove && !hasJumped)
        {
            validJumpList.Add(new Vector2Int(row, column + 1));
        }
        else
        {
            if (TileIsValid(row, column + 2) && matris[row, column + 1].state != Tile.TileState.open && !validJumpList.Contains(new Vector2Int(row, column + 2)))
            {
                validJumpList.Add(new Vector2Int(row, column + 2));
                CheckForValidMoves(matris[row, column + 2].GetComponent<Tile>(), true, board);
            }
        }
    }
    public void SouthEastMove(int row, int column, bool hasJumped, Tile.TileState[,] board)
    {
        if (row % 2 == 0)
        {
            if (TileIsValid(row - 1, column + 1) && !matris[row, column].validMove && !hasJumped)
            {
                validJumpList.Add(new Vector2Int(row - 1, column + 1));
            }
            else
            {
                if (TileIsValid(row - 2, column + 1) && matris[row - 1, column + 1].state != Tile.TileState.open && !validJumpList.Contains(new Vector2Int(row - 2, column + 1)))
                {
                    validJumpList.Add(new Vector2Int(row - 2, column + 1));
                    CheckForValidMoves(matris[row - 2, column + 1].GetComponent<Tile>(), true, board);
                }
            }
        }
        else
        {
            if (TileIsValid(row - 1, column) && !matris[row, column].validMove && !hasJumped)
            {
                validJumpList.Add(new Vector2Int(row - 1, column));
            }
            else
            {
                if (TileIsValid(row - 2, column + 1) && matris[row - 1, column].state != Tile.TileState.open && !validJumpList.Contains(new Vector2Int(row - 2, column + 1)))
                {
                    validJumpList.Add(new Vector2Int(row - 2, column + 1));
                    CheckForValidMoves(matris[row - 2, column + 1].GetComponent<Tile>(), true, board);
                }
            }
        }
    }
    public void SouthWestMove(int row, int column, bool hasJumped, Tile.TileState[,] board)
    {
        if (row % 2 == 0)
        {
            if (TileIsValid(row - 1, column) && !matris[row, column].validMove && !hasJumped)
            {
                validJumpList.Add(new Vector2Int(row - 1, column));
            }
            else
            {
                if (TileIsValid(row - 2, column - 1) && matris[row - 1, column].state != Tile.TileState.open && !validJumpList.Contains(new Vector2Int(row - 2, column - 1)))
                {
                    validJumpList.Add(new Vector2Int(row - 2, column - 1));
                    CheckForValidMoves(matris[row - 2, column - 1].GetComponent<Tile>(), true, board);
                }
            }
        }
        else
        {
            if (TileIsValid(row - 1, column - 1) && !matris[row, column].validMove && !hasJumped)
            {
                validJumpList.Add(new Vector2Int(row - 1, column - 1));
            }
            else
            {
                if (TileIsValid(row - 2, column - 1) && matris[row - 1, column - 1].state != Tile.TileState.open && !validJumpList.Contains(new Vector2Int(row - 2, column - 1)))
                {
                    validJumpList.Add(new Vector2Int(row - 2, column - 1));
                    CheckForValidMoves(matris[row - 2, column - 1].GetComponent<Tile>(), true, board);
                }
            }
        }
    }
    public void WestMove(int row, int column, bool hasJumped, Tile.TileState[,] board)
    {
        if (TileIsValid(row, column - 1) && !matris[row, column].validMove && !hasJumped)
        {
            validJumpList.Add(new Vector2Int(row, column - 1));
        }
        else
        {
            if (TileIsValid(row, column - 2) && matris[row, column - 1].state != Tile.TileState.open && !validJumpList.Contains(new Vector2Int(row, column - 2)))
            {
                validJumpList.Add(new Vector2Int(row, column - 2));
                CheckForValidMoves(matris[row, column - 2].GetComponent<Tile>(), true, board);
            }
        }
    }
    public void NorthWestMove(int row, int column, bool hasJumped, Tile.TileState[,] board)
    {
        if (row % 2 == 0)
        {
            if (TileIsValid(row + 1, column) && !matris[row, column].validMove && !hasJumped)
            {
                validJumpList.Add(new Vector2Int(row + 1, column));
            }
            else
            {
                if (TileIsValid(row + 2, column - 1) && matris[row + 1, column].state != Tile.TileState.open && !validJumpList.Contains(new Vector2Int(row + 1, column - 1)))
                {
                    validJumpList.Add(new Vector2Int(row + 2, column - 1));
                    CheckForValidMoves(matris[row + 2, column - 1].GetComponent<Tile>(), true, board);
                }
            }
        }
        else
        {
            if (TileIsValid(row + 1, column - 1) && !matris[row, column].validMove && !hasJumped)
            {
                validJumpList.Add(new Vector2Int(row + 1, column - 1));
            }
            else
            {
                if (TileIsValid(row + 2, column - 1) && matris[row + 1, column - 1].state != Tile.TileState.open && !validJumpList.Contains(new Vector2Int(row + 2, column - 1)))
                {
                    validJumpList.Add(new Vector2Int(row + 2, column - 1));
                    CheckForValidMoves(matris[row + 2, column - 1].GetComponent<Tile>(), true, board);
                }
            }
        }
    }

    public void CheckForValidMoves(Tile tile, bool jumped, Tile.TileState[,] board)
    {
        int row = tile.row;
        int column = tile.column;


        NorthEastMove(row, column, jumped, board);
        EastMove(row, column, jumped, board);
        SouthEastMove(row, column, jumped, board);
        SouthWestMove(row, column, jumped, board);
        WestMove(row, column, jumped, board);
        NorthWestMove(row, column, jumped, board);

        foreach (Vector2Int jump in validJumpList)
        {
            matris[jump.x, jump.y].MoveIsValid(true);
        }
    }
   
    void ChangeList(Tile previous, Tile next, int playerNum)
    {
        Player temp = playerList.Find(x => x.PlayerNumber == playerNum);
        temp.PlayerPieces.Remove(previous);
        temp.PlayerPieces.Add(next);
    }

    void ClickTile(Tile tileHit)
    {
        //pick up red ball if you're not already holding a ball
        if (!HolingBall && tileHit.state == (Tile.TileState)currentPlayer.PlayerNumber)
        {
            ballTile = tileHit;

            CheckForValidMoves(ballTile, false, EnumMatris());
            HolingBall = true;
        }
        //if holding a "ball", put down ball if tile is open and a valid move
        if (HolingBall && tileHit.state == Tile.TileState.open && tileHit.validMove)
        {
            destinationTile = tileHit;

            ResetSelectedBall();

            //set state and color of tiles
            destinationTile.SetState(currentPlayer.PlayerNumber);
            ballTile.SetState(1);

            //update list of player pieces
            ChangeList(ballTile, destinationTile, currentPlayer.PlayerNumber);

            //resets varibles
            if (ballTile != null)
            {
                ballTile = null;
                HolingBall = false;
            }

            if (CheckWin(currentPlayer))
            {
                print((Tile.TileState)currentPlayer.PlayerNumber + " WIINS!");
            }
            NextPlayer();
            PlayAI();

        }
    }        
    
    //moves to next player
    void NextPlayer()
    {
        int playerIndex = playerList.IndexOf(currentPlayer) + 1;
        if (playerIndex >= playerList.Count)
        {
            playerIndex = 0;
        }
        currentPlayer = playerList[playerIndex];
    }

    bool CheckWin(Player currentPlayer)
    {
        List<Tile> winPos = currentPlayer.WinPositions;
        foreach (var tile in winPos)
        {
            if((int)tile.state != currentPlayer.PlayerNumber)
            {
                return false;
            }
        }
        return true;
    }

    //resets validJumpList list and highlighting
    public void ResetSelectedBall()
    {
        foreach (Vector2Int jump in validJumpList)
        {
            matris[jump.x, jump.y].MoveIsValid(false);
        }
        validJumpList.Clear();
    }

    //returns a copy of matris with enum state values
    Tile.TileState[,] EnumMatris()
    {
        Tile.TileState[,] tempMatris = new Tile.TileState[rows, columns];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if(matris[i, j] != null)
                {
                    tempMatris[i, j] = matris[i, j].state;
                }
            }
        }
        return tempMatris;
    }
    //only works for player vs one AI right now, wip
    void PlayAI()
    {
        BoardClone newBoard = (BoardClone)MiniMax.Select(new BoardClone(EnumMatris(), this), playerList[1], playerList[0], 1, true);

        Tile prev = null;
        Tile nex = null;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (matris[i, j] != null)
                {
                    if (matris[i, j].state != newBoard.board[i, j])
                    {
                        if(matris[i, j].state == Tile.TileState.open)
                        {
                            nex = matris[i, j];
                        }
                        if(matris[i, j].state == (Tile.TileState)playerList[1].PlayerNumber)
                        {
                            prev = matris[i, j];
                        }
                        matris[i, j].SetState((int)newBoard.board[i, j]);
                    }
                }
            }
        }
        ChangeList(prev, nex, playerList[1].PlayerNumber);
        NextPlayer();
    }

    //move with mouse clicks
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayAI();
        }
        if (Input.GetButtonDown("Fire1") && !paused)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
                {
                    //resets if you click the same tile as the ball is on
                    if (hit.collider.gameObject.GetComponent<Tile>().Equals(ballTile))
                    {
                        ResetSelectedBall();

                        ballTile = null;
                        HolingBall = false;
                    }
                    else
                    {
                        ClickTile(hit.collider.gameObject.GetComponent<Tile>());
                    }
                }
                //resets if you click outside the game board
                else
                {
                    ResetSelectedBall();

                    if (ballTile != null)
                    {
                        ballTile = null;
                        HolingBall = false;
                    }
                }
            }
        }
        //rightclick to spawn red ball --- for testing only
        if (Input.GetButtonDown("Fire2"))
        {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
                {
                    if (hit.collider.gameObject.GetComponent<Tile>().state == Tile.TileState.open)
                    {
                        hit.collider.gameObject.GetComponent<Tile>().SetState(2);
                        
                    }
                }
            }
        }
    }

}
