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
        //GetComponentInChildren<SetNeighbours>().CheckNeighbours();
    }

    void FIndNeighbours()
    {
        if (row % 2 == 0)
        {

            canJumpTo.Add(gameManager.GetComponent<BoardSpawn>().GetMatris[row + 1, column + 1]);
            canJumpTo.Add(gameManager.GetComponent<BoardSpawn>().GetMatris[row, column + 1]);
            canJumpTo.Add(gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column + 1]);
            canJumpTo.Add(gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column]);
            canJumpTo.Add(gameManager.GetComponent<BoardSpawn>().GetMatris[row, column - 1]);
            canJumpTo.Add(gameManager.GetComponent<BoardSpawn>().GetMatris[row + 1, column]);
        }
        else
        {
            canJumpTo.Add(gameManager.GetComponent<BoardSpawn>().GetMatris[row + 1, column]);
            canJumpTo.Add(gameManager.GetComponent<BoardSpawn>().GetMatris[row, column + 1]);
            canJumpTo.Add(gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column]);
            canJumpTo.Add(gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column - 1]);
            canJumpTo.Add(gameManager.GetComponent<BoardSpawn>().GetMatris[row, column - 1]);
            canJumpTo.Add(gameManager.GetComponent<BoardSpawn>().GetMatris[row + 1, column - 1]);
        }
    }


    public void CheckForMoves()
    {
        canJumpTo.Clear();

        FIndNeighbours();

/*
        //gör om till metod? kalla på den igen på objektet som läggs till i listan "canJumpTo"
        for (int i = 0; i < neighbours.Count; i++)
        {
            if(canJumpTo[i] != null && canJumpTo[i].GetComponent<Tile>().neighbours[i] != null)
            {
                if (canJumpTo[i].GetComponent<Tile>().state != TileState.open && canJumpTo[i].GetComponent<Tile>().neighbours[i].GetComponent<Tile>().state == TileState.open)
                {
                    canJumpTo.Add(canJumpTo[i].GetComponent<Tile>().neighbours[i]);
                }
            }
            
        }
        */


        //GetComponentInChildren<SetNeighbours>().CheckNeighbours();
        

        for (int i = 0; i < canJumpTo.Count; i++)
        {
            if (canJumpTo[i] != null)
            {
                if (canJumpTo[i].GetComponent<Tile>().state == TileState.open)
                {
                    canJumpTo[i].GetComponent<Tile>().MoveIsValid(true);
                }
                else
                {
                    //add tile to list if you can jump over a ball to it
                    //canJumpTo[i].GetComponentInChildren<SetNeighbours>().CheckNeighbours();

                    //canJumpTo[i].GetComponent<Tile>().CheckIfJumpIsValid();



                    /*
                    if (canJumpTo[i].GetComponent<Tile>().neighbours[i].CheckIfJumpIsValid() != null)
                    {
                        canJumpTo.Add(canJumpTo[i].GetComponent<Tile>().neighbours[i].CheckIfJumpIsValid(i));
                        
                    }
                    */
                }
            }
        }
    }
    public GameObject CheckIfJumpIsValid()
    {
        for (int i = 0; i < neighbours.Count; i++)
        {
            if (neighbours[i].GetComponent<Tile>().state == TileState.open)
            {
                neighbours[i].GetComponent<Tile>().MoveIsValid(true);

                canJumpTo.Add(neighbours[i]);

                return neighbours[i];
            }
        }

        return null;
    }

    public void ResetTiles()
    {
        for (int i = 0; i < canJumpTo.Count; i++)
        {
            if(canJumpTo[i] != null)
            {
                canJumpTo[i].GetComponent<Tile>().MoveIsValid(false);
            }
        }
        canJumpTo.Clear();
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
