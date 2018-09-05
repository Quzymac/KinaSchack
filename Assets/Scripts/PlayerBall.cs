using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour {

    public enum Team { red, blue, yellow, green, white, black }

    public Team ballTeam;

    public string GetBallTeam() 
        {
        return ballTeam.ToString();
        }

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
		
	}
}
