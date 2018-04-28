using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMExplosion : MonoBehaviour {
    SphereCollider myCollider;

    public float despawnTimer;
    int damage;
    bool canHitPlayer = true;

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
        canHitPlayer = true;
        StartCoroutine(Despawner(despawnTimer));
        StartCoroutine(PlayerHitCD(0.2f));
    }

    IEnumerator Despawner(float time)
    {
        yield return new WaitForSeconds(time);
        myCollider.enabled = false;
    }

    IEnumerator PlayerHitCD(float time)
    {
        yield return new WaitForSeconds(time);
        canHitPlayer = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
        if (other.tag == "Player" && canHitPlayer)
        {
            Player p1 = other.GetComponent<Player>();
            if (p1.friendlyFireOn)
            {
                p1.TakeDamage(damage);
            }
        }
    }
}
