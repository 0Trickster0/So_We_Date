using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;


public class ChoiceManager : MonoBehaviour {

    public GameObject choiceBox;
    public string text;
    public List<Choice> choiceArray = new List<Choice>();
    public string fileName;

    private int k;
    
    public struct Choice
    {
        public int id;
        public int boxesNumber;
        public int[] skipDialogNumber;
    }

	// Use this for initialization
	void Awake () {
        choiceArray = ReadJsonFile(fileName);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetChoiceBox(int position, string choiceText,int skipDialogNumber)
    {
        GameObject cB = Instantiate(choiceBox, transform.position + new Vector3(0, 160*position, 0), Quaternion.identity);
        cB.transform.parent = GameObject.Find("ChoiceCanvas").gameObject.transform;
        Text buttonText = cB.GetComponentInChildren<Text>();
        buttonText.text = choiceText;
        cB.GetComponent<ChoiceBox>().skipDialogNumber = skipDialogNumber;
    }

    public void SetMultipleBoxes(int boxesNumber,int[] skipDialogNumberArray)
    {
        int j = 1;
        for (j = 1; j <= boxesNumber; j++)
        {
            SetChoiceBox(2 - j, DialogManager.Instance.nodeArray[DialogManager.Instance.i+j].text,skipDialogNumberArray[j-1]);   
        }
    }

    public void SetAllChoiceBoxes()
    {
        //for (k = 0; k < choiceArray.Count; k++)
        //{
        //    SetMultipleBoxes(choiceArray[k].boxesNumber, choiceArray[k].skipDialogNumber);
        //}
        SetMultipleBoxes(choiceArray[k].boxesNumber, choiceArray[k].skipDialogNumber);
        k++;

    }

    public List<Choice> ReadJsonFile(string fileName)
    {
        List<Choice> choiceList = new List<Choice>();
        choiceList = JsonMapper.ToObject<List<Choice>>(File.ReadAllText(Application.dataPath + fileName));
        return choiceList;
    }
}
