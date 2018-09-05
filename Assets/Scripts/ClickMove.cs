using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMove : MonoBehaviour
{


    [SerializeField] Camera cam;
    public bool holdingBall { get; set; }
    [SerializeField] GameObject ballTile;
    [SerializeField] GameObject destinationTile;
    GameObject ball;

    //ignore layer 9 (Ball)
    int ignoreLayer = ~(1 << 9);


    // Use this for initialization
    void Start()
    {

    }
    public GameObject GetBallTile()
    {
        return ballTile;
    }
    public GameObject GetDestinationTile()
    {
        return destinationTile;
    }

    //make new methods
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, ignoreLayer))
                {
                    if (hit.collider.tag == "Tile")
                    {
                        //pick up red ball if you're not already holding a ball
                        if (!holdingBall && hit.collider.gameObject.GetComponent<Tile>().state == Tile.TileState.red)
                        {
                            if (destinationTile != null)
                            {
                                this.destinationTile.GetComponent<Tile>().isHighlighted = false;
                            }

                            destinationTile = null;
                            ballTile = hit.collider.gameObject;
                            this.ballTile.GetComponent<Tile>().isHighlighted = true;

                            holdingBall = true;
                        }
                        //put down ball if tile is open, 
                        if (holdingBall && hit.collider.gameObject.GetComponent<Tile>().state == Tile.TileState.open)  
                        {
                            destinationTile = hit.collider.gameObject;
                            this.ballTile.GetComponent<Tile>().isHighlighted = false;
                            //this.destinationTile.GetComponent<Tile>().isHighlighted = true;

                            ballTile.GetComponent<Tile>().ball.transform.position = destinationTile.transform.position;
                            destinationTile.GetComponent<Tile>().ball = ballTile.GetComponent<Tile>().ball;
                            ballTile.GetComponent<Tile>().ball = null;

                            this.destinationTile.GetComponent<Tile>().state = Tile.TileState.red;
                            this.ballTile.GetComponent<Tile>().state = Tile.TileState.open;

                            ballTile = null;
                            holdingBall = false;
                        }
                    }
                    //resets if you click outside the game board
                    else
                    {
                        if(ballTile != null)
                        {
                            this.ballTile.GetComponent<Tile>().isHighlighted = false;

                        }
                        if(destinationTile != null)
                        {
                            this.destinationTile.GetComponent<Tile>().isHighlighted = false;

                        }
                        ballTile = null;
                        holdingBall = false;
                        destinationTile = null;
                    }
                }
            }
        }
    }
}
