using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTiles : MonoBehaviour {

    public float movementSpeedX;
    public float movementSpeedY;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(Time.time * movementSpeedX, Time.time * -movementSpeedY);
    }
}
