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

    public int GetTileState(int row, int col)
    {
        return (int)board[row, col];
    }

    bool SameBoard(BoardClone boardOne, BoardClone boardTwo, IPlayer player)
    {
        if(boardOne == null || boardTwo == null)
        {
            return false;
        }
        
        for (int i = 0; i < MAXROW; i++)
        {
            for (int j = 0; j < MAXCOL; j++)
            {
                if(boardOne.GetTileState(i, j) == ((Player)player).PlayerNumber)// && != boardTwo.GetTileState(i, j))
                {
                    if(boardTwo.GetTileState(i,j) != ((Player)player).PlayerNumber)
                    {
                        return false;

                    }
                }
            }
        }
        return true;
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

                //foreach (BoardClone prevBoard in boardController.previousBoards)
                //{
                //    if (!SameBoard(newBoard, prevBoard, player))
                //    {
                //        output.Add(newBoard);
                //    }
                //}
                if (boardController.previousBoards.Count > 20)
                {
                    for (int i = 1; i < 5; i++)
                    {

                        if (!SameBoard(newBoard, boardController.previousBoards[boardController.previousBoards.Count - i], player))
                        {
                            output.Add(newBoard);
                        }
                    }
                }
                
                //output.Add(newBoard);
            }
            boardController.ResetSelectedBall();
        }
        return (output);
    }       
    
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
                    points -= (int)(Mathf.Pow(d, 3f));


                    foreach (var pos in p.WinPositions)
                    {
                        if(i == pos.row && j == pos.column)
                        {
                            points += 10000;
                        }
                    }
                    
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
    public bool OneLeft(IPlayer player)
    {
        int c = 0;
        List<Tile> winPos = ((Player)player).WinPositions;
        foreach (var tile in winPos)
        {
            if ((int)tile.state != ((Player)player).PlayerNumber)
            {
                c++;
                if(c >= 2)
                {
                    //for (int i = 0; i < MAXROW; i++)
                    //{
                    //    for (int j = 0; j < MAXCOL; j++)
                    //    {
                    //        if((int)board[i,j] == ((Player)player).PlayerNumber)
                    //        {
                    //            bool inGoal = false;
                    //            for (int x = 0; x < ((Player)player).WinPositions.Count; x++)
                    //            {
                    //                if (i == ((Player)player).WinPositions[x].row && j == ((Player)player).WinPositions[x].column)
                    //                {
                    //                    inGoal = true;
                    //                }
                    //            }
                    //            if (!inGoal)
                    //            {
                    //                ((Player)player).PlayerPieces.Clear();
                    //                ((Player)player).PlayerPieces.Add(boardController.Matris[i, j]);
                                    
                    //            }
                    //        }
            
                    //    }
                    //}

                    //((Player)player).OnePiece(this);
                    return false;
                }
            }
        }
        return true;
    }
}
