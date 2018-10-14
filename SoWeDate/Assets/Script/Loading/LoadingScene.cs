using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour {

    private AsyncOperation mAsyn;//声明异步变量
    private float progress = 0;

    public Image loadBar;

	// Use this for initialization
	void Start () {
        StartCoroutine(LoadScene());
	}
	
	// 让进度条与加载进度同步
	void Update () {
        progress = mAsyn.progress;
        loadBar.fillAmount = progress;
        //Debug.Log(loadBar.fillAmount);
	}

    // 加载01场景的协程
    IEnumerator LoadScene()
    {
        mAsyn = SceneManager.LoadSceneAsync("01");
        mAsyn.allowSceneActivation = true;
        yield return mAsyn;
    }
}
