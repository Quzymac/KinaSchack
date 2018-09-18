using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {


    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject Highlighted;

    public Material standardMaterial { get; set; }
    [SerializeField] Transform raycastOrigin;

    [SerializeField] LayerMask ballMask;

    public bool validMove { get; set; }

    public enum TileState { invalid, open, red, blue, yellow, green, purple, orange };
    public TileState state { get; set; }

    public GameObject ball { get; set; }

    public List<GameObject> neighbours = new List<GameObject>();

    List<GameObject> canJumpTo = new List<GameObject>();

    public int row { get; set; }
    public int column { get; set; }

    [SerializeField] Material red;

 

    void Start()
    {
        gameManager = FindObjectOfType<BoardSpawn>().gameObject;

        standardMaterial = GetComponent<Renderer>().material;
        
        RaycastHit hit;

        if (Physics.Raycast(raycastOrigin.position, Vector3.down, out hit, Mathf.Infinity, ballMask))
        {
            ball = hit.collider.gameObject;

            switch (hit.collider.gameObject.GetComponent<PlayerBall>().ballTeam.ToString())
            {
                case "red":
                    state = TileState.red;
                    GetComponent<Renderer>().material = red;

                    break;
                case "blue":
                    state = TileState.blue;
                    break;
                case "yellow":
                    state = TileState.yellow;
                    break;
                case "green":
                    state = TileState.green;
                    break;
                case "purple":
                    state = TileState.purple;
                    break;
                case "orange":
                    state = TileState.orange;
                    break;
            }
        }
        else
        {
            state = TileState.open;
        }
    }

    public void CheckIfCanMove()
    {
        if(state == TileState.open)
        {
            MoveIsValid(true);
        }
        else
        {
            //check if you can jump over
        }
    }
    public void Resets()
    {
        if (validMove)
        {
            MoveIsValid(false);
        }
    }
    public bool CanMove()
    {
        if(state == TileState.open)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanJump()
    {
        if(state == TileState.red)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CheckForJumps()
    {
        if (row % 2 == 0)
        {
            for (int i = 0; i < 6; i++)
            {
                switch (i)
                {
                    case 0:
                        if (gameManager.GetComponent<BoardSpawn>().GetMatris[row + 1, column + 1].GetComponent<Tile>().CanJump() &&)
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row + 2, column + 1].GetComponent<Tile>().MoveIsValid(true);
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row + 2, column + 1].GetComponent<Tile>().CheckForJumps();
                        }
                        break;
                    case 1:
                        if (gameManager.GetComponent<BoardSpawn>().GetMatris[row, column + 1].GetComponent<Tile>().CanJump())
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row, column + 2].GetComponent<Tile>().MoveIsValid(true);
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row, column + 2].GetComponent<Tile>().CheckForJumps();
                        }
                        break;
                    case 2:
                        if (gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column + 1].GetComponent<Tile>().CanJump())
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row - 2, column + 1].GetComponent<Tile>().MoveIsValid(true);
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row - 2, column + 1].GetComponent<Tile>().CheckForJumps();
                        }
                        break;
                    case 3:
                        if (gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column].GetComponent<Tile>().CanJump())
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row - 2, column - 1].GetComponent<Tile>().MoveIsValid(true);
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row - 2, column - 1].GetComponent<Tile>().CheckForJumps();
                        }
                        break;
                    case 4:
                        if (gameManager.GetComponent<BoardSpawn>().GetMatris[row, column - 1].GetComponent<Tile>().CanJump())
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row, column - 2].GetComponent<Tile>().MoveIsValid(true);
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row, column - 2].GetComponent<Tile>().CheckForJumps();
                        }
                        break;
                    case 5:
                        if (gameManager.GetComponent<BoardSpawn>().GetMatris[row + 1, column].GetComponent<Tile>().CanJump())
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row + 2, column - 1].GetComponent<Tile>().MoveIsValid(true);
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row + 2, column - 1].GetComponent<Tile>().CheckForJumps();
                        }
                        break;
                }
            }
        }
        else
        {
            for (int i = 0; i < 6; i++)
            {
                switch (i)
                {
                    case 0:
                        if (gameManager.GetComponent<BoardSpawn>().GetMatris[row + 1, column].GetComponent<Tile>().CanJump())
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row + 2, column + 1].GetComponent<Tile>().MoveIsValid(true);
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row + 2, column + 1].GetComponent<Tile>().CheckForJumps();
                        }
                        break;
                    case 1:
                        if (gameManager.GetComponent<BoardSpawn>().GetMatris[row, column + 1].GetComponent<Tile>().CanJump())
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row, column + 2].GetComponent<Tile>().MoveIsValid(true);
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row, column + 2].GetComponent<Tile>().CheckForJumps();
                        }
                        break;
                    case 2:
                        if (gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column].GetComponent<Tile>().CanJump())
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row - 2, column + 1].GetComponent<Tile>().MoveIsValid(true);
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row - 2, column + 1].GetComponent<Tile>().CheckForJumps();
                        }
                        break;
                    case 3:
                        if (gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column - 1].GetComponent<Tile>().CanJump())
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row - 2, column - 1].GetComponent<Tile>().MoveIsValid(true);
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row - 2, column - 1].GetComponent<Tile>().CheckForJumps();
                        }
                        break;
                    case 4:
                        if (gameManager.GetComponent<BoardSpawn>().GetMatris[row, column - 1].GetComponent<Tile>().CanJump())
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row, column - 2].GetComponent<Tile>().MoveIsValid(true);
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row, column - 2].GetComponent<Tile>().CheckForJumps();
                        }
                        break;
                    case 5:
                        if (gameManager.GetComponent<BoardSpawn>().GetMatris[row + 1, column - 1].GetComponent<Tile>().CanJump())
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row + 2, column - 1].GetComponent<Tile>().MoveIsValid(true);
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row + 2, column - 1].GetComponent<Tile>().CheckForJumps();
                        }
                        break;
                }
            }
        }
    }


    //public void CheckForJumps()
    //{
    //    if (row % 2 == 0)
    //    {
    //        for (int i = 0; i < 6; i++)
    //        {
    //            switch (i)
    //            {
    //                case 0:
    //                    if (gameManager.GetComponent<BoardSpawn>().GetMatris[row + 1, column + 1].GetComponent<Tile>().CanMove() == false)
    //                    {
    //                        if (gameManager.GetComponent<BoardSpawn>().GetMatris[row + 2, column + 1].GetComponent<Tile>().CanMove() && gameManager.GetComponent<BoardSpawn>().GetMatris[row + 2, column + 1].GetComponent<Tile>().validMove == false)
    //                        {
    //                            gameManager.GetComponent<BoardSpawn>().GetMatris[row + 2, column + 1].GetComponent<Tile>().CanMove();
    //                            gameManager.GetComponent<BoardSpawn>().GetMatris[row + 2, column + 1].GetComponent<Tile>().CheckForJumps();
    //                        }
    //                    }
    //                    break;
    //                case 1:
    //                    if (gameManager.GetComponent<BoardSpawn>().GetMatris[row, column + 1].GetComponent<Tile>().CanMove())
    //                    {
    //                        gameManager.GetComponent<BoardSpawn>().GetMatris[row, column + 1].GetComponent<Tile>().CanMove();
    //                    }
    //                    else
    //                    {
    //                        gameManager.GetComponent<BoardSpawn>().GetMatris[row, column + 2].GetComponent<Tile>().CanMove();
    //                        gameManager.GetComponent<BoardSpawn>().GetMatris[row, column + 2].GetComponent<Tile>().CheckForJumps();
    //                    }
    //                    break;
    //                case 2:
    //                    if (gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column + 1].GetComponent<Tile>().CanMove())
    //                    {
    //                        gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column + 1].GetComponent<Tile>().CanMove();
    //                    }
    //                    else
    //                    {
    //                        gameManager.GetComponent<BoardSpawn>().GetMatris[row - 2, column + 1].GetComponent<Tile>().CanMove();
    //                        gameManager.GetComponent<BoardSpawn>().GetMatris[row - 2, column + 1].GetComponent<Tile>().CheckForJumps();
    //                    }
    //                    break;
    //                case 3:
    //                    if (gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column].GetComponent<Tile>().CanMove())
    //                    {
    //                        gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column].GetComponent<Tile>().CanMove();
    //                    }
    //                    else
    //                    {
    //                        gameManager.GetComponent<BoardSpawn>().GetMatris[row - 2, column - 1].GetComponent<Tile>().CanMove();
    //                        gameManager.GetComponent<BoardSpawn>().GetMatris[row - 2, column - 1].GetComponent<Tile>().CheckForJumps();
    //                    }
    //                    break;
    //                case 4:
    //                    if (gameManager.GetComponent<BoardSpawn>().GetMatris[row, column - 1].GetComponent<Tile>().CanMove() == false)
    //                    {
    //                        if (gameManager.GetComponent<BoardSpawn>().GetMatris[row, column - 2].GetComponent<Tile>().CanMove() && gameManager.GetComponent<BoardSpawn>().GetMatris[row, column - 2].GetComponent<Tile>().validMove == false)
    //                        {
    //                            gameManager.GetComponent<BoardSpawn>().GetMatris[row, column - 2].GetComponent<Tile>().CanMove();
    //                            gameManager.GetComponent<BoardSpawn>().GetMatris[row, column - 2].GetComponent<Tile>().CheckForJumps();
    //                        }
    //                    }
    //                    break;
    //                case 5:
    //                    if (gameManager.GetComponent<BoardSpawn>().GetMatris[row + 1, column].GetComponent<Tile>().CanMove())
    //                    {
    //                        gameManager.GetComponent<BoardSpawn>().GetMatris[row + 1, column].GetComponent<Tile>().CanMove();
    //                    }
    //                    else
    //                    {
    //                        gameManager.GetComponent<BoardSpawn>().GetMatris[row + 2, column - 1].GetComponent<Tile>().CanMove();
    //                        gameManager.GetComponent<BoardSpawn>().GetMatris[row + 2, column - 1].GetComponent<Tile>().CheckForJumps();
    //                    }
    //                    break;
    //            }

    //        }
    //    }
    //    else
    //    {
    //        for (int i = 0; i < 6; i++)
    //        {
    //            switch (i)
    //            {
    //                case 0:
    //                    if (gameManager.GetComponent<BoardSpawn>().GetMatris[row + 1, column].GetComponent<Tile>().CanMove() == false)
    //                    {
    //                        if (gameManager.GetComponent<BoardSpawn>().GetMatris[row + 2, column + 1].GetComponent<Tile>().CanMove() && gameManager.GetComponent<BoardSpawn>().GetMatris[row + 2, column + 1].GetComponent<Tile>().validMove == false)
    //                        {
    //                            gameManager.GetComponent<BoardSpawn>().GetMatris[row + 2, column + 1].GetComponent<Tile>().CanMove();
    //                            gameManager.GetComponent<BoardSpawn>().GetMatris[row + 2, column + 1].GetComponent<Tile>().CheckForJumps();
    //                        }
    //                    }
    //                    break;
    //                case 1:
    //                    if (gameManager.GetComponent<BoardSpawn>().GetMatris[row, column + 1].GetComponent<Tile>().CanMove())
    //                    {
    //                        gameManager.GetComponent<BoardSpawn>().GetMatris[row, column + 1].GetComponent<Tile>().CanMove();
    //                    }
    //                    else
    //                    {
    //                        gameManager.GetComponent<BoardSpawn>().GetMatris[row, column + 2].GetComponent<Tile>().CanMove();
    //                        gameManager.GetComponent<BoardSpawn>().GetMatris[row, column + 2].GetComponent<Tile>().CheckForJumps();
    //                    }
    //                    break;
    //                case 2:
    //                    if (gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column].GetComponent<Tile>().CanMove())
    //                    {
    //                        gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column].GetComponent<Tile>().CanMove();
    //                    }
    //                    else
    //                    {
    //                        gameManager.GetComponent<BoardSpawn>().GetMatris[row - 2, column + 1].GetComponent<Tile>().CanMove();
    //                        gameManager.GetComponent<BoardSpawn>().GetMatris[row - 2, column + 1].GetComponent<Tile>().CheckForJumps();
    //                    }
    //                    break;
    //                case 3:
    //                    if (gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column - 1].GetComponent<Tile>().CanMove())
    //                    {
    //                        gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column - 1].GetComponent<Tile>().CanMove();
    //                    }
    //                    else
    //                    {
    //                        gameManager.GetComponent<BoardSpawn>().GetMatris[row - 2, column - 1].GetComponent<Tile>().CanMove();
    //                        gameManager.GetComponent<BoardSpawn>().GetMatris[row - 2, column - 1].GetComponent<Tile>().CheckForJumps();
    //                    }
    //                    break;
    //                case 4:
    //                    if (gameManager.GetComponent<BoardSpawn>().GetMatris[row, column - 1].GetComponent<Tile>().CanMove() == false)
    //                    {
    //                        if (gameManager.GetComponent<BoardSpawn>().GetMatris[row, column - 2].GetComponent<Tile>().CanMove() && gameManager.GetComponent<BoardSpawn>().GetMatris[row, column - 2].GetComponent<Tile>().validMove == false)
    //                        {
    //                            gameManager.GetComponent<BoardSpawn>().GetMatris[row, column - 2].GetComponent<Tile>().CanMove();
    //                            gameManager.GetComponent<BoardSpawn>().GetMatris[row, column - 2].GetComponent<Tile>().CheckForJumps();
    //                        }
    //                    }
    //                    break;
    //                case 5:
    //                    if (gameManager.GetComponent<BoardSpawn>().GetMatris[row + 1, column - 1].GetComponent<Tile>().CanMove())
    //                    {
    //                        gameManager.GetComponent<BoardSpawn>().GetMatris[row + 1, column - 1].GetComponent<Tile>().CanMove();
    //                    }
    //                    else
    //                    {
    //                        gameManager.GetComponent<BoardSpawn>().GetMatris[row + 2, column - 1].GetComponent<Tile>().CanMove();
    //                        gameManager.GetComponent<BoardSpawn>().GetMatris[row + 2, column - 1].GetComponent<Tile>().CheckForJumps();
    //                    }
    //                    break;
    //            }

    //        }
    //    }

    //}

    public void CheckForValidMoves()
    {
        if(row%2 == 0)
        {
            for (int i = 0; i < 6; i++)
            {
                switch (i)
                {
                    case 0:
                        if (gameManager.GetComponent<BoardSpawn>().GetMatris[row + 1, column + 1].GetComponent<Tile>().CanMove())
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row + 1, column + 1].GetComponent<Tile>().MoveIsValid(true);
                        }
                        else
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row + 2, column + 1].GetComponent<Tile>().MoveIsValid(true);
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row + 2, column + 1].GetComponent<Tile>().CheckForJumps();
                        }
                        break;
                    case 1:
                        if (gameManager.GetComponent<BoardSpawn>().GetMatris[row, column + 1].GetComponent<Tile>().CanMove()){
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row, column + 1].GetComponent<Tile>().MoveIsValid(true);
                        }
                        else
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row, column + 2].GetComponent<Tile>().MoveIsValid(true);
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row, column + 2].GetComponent<Tile>().CheckForJumps();
                        }
                        break;
                    case 2:
                        if (gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column + 1].GetComponent<Tile>().CanMove()) {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column + 1].GetComponent<Tile>().MoveIsValid(true);
                        }
                        else
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row - 2, column + 1].GetComponent<Tile>().MoveIsValid(true);
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row - 2, column + 1].GetComponent<Tile>().CheckForJumps();
                        }
                        break;
                    case 3:
                        if (gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column].GetComponent<Tile>().CanMove()) {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column].GetComponent<Tile>().MoveIsValid(true);
                        }
                        else
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row - 2, column - 1].GetComponent<Tile>().MoveIsValid(true);
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row - 2, column - 1].GetComponent<Tile>().CheckForJumps();
                        }
                        break;
                    case 4:
                        if (gameManager.GetComponent<BoardSpawn>().GetMatris[row, column - 1].GetComponent<Tile>().CanMove()){
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row, column - 1].GetComponent<Tile>().MoveIsValid(true);
                        }
                        else
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row, column - 2].GetComponent<Tile>().MoveIsValid(true);
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row, column - 2].GetComponent<Tile>().CheckForJumps();
                        }
                        break;
                    case 5:
                        if (gameManager.GetComponent<BoardSpawn>().GetMatris[row + 1, column].GetComponent<Tile>().CanMove())  {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row + 1, column].GetComponent<Tile>().MoveIsValid(true);
                        }
                        else
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row + 2, column - 1].GetComponent<Tile>().MoveIsValid(true);
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row + 2, column - 1].GetComponent<Tile>().CheckForJumps();
                        }
                        break;
                }
               
            }
        }
        else
        {
            for (int i = 0; i < 6; i++)
            {
                switch (i)
                {
                    case 0:
                        if (gameManager.GetComponent<BoardSpawn>().GetMatris[row + 1, column].GetComponent<Tile>().CanMove())
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row + 1, column].GetComponent<Tile>().MoveIsValid(true);
                        }
                        else
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row + 2, column + 1].GetComponent<Tile>().MoveIsValid(true);
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row + 2, column + 1].GetComponent<Tile>().CheckForJumps();
                        }
                        break;
                    case 1:
                        if (gameManager.GetComponent<BoardSpawn>().GetMatris[row, column + 1].GetComponent<Tile>().CanMove())
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row, column + 1].GetComponent<Tile>().MoveIsValid(true);
                        }
                        else
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row, column + 2].GetComponent<Tile>().MoveIsValid(true);
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row, column + 2].GetComponent<Tile>().CheckForJumps();
                        }
                        break;
                    case 2:
                        if (gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column].GetComponent<Tile>().CanMove())
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column].GetComponent<Tile>().MoveIsValid(true);
                        }
                        else
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row - 2, column + 1].GetComponent<Tile>().MoveIsValid(true);
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row - 2, column + 1].GetComponent<Tile>().CheckForJumps();
                        }
                        break;
                    case 3:
                        if (gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column - 1].GetComponent<Tile>().CanMove())
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column - 1].GetComponent<Tile>().MoveIsValid(true);
                        }
                        else
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row - 2, column - 1].GetComponent<Tile>().MoveIsValid(true);
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row - 2, column - 1].GetComponent<Tile>().CheckForJumps();
                        }
                        break;
                    case 4:
                        if (gameManager.GetComponent<BoardSpawn>().GetMatris[row, column - 1].GetComponent<Tile>().CanMove())
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row, column - 1].GetComponent<Tile>().MoveIsValid(true);
                        }
                        else
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row, column - 2].GetComponent<Tile>().MoveIsValid(true);
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row, column - 2].GetComponent<Tile>().CheckForJumps();
                        }
                        break;
                    case 5:
                        if (gameManager.GetComponent<BoardSpawn>().GetMatris[row + 1, column - 1].GetComponent<Tile>().CanMove())
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row + 1, column - 1].GetComponent<Tile>().MoveIsValid(true);
                        }
                        else
                        {
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row + 2, column - 1].GetComponent<Tile>().MoveIsValid(true);
                            gameManager.GetComponent<BoardSpawn>().GetMatris[row + 2, column - 1].GetComponent<Tile>().CheckForJumps();
                        }
                        break;
                }

            }
        }

    }

   
    public void ResetTiles()
    {
        if (row % 2 == 0)
        {
            gameManager.GetComponent<BoardSpawn>().GetMatris[row + 1, column + 1].GetComponent<Tile>().Resets();
            gameManager.GetComponent<BoardSpawn>().GetMatris[row, column + 1].GetComponent<Tile>().Resets();
            gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column + 1].GetComponent<Tile>().Resets();
            gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column].GetComponent<Tile>().Resets();
            gameManager.GetComponent<BoardSpawn>().GetMatris[row, column - 1].GetComponent<Tile>().Resets();
            gameManager.GetComponent<BoardSpawn>().GetMatris[row + 1, column].GetComponent<Tile>().Resets();
        }
        else
        {
            gameManager.GetComponent<BoardSpawn>().GetMatris[row + 1, column].GetComponent<Tile>().Resets();
            gameManager.GetComponent<BoardSpawn>().GetMatris[row, column + 1].GetComponent<Tile>().Resets();
            gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column].GetComponent<Tile>().Resets();
            gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column - 1].GetComponent<Tile>().Resets();
            gameManager.GetComponent<BoardSpawn>().GetMatris[row, column - 1].GetComponent<Tile>().Resets();
            gameManager.GetComponent<BoardSpawn>().GetMatris[row + 1, column - 1].GetComponent<Tile>().Resets();
        }
    }

    public void MoveIsValid(bool valid)
    {
        if (valid)
        {
            Highlighted.SetActive(true);
            validMove = true;
        }
        else
        {
            Highlighted.SetActive(false);
            validMove = false;
           
        }
    }
}
