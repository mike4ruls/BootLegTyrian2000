using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    AudioSource myAudio;
    MeshRenderer myMeshRend;

    public float bulletSpeed;
    public float bulletDespawnTimer;

    public int damage;

    bool isActive;
    bool reflected = false;
	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().material.color = new Vector4(0.0f, 1.0f, 1.0f, 1.0f);
        myAudio = GetComponent<AudioSource>();
        myMeshRend = GetComponent<MeshRenderer>();

        isActive = true;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.forward * (bulletSpeed * Time.deltaTime);
	}
    public void ActivateBullet(Vector3 spawnPoint, Vector3 fwd, int dam)
    {
        transform.position = spawnPoint;
        transform.forward = fwd;
        damage = dam;
        myAudio.Play();
        isActive = true;
        myMeshRend.enabled = true;
        StartCoroutine(Despawner(bulletDespawnTimer));
    }

    IEnumerator Despawner(float time)
    {
        yield return new WaitForSeconds(time);
        reflected = false;
        this.gameObject.SetActive(false);   
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && isActive)
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            isActive = false;
            myMeshRend.enabled = false;
            StartCoroutine(Despawner(0.5f));
        }
        else if (other.tag == "EnemyBarrier" && isActive)
        {
            transform.forward *= -1;
            reflected = true;
            //isActive = false;
            //myMeshRend.enabled = false;
            //StartCoroutine(Despawner(0.5f));
        }
        else if (other.tag == "BulletBlocker")
        {
            isActive = false;
            myMeshRend.enabled = false;
            StartCoroutine(Despawner(0.5f));
        }
        else if (other.tag == "Player" && reflected)
        {
            other.GetComponent<Player>().TakeDamage(damage);
            isActive = false;
            myMeshRend.enabled = false;
            StartCoroutine(Despawner(0.5f));
        }
    }
}
