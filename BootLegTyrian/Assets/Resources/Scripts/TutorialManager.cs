using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    Player p1 = null;
    ShopScript shop;
    GameObject tutorialUI = null;
    GameObject curTutorial = null;


    GameObject botBackground;
    GameObject topBackground;

    //GameObject botBackgurond;
    //GameObject topBackgurond;

    public float strafeWaitTimer;
    public float turnWaitTimer;

    int firstInteraction = 0;
    int firstEnemy = 0;
    int firstStrafe = 0;
    int firstTurnAround = 0;
    int firstMoney = 0;

    int firstTimeStats = 0;
    int firstTimeShop = 0;
    int firstTimeUpgrades = 0;
    int firstTimeAbilities = 0;
    int firstTimeSubAbilities = 0;

    int firstSheild = 0;
    int firstMissle = 0;

    int numOfTutorials = 12;
    int numOfCompletedTutorials = 0;

    int curSlide = 1;

    bool tutorialInUse = false;
    bool shopIsActive = false;

    bool statsTutorial = false;
    bool shopTutorial = false;
    bool upgradeTutorial = false;
    bool abilitiesTutorial = false;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () 
    {
       
        if ((p1 == null) || (tutorialUI == null))
        {
            p1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            if (!p1.sandBoxMode)
            {
                tutorialUI = GameObject.FindGameObjectWithTag("TutorialUI");
                botBackground = tutorialUI.transform.GetChild(0).gameObject;
                topBackground = tutorialUI.transform.GetChild(1).gameObject;
            }
        }
        if (!p1.sandBoxMode)
        {
            if (p1.inHUBWorld && (shop == null))
            {
                shop = GameObject.FindGameObjectWithTag("ShopMenuUI").GetComponent<ShopScript>();
            }
            shopIsActive = (shop == null) ? false : true;
            Tutorials();

            if (numOfCompletedTutorials == numOfTutorials)
            {
                this.enabled = false;
            }
        }
	}
    void TurnOnTutorialBackground()
    {
        botBackground.gameObject.SetActive(true);
        topBackground.gameObject.SetActive(true);
    }
    void TurnOffTutorialBackground()
    {
        botBackground.gameObject.SetActive(false);
        topBackground.gameObject.SetActive(false);
    }
    void Tutorials()
    {
        if ((firstInteraction == 0) && p1.inHUBWorld)
        {
            if (curTutorial == null)
            {
                TurnOnTutorialBackground();
                curTutorial = tutorialUI.transform.GetChild(2).gameObject;
                curTutorial.SetActive(true);
            }

            if (Input.GetButtonDown("Strafe"))
            {
                firstInteraction++;
                numOfCompletedTutorials++;
                TurnOffTutorialBackground();
                curTutorial.SetActive(false);
                curTutorial = null;
            }
        }
        else if ((firstEnemy == 0) && !p1.inHUBWorld && !p1.sandBoxMode)
        {
            if (curTutorial == null)
            {
                TurnOnTutorialBackground();
                curTutorial = tutorialUI.transform.GetChild(3).gameObject;
                curTutorial.SetActive(true);
                Time.timeScale = 0.0f;
            }

            if ((Input.GetAxis("Fire1") < 0.0f) || Input.GetButton("Fire1"))
            {
                firstEnemy++;
                numOfCompletedTutorials++;
                TurnOffTutorialBackground();
                curTutorial.SetActive(false);
                curTutorial = null;
                Time.timeScale = 1.0f;
                StartCoroutine(StrafeWait(strafeWaitTimer));
            }
        }
        else if (firstStrafe == 1)
        {
            if (curTutorial == null)
            {
                TurnOnTutorialBackground();
                curTutorial = tutorialUI.transform.GetChild(4).gameObject;
                curTutorial.SetActive(true);
                Time.timeScale = 0.0f;
            }

            if (Input.GetButtonDown("Strafe"))
            {
                firstStrafe++;
                numOfCompletedTutorials++;
                TurnOffTutorialBackground();
                curTutorial.SetActive(false);
                curTutorial = null;
                Time.timeScale = 1.0f;
                StartCoroutine(TurnAroundWait(turnWaitTimer));
            }
        }
        else if (firstTurnAround == 1)
        {
            if (curTutorial == null)
            {
                TurnOnTutorialBackground();
                curTutorial = tutorialUI.transform.GetChild(5).gameObject;
                curTutorial.SetActive(true);
                Time.timeScale = 0.0f;
            }

            if (Input.GetButtonDown("TurnAround"))
            {
                firstTurnAround++;
                numOfCompletedTutorials++;
                TurnOffTutorialBackground();
                curTutorial.SetActive(false);
                curTutorial = null;
                Time.timeScale = 1.0f;
            }
        }
        else if ((firstMoney == 0) && p1.inHUBWorld && (firstEnemy > 0))
        {
            if (curTutorial == null)
            {
                TurnOnTutorialBackground();
                curTutorial = tutorialUI.transform.GetChild(6).gameObject;
                curTutorial.SetActive(true);
            }

            if (Input.GetButtonDown("Select"))
            {
                firstMoney++;
                numOfCompletedTutorials++;
                TurnOffTutorialBackground();
                curTutorial.SetActive(false);
                curTutorial = null;
            }
        }
        else if ((firstTimeStats == 0) && shopIsActive && (shop.statsUI.activeInHierarchy || statsTutorial) && shop.turnedOn)
        {
            if (curTutorial == null)
            {
                TurnOnTutorialBackground();
                curTutorial = tutorialUI.transform.GetChild(7).gameObject;
                curTutorial.SetActive(true);
                statsTutorial = true;
            }
            else if (!statsTutorial)
            {
                curTutorial.SetActive(false);
                curTutorial = null;
            }
                if (Input.GetButtonDown("Strafe") || Input.GetButtonDown("R1Button"))
            {
                firstTimeStats++;
                numOfCompletedTutorials++;
                TurnOffTutorialBackground();
                curTutorial.SetActive(false);
                curTutorial = null;
            }
        }
        else if ((firstTimeShop == 0) && shopIsActive && (shop.shopUI.activeInHierarchy || shopTutorial))
        {
            if (curTutorial == null)
            {
                TurnOnTutorialBackground();
                curTutorial = tutorialUI.transform.GetChild(8).gameObject;
                curTutorial.SetActive(true);
                shopTutorial = true;
            }
            else if (!shopTutorial)
            {
                curTutorial.SetActive(false);
                curTutorial = null;
            }

            if (Input.GetButtonDown("Strafe") || Input.GetButtonDown("L1Button") || Input.GetButtonDown("R1Button"))
            {
                firstTimeShop++;
                numOfCompletedTutorials++;
                TurnOffTutorialBackground();
                curTutorial.SetActive(false);
                curTutorial = null;
            }
        }
        else if ((firstTimeUpgrades == 0) && shopIsActive && (shop.upgradesUI.activeInHierarchy || upgradeTutorial))
        {
            if (curTutorial == null)
            {
                Time.timeScale = 0.0f;
                TurnOnTutorialBackground();
                curTutorial = tutorialUI.transform.GetChild(9).gameObject;
                curTutorial.SetActive(true);
                upgradeTutorial = true;
                shop.controlsOn = false;
            }
            else if (!upgradeTutorial)
            {
                curTutorial.SetActive(false);
                curTutorial = null;
                Time.timeScale = 1.0f;
            }

            if (!shop.subIconsHidden)
            {
                firstTimeUpgrades++;
                numOfCompletedTutorials++;
                TurnOffTutorialBackground();
                curTutorial.SetActive(false);
                curTutorial = null;

                firstTimeSubAbilities++;

                Time.timeScale = 1.0f;
                shop.controlsOn = true;
            }
        }
        else if ((firstTimeAbilities == 0) && shopIsActive && (shop.abilitiesUI.activeInHierarchy || abilitiesTutorial))
        {
            if (curTutorial == null)
            {
                TurnOnTutorialBackground();
                curTutorial = tutorialUI.transform.GetChild(10).gameObject;
                curTutorial.SetActive(true);
                abilitiesTutorial = true;
            }
            else if (!abilitiesTutorial)
            {
                curTutorial.SetActive(false);
                curTutorial = null;
            }

            if (Input.GetButtonDown("Strafe") || Input.GetButtonDown("L1Button"))
            {
                firstTimeAbilities++;
                numOfCompletedTutorials++;
                TurnOffTutorialBackground();
                curTutorial.SetActive(false);
                curTutorial = null;
            }
        }
        else if (firstTimeSubAbilities == 1)
        {
            if (curTutorial == null)
            {
                TurnOnTutorialBackground();
                curTutorial = tutorialUI.transform.GetChild(11).gameObject;
                curTutorial.SetActive(true);
            }

            if (Input.GetButtonDown("Strafe") || Input.GetButtonDown("L1Button") || Input.GetButtonDown("R1Button"))
            {
                firstTimeSubAbilities++;
                numOfCompletedTutorials++;
                TurnOffTutorialBackground();
                curTutorial.SetActive(false);
                curTutorial = null;
            }
        }
        else if ((firstSheild == 0) && p1.sheildComponentBought)
        {
            if (curTutorial == null)
            {
                TurnOnTutorialBackground();
                curTutorial = tutorialUI.transform.GetChild(12).gameObject;
                curTutorial.SetActive(true);
            }

            if (Input.GetButtonDown("Strafe") && curSlide == 1)
            {
                curTutorial.transform.GetChild(1).gameObject.SetActive(false);
                curTutorial.transform.GetChild(2).gameObject.SetActive(true);
                curSlide++;
            }
            else if ((Input.GetAxis("Fire1") > 0.0f) || Input.GetKey(KeyCode.R)) //<==========================gotta change
            {
                firstSheild++;
                numOfCompletedTutorials++;
                TurnOffTutorialBackground();
                curTutorial.SetActive(false);
                curTutorial = null;
            }
        }
        else if ((firstMissle == 0) && p1.guidedMissleBought && !p1.inHUBWorld && !p1.sandBoxMode)
        {
            if (curTutorial == null)
            {
                TurnOnTutorialBackground();
                curTutorial = tutorialUI.transform.GetChild(13).gameObject;
                curTutorial.SetActive(true);
                Time.timeScale = 0.0f;
            }

            if (Input.GetButtonDown("Fire2") && (Time.timeScale == 0.0f)) //<==========================gotta change
            {
                curTutorial.transform.GetChild(1).gameObject.SetActive(false);
                curTutorial.transform.GetChild(2).gameObject.SetActive(true);
                Time.timeScale = 1.0f;
            }
            else if (Input.GetButtonDown("Strafe"))
            {
                firstMissle++;
                numOfCompletedTutorials++;
                TurnOffTutorialBackground();
                curTutorial.SetActive(false);
                curTutorial = null;
            }
        }
    }
    IEnumerator StrafeWait(float time)
    {
        yield return new WaitForSeconds(time);
        firstStrafe++;
    }
    IEnumerator TurnAroundWait(float time)
    {
        yield return new WaitForSeconds(time);
        firstTurnAround++;
    }
}
