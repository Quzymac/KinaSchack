using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Minimax;


public class Player : MonoBehaviour, IPlayer {

    [SerializeField] int playerNumber;
    public int PlayerNumber { get { return playerNumber; } set { playerNumber = value; } }
    [SerializeField] List<Tile> playerPieces = new List<Tile>();
    public List<Tile> PlayerPieces { get { return playerPieces; } set { playerPieces = value; } }
    [SerializeField] List<Tile> winPositions = new List<Tile>();
    public List<Tile> WinPositions { get { return winPositions; } set { winPositions = value; } }

    [SerializeField] Vector2Int goalPos;
    public Vector2Int GoalPos { get { return goalPos; } set { goalPos = value; } }
    
    public static Player CreatePlayer(int playerNum)
    {
        Player player = new GameObject("Player").AddComponent<Player>();

        player.PlayerNumber = playerNum;

        return player;
    }
}
