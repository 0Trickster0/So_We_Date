﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Next()
    {
        GameObject.Find("DialogManager").SendMessage("ShowJsonText");
    }
}

    

