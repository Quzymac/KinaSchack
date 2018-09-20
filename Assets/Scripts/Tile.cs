using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {


    [SerializeField] BoardController boardController;
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

    void Start()
    {
        boardController = FindObjectOfType<BoardController>();

        standardMaterial = GetComponent<Renderer>().material;

        state = TileState.open;


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
    

    public void Resets()
    {
        if (validMove)
        {
            MoveIsValid(false);
        }
    }
    public bool CanMove()
    {
        if(state == TileState.open)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanJump()
    {
        if(state == TileState.red)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

   //old code

    public void ResetTiles()
    {
        if (row % 2 == 0)
        {
            boardController.Matris[row + 1, column + 1].GetComponent<Tile>().Resets();
            boardController.Matris[row, column + 1].GetComponent<Tile>().Resets();
            boardController.Matris[row - 1, column + 1].GetComponent<Tile>().Resets();
            boardController.Matris[row - 1, column].GetComponent<Tile>().Resets();
            boardController.Matris[row, column - 1].GetComponent<Tile>().Resets();
            boardController.Matris[row + 1, column].GetComponent<Tile>().Resets();
        }
        else
        {
            boardController.Matris[row + 1, column].GetComponent<Tile>().Resets();
            boardController.Matris[row, column + 1].GetComponent<Tile>().Resets();
            boardController.Matris[row - 1, column].GetComponent<Tile>().Resets();
            boardController.Matris[row - 1, column - 1].GetComponent<Tile>().Resets();
            boardController.Matris[row, column - 1].GetComponent<Tile>().Resets();
            boardController.Matris[row + 1, column - 1].GetComponent<Tile>().Resets();
        }
    }
}
