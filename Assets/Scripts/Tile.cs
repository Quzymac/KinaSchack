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

    public enum TileState { open, red, blue, yellow, green, purple, orange };
    public TileState state { get; set; }

    public GameObject ball { get; set; }

    public List<GameObject> neighbours = new List<GameObject>();

    List<GameObject> canJumpTo = new List<GameObject>();

    public int row { get; set; }
    public int column { get; set; }

    [SerializeField] Material red;

    IEnumerator addNeighbours()
    {
        yield return new WaitForSeconds(1f);
        neighbours.Clear();


        if (row < 14 && row > 5 && column < 10 && column > 2)
            {
            if (row % 2 == 0)
            {
                
                neighbours.Add(gameManager.GetComponent<BoardSpawn>().GetMatris[row + 1, column + 1]);
                neighbours.Add(gameManager.GetComponent<BoardSpawn>().GetMatris[row, column + 1]);
                neighbours.Add(gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column + 1]);
                neighbours.Add(gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column]);
                neighbours.Add(gameManager.GetComponent<BoardSpawn>().GetMatris[row, column - 1]);
                neighbours.Add(gameManager.GetComponent<BoardSpawn>().GetMatris[row + 1, column]);
            }
            else
            {
                neighbours.Add(gameManager.GetComponent<BoardSpawn>().GetMatris[row + 1, column]);
                neighbours.Add(gameManager.GetComponent<BoardSpawn>().GetMatris[row, column + 1]);
                neighbours.Add(gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column]);
                neighbours.Add(gameManager.GetComponent<BoardSpawn>().GetMatris[row - 1, column - 1]);
                neighbours.Add(gameManager.GetComponent<BoardSpawn>().GetMatris[row, column - 1]);
                neighbours.Add(gameManager.GetComponent<BoardSpawn>().GetMatris[row + 1, column - 1]);
            }
        } 
        

    }



    void Start()
    {

        //StartCoroutine(addNeighbours());


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
        GetComponentInChildren<SetNeighbours>().CheckNeighbours();
    }

    public void CheckForMoves()
    {
        canJumpTo.Clear();
        foreach (var neighbour in neighbours)
        {
            canJumpTo.Add(neighbour);
        }


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
