using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissle : MonoBehaviour {

    GameCamera mainCam;
    GameObject explosions;

    public float turnSpeed;
    public float rocketSpeed;
    public float despawnTimer;
    public int damage;

    float curXRot;
    float curZRot;

    Rigidbody myRigid;
    // Use this for initialization
    void Start () {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameCamera>();
        myRigid = GetComponent<Rigidbody>();

        explosions = GameObject.Find("GuidedMissleExplosionPool");

        GetComponent<Renderer>().material.color = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);

        curXRot = 0.0f;
        curZRot = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        float xRot = Input.GetAxis("Horizontal2");

        //Debug.Log(xRot);

        if (xRot != 0.0f)
        {
            //Debug.Log(xRot);
            transform.Rotate(0.0f, 0.0f, (turnSpeed * Time.deltaTime) * -xRot);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            //Debug.Log(xRot);
            transform.Rotate(0.0f, 0.0f, (turnSpeed * Time.deltaTime) * 1.0f);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            //Debug.Log(xRot);
            transform.Rotate(0.0f, 0.0f, (turnSpeed * Time.deltaTime) * -1.0f);
        }
        transform.position += transform.up * (rocketSpeed * Time.deltaTime);
    }
    void Explode()
    {
        ParticleSystem explosion = null;

        for (int i = 0; i < explosions.transform.childCount; i++)
        {
            ParticleSystem system = explosions.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (!system.isPlaying)
            {
                explosion = system;
            }
        }

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
            explosion.GetComponent<GMExplosion>().ActivateExplosion(damage);
            explosion.Play();
            explosion.GetComponent<AudioSource>().Play(); ;
        }

        mainCam.ShakeCamera(0.7f, 1.5f);
        gameObject.SetActive(false);
    }
    public void ActivateMissle(Vector3 spawnPoint, Vector3 fwd, int dam)
    {
        transform.position = spawnPoint;
        transform.forward = fwd;

        transform.Rotate(90.0f, 0.0f, 0.0f);

        damage = dam;
        StartCoroutine(Despawner(despawnTimer));
    }

    IEnumerator Despawner(float time)
    {
        yield return new WaitForSeconds(time);
        Explode();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            Explode();
        }
    }

}
