using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassLandsManager : MonoBehaviour {
    public int worldNum;
	// Use this for initialization
	void Start () {
        ImmortalGameManager.LoadSubLevels(worldNum);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
