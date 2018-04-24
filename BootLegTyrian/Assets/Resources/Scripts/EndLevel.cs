using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour {
    public float levelSpeed;
    public bool endLevel;
    // Use this for initialization
    void Start () {
        endLevel = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!endLevel)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - (levelSpeed * Time.deltaTime));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            endLevel = true;
        }
    }
}
