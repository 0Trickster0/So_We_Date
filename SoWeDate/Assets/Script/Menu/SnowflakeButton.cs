using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;

public class SnowflakeButton : MonoBehaviour {

    private bool isOn = false;

    public Sprite[] sprites;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Click()
    {
        if (!isOn)
        {
            GetComponent<Image>().overrideSprite = sprites[1];
            BroadcastMessage("SetOn");
            //GameObject.Find("MainMenuButton").SendMessage("SetOn");
            //GameObject.Find("SettingButton").SendMessage("SetOn");
            //GameObject.Find("SaveButton").SendMessage("SetOn");
            isOn = true;
        }
        else
        {
            GetComponent<Image>().overrideSprite = sprites[0];
            BroadcastMessage("SetOff");
            //GameObject.Find("MainMenuButton").SendMessage("SetOff");
            //GameObject.Find("SettingButton").SendMessage("SetOff");
            //GameObject.Find("SaveButton").SendMessage("SetOff");
            isOn = false;
        }
    }
}
