using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public enum TurnState
    {
        STRAIGHT,
        LEFT,
        RIGHT
    };

    Rigidbody rigidBody;
    GameCamera mainCam;

    #region HideInInspectorStuff

    [HideInInspector]
    public TurnState currentTurnState;
    [HideInInspector]
    public TurnState previousTurnState;
    [HideInInspector]
    public int frontDamage
    , leftDamage
    , rightDamage
    , guidedMissleDamage;
    [HideInInspector]
    public float frontAtkSpeed
    , leftSideAtkSpeed
    , rightSideAtkSpeed
    , guidedMissleCD;
    TurnState staticCurTurnState;
    [HideInInspector]
    public float xPosConstraint;
    [HideInInspector]
    public float xNegConstraint;
    [HideInInspector]
    public float zPosConstraint;
    [HideInInspector]
    public float zNegConstraint;

    [HideInInspector]
    public GameObject bulletPool;
    [HideInInspector]
    public GameObject bulletSpawnPoint;

    [HideInInspector]
    public GameObject leftBulletPool;
    [HideInInspector]
    public GameObject leftBulletSpawnPoint;

    [HideInInspector]
    public GameObject rightBulletPool;
    [HideInInspector]
    public GameObject rightBulletSpawnPoint;

    [HideInInspector]
    public GameObject guidedMisslePool;
    [HideInInspector]
    public GameObject guidedMissleSpawnPoint;

    [HideInInspector]
    public bool frontCanShoot
, leftCanShoot
, rightCanShoot
,guidedMissleCanShoot;
    //[HideInInspector]
    public bool sheildComponentBought
