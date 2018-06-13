using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryScript : MonoBehaviour {
    Enemy myParent;

	// Use this for initialization
	void Start () {
        myParent = transform.parent.GetComponent<Enemy>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            myParent.enemySpeed = 0;
            this.gameObject.SetActive(false);
        }
    }
}
