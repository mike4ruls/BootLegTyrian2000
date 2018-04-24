using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SubLevelScript : MonoBehaviour
{
    public string levelName;
    public int levelNum;
    public float normRotSpeed;
    public float hoverRotSpeed;
    public int sceneNum;
    public int minEnemyLvl;
    public int maxEnemyLvl;
    public int minEnemySpawns;
    public int maxEnemySpawns;
    public int numOfWaves;

    public bool reverseRot;
    public bool levelLocked;
    public bool levelCompleted;

    public GameObject[] connectingLevels;

    Text levelTextUI;
    Text enemyLvlTextUI;

    float rotSpeed;
    bool playerColliding;
    bool isALevel = true;

    // Use this for initialization
    void Start()
    {
        rotSpeed = normRotSpeed;
        playerColliding = false;

        levelTextUI = GameObject.Find("LevelText").GetComponent<Text>();
        enemyLvlTextUI = GameObject.Find("EnemyLvlText").GetComponent<Text>();

        if (GetComponent<SpawnerManagerExtended>() == null)
        {
            isALevel = false;
        }

        Init();

    }

    // Update is called once per frame
    void Update()
    {
        if (reverseRot)
        {
            transform.Rotate(new Vector3(0.0f, -rotSpeed * Time.deltaTime, 0.0f));
        }
        else
        {
            transform.Rotate(new Vector3(0.0f, rotSpeed * Time.deltaTime, 0.0f));
        }

        if (playerColliding && !levelLocked)
        {
            if (Input.GetButtonDown("Submit"))
            {
                ImmortalGameManager.SavePlayerInfo();
                if (isALevel)
                {
                    ImmortalGameManager.SaveGameLevelInfo(this, GetComponent<SpawnerManagerExtended>());
                }
                SceneManager.LoadScene(sceneNum);
            }
        }
    }

    public void Init()
    {
        if (levelCompleted)
        {
            GetComponent<Renderer>().material = Resources.Load("Materials/GoldStar", typeof(Material)) as Material;
            for (int i = 0; i < connectingLevels.Length; i++)
            {
                connectingLevels[i].GetComponent<SubLevelScript>().levelLocked = false;
            }
        }
        else if (levelLocked)
        {
            GetComponent<Renderer>().material.color = new Vector4(0.1f, 0.1f, 0.1f, 1.0f);
        }

        for (int i = 0; i < connectingLevels.Length; i++)
        {
            GameObject connector = (GameObject)Instantiate(Resources.Load("Prefabs/LevelConnectorObj"));
            connector.transform.SetParent(null);
            connector.transform.position = transform.position;// - new Vector3(0.0f, 2.0f, 0.0f);

            Vector3 distToNextLvl = connectingLevels[i].transform.position - transform.position;
            connector.transform.position += new Vector3(distToNextLvl.x / 2.0f, 0.0f, distToNextLvl.z / 2.0f);

            float dot = Vector3.Dot(distToNextLvl, new Vector3(1.0f, 0.0f, 0.0f));
            float mag = distToNextLvl.magnitude;
            float angle = dot / (mag);
            angle = Mathf.Acos(angle);

            angle = angle * (180.0f / Mathf.PI);

            if (distToNextLvl.z < 0.0f)
            {
                connector.transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
            }
            else
            {
                connector.transform.rotation = Quaternion.Euler(0.0f, -angle, 0.0f);
            }
            //Debug.Log(mag);
            connector.transform.localScale = new Vector3(mag / 10.0f, connector.transform.localScale.y, connector.transform.localScale.z);

            if (!connectingLevels[i].GetComponent<SubLevelScript>().levelLocked)
            {
                connector.GetComponent<Renderer>().material.color = new Vector4(1.0f, 1.0f, 0.0f, 1.0f);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !levelLocked)
        {
            levelTextUI.text = levelName;
            if (isALevel)
            {
                enemyLvlTextUI.text = "Enemy Lvl: " + minEnemyLvl + " ~ " + maxEnemyLvl;
            }

            rotSpeed = hoverRotSpeed;
            playerColliding = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            levelTextUI.text = "";
            enemyLvlTextUI.text = "";

            rotSpeed = normRotSpeed;
            playerColliding = false;
        }
    }
}

