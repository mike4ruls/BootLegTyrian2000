using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    GameManager manager;

    public int level;
    public int health;
    public float enemySpeed;
    public int baseAtkDamage;
    public int maxMoneyRange;
    public int minMoneyRange;
    public int numOfFlashes;
    int currentFlashes;
    protected int damage;
    int worth;

    public float shakeTime;
    public float shakeAmmount;


    GameCamera cam;

    bool startFlashing;
	// Use this for initialization
	void Start () {
        Init();
	}
	
	// Update is called once per frame
	void Update () {

        CheckIfDead();
        Move();
        CheckIfHit();
	}
    public void Init()
    {
        startFlashing = false;
        currentFlashes = 0;

        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameCamera>();
        damage = baseAtkDamage * level;
        worth = Random.Range(minMoneyRange * level, maxMoneyRange * level);

    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        startFlashing = true;
    }
    public void CheckIfDead()
    {
        if (health <= 0)
        {
            cam.ShakeCamera(shakeTime, shakeAmmount);
            ParticleSystem explosion = manager.GetExplosion();

            if (explosion != null)
            {
                explosion.transform.position = transform.position;
                explosion.Play();
                explosion.GetComponent<AudioSource>().Play(); ;
            }

            Destroy(this.gameObject, 0);
            if (GameObject.FindGameObjectWithTag("Player").activeInHierarchy)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddPoints(worth);
            }
        }
    }
    public void Move()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - (enemySpeed * Time.deltaTime));
    }
    public void CheckIfHit()
    {
        if (startFlashing)
        {
            FlashRed();
        }
    }

    void FlashRed()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
        StartCoroutine(flash(0.05f));
        currentFlashes++;
        startFlashing = false;
    }
    IEnumerator flash(float time)
    {
        yield return new WaitForSeconds(time);
        GetComponent<MeshRenderer>().material.color = Color.white;

        if (currentFlashes >= numOfFlashes)
        {
            startFlashing = false;
        }
        else
        {
            startFlashing = true;
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
