using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldTextScript : MonoBehaviour {

    Text myText;

	// Use this for initialization
	void Start () {
        myText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetText(string worldName, Vector4 color)
    {
        myText.text = worldName;
        myText.color = color;
    }
}
