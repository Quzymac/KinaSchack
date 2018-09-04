using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour {

    public bool moving { get; set; }

    [SerializeField] Camera cam;
    [SerializeField] GameObject gameManager;

    // Update is called once per frame
    void Update () {
        /*if (gameManager.GetComponent<ClickMove>().GetBall() == this.gameObject)
        {
            Vector3 temp = Input.mousePosition;
            temp.y = 0.5f; // Set this to be the distance you want the object to be placed in front of the camera.
            this.transform.position = cam.ScreenToWorldPoint(temp);
        }*/
    }
}
