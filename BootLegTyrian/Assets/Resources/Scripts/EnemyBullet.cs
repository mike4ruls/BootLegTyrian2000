using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
    public float bulletSpeed;
    public float bulletDespawnTimer;

    public int damage;
	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().material.color = new Vector4(1.0f, 0.0f, 1.0f, 1.0f);
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
        StartCoroutine(Despawner(bulletDespawnTimer));
    }

    IEnumerator Despawner(float time)
    {
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().TakeDamage(damage);
            this.gameObject.SetActive(false);
        }
    }
}
