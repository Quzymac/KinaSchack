﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool[] playersActive = new bool[6] { false, false, false, false, false, false };

    public int PlayerTurn { get; set; }

    Tile[,] matris = new Tile[rows, columns];
    
    List<Tile> validJumpList = new List<Tile>();

    List<Tile> playerOnePieces = new List<Tile>();
    List<Tile> playerFourPieces = new List<Tile>();

    public List<Tile> PlayerOnePieces { get { return playerOnePieces; } set { playerOnePieces = value; }  }
    public List<Tile> PlayerFourPieces { get { return playerFourPieces; } set { playerFourPieces = value; } }

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

    
    //add returns to methods if possible
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

    public void NorthEastMove(int row, int column, bool hasJumped)
    {
        if (row % 2 == 0)
        {
            if (TileIsValid(row + 1, column + 1) && !matris[row, column].validMove && !hasJumped)
            {
                validJumpList.Add(matris[row + 1, column + 1]);
            }

            else
            {
                if (TileIsValid(row + 2, column + 1) && matris[row + 1, column + 1].state != Tile.TileState.open && !validJumpList.Contains(matris[row + 2, column + 1]))
                {
                    validJumpList.Add(matris[row + 2, column + 1]);
                    CheckForValidMoves(matris[row + 2, column + 1].GetComponent<Tile>(), true);
                }
            }
        }
        else
        {
            if (TileIsValid(row + 1, column) && !matris[row, column].validMove && !hasJumped)
            {
                validJumpList.Add(matris[row + 1, column]);
            }
            else
            {
                if (TileIsValid(row + 2, column + 1) && matris[row + 1, column].state != Tile.TileState.open && !validJumpList.Contains(matris[row + 2, column + 1]))
                {
                    validJumpList.Add(matris[row + 2, column + 1]);
                    CheckForValidMoves(matris[row + 2, column + 1].GetComponent<Tile>(), true);
                }
            }
        }
    }
    public void EastMove(int row, int column, bool hasJumped)
    {
        if (TileIsValid(row, column + 1) && !matris[row, column].validMove && !hasJumped)
        {
            validJumpList.Add(matris[row, column + 1]);
        }
        else
        {
            if (TileIsValid(row, column + 2) && matris[row, column + 1].state != Tile.TileState.open && !validJumpList.Contains(matris[row, column + 2]))
            {
                validJumpList.Add(matris[row, column + 2]);
                CheckForValidMoves(matris[row, column + 2].GetComponent<Tile>(), true);
            }
        }
    }
    public void SouthEastMove(int row, int column, bool hasJumped)
    {
        if (row % 2 == 0)
        {
            if (TileIsValid(row - 1, column + 1) && !matris[row, column].validMove && !hasJumped)
            {
                validJumpList.Add(matris[row - 1, column + 1]);
            }
            else
            {
                if (TileIsValid(row - 2, column + 1) && matris[row - 1, column + 1].state != Tile.TileState.open && !validJumpList.Contains(matris[row - 2, column + 1]))
                {
                    validJumpList.Add(matris[row - 2, column + 1]);
                    CheckForValidMoves(matris[row - 2, column + 1].GetComponent<Tile>(), true);
                }
            }
        }
        else
        {
            if (TileIsValid(row - 1, column) && !matris[row, column].validMove && !hasJumped)
            {
                validJumpList.Add(matris[row - 1, column]);
            }
            else
            {
                if (TileIsValid(row - 2, column + 1) && matris[row - 1, column].state != Tile.TileState.open && !validJumpList.Contains(matris[row - 2, column + 1]))
                {
                    validJumpList.Add(matris[row - 2, column + 1]);
                    CheckForValidMoves(matris[row - 2, column + 1].GetComponent<Tile>(), true);
                }
            }
        }
    }
    public void SouthWestMove(int row, int column, bool hasJumped)
    {
        if (row % 2 == 0)
        {
            if (TileIsValid(row - 1, column) && !matris[row, column].validMove && !hasJumped)
            {
                validJumpList.Add(matris[row - 1, column]);
            }
            else
            {
                if (TileIsValid(row - 2, column - 1) && matris[row - 1, column].state != Tile.TileState.open && !validJumpList.Contains(matris[row - 2, column - 1]))
                {
                    validJumpList.Add(matris[row - 2, column - 1]);
                    CheckForValidMoves(matris[row - 2, column - 1].GetComponent<Tile>(), true);
                }
            }
        }
        else
        {
            if (TileIsValid(row - 1, column - 1) && !matris[row, column].validMove && !hasJumped)
            {
                validJumpList.Add(matris[row - 1, column - 1]);
            }
            else
            {
                if (TileIsValid(row - 2, column - 1) && matris[row - 1, column - 1].state != Tile.TileState.open && !validJumpList.Contains(matris[row - 2, column - 1]))
                {
                    validJumpList.Add(matris[row - 2, column - 1]);
                    CheckForValidMoves(matris[row - 2, column - 1].GetComponent<Tile>(), true);
                }
            }
        }
    }
    public void WestMove(int row, int column, bool hasJumped)
    {
        if (TileIsValid(row, column - 1) && !matris[row, column].validMove && !hasJumped)
        {
            validJumpList.Add(matris[row, column - 1]);
        }
        else
        {
            if (TileIsValid(row, column - 2) && matris[row, column - 1].state != Tile.TileState.open && !validJumpList.Contains(matris[row, column - 2]))
            {
                validJumpList.Add(matris[row, column - 2]);
                CheckForValidMoves(matris[row, column - 2].GetComponent<Tile>(), true);
            }
        }
    }
    public void NorthWestMove(int row, int column, bool hasJumped)
    {
        if (row % 2 == 0)
        {
            if (TileIsValid(row + 1, column) && !matris[row, column].validMove && !hasJumped)
            {
                validJumpList.Add(matris[row + 1, column]);
            }
            else
            {
                if (TileIsValid(row + 2, column - 1) && matris[row + 1, column].state != Tile.TileState.open && !validJumpList.Contains(matris[row + 1, column - 1]))
                {
                    validJumpList.Add(matris[row + 2, column - 1]);
                    CheckForValidMoves(matris[row + 2, column - 1].GetComponent<Tile>(), true);
                }
            }
        }
        else
        {
            if (TileIsValid(row + 1, column - 1) && !matris[row, column].validMove && !hasJumped)
            {
                validJumpList.Add(matris[row + 1, column - 1]);
            }
            else
            {
                if (TileIsValid(row + 2, column - 1) && matris[row + 1, column - 1].state != Tile.TileState.open && !validJumpList.Contains(matris[row + 2, column - 1]))
                {
                    validJumpList.Add(matris[row + 2, column - 1]);
                    CheckForValidMoves(matris[row + 2, column - 1].GetComponent<Tile>(), true);
                }
            }
        }
    }

    void CheckForValidMoves(Tile tile, bool jumped)
    {
        int row = tile.row;
        int column = tile.column;
        
        NorthEastMove(row, column, jumped);
        EastMove(row, column, jumped);
        SouthEastMove(row, column, jumped);
        SouthWestMove(row, column, jumped);
        WestMove(row, column, jumped);
        NorthWestMove(row, column, jumped);

        foreach (Tile jump in validJumpList)
        {
            jump.MoveIsValid(true);
        }

    }

    void ClickTile(Tile tileHit)
    {
        //pick up red ball if you're not already holding a ball
        if (!HolingBall && tileHit.state == (Tile.TileState)PlayerTurn + 2)
        {
            ballTile = tileHit;

            CheckForValidMoves(ballTile, false);
            HolingBall = true;
        }
        //if holding a "ball", put down ball if tile is open and a valid move
        if (HolingBall && tileHit.state == Tile.TileState.open && tileHit.validMove)
        {
            destinationTile = tileHit;
            ResetSelectedBall();



            //set state and color of tiles
            destinationTile.SetState(PlayerTurn + 2);
            ballTile.SetState(1);

            //resets varibles
            if (ballTile != null)
            {
                ballTile = null;
                HolingBall = false;
            }

            //moves to next player
            PlayerTurn++;
            if (PlayerTurn == 6)
            {
                PlayerTurn = 0;
            }
            //if next player is inactive move to next
            if (!playersActive[PlayerTurn])
            {
                PlayerTurn++;
                if (PlayerTurn == 6)
                {
                    PlayerTurn = 0;
                }
                //if next player is also inactive move to next, 
                if (!playersActive[PlayerTurn])
                {
                    PlayerTurn++;
                    if (PlayerTurn == 6)
                    {
                        PlayerTurn = 0;
                    }
                }
            }
            if (PlayerTurn == 6)
            {
                PlayerTurn = 0;
            }
        }
    }

    //resets validJumpList list and highlighting
    void ResetSelectedBall()
    {
        foreach (Tile jump in validJumpList)
        {
            jump.MoveIsValid(false);
        }
        validJumpList.Clear();
    }

    //move with mouse clicks
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //Vector3 ray = cam.ScreenToWorldPoint(Input.mousePosition); //ger en position i världen(vector3) översätt detta till object på denna position, all information finns eftersom inget flyttar på sig.

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
