using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMove : MonoBehaviour
{


    [SerializeField] Camera cam;
    RaycastHit hit;

    public bool HolingBall { get; set; }
    [SerializeField] GameObject ballTile;
    [SerializeField] GameObject destinationTile;

    [SerializeField] LayerMask mask;

    [SerializeField] Material red;

    

    void ClickTile(GameObject tileHit)
    {

        //pick up red ball if you're not already holding a ball
        if (!HolingBall && tileHit.GetComponent<Tile>().state == Tile.TileState.red)
        {
            ballTile = hit.collider.gameObject;
            ballTile.GetComponent<Tile>().CheckForMoves();
            HolingBall = true;
        }
        //if holding a ball, put down ball if tile is open
        if (HolingBall && tileHit.GetComponent<Tile>().state == Tile.TileState.open && tileHit.GetComponent<Tile>().validMove)
        {
            destinationTile = tileHit;

            //set state of tiles
            destinationTile.GetComponent<Tile>().state = Tile.TileState.red;
            ballTile.GetComponent<Tile>().state = Tile.TileState.open;

            //set color of tiles
            ballTile.GetComponent<Renderer>().material = ballTile.GetComponent<Tile>().standardMaterial;
            destinationTile.GetComponent<Renderer>().material = red;
            
            //moves physical ball -- remove later
            //ballTile.GetComponent<Tile>().ball.transform.position = destinationTile.transform.position;
            //destinationTile.GetComponent<Tile>().ball = ballTile.GetComponent<Tile>().ball;
            //ballTile.GetComponent<Tile>().ball = null;

            ResetSelectedBall(); 
        }
    }

    //resets varibles, canJumpTo list and highlighting
    void ResetSelectedBall()
    {
        if(ballTile != null)
        {
            ballTile.GetComponent<Tile>().ResetTiles();
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
