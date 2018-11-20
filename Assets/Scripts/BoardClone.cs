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
    
    public void SetTile(int row, int col, Tile.TileState state)
    {
        board[row, col] = state;
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
            boardController.CheckForValidMoves(piece, false, board); // check on current board

            foreach (var validMove in boardController.GetValidMovesList())
            {
                //clones the board for each possible move and then makes the move on the clone-board
                BoardClone newBoard = new BoardClone((Tile.TileState[,])board.Clone(), boardController);
                newBoard.SetTile(piece.row, piece.column, Tile.TileState.open);
                newBoard.SetTile(validMove.x, validMove.y, (Tile.TileState)((Player)player).PlayerNumber);

                output.Add(newBoard);
            }
            boardController.ResetSelectedBall();
        }
        return (output);
    }       
    
    //return value for player, only for one AI right now, thinking about checking vector2 distance to win condition to set point for each position, WIP
    public int Value(IPlayer player)
    {
        Player p = (Player)player;
        if (Won(player))
        {
            return int.MaxValue;
        }

        //float points = 0;

        //Vector2Int goalPos = new Vector2Int(p.WinPositions[0].row, p.WinPositions[0].column);
        //Debug.Log(goalPos);
        
        //foreach (var piece in p.PlayerPieces)
        //{
        //    points -= Vector2Int.Distance(new Vector2Int(piece.row, piece.column), p.GoalPos);
        //}
        //int value = (int)(points * points);
        ////Debug.Log(points);

        int points = 0;
        for (int i = 0; i < MAXROW; i++) 
        {
            for (int j = 0; j < MAXCOL; j++)
            {
                if ((int)board[i, j] == p.PlayerNumber)
                {

                    //fult men fungerar typ, fastnar när den nästan är inne ---------------------->
                    float d = Vector2Int.Distance(new Vector2Int(p.WinPositions[0].row, p.WinPositions[0].column), new Vector2Int(i, j));
                    points -= (int)(Mathf.Pow(d, 3));
                    //if (j <= 6)
                    //{
                    //    points += j;
                    //}
                    //else
                    //{
                    //    switch (j) // snabb fullösning för att slippa beräkningar
                    //    {
                    //        case 7:
                    //            points += 5;
                    //            break;
                    //        case 8:
                    //            points += 4;
                    //            break;
                    //        case 9:
                    //            points += 3;
                    //            break;
                    //        case 10:
                    //            points += 2;
                    //            break;
                    //        case 11:
                    //            points += 1;
                    //            break;
                    //    }
                    //}
                }
            }
        }
        return points;


        //return value;

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
