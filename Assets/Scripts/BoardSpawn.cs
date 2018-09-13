using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSpawn : MonoBehaviour {

    const int rows = 17;
    const int columns = 13;

    GameObject[,] matris = new GameObject[rows, columns];


    //Tile[,] allTiles = new Tile[rows, columns];

    [SerializeField] GameObject tilePrefab;
    GameObject newObj;

    public GameObject[,] GetMatris
    {
        get { return matris; }
        set { matris = value; }
    }


    // Use this for initialization
    void Start () {

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if(i%2 == 0)
                {
                    newObj = Instantiate(tilePrefab, new Vector3(0.865f +j * 1.73f, 0, i * 1.5f), transform.rotation);

                }
                else
                {
                    newObj = Instantiate(tilePrefab, new Vector3(j * 1.73f, 0, i * 1.5f), transform.rotation);
                }
                newObj.GetComponent<Tile>().row = i;
                newObj.GetComponent<Tile>().column = j;
                //------------tilldela tileobjektet värden för dess egen position i matrisen(lägg till variabler för detta i Tile scriptet)
                matris[i, j] = newObj;
            }

        }

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print(matris[1, 1].transform.position);
        }
		
	}
}
