using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheildBarUI : MonoBehaviour {
    Player p1;
    float topDisplaySheild;
    float botDisplaySheild;

    RectTransform backSheildUI;
    RectTransform frontSheildUI;
    RectTransform topDisplaySheildUI;
    RectTransform botDisplaySheildUI;

    Vector3 topSheildScale;
    Vector3 botSheildScale;

    bool turnedOff;

    float backBaseScale;
    float frontBaseScale;
    float topBaseScale;
    float botBaseScale;

    float lerpTime = 0.0f;

    // Use this for initialization
    void Start () {
        p1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        backSheildUI = transform.GetChild(0).GetComponent<RectTransform>();
        frontSheildUI = transform.GetChild(1).GetComponent<RectTransform>();
        botDisplaySheildUI = transform.GetChild(2).GetComponent<RectTransform>();
        topDisplaySheildUI = transform.GetChild(3).GetComponent<RectTransform>();

        backBaseScale = backSheildUI.localScale.x;
        frontBaseScale = frontSheildUI.localScale.x;
        topBaseScale = topDisplaySheildUI.localScale.x;
        botBaseScale = botDisplaySheildUI.localScale.x;

        Init();
    }
	public void Init()
    {
        topDisplaySheild = p1.sheild;
        botDisplaySheild = p1.sheild;

        backSheildUI = transform.GetChild(0).GetComponent<RectTransform>();
        frontSheildUI = transform.GetChild(1).GetComponent<RectTransform>();
        botDisplaySheildUI = transform.GetChild(2).GetComponent<RectTransform>();
        topDisplaySheildUI = transform.GetChild(3).GetComponent<RectTransform>();

        if (backSheildUI.localScale.x < 3.0f)
        {

            float offset = p1.sCPowerLvl * 0.01f;

            backSheildUI.localScale = new Vector3(backBaseScale + offset, backSheildUI.localScale.y, backSheildUI.localScale.z);
            frontSheildUI.localScale = new Vector3(frontBaseScale + offset, frontSheildUI.localScale.y, frontSheildUI.localScale.z);
            botDisplaySheildUI.localScale = new Vector3(botBaseScale + offset, botDisplaySheildUI.localScale.y, botDisplaySheildUI.localScale.z);
            topDisplaySheildUI.localScale = new Vector3(topBaseScale + offset, topDisplaySheildUI.localScale.y, topDisplaySheildUI.localScale.z);
            //Debug.Log(backSheildUI.localScale.x + offset);
        }

        topSheildScale = topDisplaySheildUI.localScale;
        botSheildScale = botDisplaySheildUI.localScale;

        turnedOff = false;
    }
	// Update is called once per frame
	void Update ()
    {
        if (!p1.sheildComponentBought && !turnedOff)
        {
            TurnOffUI();
            turnedOff = true;
        }
        if (p1.sheildComponentBought && turnedOff)
        {
            TurnOnUI();
            turnedOff = false;
        }
        if (!turnedOff)
        {
            if (p1.sheild < botDisplaySheild)
            {
                botDisplaySheild -= 20.0f * Time.deltaTime;
                if (botDisplaySheild < p1.sheild)
                {
                    botDisplaySheild = p1.sheild;
                }
            }
            if (p1.sheild > topDisplaySheild)
            {
                topDisplaySheild += 20.0f * Time.deltaTime;
                if (topDisplaySheild > p1.sheild)
                {
                    topDisplaySheild = p1.sheild;
                }
            }
            if (p1.sheildComponentBought)
            {
                topDisplaySheildUI.localScale = new Vector3(topSheildScale.x * (topDisplaySheild / p1.maxSheild), topSheildScale.y, topSheildScale.z);
                botDisplaySheildUI.localScale = new Vector3(botSheildScale.x * (botDisplaySheild / p1.maxSheild), botSheildScale.y, botSheildScale.z);
            }
            topDisplaySheild = p1.sheild;
        }
        if (!p1.canRepairSheild)
        {
            botDisplaySheildUI.localScale = new Vector3(botSheildScale.x * lerpTime, botSheildScale.y, botSheildScale.z);
            lerpTime += (Time.deltaTime * p1.sheildWaitTime * 0.1f);
        }
        else
        {
            lerpTime = 0.0f;
        }
    }
    public void TurnOnUI()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(true);
    }
    public void TurnOffUI()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
    }
}
