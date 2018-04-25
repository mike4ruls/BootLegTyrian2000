using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour {
    enum ShopState
    {
        StatsMenu,
        ShopMenu,
        UpgradesMenu,
        AbilitiesMenu
    };
    public AudioManager audioManager;

    ShopState curShopState;
    IconType curIconOpen;
    SubMenuIcon curSubMenu;
    SubIconType curSubIconOpen;


    Player p1;

    public GameObject statsUI;
    public GameObject shopUI;
    public GameObject upgradesUI;
    public GameObject abilitiesUI;
    public GameObject infoUI;
    public GameObject playerMoneyUI;
    public GameObject buyButtonUI;
    public GameObject TopBarUI;
    public Color textColor;
    public float maxHeight;
    public float minHeight;
    public float moveSpeed;
    public float brokenComponentRate;
    public bool turnedOn;
    [HideInInspector]
    public bool controlsOn;

    float valueIncrease;
    float subValueIncrease;

    RectTransform myRect;

    Button statsButton;
    Button shopButton;
    Button upgradesButton;
    Button abilitiesButton;

    Text playerMonetText;
    Image buyButton;

    Image linkerBar;
    Image firstSubIcon;
    Image secondSubIcon;

    Image iconDisplay;
    Text titleInfotext;
    Text titleLvlText;
    Text subTitleText;
    Text subTitleLvlText;
    Text descTitleText;
    Text descText;
    Image currencySymbol;
    Text costText;
    Text statsCurText;
    Text statsNextText;

    Vector4 fullAlpha;
    Vector4 noAlpha;
    Vector4 iconColor;

    [HideInInspector]
    public bool subIconsHidden;
    int flashCount, flashMaxCount;
    int baseCost, ratePerLvl, subBaseCost, subRatePerLvl, finalCost;
    // Use this for initialization
    void Start () {
        curShopState = ShopState.StatsMenu;
        curIconOpen = IconType.NONE;
        curSubIconOpen = SubIconType.NONE;
        subIconsHidden = true;
        finalCost = 0;
        flashMaxCount = 5;
        flashCount = 0;

        myRect = GetComponent<RectTransform>();

        p1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        fullAlpha = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        noAlpha = new Vector4(1.0f, 1.0f, 1.0f, 0.0f);

        statsButton = TopBarUI.transform.GetChild(0).GetComponent<Button>();
        shopButton = TopBarUI.transform.GetChild(1).GetComponent<Button>();
        upgradesButton = TopBarUI.transform.GetChild(2).GetComponent<Button>();
        abilitiesButton = TopBarUI.transform.GetChild(3).GetComponent<Button>();

        playerMonetText = playerMoneyUI.GetComponent<Text>();
        buyButton = buyButtonUI.GetComponent<Image>();

        linkerBar = infoUI.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        firstSubIcon = infoUI.transform.GetChild(1).GetChild(1).GetComponent<Image>();
        secondSubIcon = infoUI.transform.GetChild(1).GetChild(2).GetComponent<Image>();

        iconDisplay = infoUI.transform.GetChild(2).GetComponent<Image>();
        titleInfotext = infoUI.transform.GetChild(3).GetComponent<Text>();
        titleLvlText = infoUI.transform.GetChild(4).GetComponent<Text>();
        subTitleText = infoUI.transform.GetChild(5).GetComponent<Text>();
        subTitleLvlText = infoUI.transform.GetChild(6).GetComponent<Text>();
        descTitleText = infoUI.transform.GetChild(7).GetComponent<Text>();
        descText = infoUI.transform.GetChild(8).GetComponent<Text>();
        currencySymbol = infoUI.transform.GetChild(9).GetComponent<Image>();
        costText = infoUI.transform.GetChild(10).GetComponent<Text>();
        statsCurText = infoUI.transform.GetChild(11).GetComponent<Text>();
        statsNextText = infoUI.transform.GetChild(12).GetComponent<Text>();
        costText.text = "0";

        titleInfotext.color = textColor;
         titleLvlText.color = textColor;
        subTitleText.color = textColor;
        subTitleLvlText.color = textColor;
        descTitleText.color = textColor;
        descText.color = textColor;
        costText.color = textColor;
        playerMonetText.color = textColor;

        controlsOn = true;

        if (!turnedOn)
        {
            myRect.localPosition = new Vector3(0.0f, minHeight, 0.0f);
        }

    }
	
	// Update is called once per frame
	void Update () {
        playerMonetText.text = "" + p1.money;
        if (Input.GetButtonDown("Select"))
        {
            ToggleShopUI();
        }

        if (turnedOn)
        {
            if (myRect.localPosition.y < maxHeight)
            {
                myRect.localPosition = new Vector3(0.0f, myRect.localPosition.y + (moveSpeed * Time.deltaTime), 0.0f);

                if (myRect.localPosition.y >= maxHeight)
                {
                    myRect.localPosition = new Vector3(0.0f, maxHeight, 0.0f);
                }
            }
            if (Input.GetButtonDown("R1Button"))
            {
                //Debug.Log("we in here");
                switch (curShopState)
                {
                    case ShopState.StatsMenu:
                        {
                            
                            DisplayShopUI();
                            break;
                        }
                    case ShopState.ShopMenu:
                        {
                            DisplayUpgradesUI();
                            break;
                        }
                    case ShopState.UpgradesMenu:
                        {
                            DisplayAbilitiesUI();
                            break;
                        }
                }
            }
            if (Input.GetButtonDown("L1Button"))
            {
                switch (curShopState)
                {
                    case ShopState.ShopMenu:
                        {
                            DisplayStatsUI();
                            break;
                        }
                    case ShopState.UpgradesMenu:
                        {
                            DisplayShopUI();
                            break;
                        }
                    case ShopState.AbilitiesMenu:
                        {
                            DisplayUpgradesUI();
                            break;
                        }
                }
            }
        }

        if (!turnedOn && myRect.localPosition.y > minHeight)
        {
            myRect.localPosition = new Vector3(0.0f, myRect.localPosition.y - (moveSpeed * Time.deltaTime), 0.0f);

            if (myRect.localPosition.y <= minHeight)
            {
                myRect.localPosition = new Vector3(0.0f, minHeight, 0.0f);
            }
        }
    }
    public void DisplayTitleInfo(IconType type, Sprite icon, Vector4 color, string tleTxt, string tleLvlTxt, string desc, int baseC, int rate, float valRate)
    {
        curIconOpen = type;
        iconDisplay.sprite = icon;
        iconColor = color;
        iconDisplay.color = iconColor;
        titleInfotext.text = tleTxt;
        titleLvlText.text = tleLvlTxt;
        subTitleText.text = "";
        subTitleLvlText.text = "";
        descTitleText.text = "Description:";
        descText.text = desc;
        baseCost = baseC;
        ratePerLvl = rate;
        valueIncrease = valRate;

        audioManager.PlayButtonClicked();
        curSubIconOpen = SubIconType.NONE;

        DisplayPlayerLevel2Icon();
    }
    public void DisplayTitleInfo(IconType type, Sprite icon, Vector4 color, SubMenuIcon sub, string tleTxt, string tleLvlTxt, string desc, int baseC, int rate, float valRate)
    {
        curIconOpen = type;
        iconColor = color;
        iconDisplay.color = iconColor;
        iconDisplay.color = fullAlpha;
        curSubMenu = sub;
        titleInfotext.text = tleTxt;
        titleLvlText.text = tleLvlTxt;
        subTitleText.text = "";
        subTitleLvlText.text = "";
        descTitleText.text = "Description:";
        descText.text = desc;
        baseCost = baseC;
        ratePerLvl = rate;
        valueIncrease = valRate;

        audioManager.PlayButtonClicked();
        curSubIconOpen = SubIconType.NONE;

        if (DisplayPlayerLevel2Icon())
        {
            finalCost = 0;
            DisplaySubIcons();
        }
        else
        {
            HideSubIcons();
        }

    }
    public void DisplayFirstSubAbilitesInfo()
    {
        if (subIconsHidden) return;

        audioManager.PlayButtonClicked();
        subBaseCost = curSubMenu.firstBaseCost;
        subRatePerLvl = curSubMenu.firstRatePerLvl;
        curSubIconOpen = curSubMenu.myFirstType;
        subTitleText.text = curSubMenu.firstSubTitle;
        descTitleText.text = "Description:";
        descText.text = curSubMenu.firstSubDescription;
        subValueIncrease = curSubMenu.firstVauleIncreaseRate;

        DisplayPlayerLevel2SubIcon(curSubMenu.myFirstType);
    }
    public void DisplaySecondSubAbilitesInfo()
    {
        if (subIconsHidden) return;

        audioManager.PlayButtonClicked();
        subBaseCost = curSubMenu.secondBaseCost;
        subRatePerLvl = curSubMenu.secondRatePerLvl;
        curSubIconOpen = curSubMenu.mySecondType;
        subTitleText.text = curSubMenu.secondSubTitle;
        descTitleText.text = "Description:";
        descText.text = curSubMenu.secondSubDescription;
        subValueIncrease = curSubMenu.secondVauleIncreaseRate;

        DisplayPlayerLevel2SubIcon(curSubMenu.mySecondType);
    }
    bool DisplayPlayerLevel2Icon()
    {
        bool rendSub = true;
        statsCurText.text = "";
        statsNextText.text = "";

        switch (curIconOpen)
        {
            case IconType.NONE:
                {
                    rendSub = false;
                    break;
                }
            case IconType.HealthIcon:
                {
                    titleLvlText.text = "Lvl " + p1.healthLvl;
                    finalCost = (baseCost + (ratePerLvl * p1.healthLvl));

                    string title = "Max " + titleInfotext.text + ": ";
                    statsCurText.text = title + p1.maxHealth + "\nvvv";
                    statsNextText.text = "(+" + valueIncrease +")"+  title + (p1.maxHealth + valueIncrease);

                    rendSub = false;
                    break;
                }
            case IconType.DefenseIcon:
                {
                    titleLvlText.text = "Lvl " + p1.defenseLvl;
                    finalCost = (baseCost + (ratePerLvl * p1.defenseLvl));

                    string title = "Max " + titleInfotext.text + ": ";
                    statsCurText.text = title + p1.defense + "\nvvv";
                    statsNextText.text = "(+" + valueIncrease + ")" + title + (p1.defense + valueIncrease);

                    rendSub = false;
                    break;
                }
            case IconType.GuidedMissleIcon:
                {
                    if (p1.guidedMissleBought)
                    {
                        if (p1.guidingMissleBroken)
                        {
                            titleLvlText.text = "BROKEN";
                            finalCost = (baseCost + (int)((float)ratePerLvl * brokenComponentRate * (float)p1.guidingMissleLvl));

                            rendSub = false;
                        }
                        else
                        {
                            titleLvlText.text = "Lvl " + p1.guidingMissleLvl;
                        }
                    }
                    else
                    {
                        titleLvlText.text = "Not Bought";
                        finalCost = baseCost;
                        rendSub = false;
                    }   
                    break;
                }
            case IconType.SheildIcon:
                {
                    if (p1.sheildComponentBought)
                    {
                        if (p1.sheildComponentBroken)
                        {
                            titleLvlText.text = "BROKEN";
                            finalCost = (baseCost + (int)((float)ratePerLvl * brokenComponentRate * (float)p1.sheildComponentLvl));
                            rendSub = false;
                        }
                        else
                        {
                            titleLvlText.text = "Lvl " + p1.sheildComponentLvl;
                        }
                    }
                    else
                    {
                        titleLvlText.text = "Not Bought";
                        finalCost = baseCost;
                        rendSub = false;
                    }
                    break;
                }
            case IconType.FrontBlasterIcon:
                {
                    titleLvlText.text = "Lvl " + p1.frontBlasterLvl;
                    finalCost = baseCost;
                    break;
                }
            case IconType.LeftSideBlasterIcon:
                {
                    if (p1.leftSideBlasterBought)
                    {
                        if (p1.LeftSideBlasterBroken)
                        {
                            titleLvlText.text = "BROKEN";
                            finalCost = (baseCost + (int)((float)ratePerLvl * brokenComponentRate * (float)p1.leftSideBlasterLvl));
                            rendSub = false;
                        }
                        else
                        {
                            titleLvlText.text = "Lvl " + p1.leftSideBlasterLvl;

                        }
                    }
                    else
                    {
                        titleLvlText.text = "Not Bought";
                        finalCost = baseCost;
                        rendSub = false;
                    }
                    break;
                }
            case IconType.RightSideBlasterIcon:
                {
                    if (p1.rightSideBlasterBought)
                    {
                        if (p1.rightSideBlasterBroken)
                        {
                            titleLvlText.text = "BROKEN";
                            finalCost = (baseCost + (int)((float)ratePerLvl * brokenComponentRate * (float)p1.rightSideBlasterLvl));
                            rendSub = false;
                        }
                        else
                        {
                            titleLvlText.text = "Lvl " + p1.rightSideBlasterLvl;
                        }     
                    }
                    else
                    {
                        titleLvlText.text = "Not Bought";
                        finalCost = baseCost;
                        rendSub = false;
                    }
                    break;
                }
            case IconType.StrafeImmuneIcon:
                {
                    if (p1.strafeImmuneBought)
                    {
                        titleLvlText.text = "OWNED";
                        finalCost = 0;
                    }
                    else
                    {
                        titleLvlText.text = "Not Bought";
                        finalCost = baseCost;
                    }
                    rendSub = false;
                    break;
                }
            case IconType.SecondChanceIcon:
                {
                    if (p1.secondWindBought)
                    {
                        titleLvlText.text = "OWNED";
                        finalCost = 0;
                    }
                    else
                    {
                        titleLvlText.text = "Not Bought";
                        finalCost = baseCost;
                    }
                    rendSub = false;
                    break;
                }
            case IconType.BulletBounceBackIcon:
                {
                    if (p1.bouncingBulletsBought)
                    {
                        titleLvlText.text = "OWNED";
                        finalCost = 0;
                    }
                    else
                    {
                        titleLvlText.text = "Not Bought";
                        finalCost = baseCost;
                    }
                    rendSub = false;
                    break;
                }
            case IconType.NotSureIcon:
                {
                    if (p1.notSureBought)
                    {
                        titleLvlText.text = "OWNED";
                        finalCost = 0;
                    }
                    else
                    {
                        titleLvlText.text = "Not Bought";
                        finalCost = baseCost;
                    }
                    rendSub = false;
                    break;
                }
        }
        costText.text = "" + finalCost;
        return rendSub;
    }
    void DisplayPlayerLevel2SubIcon(SubIconType type)
    {
        switch (type)
        {
            case SubIconType.AttackDamageIcon:
                {
                    if (curIconOpen == IconType.GuidedMissleIcon)
                    {
                        subTitleLvlText.text = "Lvl " + p1.gMAttackDamageLvl;
                        finalCost = (subBaseCost + (subRatePerLvl * p1.gMAttackDamageLvl));

                        string title = "Max " + subTitleText.text + ": ";
                        statsCurText.text = title + p1.guidedMissleDamage + "\nvvv";
                        statsNextText.text = "(+" + subValueIncrease + ")" + title + (p1.guidedMissleDamage + subValueIncrease);

                    }
                    else if (curIconOpen == IconType.FrontBlasterIcon)
                    {
                        subTitleLvlText.text = "Lvl " + p1.fBAttackDamageLvl;
                        finalCost = (subBaseCost + (subRatePerLvl * p1.fBAttackDamageLvl));

                        string title = "Max " + subTitleText.text + ": ";
                        statsCurText.text = title + p1.frontDamage + "\nvvv";
                        statsNextText.text = "(+" + subValueIncrease + ")" + title + (p1.frontDamage + subValueIncrease);

                    }
                    else if (curIconOpen == IconType.LeftSideBlasterIcon)
                    {
                        subTitleLvlText.text = "Lvl " + p1.lBAttackDamageLvl;
                        finalCost = (subBaseCost + (subRatePerLvl * p1.lBAttackDamageLvl));

                        string title = "Max " + subTitleText.text + ": ";
                        statsCurText.text = title + p1.leftDamage + "\nvvv";
                        statsNextText.text = "(+" + subValueIncrease + ")" + title + (p1.leftDamage + subValueIncrease);
                    }
                    else if (curIconOpen == IconType.RightSideBlasterIcon)
                    {
                        subTitleLvlText.text = "Lvl " + p1.rBAttackDamageLvl;
                        finalCost = (subBaseCost + (subRatePerLvl * p1.rBAttackDamageLvl));

                        string title = "Max " + subTitleText.text + ": ";
                        statsCurText.text = title + p1.rightDamage + "\nvvv";
                        statsNextText.text = "(+" + subValueIncrease + ")" + title + (p1.rightDamage + subValueIncrease);
                    }
                    break;
                }
            case SubIconType.AttackSpeedIcon:
                {
                    if (curIconOpen == IconType.FrontBlasterIcon)
                    {
                        subTitleLvlText.text = "Lvl " + p1.fBAttackSpeedLvl;
                        finalCost = (subBaseCost + (subRatePerLvl * p1.fBAttackSpeedLvl));

                        string title = "Max " + subTitleText.text + ": ";
                        statsCurText.text = title + p1.frontAtkSpeed + "\nvvv";
                        statsNextText.text = "(-" + subValueIncrease + ")" + title + (p1.frontAtkSpeed - subValueIncrease);

                    }
                    else if (curIconOpen == IconType.LeftSideBlasterIcon)
                    {
                        subTitleLvlText.text = "Lvl " + p1.lBAttackSpeedLvl;
                        finalCost = (subBaseCost + (subRatePerLvl * p1.lBAttackSpeedLvl));

                        string title = "Max " + subTitleText.text + ": ";
                        statsCurText.text = title + p1.leftSideAtkSpeed + "\nvvv";
                        statsNextText.text = "(-" + subValueIncrease + ")" + title + (p1.leftSideAtkSpeed - subValueIncrease);
                    }
                    else if (curIconOpen == IconType.RightSideBlasterIcon)
                    {
                        subTitleLvlText.text = "Lvl " + p1.rBAttackSpeedLvl;
                        finalCost = (subBaseCost + (subRatePerLvl * p1.rBAttackSpeedLvl));

                        string title = "Max " + subTitleText.text + ": ";
                        statsCurText.text = title + p1.rightSideAtkSpeed + "\nvvv";
                        statsNextText.text = "(-" + subValueIncrease + ")" + title + (p1.rightSideAtkSpeed - subValueIncrease);
                    }
                    break;
                }
            case SubIconType.RechargeRateIcon:
                {
                    if (curIconOpen == IconType.GuidedMissleIcon)
                    {
                        subTitleLvlText.text = "Lvl " + p1.gMRechargeRateLvl;
                        finalCost = (subBaseCost + (subRatePerLvl * p1.gMRechargeRateLvl));

                        string title = "Max " + subTitleText.text + ": ";
                        statsCurText.text = title + p1.guidedMissleCD + "\nvvv";
                        statsNextText.text = "(-" + subValueIncrease + ")" + title + (p1.guidedMissleCD - subValueIncrease);
                    }
                    if (curIconOpen == IconType.SheildIcon)
                    {
                        subTitleLvlText.text = "Lvl " + p1.sCRechargeRate;
                        finalCost = (subBaseCost + (subRatePerLvl * p1.sCRechargeRate));

                        string title = "Max " + subTitleText.text + ": ";
                        statsCurText.text = title + p1.sheildRechargeRate + "\nvvv";
                        statsNextText.text = "(-" + subValueIncrease + ")" + title + (p1.sheildRechargeRate - subValueIncrease);
                    }
                    break;
                }
            case SubIconType.SheildPowerIcon:
                {

                    subTitleLvlText.text = "Lvl " + p1.sCPowerLvl;
                    finalCost = (subBaseCost + (subRatePerLvl * p1.sCPowerLvl));

                    string title = "Max " + subTitleText.text + ": ";
                    statsCurText.text = title + p1.maxSheild + "\nvvv";
                    statsNextText.text = "(+" + subValueIncrease + ")" + title + (p1.maxSheild + subValueIncrease);
                    break;
                }
        }
        costText.text = "" + finalCost;
    }
    public void DisplaySubIcons()
    {
        subIconsHidden = false;
        firstSubIcon.sprite = curSubMenu.firstSubIcon;
        secondSubIcon.sprite = curSubMenu.secondSubIcon;

        firstSubIcon.color = iconColor;
        secondSubIcon.color = iconColor;
        linkerBar.color = new Vector4(linkerBar.color.r, linkerBar.color.g, linkerBar.color.b, 1.0f);
    }
    public void HideSubIcons()
    {
        subIconsHidden = true;
        firstSubIcon.color = noAlpha;
        secondSubIcon.color = noAlpha;
        linkerBar.color = new Vector4(linkerBar.color.r, linkerBar.color.g, linkerBar.color.b, 0.0f);
    }
    public void DisplayStatsUI()
    {
        if (!controlsOn) return;

        if (curShopState != ShopState.StatsMenu)
        {
            HideCurrentMenuUI(curShopState);
            curShopState = ShopState.StatsMenu;

            var color = statsButton.colors;
            color.normalColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            statsButton.colors = color;
        }

        statsUI.SetActive(true);
    }
    public void DisplayShopUI()
    {
        if (!controlsOn) return;

        if (curShopState != ShopState.ShopMenu)
        {
            HideCurrentMenuUI(curShopState);
            curShopState = ShopState.ShopMenu;

            var color = shopButton.colors;
            color.normalColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            shopButton.colors = color;
        }

        shopUI.SetActive(true);
    }
    public void DisplayUpgradesUI()
    {
        if (!controlsOn) return;

        if (curShopState != ShopState.UpgradesMenu)
        {
            HideCurrentMenuUI(curShopState);
            curShopState = ShopState.UpgradesMenu;

            var color = upgradesButton.colors;
            color.normalColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            upgradesButton.colors = color;
        }

        upgradesUI.SetActive(true);
    }
    public void DisplayAbilitiesUI()
    {
        if (!controlsOn) return;

        if (curShopState != ShopState.AbilitiesMenu)
        {
            HideCurrentMenuUI(curShopState);
            curShopState = ShopState.AbilitiesMenu;

            var color = abilitiesButton.colors;
            color.normalColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            abilitiesButton.colors = color;
        }

        abilitiesUI.SetActive(true);
        abilitiesUI.GetComponent<AbilitiesUI>().TurnOn();
    }

    void HideStatsUI()
    {
        var color = statsButton.colors;
        color.normalColor = new Vector4(0.4f, 0.4f, 0.4f, 1.0f);
        statsButton.colors = color;

        statsUI.SetActive(false);
    }
    void HideShopUI()
    {
        var color = shopButton.colors;
        color.normalColor = new Vector4(0.4f, 0.4f, 0.4f, 1.0f);
        shopButton.colors = color;

        shopUI.SetActive(false);
    }
    void HideUpgradesUI()
    {
        var color = upgradesButton.colors;
        color.normalColor = new Vector4(0.4f, 0.4f, 0.4f, 1.0f);
        upgradesButton.colors = color;

        upgradesUI.SetActive(false);
    }
    void HideAbilitiesUI()
    {
        var color = abilitiesButton.colors;
        color.normalColor = new Vector4(0.4f, 0.4f, 0.4f, 1.0f);
        abilitiesButton.colors = color;

        abilitiesUI.GetComponent<AbilitiesUI>().TurnOff();
    }

    void ClearShopInfo()
    {
        curIconOpen = IconType.NONE;
        curSubIconOpen = SubIconType.NONE;

        iconDisplay.color = noAlpha;
        linkerBar.color = new Vector4(linkerBar.color.r, linkerBar.color.g, linkerBar.color.b, 0.0f);
        firstSubIcon.color = noAlpha;
        secondSubIcon.color = noAlpha;

        titleInfotext.text = "";
        titleLvlText.text = "";
        subTitleText.text = "";
        subTitleLvlText.text = "";
        descTitleText.text = "";
        descText.text = "";
        costText.text = "0";
        statsCurText.text = "";
        statsNextText.text = "";
    }

    void HideCurrentMenuUI(ShopState state)
    {
        ClearShopInfo();
        audioManager.PlayTabChange();
        switch (state)
        {
            case ShopState.StatsMenu:
                {
                    HideStatsUI();
                    break;
                }
            case ShopState.ShopMenu:
                {
                    HideShopUI();
                    break;
                }
            case ShopState.UpgradesMenu:
                {
                    HideUpgradesUI();
                    break;
                }
            case ShopState.AbilitiesMenu:
                {
                    HideAbilitiesUI();
                    break;
                }
        }
    }
    public void ToggleShopUI()
    {
        if (controlsOn)
        {
            turnedOn = turnedOn ? false : true;
        }
    }
    public void Buy()
    {
        if (p1.money < finalCost)
        {
            flashCount = 0;
            StartCoroutine(FlashRed(0.08f));
            audioManager.PlayBuyFailed();
            return;
        }
       
        if (subIconsHidden)
        {
            switch (curIconOpen)
            {
                case IconType.HealthIcon:
                    {
                        p1.healthLvl++;
                        p1.maxHealth += valueIncrease;
                        p1.health = p1.maxHealth;
                        p1.money -= finalCost;
                        audioManager.PlayBuySucceed();
                        break;
                    }
                case IconType.DefenseIcon:
                    {
                        p1.defenseLvl++;
                        p1.defense += valueIncrease;
                        p1.money -= finalCost;
                        audioManager.PlayBuySucceed();
                        break;
                    }
                case IconType.GuidedMissleIcon:
                    {
                        if (!p1.guidedMissleBought)
                        {
                            p1.guidingMissleLvl++;
                            p1.guidedMissleBought = true;
                            p1.money -= finalCost;
                            audioManager.PlayBuySucceed();
                        }
                        break;
                    }
                case IconType.SheildIcon:
                    {
                        if (!p1.sheildComponentBought)
                        {
                            p1.sheildComponentLvl++;
                            p1.sheildComponentBought = true;
                            p1.maxSheild = 50;
                            p1.money -= finalCost;
                            audioManager.PlayBuySucceed();
                        }
                        break;
                    }
                case IconType.LeftSideBlasterIcon:
                    {
                        if (!p1.leftSideBlasterBought)
                        {
                            p1.leftSideBlasterLvl++;
                            p1.leftSideBlasterBought = true;
                            p1.money -= finalCost;
                            audioManager.PlayBuySucceed();
                        }
                        break;
                    }
                case IconType.RightSideBlasterIcon:
                    {
                        if (!p1.rightSideBlasterBought)
                        {
                            p1.rightSideBlasterLvl++;
                            p1.rightSideBlasterBought = true;
                            p1.money -= finalCost;
                            audioManager.PlayBuySucceed();
                        }
                        break;
                    }
                case IconType.StrafeImmuneIcon:
                    {
                        if (!p1.strafeImmuneBought)
                        {
                            p1.strafeImmuneBought = true;
                            p1.money -= finalCost;
                            audioManager.PlayBuySucceed();
                        }
                        break;
                    }
                case IconType.SecondChanceIcon:
                    {
                        if (!p1.secondWindBought)
                        {
                            p1.secondWindBought = true;
                            p1.money -= finalCost;
                            audioManager.PlayBuySucceed();
                        }
                        break;
                    }
                case IconType.BulletBounceBackIcon:
                    {
                        if (!p1.bouncingBulletsBought)
                        {
                            p1.bouncingBulletsBought = true;
                            p1.money -= finalCost;
                            audioManager.PlayBuySucceed();
                        }
                        break;
                    }
                case IconType.NotSureIcon:
                    {
                        if (!p1.notSureBought)
                        {
                            p1.notSureBought = true;
                            p1.money -= finalCost;
                            audioManager.PlayBuySucceed();
                        }
                        break;
                    }
            }
            finalCost = 0;
        }
        else
        {
            p1.money -= finalCost;
            switch (curSubIconOpen)
            {
                case SubIconType.AttackDamageIcon:
                    {
                        if (curIconOpen == IconType.GuidedMissleIcon)
                        {
                            //p1.guidedMissleDamage += 10;
                            p1.guidedMissleDamage += (int)subValueIncrease;
                            p1.gMAttackDamageLvl++;
                            audioManager.PlayBuySucceed();
                        }
                        else if (curIconOpen == IconType.FrontBlasterIcon)
                        {
                            //p1.frontDamage += 1;
                            p1.frontDamage += (int)subValueIncrease;
                            p1.fBAttackDamageLvl++;
                            audioManager.PlayBuySucceed();
                        }
                        else if (curIconOpen == IconType.LeftSideBlasterIcon)
                        {
                            //p1.leftDamage += 1;
                            p1.leftDamage += (int)subValueIncrease;
                            p1.lBAttackDamageLvl++;
                            audioManager.PlayBuySucceed();
                        }
                        else if (curIconOpen == IconType.RightSideBlasterIcon)
                        {
                            //p1.rightDamage += 1;
                            p1.rightDamage += (int)subValueIncrease;
                            p1.rBAttackDamageLvl++;
                            audioManager.PlayBuySucceed();
                        }
                        break;
                    }
                case SubIconType.AttackSpeedIcon:
                    {
                        if (curIconOpen == IconType.FrontBlasterIcon)
                        {
                            //p1.frontAtkSpeed -= 0.01f;
                            p1.frontAtkSpeed -= subValueIncrease;
                            p1.fBAttackSpeedLvl++;
                            audioManager.PlayBuySucceed();
                        }
                        else if (curIconOpen == IconType.LeftSideBlasterIcon)
                        {
                            //p1.leftSideAtkSpeed -= 0.01f;
                            p1.leftSideAtkSpeed -= subValueIncrease;
                            p1.lBAttackSpeedLvl++;
                            audioManager.PlayBuySucceed();
                        }
                        else if (curIconOpen == IconType.RightSideBlasterIcon)
                        {
                            //p1.rightSideAtkSpeed -= 0.01f;
                            p1.rightSideAtkSpeed -= subValueIncrease;
                            p1.rBAttackSpeedLvl++;
                            audioManager.PlayBuySucceed();
                        }
                        break;
                    }
                case SubIconType.RechargeRateIcon:
                    {
                        if (curIconOpen == IconType.GuidedMissleIcon)
                        {
                            //p1.guidedMissleCD -= 0.01f;
                            p1.guidedMissleCD -= subValueIncrease;
                            p1.gMRechargeRateLvl++;
                            audioManager.PlayBuySucceed();
                        }
                        if (curIconOpen == IconType.SheildIcon)
                        {
                            //p1.sheildRechargeRate -= 0.02f;
                            p1.sheildRechargeRate -= subValueIncrease;
                            p1.sCRechargeRate++;
                            audioManager.PlayBuySucceed();
                        }
                        break;
                    }
                case SubIconType.SheildPowerIcon:
                    {
                        //p1.maxSheild += 10;
                        p1.maxSheild += subValueIncrease;
                        p1.sCPowerLvl++;
                        audioManager.PlayBuySucceed();
                        break;
                    }
            }
            
        }
        p1.CalculateFinalStats();
        if (DisplayPlayerLevel2Icon())
        {
            DisplayPlayerLevel2SubIcon(curSubIconOpen);
            DisplaySubIcons();
        }
        else
        {
            HideSubIcons();
        }
    }
    IEnumerator FlashRed(float time)
    {
        playerMonetText.color = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
        buyButton.color = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);

        yield return new WaitForSeconds(time);

        playerMonetText.color = textColor;
        buyButton.color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);

        yield return new WaitForSeconds(time);
        flashCount++;

        if (flashCount <= flashMaxCount)
        {
            StartCoroutine(FlashRed(time));
        }
    }
    }

