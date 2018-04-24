using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesIconScript : MonoBehaviour {
    public float rotSpeed;
    public float visibilitySpeed;
    public float pulsateSpeed;
    public float maxPulsateAlpha;
    public float minPulsateAlpha;
    public bool visible;
    public bool canPulsate;

    [HideInInspector]
    public bool fullyTurnedOff;

    bool startPulsate;
    bool pulsate;

    Image myImage;
    RectTransform myRectTransform;
	// Use this for initialization
	void Start () {
        myImage = GetComponent<Image>();
        myRectTransform = GetComponent<RectTransform>();
        pulsate = false;
        fullyTurnedOff = false;
        if (!visible)
        {
            myImage.color = new Vector4(myImage.color.r, myImage.color.g, myImage.color.b, 0.0f);
            fullyTurnedOff = true;
        }
	}
	
	// Update is called once per frame
	void Update () {

        myRectTransform.rotation = Quaternion.Euler(myRectTransform.rotation.eulerAngles.x, myRectTransform.rotation.eulerAngles.y, myRectTransform.rotation.eulerAngles.z + (rotSpeed * Time.deltaTime));
        if (visible && myImage.color.a < 1.0f && !startPulsate)
        {
            if (fullyTurnedOff)
            {
                fullyTurnedOff = false;
            }
            myImage.color = new Vector4(myImage.color.r, myImage.color.g, myImage.color.b, myImage.color.a + (visibilitySpeed * Time.deltaTime));

            if(myImage.color.a >= maxPulsateAlpha)
            {
                myImage.color = new Vector4(myImage.color.r, myImage.color.g, myImage.color.b, maxPulsateAlpha);
                if (canPulsate)
                {
                    startPulsate = true;
                }
            }
        }
        else
        {
            if (startPulsate)
            {
                StartPulsating();
            }
        }

        if (!visible && myImage.color.a > 0.0f)
        {
            myImage.color = new Vector4(myImage.color.r, myImage.color.g, myImage.color.b, myImage.color.a - (visibilitySpeed * Time.deltaTime));
            if (startPulsate)
            {
                startPulsate = false;
            }
            if (myImage.color.a <= 0.0f)
            {
                myImage.color = new Vector4(myImage.color.r, myImage.color.g, myImage.color.b, 0.0f);
                fullyTurnedOff = true;
            }
        }
    }
    void StartPulsating()
    {
        if (pulsate && myImage.color.a < maxPulsateAlpha)
        {
            myImage.color = new Vector4(myImage.color.r, myImage.color.g, myImage.color.b, myImage.color.a + (pulsateSpeed * Time.deltaTime));

            if (myImage.color.a >= maxPulsateAlpha)
            {
                myImage.color = new Vector4(myImage.color.r, myImage.color.g, myImage.color.b, maxPulsateAlpha);
                pulsate = false;
            }
        }
        if (!pulsate && myImage.color.a > minPulsateAlpha)
        {
            myImage.color = new Vector4(myImage.color.r, myImage.color.g, myImage.color.b, myImage.color.a - (pulsateSpeed * Time.deltaTime));

            if (myImage.color.a <= minPulsateAlpha)
            {
                myImage.color = new Vector4(myImage.color.r, myImage.color.g, myImage.color.b, minPulsateAlpha);
                pulsate = true;
            }
        }
    }
}
