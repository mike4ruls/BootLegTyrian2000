using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBarrier : MonoBehaviour {
    Enemy parent;
    public int health;
    int maxHealth;
    public List<Texture> cracks;
    int numOfCracks;
    public int stage = 0;
    Material myMat;
	// Use this for initialization
	void Start () {
        parent = transform.parent.GetComponent<Enemy>();
        parent.invincible = true;
        myMat = GetComponent<Renderer>().material;
        numOfCracks = cracks.Count;
        maxHealth = health;
	}
	
	// Update is called once per frame
	void Update ()
    {
        myMat.mainTexture = cracks[stage];

	}
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < maxHealth - ((maxHealth / numOfCracks) *  (stage+1)))
        {
            stage++;
        }
        if (health <= 0)
        {
            parent.invincible = false;
            this.gameObject.SetActive(false);
        }
    }
}
