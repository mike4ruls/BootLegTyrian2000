using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGraph : MonoBehaviour {

    [HideInInspector]
    public ButtonGraph leftGraph,rightGraph,upGraph,downGraph;

    public Button myButton; 

    public bool isActive;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
            var color = myButton.colors;
            color.normalColor = isActive ? new Vector4(1.0f, 1.0f, 1.0f, 1.0f) : new Vector4(0.4f, 0.4f, 0.4f, 1.0f);
            myButton.colors = color;
	}
}
