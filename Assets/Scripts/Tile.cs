using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    [SerializeField] GameObject middlePart;

    [SerializeField] Material highlightedMaterial;
    [SerializeField] Material standardMaterial;
    [SerializeField] Transform raycastOrigin;

    public bool validMove;

    public enum TileState { open, red, blue, yellow, green, purple, black };
    public TileState state;

    public GameObject ball;

    public List<GameObject> neighbours = new List<GameObject>();

    void Start()
    {
        
        RaycastHit hit;

        if (Physics.Raycast(raycastOrigin.position, Vector3.down, out hit))
        {
            if (hit.collider.tag == "Ball")
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
        for (int i = 0; i < 6; i++)
        {
            if (neighbours[i] != null)
            {
                if (neighbours[i].GetComponent<Tile>().state == TileState.open)
                {
                    neighbours[i].GetComponent<Tile>().Highlighted(true);
                }
            }
        }
    }

    public void Highlighted(bool isHighlighted)
    {
        if (isHighlighted)
        {
            middlePart.GetComponent<Renderer>().material = highlightedMaterial;
            validMove = true;
        }
        else
        {
            for (int i = 0; i < 6; i++)
            {
                if (neighbours[i] != null)
                {
                    neighbours[i].GetComponent<Tile>().middlePart.GetComponent<Renderer>().material = standardMaterial;
                    neighbours[i].GetComponent<Tile>().validMove = false;
                    
                }
            }
            middlePart.GetComponent<Renderer>().material = standardMaterial;
            validMove = false;
        }
    }
}
