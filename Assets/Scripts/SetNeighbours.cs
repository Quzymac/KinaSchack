using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNeighbours : MonoBehaviour
{

    [SerializeField] LayerMask mask;
    RaycastHit hit;

    // Use this for initialization
    void Start()
    {

    }

    public void CheckNeighbours()
    {
         GetComponentInParent<Tile>().neighbours.Clear();

        for (int i = 0; i < 6; i++)
        {
            //raycasts six times to find the neighbouring tiles
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
}
