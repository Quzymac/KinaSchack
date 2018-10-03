using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Minimax;

public class BoardClone : IState
{
    const int MAXROW = 17;
    const int MAXCOL = 13;
    public Tile.TileState[,] board = new Tile.TileState[MAXROW, MAXCOL];
    BoardController boardController;
    


    public void BoardCreate()
    {
        board = new Tile.TileState[MAXROW, MAXCOL];

        for (int i = 0; i < MAXROW; i++)
        {
            for (int j = 0; j < MAXCOL; j++)
            {
                if (boardController.Matris[i, j] != null)
                {
                    board[i, j] = boardController.Matris[i, j].state;
                }
            }
        }
    }
    public BoardClone(Tile.TileState[,] board, BoardController bC)
    {
        this.board = board;
        boardController = bC;
    }
    
    public void SetTile(int row, int col, int playerNumber)
    {
        board[row, col] = (Tile.TileState)playerNumber;
    }

    public void SetValue(int value)
    {
        currentValue = value;
    }

    public int currentValue { set; get; } = 0;

    public List<IState> Expand(IPlayer player, IPlayer otherPlayer)  //Clones the board for the AI to use, one clone for each individual move the AI can make
    {

        List<IState> output = new List<IState>();

        foreach (var piece in ((Player)player).PlayerPieces)
        {
            boardController.CheckForValidMoves(piece, false);

            foreach (var validMove in boardController.GetValidMovesList())
            {
                
                    BoardClone newBoard = new BoardClone((Tile.TileState[,])board.Clone(), boardController);
                    newBoard.SetTile(piece.row, piece.column, (int)Tile.TileState.open);
                    newBoard.SetTile(validMove.row, validMove.column, ((Player)player).PlayerNumber);

                    newBoard.SetValue(newBoard.Value(player));
                    
                    //Debug.Log(newBoard.currentValue);

                    output.Add(newBoard);
                
            }
            boardController.ResetSelectedBall();
        }
        return (output);
    }
    public int Value(IPlayer player)
    {
        //return value for player
        if (Won(player))
        {
            return int.MaxValue;
        }
        int points = 0;

        List<Vector2Int> playerPieces = new List<Vector2Int>();
        for (int i = 0; i < MAXROW; i++)
        {
            for (int j = 0; j < MAXCOL; j++)
            {
                if ((int)board[i, j] == ((Player)player).PlayerNumber)
                {

                    playerPieces.Add(new Vector2Int(i,j));

                }
            }
        }

        foreach (var piece in playerPieces)
        {   

            points += piece.x + piece.y * piece.y ;

            //Debug.Log(piece.Points);
        }
        //Debug.Log(points);
        return points;
    }
    public bool Won(IPlayer player)
    {
        List<Tile> winPos = ((Player)player).WinPositions;
        foreach (var tile in winPos)
        {
            if ((int)tile.state != ((Player)player).PlayerNumber)
            {
                return false;
            }
        }
        return true;
    }
}
