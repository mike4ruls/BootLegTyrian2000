using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShakeInfo : MonoBehaviour {
    public float timerCD;
    public float shakeAmmount;
    public bool active;
    // Use this for initialization
    public CamShakeInfo(float shkTime, float shkAm)
    {
        timerCD = shkTime;
        shakeAmmount = shkAm;
        active = false;
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void CountDown(float deltaTime) { timerCD -= deltaTime; }
    public void SetTimerCD(float cd) { timerCD = cd; }
    public void SetShakeAmmount(float shkAmt) { shakeAmmount = shkAmt; }
    public void SetActive(bool act) { active = act; }
}
