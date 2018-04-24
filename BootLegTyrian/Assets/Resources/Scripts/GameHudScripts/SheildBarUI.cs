using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheildBarUI : MonoBehaviour {
    Player p1;
    float topDisplaySheild;
    float botDisplaySheild;

    RectTransform topDisplaySheildUI;
    RectTransform botDisplaySheildUI;

    Vector3 topSheildScale;
    Vector3 botSheildScale;

    bool turnedOff;

    // Use this for initialization
    void Start () {
        p1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        Init();
    }
	public void Init()
    {
        topDisplaySheild = p1.sheild;
        botDisplaySheild = p1.sheild;

        topDisplaySheildUI = transform.GetChild(3).GetComponent<RectTransform>();
        botDisplaySheildUI = transform.GetChild(2).GetComponent<RectTransform>();

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
