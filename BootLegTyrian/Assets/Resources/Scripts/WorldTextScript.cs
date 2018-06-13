using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldTextScript : MonoBehaviour {

    public GameObject backGrd;
    Text myText;
    public Text pressFText;
    public bool backGrdOn;

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
        pressFText.gameObject.SetActive(true);
        myText.color = color;
        if (backGrdOn)
        {
            backGrd.SetActive(true);
        } 
        //pressFText.color = color;
    }
    public void ClearText()
    {
        myText.text = "";
        pressFText.gameObject.SetActive(false);
        if (backGrdOn)
        {
            backGrd.SetActive(false);
        }
    }
}
