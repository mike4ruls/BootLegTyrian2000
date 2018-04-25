using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUBMovement : MonoBehaviour {

    Player p1;
    public float playerSpeed;

    ParticleSystem leftTrail;
    ParticleSystem rightTrail;

    // Use this for initialization
    void Start () {
        p1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        leftTrail =  p1.transform.GetChild(5).GetComponent<ParticleSystem>();
        rightTrail = p1.transform.GetChild(6).GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        float zRot = Input.GetAxis("Vertical");
        float xRot = Input.GetAxis("Horizontal");

        if (xRot != 0.0f || zRot != 0.0f)
        {
            p1.transform.forward = new Vector3(xRot, 0.0f, zRot);
            //transform.rotation = Quaternion.Euler(xRot * 360, 0.0f, zRot * 360);
            if (p1.repairingSheild)
            {
                p1.transform.position += p1.transform.forward * (playerSpeed * p1.sheildRepairSpeedDebuff * Time.deltaTime);
            }
            else
            {
                p1.transform.position += p1.transform.forward * (playerSpeed * Time.deltaTime);
            }

            leftTrail.Play();
            rightTrail.Play();
        }
        else
        {
            leftTrail.Stop();
            rightTrail.Stop();
        }
    }
}
