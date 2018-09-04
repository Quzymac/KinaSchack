using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    [SerializeField] GameObject middleDot;

    [SerializeField] Material highlightedMaterial;
    [SerializeField] Material standardMaterial;
    [SerializeField] Material ballMaterial;
    [SerializeField] Material noBallMaterial;



    public bool isHighlighted { get; set; }

    public enum TileState { open, red, blue, yellow, green, white, black };
    public TileState state;

    public GameObject ball;
    

    void Start () {

        state = TileState.open;
	}
	
	void Update () {

        //move from update, should only be called once when changes are made
        if (isHighlighted)
        {
            GetComponent<Renderer>().material = highlightedMaterial;
        }
        if(!isHighlighted)
        {
            GetComponent<Renderer>().material = standardMaterial;
        }

        //change to switch and move from update
        if (state != Tile.TileState.open)
        {
            
            middleDot.GetComponent<Renderer>().material = ballMaterial;
        }
        if(state == Tile.TileState.open)
        {
            ball = null;
            middleDot.GetComponent<Renderer>().material = noBallMaterial;
        }
    }
}
