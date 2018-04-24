using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuScript : MonoBehaviour {
    ButtonGraph currentButton;


    ButtonGraph continueGraph;
    ButtonGraph retryGraph;
    ButtonGraph quitGraph;


    bool upPressed;
    bool downPressed;

    // Use this for initialization
    void Start () {
        InitStartMenuGraph();
        upPressed = false;
        downPressed = false;
    }
	void Awake()
    {
        //currentButton = continueGraph.upGraph;
    }
	// Update is called once per frame
	void Update () {

        if (Input.GetAxis("VerticalDPad") < 0.0f)
        {
            if (!downPressed)
            {
                currentButton.isActive = false;
                currentButton = currentButton.downGraph;
                currentButton.isActive = true;

                downPressed = true;
            } 
        }
        else
        {
            downPressed = false;
        }
        if (Input.GetAxis("VerticalDPad") > 0.0f)
        {
            if (!upPressed)
            {
                currentButton.isActive = false;
                currentButton = currentButton.upGraph;
                currentButton.isActive = true;
                upPressed = true;
            }
        }
        else
        {
            upPressed = false;
        }

    }
    void InitStartMenuGraph()
    {
        currentButton = new ButtonGraph();

        continueGraph = new ButtonGraph();
        retryGraph = new ButtonGraph();
        quitGraph = new ButtonGraph();

        continueGraph.myButton = transform.GetChild(1).GetComponent<Button>();
        retryGraph.myButton = transform.GetChild(2).GetComponent<Button>();
        quitGraph.myButton = transform.GetChild(3).GetComponent<Button>();

        currentButton.downGraph = continueGraph;

        continueGraph.upGraph = currentButton;
        continueGraph.downGraph = retryGraph;

        retryGraph.upGraph = continueGraph;
        retryGraph.downGraph = quitGraph;

        quitGraph.upGraph = retryGraph;

    }
}
