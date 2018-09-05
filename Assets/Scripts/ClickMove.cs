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

    //ignore layer 9 (Ball)
    int ignoreLayer = ~(1 << 9);


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
                this.destinationTile.GetComponent<Tile>().Highlighted(false);
            }

            destinationTile = null;
            ballTile = hit.collider.gameObject;
            this.ballTile.GetComponent<Tile>().Highlighted(true);

            holdingBall = true;
        }
        //if holding a ball, put down ball if tile is open
        if (holdingBall && tileHit.GetComponent<Tile>().state == Tile.TileState.open)
        {
            destinationTile = tileHit;
            this.ballTile.GetComponent<Tile>().Highlighted(false);
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

    void ResetSelectedBall()
    {
        if(ballTile != null)
        {
            ballTile.GetComponent<Tile>().Highlighted(false);
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
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, ignoreLayer))
                {
                    if (hit.collider.tag == "Tile")
                    {
                        //if you click the same tile as the ball is on
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
}
