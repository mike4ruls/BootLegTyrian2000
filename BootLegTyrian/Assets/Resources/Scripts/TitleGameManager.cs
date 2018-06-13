using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleGameManager : MonoBehaviour {
    Player p1;
    public List<Material> backgroundTiles;
    public AudioManager audioManager;
    public GameObject MainUI;
    public GameObject ControlsUI;
    public GameObject SettingsUI;
    public Text tutorialText;
    public Text modeText;

    // Use this for initialization
    void Start () {
        p1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        int ranNum = Random.Range(0, backgroundTiles.Count);

        GameObject.FindGameObjectWithTag("BackgroundTile").GetComponent<Renderer>().material = backgroundTiles[ranNum];

        if (!ImmortalGameManager.GM.tutorialOn)
        {
            tutorialText.text = "Tutorial: OFF";
        }
        if (!ImmortalGameManager.GM.normalModeOn)
        {
            modeText.text = "Mode: Testing Mode";
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Submit") || Input.GetKeyDown("joystick button 7"))
        {
            LoadHubWorld();
        }
    }
    public void LoadHubWorld()
    {
        audioManager.PlayButtonClicked();
        SceneManager.LoadScene(1);
    }
    public void LoadSandBox()
    {
        audioManager.PlayButtonClicked();
        SceneManager.LoadScene(4);
    }
    public void TurnOnControls()
    {
        audioManager.PlayButtonClicked();
        MainUI.SetActive(false);
        ControlsUI.SetActive(true);
    }
    public void TurnOnMainUI()
    {
        audioManager.PlayButtonClicked();
        MainUI.SetActive(true);
        ControlsUI.SetActive(false);
        SettingsUI.SetActive(false);
    }
    public void TurnOnSettingUI()
    {
        audioManager.PlayButtonClicked();
        SettingsUI.SetActive(true);
        MainUI.SetActive(false);
    }
    public void UpdateTutorialText()
    {
        bool on = ImmortalGameManager.ToggleTutorial();
        tutorialText.text = on ? "Tutorial: ON" : "Tutorial: OFF";
        audioManager.PlayButtonClicked();
    }
    public void UpdateModeText()
    {
        bool on = ImmortalGameManager.ToggleMode();
        modeText.text = on ? "Mode: Normal Mode" : "Mode: Testing Mode";
        audioManager.PlayButtonClicked();
    }

}
