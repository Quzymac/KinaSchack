using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {


    [SerializeField] BoardController boardController;

    public Material standardMaterial { get; set; }
    [SerializeField] Transform raycastOrigin;

    [SerializeField] LayerMask ballMask;

    public bool validMove { get; set; }

    public enum TileState { invalid, open, red, blue, yellow, green, purple, orange };
    public TileState state { get; set; }

    public int row { get; set; }
    public int column { get; set; }

    [SerializeField] Material red;
    [SerializeField] Material highlighted;




    public void MoveIsValid(bool valid)
    {
        if (valid)
        {
            GetComponent<Renderer>().material = highlighted;
            validMove = true;
        }
        else
        {
            GetComponent<Renderer>().material = standardMaterial;
            validMove = false;

        }
    }

    void Start()
    {
        boardController = FindObjectOfType<BoardController>();

        standardMaterial = GetComponent<Renderer>().material;

        //state = TileState.invalid;


        // old code
        //RaycastHit hit;
        //if (Physics.Raycast(raycastOrigin.position, Vector3.down, out hit, Mathf.Infinity, ballMask))
        //{
        //    ball = hit.collider.gameObject;

        //    switch (hit.collider.gameObject.GetComponent<PlayerBall>().ballTeam.ToString())
        //    {
        //        case "red":
        //            state = TileState.red;
        //            GetComponent<Renderer>().material = red;

        //            break;
        //        case "blue":
        //            state = TileState.blue;
        //            break;
        //        case "yellow":
        //            state = TileState.yellow;
        //            break;
        //        case "green":
        //            state = TileState.green;
        //            break;
        //        case "purple":
        //            state = TileState.purple;
        //            break;
        //        case "orange":
        //            state = TileState.orange;
        //            break;
        //    }
        //}
        //else
        //{
        //    state = TileState.open;
        //}
    }
    
}
