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
    private List<Node> nodeArray = new List<Node>();
    private int i;
    private string sentence = "情书内容：你的美丽是那么的闪耀，我的目光难以从你的身上移开，若再不能向你传达我的心意，我的生命将在渐渐褪色中失去意义。请在今天放学后等待我的告白。";
    private string shownText = "";
    private static DialogManager instance;

    public string fileName;
    public GameObject DialogCanvas;
    public GameObject Dialog;                   //用于获取场景组件中的对话框
    public Transform DialogText;                //文本

    //每个节点的结构体
    public struct Node
    {
        public int id;
        public string text;
        public bool isChoice;
    }

    //单例
    public static DialogManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DialogManager();
            }
            return instance;
        }
    }

    public void Start()
    {
        nodeArray = ReadJsonFile(fileName);
        ShowDialog();
        SetDialogText(nodeArray[0].text);
    }

    public void Update()
    {
        //ShowAllText(sentence);
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
        StartCoroutine(LoadText(sentence));

    }

    //逐字显示文本
    IEnumerator LoadText(string sentence)
    {
        int i = 0;
        for (i = 0; i< sentence.Length; i++){
            yield return new WaitForSeconds(0.05f);
            shownText = shownText + sentence[i].ToString();
            //Debug.Log(shownText);
            if (GameObject.Find("Dialogbox"))
            {
                Dialog = GameObject.Find("Dialogbox");
                DialogText = Dialog.transform.Find("Text");
                Text dialogtext = DialogText.GetComponent<Text>();
                dialogtext.text = shownText;    
            }   
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
        if (GameObject.Find("Dialogbox"))
        {
            Dialog = GameObject.Find("Dialogbox");
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
        if (Input.GetMouseButtonDown(0))
        {
            if (shownText != nodeArray[i].text)
            {
                ShowAllText(nodeArray[i].text);
            }
            else
            {
                i++;
                shownText = "";
                SetDialogText(nodeArray[i].text);
            }
        }
    }
}
