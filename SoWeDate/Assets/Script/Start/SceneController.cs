using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    
    //加载loading界面
	void SceneShift()
    {
        SceneManager.LoadScene("Loading");
    }
}
