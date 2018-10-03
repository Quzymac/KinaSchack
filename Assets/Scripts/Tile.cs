using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    [SerializeField] LayerMask ballMask;

    public bool validMove { get; set; }

    public enum TileState { invalid, open, red, brown, yellow, blue, green, purple };
    public TileState state { get; set; }

    public int row { get; set; }
    public int column { get; set; }

    //set in inspector, represents the same as Tilestate, invalid=highlighted
    public Material[] materials = new Material[8];
    
   
    public void SetState(int setState)
    {
        state = (TileState)setState;
        GetComponent<Renderer>().material = materials[setState];
    }


    public void MoveIsValid(bool valid)
    {
        if (valid)
        {
            GetComponent<Renderer>().material = materials[0];

            validMove = true;
        }
        else
        {
            GetComponent<Renderer>().material = materials[1];

            validMove = false;
        }
    }
}
