using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Minimax;


public class Player : MonoBehaviour, IPlayer {

    int depth = 1;
    public int Depth { get { return depth; } set { depth = value; } }
    [SerializeField] int playerNumber;
    public int PlayerNumber { get { return playerNumber; } set { playerNumber = value; } }
    [SerializeField] List<Tile> playerPieces = new List<Tile>();
    public List<Tile> PlayerPieces { get { return playerPieces; } set { playerPieces = value; } }
    [SerializeField] List<Tile> winPositions = new List<Tile>();
    public List<Tile> WinPositions { get { return winPositions; } set { winPositions = value; } }

    [SerializeField] Vector2Int goalPos;
    public Vector2Int GoalPos { get { return goalPos; } set { goalPos = value; } }
    bool o = true;

    public void OnePiece(BoardClone board)
    {
        bool fill = true;
        for (int i = 0; i < 6; i++)
        {
            if(WinPositions[i].state == (Tile.TileState)PlayerNumber)
            {
                fill = false;
            }
        }
        if (fill)
        {
            foreach (var piece in PlayerPieces)
            {
                bool inGoal = false;
                for (int i = 0; i < WinPositions.Count; i++)
                {
                    if (piece.column == winPositions[i].column && piece.row == winPositions[i].row)
                    {
                        inGoal = true;
                    }
                }
                if (!inGoal)
                {
                    Tile temp = piece;
                    playerPieces.Clear();
                    PlayerPieces.Add(temp);
                }
            }
        }
    }
    public void OnePieceLeft(Vector2Int lastOpenPos, Tile[,] board)
    {
        //goalPos = lastOpenPos;
        // PlayerPieces.Clear();
        //PlayerPieces.Add(board[lastOpenPos.x, lastOpenPos.y]);
        int ccc = 0;
        foreach (var posi in WinPositions)
        {
            if((int)posi.state == playerNumber)
            {
                ccc++;
            }
        }
        if (ccc == PlayerPieces.Count -1)
        {
            if (o)
            {
                Debug.Log("oneleft");
                o = false;
                foreach (var piece in PlayerPieces)
                {
                    foreach (var winPos in WinPositions)
                    {
                        if (!(piece.row == winPos.row && piece.column == winPos.column))
                        {
                            goalPos = new Vector2Int(winPos.row, winPos.column);
                            Debug.Log(goalPos + " " + (Tile.TileState)playerNumber);

                            //Tile last = piece;

                            //PlayerPieces.Clear();
                            //PlayerPieces.Add(last);
                        }
                    }
                }
            }
        }
    }

    public static Player CreatePlayer(int playerNum)
    {
        Player player = new GameObject("Player").AddComponent<Player>();

        player.PlayerNumber = playerNum;

        return player;
    }
}
