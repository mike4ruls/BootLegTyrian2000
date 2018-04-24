using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum SubIconType
{
    NONE,
    AttackDamageIcon,
    RechargeRateIcon,
    AttackSpeedIcon,
    SheildPowerIcon,
}
public class SubMenuIcon : MonoBehaviour {

    public SubIconType myFirstType;
    public SubIconType mySecondType;

    public Sprite firstSubIcon;
    public Sprite secondSubIcon;

    public string firstSubTitle;
    [TextArea(3, 3)]
    public string firstSubDescription;
    public int firstBaseCost, firstRatePerLvl;
    public float firstVauleIncreaseRate;
    public string secondSubTitle;
    [TextArea(3, 3)]
    public string secondSubDescription;
    public int secondBaseCost, secondRatePerLvl;
    public float secondVauleIncreaseRate;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
