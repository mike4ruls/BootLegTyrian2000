using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum IconType
{
    NONE,
    HealthIcon,
    DefenseIcon,
    GuidedMissleIcon,
    SheildIcon,
    FrontBlasterIcon,
    LeftSideBlasterIcon,
    RightSideBlasterIcon,
    StrafeImmuneIcon,
    SecondChanceIcon,
    BulletBounceBackIcon,
    NotSureIcon
}
public class MenuIconScript : MonoBehaviour {

    Image myImage;
    Button myButton;

    public IconType myIconType;
    public string title;
    [TextArea(3, 3)]
    public string description;

    public int baseCost, ratePerLvl;
    public float vauleIncreaseRate;

    public bool hasSubIcons;

    ShopScript shop;
	// Use this for initialization
	void Start () {
        shop = GameObject.FindGameObjectWithTag("ShopMenuUI").GetComponent<ShopScript>();
        myImage = GetComponent<Image>();
        myButton = GetComponent<Button>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DisplayInfo()
    {
        if (hasSubIcons)
        {
            shop.DisplayTitleInfo(myIconType, myImage.sprite, myButton.colors.highlightedColor, GetComponent<SubMenuIcon>(), title, "Lvl 1", description, baseCost, ratePerLvl, vauleIncreaseRate);
        }
        shop.DisplayTitleInfo(myIconType, myImage.sprite, myButton.colors.highlightedColor, title, "Lvl 1", description, baseCost, ratePerLvl, vauleIncreaseRate);
    }
}
