using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Worlds : MonoBehaviour {
    public float rotSpeed;
    public string worldName;

    public Vector4 colorText;
    public int sceneNum;

    public bool unlocked;
    public int completion = 0;
    bool playerColliding;

    WorldTextScript worldText;
    AudioSource myAudio;

	// Use this for initialization
	void Start () {
        worldText = GameObject.FindGameObjectWithTag("WorldTextUI").GetComponent<WorldTextScript>();
        myAudio = GetComponent<AudioSource>();

        playerColliding = false;
        if (!unlocked)
        {
            GetComponent<Renderer>().material.color = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
        }
	}
	public void Init()
    {
        if (completion == 1)
        {
            completion++;
        }
    }
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0.0f, rotSpeed*Time.deltaTime, 0.0f));

        if (playerColliding)
        {
            if (Input.GetButtonDown("Submit"))
            {
                ImmortalGameManager.SavePlayerPos();
                ImmortalGameManager.SavePlayerInfo();
                SceneManager.LoadScene(sceneNum);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (unlocked)
            {
                worldText.SetText(worldName, colorText);
                myAudio.Play();
                playerColliding = true;
            }
            else
            {
                worldText.SetText("?????", colorText);
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myAudio.Pause();
            worldText.ClearText();
            playerColliding = false;
        }
    }
}
