using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    [SerializeField] GameObject middlePart;

    [SerializeField] Material highlightedMaterial;
    [SerializeField] Material standardMaterial;
    [SerializeField] Transform raycastOrigin;

    public enum TileState { open, red, blue, yellow, green, purple, black };
    public TileState state;

    public GameObject ball;


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
    }

    public void Highlighted(bool isHighlighted)
    {
        if (isHighlighted)
        {
            middlePart.GetComponent<Renderer>().material = highlightedMaterial;
        }
        else
        {
            middlePart.GetComponent<Renderer>().material = standardMaterial;
        }
    }
}
