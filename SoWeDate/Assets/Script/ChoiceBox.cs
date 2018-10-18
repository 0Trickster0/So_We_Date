using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceBox : MonoBehaviour {

    public int skipDialogNumber;
    public int nextChoice;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    //按下按钮后执行的方法
    public void Button()
    {
        DialogManager.Instance.i += skipDialogNumber;//跳过文本顺序表中一定的节点
        DialogManager.Instance.shownText = "";
        GameObject.Find("DialogManager").SendMessage("ShowDialog");
        GameObject.Find("DialogManager").SendMessage("SetDialogText", DialogManager.Instance.nodeArray[DialogManager.Instance.i].text);
        DialogManager.Instance.isDisabled = false;
        ChoiceManager.Instance.k += nextChoice;//跳过选项顺序表中一定的节点
        GameObject[] boxes=GameObject.FindGameObjectsWithTag("ChoiceBox");
        foreach(var go in boxes)//消除所有选项框
        {
            Destroy(go);
        }
    }
}
