using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour {

    Player p1;
    GameCamera mainCam;
    GameObject mySword;

    public Vector3 rot;
    public float regTravelRotSpeed;
    public float attackTravelRotSpeed;
    float travelRotSpeed;
    public float swordRotSpeed;
    public int baseDamage;

    float swordAngle = 0.0f;
    bool canSpinSword = false;
    int comboNum = 0;

    Quaternion currentQuat;
    Quaternion nextQuat;
    bool isAttacking = false;
    public enum SwordState
    {
        State1,
        State2,
        State3,
        State4,
        State5,
        State6,

    }
    SwordState currentState;
    SwordState previousState;
    float lerpTime = 0.0f;
    // Use this for initialization
    void Start ()
    {
        p1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameCamera>();

        mySword = transform.GetChild(0).gameObject;
        currentState = SwordState.State1;
        previousState = currentState;
        currentQuat = transform.rotation;
        nextQuat = Quaternion.Euler(rot.x, rot.y, rot.z);
        travelRotSpeed = regTravelRotSpeed;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (currentState != SwordState.State5)
            {
                travelRotSpeed = attackTravelRotSpeed;
            }

            isAttacking = true;
            StopAllCoroutines();
            StartCoroutine(atkCoolDownTimer(0.2f));
        }
        if (!isAttacking && travelRotSpeed > regTravelRotSpeed)
        {
            travelRotSpeed -=  attackTravelRotSpeed * Time.deltaTime;
            if (travelRotSpeed <= regTravelRotSpeed)
            {
                travelRotSpeed = regTravelRotSpeed;
            }
        }
        if (canSpinSword )//|| swordAngle != 0.0f)
        {
            swordAngle += swordRotSpeed * Time.deltaTime;
            if (swordAngle >= 360)
            {
                swordAngle = 0.0f;
            }
            Vector3 eulerAngles = mySword.transform.rotation.eulerAngles;
            //mySword.transform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, swordAngle);
            mySword.transform.rotation = Quaternion.Euler(eulerAngles.x, swordAngle, eulerAngles.z);
        }
        //transform.rotation = Quaternion.Slerp(currentQuat, nextQuat, lerpTime);
        switch (currentState)
        {
            case SwordState.State1:
                {
                    transform.rotation = Quaternion.Slerp(currentQuat, nextQuat, lerpTime);
                    if (lerpTime >= 1)
                    {
                        lerpTime = 0;
                        currentQuat = nextQuat;
                        nextQuat = Quaternion.Euler(-182.777f, 152.118f, 75.77f);                  

                        if (isAttacking)
                        {
                            previousState = currentState;
                        }
                        else
                        {
                            previousState = SwordState.State1;
                        }
                        currentState = SwordState.State2;
                    }
                    break;
                }
            case SwordState.State2:
                {
                    transform.rotation = Quaternion.Slerp(currentQuat, nextQuat, lerpTime);
                    if (lerpTime >= 1)
                    {
                        lerpTime = 0;
                        currentQuat = nextQuat;
                        nextQuat = Quaternion.Euler(185f, -23.396f, 307.741f);

                        if (isAttacking)
                        {
                            previousState = currentState;
                        }
                        else
                        {
                            previousState = SwordState.State1;
                        }
                        currentState = SwordState.State3;
                    }
                    break;
                }
            case SwordState.State3:
                {
                    transform.rotation = Quaternion.Slerp(currentQuat, nextQuat, lerpTime);
                    if (lerpTime >= 1)
                    {
                        lerpTime = 0;
                        currentQuat = nextQuat;
                        nextQuat = Quaternion.Euler(0, 0, 0);

                        if (isAttacking)
                        {
                            previousState = currentState;
                        }
                        else
                        {
                            previousState = SwordState.State1;
                        }
                        currentState = SwordState.State4;
                    }
                    break;
                }
            case SwordState.State4:
                {
                    transform.rotation = Quaternion.Slerp(currentQuat, nextQuat, lerpTime);
                    if (lerpTime >= 1)
                    {
                        mySword.transform.rotation = Quaternion.Euler(-0.007f, -166.182f, 77.889f);
                        if (!isAttacking || comboNum == 3 || previousState == SwordState.State1)
                        {
                            lerpTime = 0;
                            currentQuat = nextQuat;
                            nextQuat = Quaternion.Euler(-178.984f, 74.12599f, -3.395996f);
                            comboNum = 0;
                            if (!isAttacking)
                            {
                                canSpinSword = false;
                            }
                            //canSpinSword = false;
                            //mySword.transform.rotation = Quaternion.Euler(0, -78.081f,0);
                            //mySword.transform.rotation = Quaternion.Euler(-0.007f, -166.182f, 77.889f); 

                            if (isAttacking)
                            {
                                previousState = currentState;
                            }
                            else
                            {
                                previousState = SwordState.State1;
                            }
                            currentState = SwordState.State1;
                        }
                        else
                        {
                            lerpTime = 0;
                            currentQuat = nextQuat;
                            nextQuat = Quaternion.Euler(0, -90, 0);
                            comboNum++;
                            canSpinSword = true;
                            travelRotSpeed = 2;
                            //travelRotSpeed = regTravelRotSpeed;

                            if (isAttacking)
                            {
                                previousState = currentState;
                            }
                            else
                            {
                                previousState = SwordState.State1;
                            }
                            currentState = SwordState.State5;
                        }     
                    }
                    break;
                }
            case SwordState.State5:
                {
                    transform.rotation = Quaternion.Slerp(currentQuat, nextQuat, lerpTime);
                    if (lerpTime >= 1)
                    {

                       lerpTime = 0;
                       currentQuat = nextQuat;
                       nextQuat = Quaternion.Euler(0, -260, 0);
                       travelRotSpeed = attackTravelRotSpeed;

                        if (isAttacking)
                        {
                            previousState = currentState;
                        }
                        else
                        {
                            previousState = SwordState.State1;
                        }
                        currentState = SwordState.State6;
                    }
                    break;
                }
            case SwordState.State6:
                {
                    transform.rotation = Quaternion.Slerp(currentQuat, nextQuat, lerpTime);

                    if (lerpTime >= 1)
                    {
                        lerpTime = 0;
                        currentQuat = nextQuat;
                        nextQuat = Quaternion.Euler(0, 0, 0);

                        if (isAttacking)
                        {
                            previousState = currentState;
                        }
                        else
                        {
                            previousState = SwordState.State1;
                        }
                        currentState = SwordState.State4;
                    }
                    
                    break;
                }

        }
        lerpTime += travelRotSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && isAttacking)
        {
            other.GetComponent<Enemy>().TakeDamage(baseDamage);
            mainCam.ShakeCamera(0.05f, 0.5f);
        }
        else if (other.tag == "EnemyBarrier" && isAttacking)
        {
            other.GetComponent<EnemyBarrier>().TakeDamage(baseDamage);
        }
    }
    IEnumerator atkCoolDownTimer(float time)
    {
        yield return new WaitForSeconds(time);
        isAttacking = false;
    }
}
