using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    [SerializeField] GameObject middlePart;

    [SerializeField] Material highlightedMaterial;
    Material standardMaterial;
    [SerializeField] Transform raycastOrigin;

    [SerializeField] LayerMask ballMask;

    public bool validMove;

    public enum TileState { open, red, blue, yellow, green, purple, black };
    public TileState state { get; set; }

    public GameObject ball;

    public List<GameObject> neighbours = new List<GameObject>();


    void Start()
    {
        standardMaterial = middlePart.GetComponent<Renderer>().material;

        
        RaycastHit hit;

        if (Physics.Raycast(raycastOrigin.position, Vector3.down, out hit, Mathf.Infinity, ballMask))
        {
            ball = hit.collider.gameObject;

            switch (hit.collider.gameObject.GetComponent<PlayerBall>().ballTeam.ToString())
            {
                case "red":
                    state = TileState.red;
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
                case "black":
                    state = TileState.black;
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
        GetComponentInChildren<SetNeighbours>().CheckNeighbours();
        for (int i = 0; i < neighbours.Count; i++)
        {
            if (neighbours[i] != null)
            {
                if (neighbours[i].GetComponent<Tile>().state == TileState.open)
                {
                    neighbours[i].GetComponent<Tile>().MoveIsValid(true);
                }
                else
                {
                    //add tile to list if you can jump over a ball to it
                    if (neighbours[i].GetComponent<Tile>().CheckIfJumpIsValid(i) != null)
                    {
                        neighbours.Add(neighbours[i].GetComponent<Tile>().CheckIfJumpIsValid(i));
                    }
                    
                }
            }
        }
    }
    public GameObject CheckIfJumpIsValid(int direction)
    {
        if(neighbours[direction] != null && neighbours[direction].GetComponent<Tile>().state == TileState.open)
        {
            neighbours[direction].GetComponent<Tile>().MoveIsValid(true);
    
            return neighbours[direction];
        }
        else
        {
            return null;
        }
    }

    public void ResetTiles()
    {
        for (int i = 0; i < neighbours.Count; i++)
        {
            if(neighbours[i] != null)
            {
                neighbours[i].GetComponent<Tile>().MoveIsValid(false);
            }
        }
        neighbours.Clear();
    }


    public void MoveIsValid(bool valid)
    {
        if (valid)
        {
            middlePart.GetComponent<Renderer>().material = highlightedMaterial;
            validMove = true;
        }
        else
        {
            middlePart.GetComponent<Renderer>().material = standardMaterial;
            validMove = false;
        }
    }
}
