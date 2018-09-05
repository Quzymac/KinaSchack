using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNeighbours : MonoBehaviour {


    //ignore layer 9 (Ball)
    int ignoreLayer = ~(1 << 9);

     RaycastHit hit;

    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.A))
        {
            for (int i = 0; i < 6; i++)
            {
                switch (i)
                {
                    case 0:
                        Physics.Raycast(transform.position, (Vector3.forward + Vector3.down + Vector3.right).normalized, out hit, Mathf.Infinity, ignoreLayer);
                        break;
                    case 1:
                        Physics.Raycast(transform.position, (Vector3.down + Vector3.right).normalized, out hit, Mathf.Infinity, ignoreLayer);
                        break;
                    case 2:
                        Physics.Raycast(transform.position, (Vector3.back + Vector3.down + Vector3.right).normalized, out hit, Mathf.Infinity, ignoreLayer);
                        break;
                    case 3:
                        Physics.Raycast(transform.position, (Vector3.back + Vector3.down + Vector3.left).normalized, out hit, Mathf.Infinity, ignoreLayer);
                        break;
                    case 4:
                        Physics.Raycast(transform.position, (Vector3.down + Vector3.left).normalized, out hit, Mathf.Infinity, ignoreLayer);
                        break;
                    case 5:
                        Physics.Raycast(transform.position, (Vector3.forward + Vector3.down + Vector3.left).normalized, out hit, Mathf.Infinity, ignoreLayer);
                        break;
                }
                transform.Rotate(0, 60, 0);
                if(hit.collider != null)
                {
                 print(hit.collider.gameObject.name);

                }

            }
        }
		
	}
}
