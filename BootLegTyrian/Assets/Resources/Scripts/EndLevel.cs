using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour {
    public float levelSpeed;
    public bool endLevel;
    public bool canMove;

    [HideInInspector]
    public GameObject lastEnemy;
    // Use this for initialization
    void Start () {
        endLevel = false;
        canMove = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (canMove)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - (levelSpeed * Time.deltaTime));
        }
        else if(lastEnemy != null)
        {
            if (lastEnemy.transform.position.z + 10< transform.position.z)
            {
                canMove = true;
            }
            else
            {
                canMove = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            endLevel = true;
            canMove = false;
        }
    }
}
