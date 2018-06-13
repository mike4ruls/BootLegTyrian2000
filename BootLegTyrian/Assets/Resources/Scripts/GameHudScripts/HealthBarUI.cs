using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    Player p1;
    float topDisplayHealth;
    float botDisplayHealth;

    RectTransform backHealthUI;
    RectTransform frontHealthUI;
    RectTransform topDisplayHealthUI;
    RectTransform botDisplayHealthUI;

    Vector3 topHealthScale;
    Vector3 botHealthScale;

    float backBaseScale;
    float frontBaseScale;
    float topBaseScale;
    float botBaseScale;

    // Use this for initialization
    void Start()
    {
        p1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        backHealthUI = transform.GetChild(0).GetComponent<RectTransform>();
        frontHealthUI = transform.GetChild(1).GetComponent<RectTransform>();
        botDisplayHealthUI = transform.GetChild(2).GetComponent<RectTransform>();
        topDisplayHealthUI = transform.GetChild(3).GetComponent<RectTransform>();

        backBaseScale = backHealthUI.localScale.x;
        frontBaseScale = frontHealthUI.localScale.x;
        topBaseScale = topDisplayHealthUI.localScale.x;
        botBaseScale = botDisplayHealthUI.localScale.x;

        Init();
    }
    public void Init()
    {
        topDisplayHealth = p1.health;
        botDisplayHealth = p1.health;


        backHealthUI = transform.GetChild(0).GetComponent<RectTransform>();
        frontHealthUI = transform.GetChild(1).GetComponent<RectTransform>();
        botDisplayHealthUI = transform.GetChild(2).GetComponent<RectTransform>();
        topDisplayHealthUI = transform.GetChild(3).GetComponent<RectTransform>();

        if (backHealthUI.localScale.x < 2.0f)
        {
            float offset = p1.healthLvl * 0.01f;

            backHealthUI.localScale = new Vector3(backBaseScale + offset, backHealthUI.localScale.y, backHealthUI.localScale.z);
            frontHealthUI.localScale = new Vector3(frontBaseScale + offset, frontHealthUI.localScale.y, frontHealthUI.localScale.z);
            botDisplayHealthUI.localScale = new Vector3(botBaseScale + offset, botDisplayHealthUI.localScale.y, botDisplayHealthUI.localScale.z);
            topDisplayHealthUI.localScale = new Vector3(topBaseScale + offset, topDisplayHealthUI.localScale.y, topDisplayHealthUI.localScale.z);

            //Debug.Log(backHealthUI.localScale.x + offset);
        }


        topHealthScale = topDisplayHealthUI.localScale;
        botHealthScale = botDisplayHealthUI.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (p1.health < botDisplayHealth)
        {
            botDisplayHealth -= 20.0f * Time.deltaTime;
            if (botDisplayHealth < p1.health)
            {
                botDisplayHealth = p1.health;
            }
        }
        if (p1.health > topDisplayHealth)
        {
            topDisplayHealth += 20.0f * Time.deltaTime;
            if (topDisplayHealth > p1.health)
            {
                topDisplayHealth = p1.health;
            }

        }
        topDisplayHealthUI.localScale = new Vector3(topHealthScale.x * (topDisplayHealth / p1.maxHealth), topHealthScale.y, topHealthScale.z);
        botDisplayHealthUI.localScale = new Vector3(botHealthScale.x * (botDisplayHealth / p1.maxHealth), botHealthScale.y, botHealthScale.z);

        topDisplayHealth = p1.health;
    }
}