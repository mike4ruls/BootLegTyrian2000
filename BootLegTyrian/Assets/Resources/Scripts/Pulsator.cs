using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulsator : MonoBehaviour
{
    Material myMat;
    Color originalColor;

    public Color maxLerpColor;
    public Color minLerpColor;

    public float pulsateSpeed;
    [Range(0.0f, 1.0f)]
    public float maxPulsate;
    [Range(0.0f, 1.0f)]
    public float minPulsate;

    public bool useOriginalColor;
    public bool useCustomColor;
    public bool useAlpha;

    public bool canPulsate;

    bool lerpUp = true;
    float lerpTime;

    // Use this for initialization
    void Start()
    {
        lerpTime = 0.0f;
        myMat = GetComponent<Renderer>().material;
        originalColor = myMat.color;

        if (!useCustomColor)
        {
            minLerpColor = new Color(originalColor.r * minPulsate, originalColor.g * minPulsate, originalColor.b * minPulsate, 1.0f);
        }
        if (useOriginalColor)
        {
            maxLerpColor = new Color(originalColor.r * maxPulsate, originalColor.g * maxPulsate, originalColor.b * maxPulsate, 1.0f);
        }
        else
        {
            maxLerpColor = originalColor;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (canPulsate)
        {
            if (!useCustomColor)
            {
                minLerpColor = new Color(originalColor.r * minPulsate, originalColor.g * minPulsate, originalColor.b * minPulsate, 1.0f);
            }
            if (useOriginalColor)
            {
                maxLerpColor = new Color(originalColor.r * maxPulsate, originalColor.g * maxPulsate, originalColor.b * maxPulsate, 1.0f);
            }
            else
            {
                maxLerpColor = originalColor;
            }

            myMat.color = Color.Lerp(maxLerpColor, minLerpColor, lerpTime);
            if (useAlpha)
            {
                float newAlpha = Mathf.Lerp(maxPulsate, minPulsate, lerpTime);
                myMat.color = new Color(myMat.color.r, myMat.color.g, myMat.color.b, newAlpha);
            }
            if (lerpUp)
            {
                lerpTime += pulsateSpeed * Time.deltaTime;
                if (lerpTime >= 1.0f)
                {
                    lerpTime = 1.0f;
                    lerpUp = false;
                }
            }
            else
            {
                lerpTime -= pulsateSpeed * Time.deltaTime;
                if (lerpTime <= 0.0f)
                {
                    lerpTime = 0.0f;
                    lerpUp = true;
                }
            }
        }

    }
}