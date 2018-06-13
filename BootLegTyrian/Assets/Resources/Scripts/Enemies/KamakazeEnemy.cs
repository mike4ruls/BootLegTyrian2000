using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamakazeEnemy : MonoBehaviour
{
    GameObject player;
    Rigidbody rigid;
    public float turnSpeed;
    public float speed;
    public bool waitToTrack;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.forward = new Vector3(0.0f, 0.0f, -1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        StalkPlayer();
    }
    void StalkPlayer()
    {
        if (waitToTrack)
        {
            Vector3 dir2Player = player.transform.position - transform.position;

            float magSqr = dir2Player.magnitude;

            if (magSqr > 90)
            {
                return;
            }
        }

        Vector3 dist2Player = player.transform.position - transform.position;
        
        float mag = dist2Player.magnitude;
        dist2Player.Normalize();
        Vector3 cross = Vector3.Cross(dist2Player, new Vector3(0, 1, 0));
        cross.Normalize();

        float dot = Vector3.Dot(transform.forward, dist2Player);
        float crossDot = Vector3.Dot(transform.forward, cross);

        float angle = dot / mag;

        angle = Mathf.Acos(angle);

        angle = (angle * 180) / (2 * Mathf.PI);

        //Debug.Log(angle);
        if (crossDot > 0)
        {
            //Debug.Log(xRot);
            transform.Rotate(0.0f, (turnSpeed * Time.deltaTime) * 1.0f, 0.0f);
        }
        else if (crossDot < 0)
        {
            //Debug.Log(xRot);
            transform.Rotate(0.0f, (turnSpeed * Time.deltaTime) * -1.0f, 0.0f);
        }
        
        transform.position += transform.forward * (speed * Time.deltaTime);

        //transform.position = new Vector3(transform.position.x + (dist2Player.x * speed * Time.deltaTime), player.transform.position.y, transform.position.z);
    }
}