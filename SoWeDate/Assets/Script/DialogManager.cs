using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using System.Data.OleDb;
using System.IO;
using LitJson;

public class DialogManager : MonoBehaviour
{
    private static DialogManager instance;

    public int i;//表示当前游戏进度
    public string fileName;
    public GameObject DialogCanvas;
    public GameObject Dialog;                   //用于获取场景组件中的对话框
    public Transform DialogText;                //文本
    public List<Node> nodeArray = new List<Node>();
    public string shownText = "";
    public bool isDisabled=false;

    //每个节点的结构体
    public struct Node
    {
        public int id;
        public string text;
        public int fontSize;
        public double textInterval;
        public int shakeDegree;
        public int next;
        public int nextNode;
        public string speakerName;
    }

    //单例
    public static DialogManager Instance
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

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        nodeArray = ReadJsonFile(fileName);
        ShowDialog();
        SetDialogText(nodeArray[0].text);
    }

    public void Update()
    {
        ShowJsonText();
    }

    //显示对话框以及文字
    public void ShowDialog()
    {
        Instantiate(DialogCanvas);
    }
    
    //设置文字
    public void SetDialogText(string sentence)
    {
        //if (GameObject.Find("Dialogbox"))
        //{
        //    Dialog = GameObject.Find("Dialogbox");
        //    DialogText = Dialog.transform.Find("Text");
        //    Text dialogtext = DialogText.GetComponent<Text>();
        //    dialogtext.text = sentence;
        //}
        Text name = GameObject.Find("SpeakerName").GetComponent<Text>();
        name.text = nodeArray[i].speakerName+":";
        StartCoroutine(LoadText(sentence));

    }

    //逐字显示文本
    IEnumerator LoadText(string sentence)
    {
        int j = 0;
        for (j = 0; j< sentence.Length; j++){         
            shownText = shownText + sentence[j].ToString();
            //Debug.Log(shownText);
            if (GameObject.Find("Dialogbox"))
            {
                Dialog = GameObject.Find("Dialogbox");
                DialogText = Dialog.transform.Find("Text");
                Text dialogtext = DialogText.GetComponent<Text>();
                
                dialogtext.text = shownText;    
            }
            yield return new WaitForSeconds((float)nodeArray[i].textInterval);
        }
        StopAllCoroutines();
    }

    //单击左键后显示所有文字
    private void ShowAllText(string sentence)
    {
        if (GameObject.Find("Dialogbox"))
        {
            Dialog = GameObject.Find("Dialogbox");
            DialogText = Dialog.transform.Find("Text");
            Text dialogtext = DialogText.GetComponent<Text>();
            shownText = sentence;
            dialogtext.text = shownText;
            StopAllCoroutines();
        }
    }
    
    //消除对话框及文字
    public void DestoryDialog()
    {
        if (GameObject.Find("DialogCanvas(Clone)"))
        {
            Dialog = GameObject.Find("DialogCanvas(Clone)");
            Destroy(Dialog);
        }

    }
    
    //判断是否有对话框
    public bool IsEmptyDialog()
    {
        if (GameObject.Find("Dialogbox"))
        {

            return false;
        }
        else
        {
            return true;
        }

    }

    //读取Excel文件信息（弃用）
    public DataTable ReadExcelFile(string fileName)
    {
        string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + fileName + ";" + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
        //创建连接到数据源的对象
        OleDbConnection connection = new OleDbConnection(connectionString);
        //打开连接
        connection.Open();
        string sql = "select * from [Sheet1$]";//这个是一个查询命令
        OleDbDataAdapter adapter = new OleDbDataAdapter(sql, connection);
        DataSet dataSet = new DataSet();//用来存放数据 用来存放DataTable
        adapter.Fill(dataSet);//表示把查询的结果(datatable)放到(填充)dataset里面
        connection.Close();//释放连接资源
        //取得数据
        DataTableCollection tableCollection = dataSet.Tables;//获取当前集合中所有的表格
        DataTable table = tableCollection[0];//因为我们只往dataset里面放置了一张表格，所以这里取得索引为0的表格就是我们刚刚查询到的表格
        return table;
    }

    //读取Json文件信息
    public List<Node> ReadJsonFile(string fileName)
    {
        List<Node> nodeList = new List<Node>();
        nodeList = JsonMapper.ToObject<List<Node>>(File.ReadAllText(Application.dataPath + fileName));
        return nodeList;
    }

    //按照Json文件顺序显示文本
    public void ShowJsonText()
    {
        if (Input.GetMouseButtonDown(0)&&!isDisabled)
        {
            if (shownText != nodeArray[i].text)
            {
                ShowAllText(nodeArray[i].text);
            }
            else
            {
                //正常继续对话
                if(nodeArray[i].next == 0){
                    i=i+nodeArray[i].nextNode;
                    shownText = "";
                    SetDialogText(nodeArray[i].text);
                    DialogText.GetComponent<Text>().fontSize = nodeArray[i].fontSize;
                    GameObject.Find("Dialogbox").SendMessage("SetAnimation", nodeArray[i].shakeDegree);
                }
                //触发选项
                else if (nodeArray[i].next == 1)
                {
                    DestoryDialog();
                    GameObject.Find("ChoiceCanvas").SendMessage("SetAllChoiceBoxes");
                    isDisabled = true;
                }
            }
        }
    }
}
