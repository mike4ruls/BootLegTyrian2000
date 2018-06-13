using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy {

    public float atkSpeed;
    bool canShoot;
    bool hasBullets;
    GameObject player;
    GameObject enemyBullets;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyBullets = GameObject.FindGameObjectWithTag("EnemyBulletPool");
        hasBullets = true;
        if (enemyBullets != null)
        {
            for (int i = 0; i < enemyBullets.transform.childCount; i++)
            {
                enemyBullets.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else
        {
            hasBullets = false;
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
        if (hasBullets)
        {
            Vector3 dir2Player = player.transform.position - transform.position;

            float magSqr = dir2Player.magnitude;

            if (magSqr > 60)
            {
                return;
            }

            for (int i = 0; i < enemyBullets.transform.childCount; i++)
            {
                if (!enemyBullets.transform.GetChild(i).gameObject.activeInHierarchy)
                {
                    
                    dir2Player.Normalize();

                    enemyBullets.transform.GetChild(i).gameObject.SetActive(true);
                    enemyBullets.transform.GetChild(i).GetComponent<EnemyBullet>().ActivateBullet(transform.position, dir2Player, damage);
                    canShoot = false;
                    StartCoroutine(ShootTimer(atkSpeed));
                    break;
                }
            }
        }      
    }
    IEnumerator ShootTimer(float time)
    {
        yield return new WaitForSeconds(time);
        canShoot = true;
    }
}
