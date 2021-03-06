﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ImmortalGameManager : MonoBehaviour {

    public static ImmortalGameManager GM;
    public static Player p1Copy;

    public static List<Worlds> allWorlds;
    public static List<SubLevelScript>[] allSubLevels;

    public static SubLevelScript levelInfo;
    public static SpawnerManagerExtended levelSpawner;

    Vector3 lastWorldPos;
    Vector3 lastLevelPos;

    public bool tutorialOn = true;
    public bool normalModeOn = true;
    public bool cockPitModeOn = false;
    bool insideWorld = false;
    int startOffMoney = 0;
    int numOfWorlds = 10;
    bool firstStartedUp = true;
    bool hasAplayerLoaded = false;
    int score;
    int worldNumber;
    // Use this for initialization
    void Start () {

    }

    void Awake()
    {
        //Debug.Log("Is Awake");
        if (GM != null)
        {
           // Debug.Log("Destory GM");
            GameObject.Destroy(GM.gameObject);
        }
            
        else
        {
            GM = this;
            if (GM.firstStartedUp)
            {
                //Debug.Log("FirstStartUp");
                p1Copy = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                allWorlds = new List<Worlds>();
                allSubLevels = new List<SubLevelScript>[numOfWorlds];
                for (int i = 0; i < numOfWorlds; i++)
                {
                    allSubLevels[i] = new List<SubLevelScript>();
                }
                levelInfo = new SubLevelScript();
                levelSpawner = new SpawnerManagerExtended();
                if (!tutorialOn)
                {
                    GM.gameObject.GetComponent<TutorialManager>().enabled = false;
                }
                if (!normalModeOn)
                {
                    startOffMoney = 5000;
                }
                
                GM.firstStartedUp = false;
                GM.lastLevelPos = Vector3.zero;
                GM.lastWorldPos = Vector3.zero;

                SetCamMode(0);
            }
        }
            

        //GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(). = player;
        DontDestroyOnLoad(this);
    }
        // Update is called once per frame
        void Update () {
		
	}
    public static void SetCamMode(float zOffset)
    {
        if (GM.cockPitModeOn)
        {
            GameCamera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameCamera>();
            cam.camMode = GameCamera.CameraMode.CockPit;
            Vector3 camAngles = cam.transform.rotation.eulerAngles;
            cam.transform.rotation = Quaternion.Euler(60, camAngles.y, camAngles.z);
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z + zOffset);
            cam.CamModeInit();
        } 
    }
    public static void LoadPlayerInfo()
    {
        if (!GM.hasAplayerLoaded)
        {
            Player p1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            p1.money = GM.startOffMoney;
            return;
        }
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        player.frontDamage = p1Copy.frontDamage;
        player.leftDamage = p1Copy.leftDamage;
        player.rightDamage = p1Copy.rightDamage;

        player.frontAtkSpeed = p1Copy.frontAtkSpeed;
        player.leftSideAtkSpeed = p1Copy.leftSideAtkSpeed;
        player.rightSideAtkSpeed = p1Copy.rightSideAtkSpeed;


        player.sheildComponentBought = p1Copy.sheildComponentBought;
        player.guidedMissleBought = p1Copy.guidedMissleBought;
        player.leftSideBlasterBought = p1Copy.leftSideBlasterBought;
        player.rightSideBlasterBought = p1Copy.rightSideBlasterBought;
        player.strafeImmuneBought = p1Copy.strafeImmuneBought;
        player.secondWindBought = p1Copy.secondWindBought;
        player.bouncingBulletsBought = p1Copy.bouncingBulletsBought;
        player.notSureBought = p1Copy.notSureBought;

        player.sheildComponentBroken = p1Copy.sheildComponentBroken;
        player.guidingMissleBroken = p1Copy.guidingMissleBroken;
        player.LeftSideBlasterBroken = p1Copy.LeftSideBlasterBroken;
        player.rightSideBlasterBroken = p1Copy.rightSideBlasterBroken;

        player.playerLvl = p1Copy.playerLvl;
        player.healthLvl = p1Copy.healthLvl;
        player.defenseLvl = p1Copy.defenseLvl;
        player.guidingMissleLvl = p1Copy.guidingMissleLvl;
        player.gMAttackDamageLvl = p1Copy.gMAttackDamageLvl;
        player.gMRechargeRateLvl = p1Copy.gMRechargeRateLvl;
        player.sheildComponentLvl = p1Copy.sheildComponentLvl;
        player.sCPowerLvl = p1Copy.sCPowerLvl;
        player.sCRechargeRate = p1Copy.sCRechargeRate;
        player.frontBlasterLvl = p1Copy.frontBlasterLvl;
        player.fBAttackDamageLvl = p1Copy.fBAttackDamageLvl;
        player.fBAttackSpeedLvl = p1Copy.fBAttackSpeedLvl;
        player.leftSideBlasterLvl = p1Copy.leftSideBlasterLvl;
        player.lBAttackDamageLvl = p1Copy.lBAttackDamageLvl;
        player.lBAttackSpeedLvl = p1Copy.lBAttackSpeedLvl;
        player.rightSideBlasterLvl = p1Copy.rightSideBlasterLvl;
        player.rBAttackDamageLvl = p1Copy.rBAttackDamageLvl;
        player.rBAttackSpeedLvl = p1Copy.rBAttackSpeedLvl;

        player.maxHealth = p1Copy.maxHealth;
        player.health = p1Copy.health;

        player.maxSheild = p1Copy.maxSheild;
        player.sheild = p1Copy.sheild;

        player.money = p1Copy.money;

        GameObject.FindGameObjectWithTag("HealthBarUI").GetComponent<HealthBarUI>().Init();
        GameObject.FindGameObjectWithTag("SheildBarUI").GetComponent<SheildBarUI>().Init();
    }
    public static void SavePlayerInfo()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        p1Copy.frontDamage = player.frontDamage;
        p1Copy.leftDamage = player.leftDamage;
        p1Copy.rightDamage = player.rightDamage;

        p1Copy.frontAtkSpeed = player.frontAtkSpeed;
        p1Copy.leftSideAtkSpeed = player.leftSideAtkSpeed;
        p1Copy.rightSideAtkSpeed = player.rightSideAtkSpeed;

        p1Copy.sheildComponentBought = player.sheildComponentBought;
        p1Copy.guidedMissleBought = player.guidedMissleBought;
        p1Copy.leftSideBlasterBought = player.leftSideBlasterBought;
        p1Copy.rightSideBlasterBought = player.rightSideBlasterBought;
        p1Copy.strafeImmuneBought = player.strafeImmuneBought;
        p1Copy.secondWindBought = player.secondWindBought;
        p1Copy.bouncingBulletsBought = player.bouncingBulletsBought;
        p1Copy.notSureBought = player.notSureBought;

        p1Copy.sheildComponentBroken = player.sheildComponentBroken;
        p1Copy.guidingMissleBroken = player.guidingMissleBroken;
        p1Copy.LeftSideBlasterBroken = player.LeftSideBlasterBroken;
        p1Copy.rightSideBlasterBroken = player.rightSideBlasterBroken;

        p1Copy.playerLvl = player.playerLvl;
        p1Copy.healthLvl = player.healthLvl;
        p1Copy.defenseLvl = player.defenseLvl;
        p1Copy.guidingMissleLvl = player.guidingMissleLvl;
        p1Copy.gMAttackDamageLvl = player.gMAttackDamageLvl;
        p1Copy.gMRechargeRateLvl = player.gMRechargeRateLvl;
        p1Copy.sheildComponentLvl = player.sheildComponentLvl;
        p1Copy.sCPowerLvl = player.sCPowerLvl;
        p1Copy.sCRechargeRate = player.sCRechargeRate;
        p1Copy.frontBlasterLvl = player.frontBlasterLvl;
        p1Copy.fBAttackDamageLvl = player.fBAttackDamageLvl;
        p1Copy.fBAttackSpeedLvl = player.fBAttackSpeedLvl;
        p1Copy.leftSideBlasterLvl = player.leftSideBlasterLvl;
        p1Copy.lBAttackDamageLvl = player.lBAttackDamageLvl;
        p1Copy.lBAttackSpeedLvl = player.lBAttackSpeedLvl;
        p1Copy.rightSideBlasterLvl = player.rightSideBlasterLvl;
        p1Copy.rBAttackDamageLvl = player.rBAttackDamageLvl;
        p1Copy.rBAttackSpeedLvl = player.rBAttackSpeedLvl;

        p1Copy.maxHealth = player.maxHealth;
        p1Copy.health = player.health;

        p1Copy.maxSheild = player.maxSheild;
        p1Copy.sheild = player.sheild;

        p1Copy.money = player.money;

        GM.hasAplayerLoaded = true;
    }
    public static void LoadWorldLevels()
    {
        GameObject worlds = GameObject.FindGameObjectWithTag("Worlds");

        if (allWorlds.Count == 0)
        {
            for (int i = 0; i < worlds.transform.childCount; i++)
            {
                allWorlds.Add(worlds.transform.GetChild(i).GetComponent<Worlds>());
            }

            GM.lastWorldPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            SetCamMode(-20);
            return;
        }
        for (int i = 0; i < GM.numOfWorlds; i++)
        {
            worlds.transform.GetChild(i).GetComponent<Worlds>().completion = allWorlds[i].completion;
            worlds.transform.GetChild(i).GetComponent<Worlds>().Init();          
        }
        GM.insideWorld = false;
        GameObject.FindGameObjectWithTag("Player").transform.position = GM.lastWorldPos;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameCamera>().ReadjustCam();
        SetCamMode(-20);
    }
    public static void LoadSubLevels(int worldNum)
    {
        GameObject levels = GameObject.FindGameObjectWithTag("Levels");

        GM.worldNumber = worldNum;

        if (allSubLevels[worldNum].Count == 0)
        {
            for(int i = 0; i < levels.transform.childCount; i++)
            {
                allSubLevels[worldNum].Add(levels.transform.GetChild(i).GetComponent<SubLevelScript>());
            }
            Vector3 homeStarPos = GameObject.FindGameObjectWithTag("HomeStar").transform.position;
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            GM.lastLevelPos = new Vector3(homeStarPos.x, playerObj.transform.position.y, homeStarPos.z + 5);
            playerObj.transform.position = GM.lastLevelPos;
            GM.insideWorld = true;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameCamera>().ReadjustCam();
            SetCamMode(-20);
            return;
        }
        for (int i = 0; i < allSubLevels[worldNum].Count; i++)
        {
            levels.transform.GetChild(i).GetComponent<SubLevelScript>().levelCompleted = allSubLevels[worldNum][i].levelCompleted;
            levels.transform.GetChild(i).GetComponent<SubLevelScript>().Init();        
        }
        if (GM.insideWorld)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            playerObj.transform.position = GM.lastLevelPos;
        }
        else
        {
            GM.insideWorld = true;

            Vector3 homeStarPos = GameObject.FindGameObjectWithTag("HomeStar").transform.position;
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            GM.lastLevelPos = new Vector3(homeStarPos.x, playerObj.transform.position.y, homeStarPos.z + 5);
            playerObj.transform.position = GM.lastLevelPos;           
        }
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameCamera>().ReadjustCam();
        SetCamMode(-20);
    }
    public static void LoadGameLevel()
    {
        SetCamMode(-20);
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("EnemySpawnPoint");
        if (GM.cockPitModeOn)
        {
            spawnPoint.transform.position = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, spawnPoint.transform.position.z + 80);
        }
        EndLevel finishLine = GameObject.FindGameObjectWithTag("EndLevel").GetComponent<EndLevel>();
        GameObject lastObject = null;

        for(int i = 0; i < levelInfo.numOfWaves; i++)
        {
            SpawnInfo info = levelSpawner.SpawnObject();
            GameObject enemySpawn = info.obj;


            int numOfEnemies = Random.Range(info.minSpawnAmmount, info.maxSpawnAmmount);
            for (int j = 0; j < numOfEnemies; j++)
            {
                GameObject enemy = Instantiate(enemySpawn);

                float ranXPos = Random.Range(p1Copy.xNegConstraint + 30, p1Copy.xPosConstraint - 30);
                float ranZPos = Random.Range(p1Copy.zNegConstraint + 40, p1Copy.zPosConstraint - 40);
                //float ranZPos = Random.Range(0, 0);

                enemy.transform.position = new Vector3(spawnPoint.transform.position.x + ranXPos, spawnPoint.transform.position.y, (spawnPoint.transform.position.z + ranZPos)+(i * 45));

                Enemy enemyScript = enemy.GetComponent<Enemy>();
                enemyScript.level = Random.Range(levelInfo.minEnemyLvl, levelInfo.maxEnemyLvl);

                enemyScript.Init();
                if ((i == levelInfo.numOfWaves - 1) && (j == numOfEnemies - 1))
                {
                    lastObject = enemy;
                }
            }
        }
        finishLine.lastEnemy = lastObject;
    }
    public static void SaveGameLevelInfo(SubLevelScript info, SpawnerManagerExtended spawner)
    {
        levelInfo = info;
        //levelInfo.sceneNum = info.sceneNum;
        //levelInfo.minEnemyLvl = info.minEnemyLvl;
        //levelInfo.maxEnemyLvl = info.maxEnemyLvl;
        //levelInfo.minEnemySpawns = info.minEnemySpawns;
        //levelInfo.maxEnemySpawns = info.maxEnemySpawns;
        //levelInfo.numOfWaves = info.numOfWaves;

        levelSpawner = spawner;

        GM.lastLevelPos = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
    public static void CompleteGameLevel()
    {
        allSubLevels[GM.worldNumber][levelInfo.levelNum - 1].levelCompleted = true;
        //if ((allSubLevels[GM.worldNumber].Count == (levelInfo.levelNum + 1)) && (allWorlds[GM.worldNumber].completion == 0))
        //{
        //    CompleteWorld();
        //    SceneManager.LoadScene(1);
        //}
    }
    public static void CompleteWorld()
    {
        allWorlds[GM.worldNumber].completion = 1;
    }
    public static bool ToggleTutorial()
    {
        GM.tutorialOn = GM.tutorialOn ? false : true;

        if (GM.tutorialOn)
        {
            GM.GetComponent<TutorialManager>().enabled = true;
        }
        else
        {
            GM.GetComponent<TutorialManager>().enabled = false;
        }

        return GM.tutorialOn;
    }
    public static bool ToggleMode()
    {
        GM.normalModeOn = GM.normalModeOn ? false : true;

        if (GM.normalModeOn)
        {
            GM.startOffMoney = 0;
        }
        else
        {
            GM.startOffMoney = 5000;
        }

        return GM.normalModeOn;
    }
    public static bool ToggleCamMode()
    {
        GM.cockPitModeOn = GM.cockPitModeOn ? false : true;

        return GM.cockPitModeOn;
    }
    public static void SavePlayerPos()
    {
        GM.lastWorldPos = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
}
