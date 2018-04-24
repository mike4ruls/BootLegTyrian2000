using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMExplosion : MonoBehaviour {
    SphereCollider myCollider;

    public float despawnTimer;
    int damage;

	// Use this for initialization
	void Start () {
        myCollider = GetComponent<SphereCollider>();
        myCollider.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ActivateExplosion(int dam)
    {
        damage = dam;
        myCollider.enabled = true;
        StartCoroutine(Despawner(despawnTimer));
    }

    IEnumerator Despawner(float time)
    {
        yield return new WaitForSeconds(time);
        myCollider.enabled = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
