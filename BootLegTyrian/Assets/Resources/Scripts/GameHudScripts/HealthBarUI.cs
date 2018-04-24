using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    Player p1;
    float topDisplayHealth;
    float botDisplayHealth;

    RectTransform topDisplayHealthUI;
    RectTransform botDisplayHealthUI;

    Vector3 topHealthScale;
    Vector3 botHealthScale;


    // Use this for initialization
    void Start()
    {
        p1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        Init();
    }
    public void Init()
    {
        topDisplayHealth = p1.health;
        botDisplayHealth = p1.health;

        topDisplayHealthUI = transform.GetChild(3).GetComponent<RectTransform>();
        botDisplayHealthUI = transform.GetChild(2).GetComponent<RectTransform>();

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