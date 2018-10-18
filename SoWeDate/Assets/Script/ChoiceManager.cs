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

    public int k;
    private static ChoiceManager instance;
    
    //每个选项的结构体
    public struct Choice
    {
        public int id;
        public int boxesNumber;
        public int[] skipDialogNumber;//每个选项跳过对话的数量
        public int[] nextChoice;//每个选项跳过选项的数量
    }

    //单例
    public static ChoiceManager Instance
    {
        get
        {
            return instance;
        }
        set
        {
            instance = value;
        }
    }

	// Use this for initialization
	void Awake () {
        Instance = this;
        choiceArray = ReadJsonFile(fileName);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //设置一个选项框
    public void SetChoiceBox(int position, string choiceText,int skipDialogNumber,int nextChoice)
    {
        GameObject cB = Instantiate(choiceBox, transform.position + new Vector3(0, 160*position, 0), Quaternion.identity);
        cB.transform.parent = GameObject.Find("ChoiceCanvas").gameObject.transform;
        Text buttonText = cB.GetComponentInChildren<Text>();
        buttonText.text = choiceText;
        cB.GetComponent<ChoiceBox>().skipDialogNumber = skipDialogNumber;
        cB.GetComponent<ChoiceBox>().nextChoice = nextChoice;
    }

    //设置多个选项框
    public void SetMultipleBoxes(int boxesNumber,int[] skipDialogNumberArray,int[] nextChoiceArray)
    {
        int j = 1;
        for (j = 1; j <= boxesNumber; j++)
        {
            SetChoiceBox(2 - j, DialogManager.Instance.nodeArray[DialogManager.Instance.i+j].text,skipDialogNumberArray[j-1],nextChoiceArray[j-1]);   
        }
    }

    //设置所有选项框
    public void SetAllChoiceBoxes()
    {
        //for (k = 0; k < choiceArray.Count; k++)
        //{
        //    SetMultipleBoxes(choiceArray[k].boxesNumber, choiceArray[k].skipDialogNumber);
        //}
        SetMultipleBoxes(choiceArray[k].boxesNumber, choiceArray[k].skipDialogNumber,choiceArray[k].nextChoice);

    }

    //读取包含每个选项的参数的Json文件
    public List<Choice> ReadJsonFile(string fileName)
    {
        List<Choice> choiceList = new List<Choice>();
        choiceList = JsonMapper.ToObject<List<Choice>>(File.ReadAllText(Application.dataPath + fileName));
        return choiceList;
    }
}