, guidedMissleBought
, leftSideBlasterBought
, rightSideBlasterBought
, strafeImmuneBought
, secondWindBought
, bouncingBulletsBought
, notSureBought = false;

    [HideInInspector]
    public bool sheildComponentBroken
    , guidingMissleBroken
    , LeftSideBlasterBroken
    , rightSideBlasterBroken = false;

    [HideInInspector]
    public int playerLvl
    , healthLvl
    , defenseLvl
    , guidingMissleLvl
    , gMAttackDamageLvl
    , gMRechargeRateLvl
    , sheildComponentLvl
    , sCPowerLvl
    , sCRechargeRate
    , frontBlasterLvl
    , fBAttackDamageLvl
    , fBAttackSpeedLvl
    , leftSideBlasterLvl
    , lBAttackDamageLvl
    , lBAttackSpeedLvl
    , rightSideBlasterLvl
    , rBAttackDamageLvl
    , rBAttackSpeedLvl = 1;
    #endregion

    public float maxHealth;
    public float health;

    public float defense;

    public float maxSheild;
    public float sheild;

    public int money;

    public float strafeRotSpeed;
    public float strafeForce;
	public float tiltDegrees;
	public float tiltSpeed;
    public float sheildRechargeRate;
    public float strafeImmuneCD;
    public float damageImmuneCD;
    public float sheildRepairSpeedDebuff;
    [HideInInspector]
    public bool repairingSheild;


    float currentRot;
    float currentTilt;

    float sheildTimer;
    float strafeImmuneTimer;
    float damageImmuneTimer;
    float originalZPos;

    public bool isDead;
    public bool inHUBWorld;
    public bool sandBoxMode = false;
    bool canStrafe;
    bool canTilt;
    bool strafeImmune;
	bool damageImmune;
    bool canStrafeImmuneTimer;
	bool canDamageImmuneTimer;
    bool godMode;

    public AudioClip strafeClip;
    AudioSource myAudio;

    // Use this for initialization
    void Start () {
        myAudio = GetComponent<AudioSource>();
        bulletPool = GameObject.FindGameObjectWithTag("FrontBulletPool");
        leftBulletPool = GameObject.FindGameObjectWithTag("LeftBulletPool");
        rightBulletPool = GameObject.FindGameObjectWithTag("RightBulletPool");

        guidedMisslePool = GameObject.FindGameObjectWithTag("GuidedMisslePool");

        for (int i = 0; i < bulletPool.transform.childCount; i++)
        {
            bulletPool.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < leftBulletPool.transform.childCount; i++)
        {
            leftBulletPool.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < rightBulletPool.transform.childCount; i++)
        {
            rightBulletPool.transform.GetChild(i).gameObject.SetActive(false);
        }

        currentTurnState = TurnState.STRAIGHT;
        previousTurnState = TurnState.LEFT;
        staticCurTurnState = TurnState.LEFT;

        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameCamera>();
        rigidBody = GetComponent<Rigidbody>();

        //guidedMissleBought = true;

        guidedMissleCanShoot = true;
        frontCanShoot = true;
        leftCanShoot = true;
        rightCanShoot = true;
        canStrafe = false;
        canTilt = true;
        isDead = false;
        strafeImmune = false;
        damageImmune = false;

        canStrafeImmuneTimer = false;
        canDamageImmuneTimer = false;

        sheildComponentBought = false;

        leftSideBlasterBought = false;
        LeftSideBlasterBroken = false;

        rightSideBlasterBought = false;
        rightSideBlasterBroken = false;

        repairingSheild = false;
        godMode = false;

        health = maxHealth;
        defense = 0.0f;

        sheild = 0.0f;

        sheildRechargeRate = 0.01f;
        sheildTimer = 0.0f;

        strafeImmuneCD = 1.0f;
        damageImmuneCD = 0.01f;

        strafeImmuneTimer = strafeImmuneCD;
        damageImmuneTimer = damageImmuneCD;

        strafeRotSpeed = 1060.0f;
        currentRot = 0.0f;

        tiltDegrees = 30.0f;
        tiltSpeed = 200.0f;
        currentTilt = 0.0f;

        frontBlasterLvl = 0;

        frontAtkSpeed = 0.2f;
        leftSideAtkSpeed = 0.2f;
        rightSideAtkSpeed = 0.2f;
        guidedMissleCD = 1.0f;

        frontDamage = 1;
        leftDamage = 1;
        rightDamage = 1;
        guidedMissleDamage = 10;

        if (!inHUBWorld)
        {
            originalZPos = transform.position.z;
        }
        else
        {
            originalZPos = 0.0f;
        }

        bulletSpawnPoint = transform.GetChild(0).gameObject;
        leftBulletSpawnPoint = transform.GetChild(1).gameObject;
        rightBulletSpawnPoint = transform.GetChild(2).gameObject;
        guidedMissleSpawnPoint = transform.GetChild(3).gameObject;

        if (!sandBoxMode)
        {
            ImmortalGameManager.LoadPlayerInfo();
        }
    }

    // Update is called once per frame
    void Update() {

        if (health <= 0)
        {
            this.gameObject.SetActive(false);
            isDead = true;
        }
        if (repairingSheild)
        {
            RechargeSheild();
        }

            if (Input.GetButtonDown("Strafe") && !repairingSheild)
        {
            if (!canStrafe)
            {
                myAudio.clip = strafeClip;
                myAudio.Play();
            }
            canStrafe = true;
            canTilt = false;
            staticCurTurnState = previousTurnState;
            if (!inHUBWorld)
            {
                switch (currentTurnState)
                {
                    case TurnState.LEFT:
                        {
                            //rigidBody.AddForce(new Vector3(-strafeForce, 0.0f, 0.0f));
                            rigidBody.AddForce(new Vector3(-strafeForce, 0.0f, 0.0f), ForceMode.Impulse);
                            break;
                        }
                    case TurnState.RIGHT:
                        {
                            //rigidBody.AddForce(new Vector3(strafeForce, 0.0f, 0.0f));
                            rigidBody.AddForce(new Vector3(strafeForce, 0.0f, 0.0f), ForceMode.Impulse);
                            break;
                        }
                }
            }
            else
            {
                rigidBody.AddForce(transform.forward * strafeForce, ForceMode.Impulse);
            }

        }
        if (!isDead)
        {
            if (sheildComponentBought && !sheildComponentBroken && ((Input.GetAxis("Fire1") > 0.0f) || Input.GetKey(KeyCode.R)))
            {
                if (!repairingSheild)
                {
                    repairingSheild = true;
                }
            }
            else
            {
                if (repairingSheild)
                {
                    repairingSheild = false;
                }   
            }
            if ((guidedMissleCanShoot && guidedMissleBought && !guidingMissleBroken) && ((Input.GetAxis("Fire2") < 0.0f) || Input.GetButton("Fire2")))
            {
                for (int i = 0; i < guidedMisslePool.transform.childCount; i++)
                {
                    if (!guidedMisslePool.transform.GetChild(i).gameObject.activeInHierarchy)
                    {
                        guidedMisslePool.transform.GetChild(i).gameObject.SetActive(true);
                        guidedMisslePool.transform.GetChild(i).GetComponent<GuidedMissle>().ActivateMissle(guidedMissleSpawnPoint.transform.position, transform.forward, guidedMissleDamage);
                        break;
                    }
                }
                guidedMissleCanShoot = false;
                StartCoroutine(MissleAttacking(guidedMissleCD));
            }
            //Debug.Log(Input.GetAxis("Fire1"));
            if (frontCanShoot && ((Input.GetAxis("Fire1") < 0.0f) || Input.GetButton("Fire1")))
            {
                for (int i = 0; i < bulletPool.transform.childCount; i++)
                {
                    if (!bulletPool.transform.GetChild(i).gameObject.activeInHierarchy)
                    {
                        bulletPool.transform.GetChild(i).gameObject.SetActive(true);
                        bulletPool.transform.GetChild(i).GetComponent<Bullet>().ActivateBullet(bulletSpawnPoint.transform.position, transform.forward, frontDamage);
                        break;
                    }
                }
                frontCanShoot = false;
                StartCoroutine(FrontAttacking(frontAtkSpeed));
            }
            if ((leftCanShoot && leftSideBlasterBought && !LeftSideBlasterBroken) && ((Input.GetAxis("Fire1") < 0.0f) || Input.GetButton("Fire1")))
            {
                for (int i = 0; i < leftBulletPool.transform.childCount; i++)
                {
                    if (!leftBulletPool.transform.GetChild(i).gameObject.activeInHierarchy)
                    {
                        leftBulletPool.transform.GetChild(i).gameObject.SetActive(true);
                        leftBulletPool.transform.GetChild(i).GetComponent<Bullet>().ActivateBullet(leftBulletSpawnPoint.transform.position, transform.forward, leftDamage);
                        break;
                    }
                }
                leftCanShoot = false;
                StartCoroutine(LeftAttacking(leftSideAtkSpeed));
            }
            if ((rightCanShoot && rightSideBlasterBought && !rightSideBlasterBroken) && ((Input.GetAxis("Fire1") < 0.0f) || Input.GetButton("Fire1")))
            {
                for (int i = 0; i < rightBulletPool.transform.childCount; i++)
                {
                    if (!rightBulletPool.transform.GetChild(i).gameObject.activeInHierarchy)
                    {
                        rightBulletPool.transform.GetChild(i).gameObject.SetActive(true);
                        rightBulletPool.transform.GetChild(i).GetComponent<Bullet>().ActivateBullet(rightBulletSpawnPoint.transform.position, transform.forward, guidedMissleDamage);
                        break;
                    }
                }
                rightCanShoot = false;
                StartCoroutine(RightAttacking(rightSideAtkSpeed));
            }
            if (canStrafe)
            {
                Strafe();
            }
            if (canTilt)
            {
                Tilt();
            }
        }
       

        if (transform.position.x > xPosConstraint)
        {
            float pushBackDist = xPosConstraint - transform.position.x;
            transform.position = new Vector3(pushBackDist + transform.position.x, transform.position.y, transform.position.z);
        }
        else if(transform.position.x < xNegConstraint)
        {
            float pushBackDist = xNegConstraint - transform.position.x;
            transform.position = new Vector3(pushBackDist + transform.position.x, transform.position.y, transform.position.z);
        }


        float zOffset = transform.position.z - originalZPos;
        if (zOffset > zPosConstraint)
        {
            float pushBackDist = zPosConstraint - zOffset;
            transform.position = new Vector3(transform.position.x, transform.position.y, pushBackDist + transform.position.z);
        }
        else if (zOffset < zNegConstraint)
        {
            float pushBackDist = zNegConstraint - zOffset;
            transform.position = new Vector3(transform.position.x, transform.position.y, pushBackDist + transform.position.z);
        }

        if (canDamageImmuneTimer)
        {
            damageImmuneTimer -= 1.0f * Time.deltaTime;
            if (damageImmuneTimer <= 0.0f)
            {
                damageImmune = false;
                canDamageImmuneTimer = false;
                damageImmuneTimer = damageImmuneCD;
            }
        }
        if (canStrafeImmuneTimer)
        {
            strafeImmuneTimer -= 1.0f * Time.deltaTime;
            if (strafeImmuneTimer <= 0.0f)
            {
                strafeImmune = false;
                canStrafeImmuneTimer = false;
                strafeImmuneTimer = strafeImmuneCD;
            }
        }
    }
    void Strafe()
    {
        if (staticCurTurnState == TurnState.LEFT)
        {
            currentRot += strafeRotSpeed * Time.deltaTime;
        }
        else if (staticCurTurnState == TurnState.RIGHT)
        {
            currentRot -= strafeRotSpeed * Time.deltaTime;
        }
        //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, currentRot);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, currentRot);
        //transform.rotation = Quaternion.AngleAxis(currentRot, transform.forward);

        if (currentRot >= 360.0f || currentRot <= -360.0f)
        {
            canStrafe = false;
            if (!inHUBWorld)
            {
                canTilt = true;
            }
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0.0f);
            rigidBody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            currentRot = 0.0f;
            //speed = normSpeed;
        }
    }
    void Tilt()
    {
        if (inHUBWorld)
        {
            canTilt = false;
            return;
        }
        //Debug.Log(transform.forward);
        if (currentTurnState == TurnState.LEFT)
        {
            if (currentTilt < tiltDegrees)
            {
                currentTilt += tiltSpeed * Time.deltaTime;
            }
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, currentTilt);
        }
        else if (currentTurnState == TurnState.RIGHT)
        {
            if (currentTilt > -tiltDegrees)
            {
                currentTilt -= tiltSpeed * Time.deltaTime;
            }
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, currentTilt);
        }
        else
        {
            if (previousTurnState == TurnState.LEFT)
            {
                if (currentTilt > 0.0f)
                {
                    currentTilt -= tiltSpeed * Time.deltaTime;
                }
                else
                {
                    currentTilt = 0.0f;
                }
            }
            else if (previousTurnState == TurnState.RIGHT)
            {
                if (currentTilt < 0.0f)
                {
                    currentTilt += tiltSpeed * Time.deltaTime;
                }
                else
                {
                    currentTilt = 0.0f;
                }
            }
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, currentTilt);
        }
    }
    public void TakeDamage(int damage)
    {
        if (!damageImmune && !strafeImmune && !isDead)
        {
            if (sheild == 0 || !sheildComponentBought)
            {
                health -= damage;
                if (health <= 0.0f)
                {
                    health = 0.0f;
                    isDead = true;
                    gameObject.SetActive(false);
                }
            }
            else
            {
                sheild -= damage;
                if (sheild <= 0.0f)
                {
                    sheild = 0.0f;
                }
            }

            damageImmune = true;
            canDamageImmuneTimer = true;
            mainCam.ShakeCamera(0.1f, 0.2f);
        }
    }
    public void AddPoints(int pts)
    {
        money += pts;
    }
    IEnumerator FrontAttacking(float time)
    {
        yield return new WaitForSeconds(time);
        frontCanShoot = true;
    }
    IEnumerator LeftAttacking(float time)
    {
        yield return new WaitForSeconds(time);
        leftCanShoot = true;
    }
    IEnumerator RightAttacking(float time)
    {
        yield return new WaitForSeconds(time);
        rightCanShoot = true;
    }
    IEnumerator MissleAttacking(float time)
    {
        yield return new WaitForSeconds(time);
        guidedMissleCanShoot = true;
    }
    public void RechargeSheild()
    {
        if (sheild != maxSheild && sheildComponentBought && !sheildComponentBroken)
        {
            sheildTimer += Time.deltaTime;
            if (sheildTimer >= sheildRechargeRate)
            {
                sheild += 0.2f;
                sheildTimer = 0.0f;

                if (sheild >= maxSheild)
                {
                    sheild = maxSheild;
                }
            }
        }
    }

    public void CalculateFinalStats()
    {
        frontBlasterLvl = (fBAttackDamageLvl + fBAttackSpeedLvl) / 2;
        leftSideBlasterLvl = (lBAttackDamageLvl + lBAttackSpeedLvl) / 2;
        rightSideBlasterLvl = (rBAttackDamageLvl + rBAttackSpeedLvl) / 2;
        sheildComponentLvl = (sCPowerLvl + sCRechargeRate ) / 2;
        guidingMissleLvl = (gMAttackDamageLvl + gMRechargeRateLvl) / 2;
    }
}
