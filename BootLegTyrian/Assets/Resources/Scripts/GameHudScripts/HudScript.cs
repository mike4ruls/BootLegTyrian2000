using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudScript : MonoBehaviour {
    public GameObject ControlsUI;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (ControlsUI.activeInHierarchy)
            {
                TurnOffControls();
            }
            else
            {
                TurnOnControls();
            }
        }
	}
    void TurnOnControls()
    {
        ControlsUI.SetActive(true);
    }

    public void TurnOffControls()
    {
        ControlsUI.SetActive(false);
    }
}
