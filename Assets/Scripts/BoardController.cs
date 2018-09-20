using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{


    [SerializeField] Camera cam;
    RaycastHit hit;

    public bool HolingBall { get; set; }
    [SerializeField] Tile ballTile;
    [SerializeField] Tile destinationTile;

    const int rows = 17;
    const int columns = 13;

    Tile[,] matris = new Tile[rows, columns];

    [SerializeField] LayerMask mask;

    [SerializeField] Material red;

    List<Tile> validJumpList = new List<Tile>();
    List<Tile> previouslyTraveled = new List<Tile>();

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


    public void NorthEastMove(int row, int column, bool hasJumped)
    {
        if (row % 2 == 0)
        {
            if (!hasJumped)
            {
                if (matris[row + 1, column + 1].state == Tile.TileState.open &&
                                            !matris[row, column].validMove)
                {
                    validJumpList.Add(matris[row + 1, column + 1]);
                }
            }
            else
            {
                if (matris[row, column].state == Tile.TileState.open && matris[row + 1, column + 1].state == Tile.TileState.open)
                {
                    return;
                }
                if (matris[row + 2, column + 1].state == Tile.TileState.open &&
                    matris[row + 1, column + 1].state != Tile.TileState.open && !validJumpList.Contains(matris[row + 2, column + 1]))
                {
                    validJumpList.Add(matris[row + 2, column + 1]);
                    CheckForValidMoves(matris[row + 2, column + 1].GetComponent<Tile>(), true);
                }
            }
        }
        else
        {
            if (!hasJumped)
            {
                if (matris[row + 1, column].state == Tile.TileState.open &&
                                !matris[row, column].validMove)
                {
                    validJumpList.Add(matris[row + 1, column]);
                    previouslyTraveled.Add(matris[row + 1, column]);
                }
            }
            else
            {
                if (matris[row, column].state == Tile.TileState.open && matris[row + 1, column].state == Tile.TileState.open)
                {
                    return;
                }
                if (matris[row + 2, column + 1].state == Tile.TileState.open &&
                    matris[row + 1, column].state != Tile.TileState.open && !validJumpList.Contains(matris[row + 2, column + 1]))
                {
                    validJumpList.Add(matris[row + 2, column + 1]);
                    CheckForValidMoves(matris[row + 2, column + 1].GetComponent<Tile>(), true);
                }
            }
        }
    }
    public void EastMove(int row, int column, bool hasJumped)
    {
        if (matris[row, column + 1].state == Tile.TileState.open &&
                        !matris[row, column].validMove && !hasJumped)
        {
            validJumpList.Add(matris[row, column + 1]);
        }
        else
        {
            if (matris[row, column].state == Tile.TileState.open && matris[row, column + 1].state == Tile.TileState.open)
            {
                return;
            }
            if (matris[row, column + 2].state == Tile.TileState.open &&
                matris[row, column + 1].state != Tile.TileState.open && !validJumpList.Contains(matris[row, column + 2]))
            {
                validJumpList.Add(matris[row, column + 2]);
                CheckForValidMoves(matris[row, column + 2].GetComponent<Tile>(), true);
            }
            //EastMove(row, column + 1);
        }
    }
    public void SouthEastMove(int row, int column, bool hasJumped)
    {
        if (row % 2 == 0)
        {
            if (matris[row - 1, column + 1].state == Tile.TileState.open &&
                            !matris[row, column].validMove && !hasJumped)
            {
                validJumpList.Add(matris[row - 1, column + 1]);
                previouslyTraveled.Add(matris[row + 1, column + 1]);
            }
            else
            {
                if (matris[row, column].state == Tile.TileState.open && matris[row - 1, column + 1].state == Tile.TileState.open)
                {
                    return;
                }
                if (matris[row - 2, column + 1].state == Tile.TileState.open &&
                    matris[row - 1, column + 1].state != Tile.TileState.open && !validJumpList.Contains(matris[row - 2, column + 1]))
                {
                    validJumpList.Add(matris[row - 2, column + 1]);
                    CheckForValidMoves(matris[row - 2, column + 1].GetComponent<Tile>(), true);
                }
            }
        }
        else
        {
            if (matris[row - 1, column].state == Tile.TileState.open &&
                            !matris[row, column].validMove && !hasJumped)
            {
                validJumpList.Add(matris[row - 1, column]);
                previouslyTraveled.Add(matris[row + 1, column + 1]);
            }
            else
            {
                if (matris[row, column].state == Tile.TileState.open && matris[row- 1, column + 1].state == Tile.TileState.open)
                {
                    return;
                }
                if (matris[row - 2, column + 1].state == Tile.TileState.open &&
                    matris[row - 1, column].state != Tile.TileState.open && !validJumpList.Contains(matris[row - 2, column + 1]))
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
            if (matris[row - 1, column].state == Tile.TileState.open &&
                            !matris[row, column].validMove && !hasJumped)
            {
                validJumpList.Add(matris[row - 1, column]);
                previouslyTraveled.Add(matris[row + 1, column + 1]);
            }
            else
            {
                if (matris[row - 2, column - 1].state == Tile.TileState.open &&
                    matris[row - 1, column].state != Tile.TileState.open && !validJumpList.Contains(matris[row - 2, column - 1]))
                {
                    validJumpList.Add(matris[row - 2, column - 1]);
                    CheckForValidMoves(matris[row - 2, column - 1].GetComponent<Tile>(), true);
                }
            }
        }
        else
        {
            if (matris[row - 1, column - 1].state == Tile.TileState.open &&
                            !matris[row, column].validMove && !hasJumped)
            {
                validJumpList.Add(matris[row - 1, column - 1]);
                previouslyTraveled.Add(matris[row + 1, column + 1]);
            }
            else
            {
                if (matris[row - 2, column - 1].state == Tile.TileState.open &&
                    matris[row - 1, column - 1].state != Tile.TileState.open && !validJumpList.Contains(matris[row - 2, column - 1]))
                {
                    validJumpList.Add(matris[row - 2, column - 1]);
                    CheckForValidMoves(matris[row - 2, column - 1].GetComponent<Tile>(), true);
                }
            }
        }
    }
    public void WestMove(int row, int column, bool hasJumped)
    {
        if (matris[row, column - 1].state == Tile.TileState.open &&
                        !matris[row, column].validMove && !hasJumped)
        {
            validJumpList.Add(matris[row, column - 1]);
            previouslyTraveled.Add(matris[row + 1, column + 1]);
        }
        else
        {
            if (matris[row, column].state == Tile.TileState.open && matris[row, column - 1].state == Tile.TileState.open)
            {
                return;
            }
            if (matris[row, column - 2].state == Tile.TileState.open &&
                matris[row, column - 1].state != Tile.TileState.open && !validJumpList.Contains(matris[row, column - 2]))
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
            if (matris[row + 1, column].state == Tile.TileState.open &&
                            !matris[row, column].validMove && !hasJumped)
            {
                validJumpList.Add(matris[row + 1, column]);
                previouslyTraveled.Add(matris[row + 1, column + 1]);
            }
            else
            {
                if (matris[row + 2, column - 1].state == Tile.TileState.open &&
                    matris[row + 1, column].state != Tile.TileState.open && !validJumpList.Contains(matris[row + 2, column - 1]))
                {
                    validJumpList.Add(matris[row + 2, column - 1]);
                    CheckForValidMoves(matris[row + 2, column - 1].GetComponent<Tile>(), true);
                }
            }
        }
        else
        {
            if (matris[row + 1, column - 1].state == Tile.TileState.open &&
                            !matris[row, column].validMove && !hasJumped)
            {
                validJumpList.Add(matris[row + 1, column - 1]);
                previouslyTraveled.Add(matris[row + 1, column + 1]);
            }
            else
            {
                if (matris[row + 2, column - 1].state == Tile.TileState.open &&
                    matris[row + 1, column - 1].state != Tile.TileState.open && !validJumpList.Contains(matris[row + 2, column - 1]))
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

        previouslyTraveled.Add(tile);

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
        if (!HolingBall && tileHit.state == Tile.TileState.red)
        {
            ballTile = hit.collider.gameObject.GetComponent<Tile>();

            CheckForValidMoves(ballTile, false);
            //ballTile.GetComponent<Tile>().CheckForValidMoves();
            HolingBall = true;
        }
        //if holding a ball, put down ball if tile is open
        if (HolingBall && tileHit.state == Tile.TileState.open && tileHit.validMove)
        {
            destinationTile = tileHit;

            //set state of tiles
            destinationTile.state = Tile.TileState.red;
            ballTile.state = Tile.TileState.open;

            //set color of tiles
            ballTile.GetComponent<Renderer>().material = ballTile.standardMaterial;
            destinationTile.GetComponent<Renderer>().material = red;

            //moves physical ball -- remove later
            //ballTile.GetComponent<Tile>().ball.transform.position = destinationTile.transform.position;
            //destinationTile.GetComponent<Tile>().ball = ballTile.GetComponent<Tile>().ball;
            //ballTile.GetComponent<Tile>().ball = null;


            foreach (Tile jump in validJumpList)
            {
                jump.MoveIsValid(false);
            }
            validJumpList.Clear();
            previouslyTraveled.Clear();

            ResetSelectedBall();
        }
    }

    //resets varibles, canJumpTo list and highlighting
    void ResetSelectedBall()
    {
        if (ballTile != null)
        {
            ballTile.ResetTiles();
            ballTile = null;
            HolingBall = false;
        }
    }

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
                    if (hit.collider.gameObject.Equals(ballTile))
                    {
                        ResetSelectedBall();
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
                        hit.collider.gameObject.GetComponent<Tile>().state = Tile.TileState.red;
                        hit.collider.gameObject.GetComponent<Renderer>().material = red;

                    }
                }
            }
        }
    }

}
