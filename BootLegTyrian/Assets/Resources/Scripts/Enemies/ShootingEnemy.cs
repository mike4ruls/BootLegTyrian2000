using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy {

    public float atkSpeed;
    bool canShoot;
    GameObject player;
    GameObject enemyBullets;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyBullets = GameObject.FindGameObjectWithTag("EnemyBulletPool");
        for (int i = 0; i < enemyBullets.transform.childCount; i++)
        {
            enemyBullets.transform.GetChild(i).gameObject.SetActive(false);
        }

        canShoot = true;
        Init();
	}
	
	// Update is called once per frame
	void Update () {
        CheckIfDead();
        Move();
        CheckIfHit();
        if (canShoot)
        {
            Shoot();
        }
	}

    void Shoot()
    {
        for (int i = 0; i < enemyBullets.transform.childCount; i++)
        {
            if (!enemyBullets.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                Vector3 dir2Player = player.transform.position - transform.position;
                dir2Player.Normalize();

                enemyBullets.transform.GetChild(i).gameObject.SetActive(true);
                enemyBullets.transform.GetChild(i).GetComponent<EnemyBullet>().ActivateBullet(transform.position, dir2Player, damage);
                canShoot = false;
                StartCoroutine(ShootTimer(atkSpeed));
                break;
            }
        }
    }
    IEnumerator ShootTimer(float time)
    {
        yield return new WaitForSeconds(time);
        canShoot = true;
    }
}
