using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceBox : MonoBehaviour {

    public int skipDialogNumber;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void Button()
    {
        Debug.Log("OnClick");
        DialogManager.Instance.i += skipDialogNumber;
        DialogManager.Instance.shownText = "";
        GameObject.Find("DialogManager").SendMessage("ShowDialog");
        GameObject.Find("DialogManager").SendMessage("SetDialogText", DialogManager.Instance.nodeArray[DialogManager.Instance.i].text);
        DialogManager.Instance.isDisabled = false;
        GameObject[] boxes=GameObject.FindGameObjectsWithTag("ChoiceBox");
        foreach(var go in boxes)
        {
            Destroy(go);
        }
    }
}
