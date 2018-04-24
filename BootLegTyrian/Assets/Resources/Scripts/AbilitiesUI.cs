using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesUI : MonoBehaviour {

    [HideInInspector]
    public bool startTurnOff;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (startTurnOff)
        {
            int count = 1;
            for (int i = 1; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<AbilitiesIconScript>().fullyTurnedOff)
                {
                    count++;
                }
            }
            if (count == transform.childCount)
            {
                this.gameObject.SetActive(false);
            }
        }
	}
    public void TurnOn()
    {
        startTurnOff = false;
        transform.GetChild(0).gameObject.SetActive(true);
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<AbilitiesIconScript>().visible = true;
        }
    }
    public void TurnOff()
    {
        startTurnOff = true;
        transform.GetChild(0).gameObject.SetActive(false);
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<AbilitiesIconScript>().visible = false;
        }
    }
 }
