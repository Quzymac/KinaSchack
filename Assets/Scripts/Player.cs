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
        if (!fill)
        {
            foreach (var piece in WinPositions)
            {
                bool inGoal = false;
                for (int i = 0; i < PlayerPieces.Count; i++)
                {
                    if (piece.column == PlayerPieces[i].column && piece.row == PlayerPieces[i].row)
                    {
                        inGoal = true;
                    }
                }
                if (!inGoal)
                {
                    GoalPos = new Vector2Int(piece.row, piece.column);
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
