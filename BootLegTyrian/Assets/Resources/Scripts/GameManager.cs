﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    GameObject[] explosions;

    public float controlDisplayTimer;
    public int sceneNum;

    GameObject pointsTextObj;
    GameObject healthTextObj;
    GameObject gameOverTextObj;
    GameObject controlTextObj;
    GameObject finisheLevelTextObj;

    GameObject startMenuObj;

    Player p1;
    EndLevel levelBar;

    Button continueButton;

    bool init;

    bool isPaused;
    // Use this for initialization
    void Start () {

        explosions = GameObject.FindGameObjectsWithTag("Explosion");

        GameObject.FindGameObjectWithTag("BackgroundTile").GetComponent<BackgroundTiles>().movementSpeedY = Random.Range(0.4f, 0.7f);

        pointsTextObj = GameObject.Find("PointsText");
        healthTextObj = GameObject.Find("HealthText");
        gameOverTextObj = GameObject.Find("GameOver");
        controlTextObj = GameObject.Find("ControlsText");
        finisheLevelTextObj = GameObject.Find("FinisheLevel");
        startMenuObj = GameObject.Find("StartMenu");
        continueButton = GameObject.Find("ContinueButton").GetComponent<Button>();

        p1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        levelBar = GameObject.FindGameObjectWithTag("EndLevel").GetComponent<EndLevel>();

        gameOverTextObj.SetActive(false);
        finisheLevelTextObj.SetActive(false);
        continueButton.gameObject.SetActive(false);
        startMenuObj.SetActive(false);

        init = true;
        isPaused = false;
        StartCoroutine(turnOffContol(controlDisplayTimer));
        ImmortalGameManager.LoadGameLevel();
	}
	
	// Update is called once per frame
	void Update () {
        pointsTextObj.GetComponent<Text>().text = "Points: " + p1.money;
        healthTextObj.GetComponent<Text>().text = "Health: " + p1.health;

        if (p1.isDead)
        {
            gameOverTextObj.SetActive(true);
            continueButton.gameObject.SetActive(true);

            if (Input.GetButtonDown("Strafe"))
            {
                LoadWorld();
            }

        }
        if (levelBar.endLevel)
        {
            finisheLevelTextObj.SetActive(true);
            continueButton.gameObject.SetActive(true);
            p1.GetComponent<BoxCollider>().enabled = false;
            if (Input.GetButtonDown("Strafe"))
            {
                LoadWorld();
            }
        }

        if (Input.GetButtonDown("Cancel"))
        {
            if(!levelBar.endLevel && !p1.isDead)
            {
                if (isPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }     
        }
    }
    IEnumerator turnOffContol(float time)
    {
        yield return new WaitForSeconds(time);
        controlTextObj.SetActive(false);
    }
    public ParticleSystem GetExplosion()
    {
        for (int i = 0; i< explosions.Length; i++)
        {
            ParticleSystem system = explosions[i].GetComponent<ParticleSystem>();
            if (!system.isPlaying)
            {
                return system;
            }
        }
        return null;
    }
    public void LoadWorld()
    {
        if (levelBar.endLevel)
        {
            ImmortalGameManager.SavePlayerInfo();
            ImmortalGameManager.CompleteGameLevel();
        }
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneNum);    
    }

    public void Pause()
    {
        startMenuObj.SetActive(true);
        Time.timeScale = 0.0f;
        isPaused = true;
    }
    public void Resume()
    {
        startMenuObj.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }
    public void ReloadStage()
    {

    }
}
