using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkingEnemy : MonoBehaviour {
    GameObject player;
    public float speed;
    public bool waitToStalk;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

	}
	
	// Update is called once per frame
	void Update () {
        StalkPlayer();
	}
    void StalkPlayer()
    {
        if (waitToStalk)
        {
            Vector3 dir2Player = player.transform.position - transform.position;

            float magSqr = dir2Player.magnitude;

            if (magSqr > 50)
            {
                return;
            }
        }

        Vector3 dist2Player = player.transform.position - transform.position;
        dist2Player.Normalize();
        transform.position = new Vector3(transform.position.x + (dist2Player.x * speed * Time.deltaTime), player.transform.position.y, transform.position.z);
    }
}
