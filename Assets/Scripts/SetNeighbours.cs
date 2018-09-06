using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNeighbours : MonoBehaviour
{


    //ignore layer 9 (Ball)
    //int ignoreLayer = ~(1 << 9);
    [SerializeField] LayerMask mask;

    RaycastHit hit;

    //list of neighbouring positions (2 = occupied, 1 = open, 0 = no neighbour)
    //public List<GameObject> neighbours = new List<GameObject>();


    // Use this for initialization
    void Start()
    {

    }

    public void CheckNeighbours()
    {
         GetComponentInParent<Tile>().neighbours.Clear();

        for (int i = 0; i < 6; i++)
        {
            //raycasts six times to find the surounding tiles
            switch (i)
            {
                case 0:
                    Physics.Raycast(transform.position, (Vector3.forward + Vector3.down + Vector3.right).normalized, out hit, Mathf.Infinity, mask);
                    break;
                case 1:
                    Physics.Raycast(transform.position, (Vector3.down + Vector3.right).normalized, out hit, Mathf.Infinity, mask);
                    break;
                case 2:
                    Physics.Raycast(transform.position, (Vector3.back + Vector3.down + Vector3.right).normalized, out hit, Mathf.Infinity, mask);
                    break;
                case 3:
                    Physics.Raycast(transform.position, (Vector3.back + Vector3.down + Vector3.left).normalized, out hit, Mathf.Infinity, mask);
                    break;
                case 4:
                    Physics.Raycast(transform.position, (Vector3.down + Vector3.left).normalized, out hit, Mathf.Infinity, mask);
                    break;
                case 5:
                    Physics.Raycast(transform.position, (Vector3.forward + Vector3.down + Vector3.left).normalized, out hit, Mathf.Infinity, mask);
                    break;
            }

            transform.Rotate(0, 60, 0);

            if (hit.collider == null)
            {
                GetComponentInParent<Tile>().neighbours.Add(null);
            }
            else
            {
                GetComponentInParent<Tile>().neighbours.Add(hit.collider.gameObject);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            for (int i = 0; i < 6; i++)
            {
                if (GetComponentInParent<Tile>().neighbours[i] != null)
                {
                    print(i.ToString() + GetComponentInParent<Tile>().neighbours[i].GetComponent<Tile>().state);

                }
                else
                {
                    print(i +" empty space");
                }
            }
        }
                        


        if (Input.GetKeyDown(KeyCode.A))
        {
                CheckNeighbours();
        }
    }
}
