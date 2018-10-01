using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Minimax;

public class BoardClone : MonoBehaviour, IState
{
    const int MAXROW = 17;
    const int MAXCOL = 13;
    Tile[,] board = new Tile[MAXROW, MAXCOL];

    public BoardClone() { }
    public BoardClone(Tile[,] board)
    {
        this.board = board;
    }
    
    public void SetTile(int row, int col, int playerNumber)
    {
        board[row, col].SetState(playerNumber);
    }

    public void SetValue(int value)
    {
        currentValue = value;
    }

    public int currentValue { set; get; } = 0;

    public List<IState> Expand(IPlayer player, IPlayer otherPlayer)
    {
        List<IState> output = new List<IState>();
        for (int row = 0; row < MAXROW; row++)
            for (int col = 0; col < MAXCOL; col++)
            {
                if (board[row, col].state == Tile.TileState.open)
                {
                    BoardClone newBoard = new BoardClone((Tile[,])board.Clone());
                    newBoard.SetTile(row, col, ((Player)player).PlayerNumber);
                    newBoard.SetValue(newBoard.Value(player));
                    output.Add(newBoard);
                }
            }
        return (output);
    }
    public int Value(IPlayer player)
    {

        return 0;
    }
    public bool Won()
    {
        return (false);
    }
}
