using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public List<AudioClip> buyButtonClickAudio;
    public AudioClip buttonClickAudio;
    public AudioClip buyFailedAudio;
    public AudioClip tabChangeAudio;
    AudioSource myAudio;
    // Use this for initialization
    void Start () {
        myAudio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void PlayButtonClicked()
    {
        myAudio.clip = buttonClickAudio;
        myAudio.Play();
    }
    public void PlayBuySucceed()
    {
        int ranNum = Random.Range(0, buyButtonClickAudio.Count);

        myAudio.clip = buyButtonClickAudio[ranNum];
        myAudio.Play();
    }
    public void PlayBuyFailed()
    {
        myAudio.clip = buyFailedAudio;
        myAudio.Play();
    }
    public void PlayTabChange()
    {
        myAudio.clip = tabChangeAudio;
        myAudio.Play();
    }
}
