using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class FadeTransition :FSingleton.SingletonMono<FadeTransition>  {

    //需要用代码生成loading的黑屏image和canvas
    private Image loadingImag;
    private Canvas loadingCanvas;
    //下一关的名字，检查是否加载完毕，进入下一关
    private string nextScene = "";
    private float sceneStartTimeStamp;

    //private static FadeTransition _instance;
    //public static FadeTransition instance
    //{
    //    get
    //    {
    //        if (!_instance)
    //        {
    //            // check if there is a TransitionKit instance already available in the scene graph before creating one
    //            _instance = FindObjectOfType(typeof(FadeTransition)) as FadeTransition;

    //            if (!_instance)
    //            {
    //                //生成 fade object
    //                var obj = new GameObject("FadeTransition");
    //                _instance = obj.AddComponent<FadeTransition>();
    //                DontDestroyOnLoad(obj);
    //            }
    //        }
    //        return _instance;
    //    }
    //}

    private void Start()
    {
        loadingImag = this.transform.Find("Canvas/Image").GetComponent<Image>();
        loadingCanvas = this.transform.Find("Canvas").GetComponent<Canvas>();
    }

    /// <summary>
    /// 渐入渐出场景调用
    /// </summary>
    /// <param name="name">Name.</param>
    public void TransitionScene(string name, float p_fadeOutDuration, float p_fadeInDuration)
    {
        nextScene = name;
        StartCoroutine(Transition(p_fadeOutDuration, p_fadeInDuration));
    }

    /// <summary>
    /// 渐出过程
    /// </summary>
    private IEnumerator Transition(float p_fadeOutDuration, float p_fadeInDuration)
    {
        //fade out
        StartCoroutine(tickChangeAlpha(p_fadeOutDuration));
        yield return null;
        //fade in
        //StartCoroutine(tickChangeAlpha(p_fadeInDuration, true))
    }
    
    /// <summary>
    /// image颜色渐变
    /// </summary>
    private IEnumerator tickChangeAlpha(float duration, bool reverseDirection = false)
    {
        loadingCanvas.gameObject.SetActive(true);
        var start = reverseDirection ? Color.black : Color.clear;
        var end = reverseDirection ? Color.clear : Color.black;

        var elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Color step = Color.Lerp(start, end, Mathf.Pow(elapsed / duration, 2f));
            if (loadingImag != null)
            {
                loadingImag.color = step;
            }
            yield return null;
        }

        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        SceneManager.LoadSceneAsync(nextScene);//异步加载场景
        while (SceneManager.GetActiveScene().name != PlayerPrefs.GetString("nextscene"))
        {
            yield return null;
        }
        loadingCanvas.gameObject.SetActive(false);
    }
}
