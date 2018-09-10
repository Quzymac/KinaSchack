using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMove : MonoBehaviour
{


    [SerializeField] Camera cam;
    RaycastHit hit;

    public bool holdingBall { get; set; }
    [SerializeField] GameObject ballTile;
    [SerializeField] GameObject destinationTile;
    GameObject ball;

    [SerializeField] LayerMask mask;


    // Use this for initialization
    void Start()
    {

    }
   

    void ClickTile(GameObject tileHit)
    {

        //pick up red ball if you're not already holding a ball
        if (!holdingBall && tileHit.GetComponent<Tile>().state == Tile.TileState.red)
        {
            if (destinationTile != null)
            {
                this.destinationTile.GetComponent<Tile>().MoveIsValid(false);
            }

            destinationTile = null;
            ballTile = hit.collider.gameObject;
            this.ballTile.GetComponent<Tile>().MoveIsValid(true);
            ballTile.GetComponent<Tile>().CheckForMoves();

            holdingBall = true;

        }
        //if holding a ball, put down ball if tile is open
        if (holdingBall && tileHit.GetComponent<Tile>().state == Tile.TileState.open && tileHit.GetComponent<Tile>().validMove)
        {
            destinationTile = tileHit;

            ballTile.GetComponent<Tile>().ball.transform.position = destinationTile.transform.position;
            destinationTile.GetComponent<Tile>().ball = ballTile.GetComponent<Tile>().ball;
            ballTile.GetComponent<Tile>().ball = null;

            this.destinationTile.GetComponent<Tile>().state = Tile.TileState.red;
            this.ballTile.GetComponent<Tile>().state = Tile.TileState.open;

            ResetSelectedBall();

        }
       
    }

    void ResetSelectedBall()
    {
        if(ballTile != null)
        {
            ballTile.GetComponent<Tile>().ResetTiles();
            ballTile.GetComponent<Tile>().MoveIsValid(false);
            ballTile = null;
            holdingBall = false;
        }
        
    }

    //make new methods
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
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
                        ClickTile(hit.collider.gameObject);
                    }
                }
                //resets if you click outside the game board
                else
                {
                    ResetSelectedBall();
                } 
                
            }
        }
    }
}
