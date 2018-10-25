using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;

public class MainMenuButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Click()
    {
        
    }

    public void SetOn()
    {
        GetComponent<Image>().enabled = true;
        GetComponent<Button>().enabled = true;
        gameObject.SetActive(true);
        GetComponent<Animator>().SetBool("isDisplayed", true);
    }

    public void SetOff()
    {
        GetComponent<Animator>().SetBool("isDisplayed", false);
        Invoke("Disappear", 0.8f);
    }

    public void Disappear()
    {
        GetComponent<Image>().enabled = false;
        GetComponent<Button>().enabled = false;
    }
}
