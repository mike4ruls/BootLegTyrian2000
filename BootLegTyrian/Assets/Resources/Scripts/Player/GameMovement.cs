using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMovement : MonoBehaviour {

    Player p1;
    public float playerSpeed;
    public float rotBackwardsSpeed;
    float rotBackwardsAngle;

    bool canRotBackwards;
    // Use this for initialization
    void Start () {
        p1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rotBackwardsAngle = 0.0f;
        canRotBackwards = false;
    }
	
	// Update is called once per frame
	void Update () {
        p1.currentTurnState = Player.TurnState.STRAIGHT;

        if (Input.GetButtonDown("TurnAround") && !p1.repairingSheild)
        {
            canRotBackwards = canRotBackwards ? false : true;
            p1.strafeForce *= -1;
        }
        RotateBackwards();
        float speed = 0.0f;
        if (p1.repairingSheild && p1.canRepairSheild)
        {
            speed = playerSpeed * p1.sheildRepairSpeedDebuff;
        }
        else
        {
            speed = playerSpeed;
        }

            if ((Input.GetAxis("Vertical") > 0.0f))
        {
            p1.transform.position = new Vector3(p1.transform.position.x, p1.transform.position.y, p1.transform.position.z + (speed * Time.deltaTime));
        }
        if ((Input.GetAxis("Vertical") < 0.0f))
        {
            p1.transform.position = new Vector3(p1.transform.position.x, p1.transform.position.y, p1.transform.position.z - (speed * Time.deltaTime));
        }
        if ((Input.GetAxis("Horizontal") < 0.0f))
        {   
            p1.currentTurnState = canRotBackwards ? Player.TurnState.RIGHT : Player.TurnState.LEFT;
            p1.previousTurnState = p1.currentTurnState;
            p1.transform.position = new Vector3(p1.transform.position.x - (speed * Time.deltaTime), p1.transform.position.y, p1.transform.position.z);
        }
        if ((Input.GetAxis("Horizontal") > 0.0f))
        {
            p1.currentTurnState = canRotBackwards ? Player.TurnState.LEFT : Player.TurnState.RIGHT;
            p1.previousTurnState = p1.currentTurnState;
            p1.transform.position = new Vector3(p1.transform.position.x + (speed * Time.deltaTime), p1.transform.position.y, p1.transform.position.z);
        }
    }
    void RotateBackwards()
    {
        if (canRotBackwards)
        {
            if (rotBackwardsAngle < 180.0f)
            {
                rotBackwardsAngle += rotBackwardsSpeed * Time.deltaTime;
                if (rotBackwardsAngle >= 180.0f)
                {
                    rotBackwardsAngle = 180.0f;
                }
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, rotBackwardsAngle, transform.rotation.eulerAngles.z);
            }
        }
        else
        {
            if (rotBackwardsAngle > 0.0f)
            {
                rotBackwardsAngle -= rotBackwardsSpeed * Time.deltaTime;
                if (rotBackwardsAngle <= 0.0f)
                {
                    rotBackwardsAngle = 0.0f;
                }
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, rotBackwardsAngle, transform.rotation.eulerAngles.z);
            }
        }
    }
}
