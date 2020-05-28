using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadAsyncScene : MonoBehaviour {

    // 目标进度
    float target = 0;
    // 读取场景的进度，取值范围0~1
    float progress = 0;
    // 异步对象
    AsyncOperation op = null;

    private Image progressImg;
    private Text progressTxt;

    void Start()
    {
        progressImg = this.transform.Find("ProgressBg").GetComponent<Image>();
        progressTxt = this.transform.Find("ProgressNum").GetComponent<Text>();

        op = SceneManager.LoadSceneAsync(PlayerPrefs.GetString("nextscene"));
        op.allowSceneActivation = false;
        // 开启协程，开始调用加载方法
        StartCoroutine(processLoading());
    }

    bool isCompelate = false;

    // 加载进度
    IEnumerator processLoading()
    {
        while (!isCompelate)
        {
            target = op.progress; // 进度条取值
            Debug.Log(target);
            SetLoadingPercentage(target);
            if (target >= 0.9f)
            {
                target = 1;
                isCompelate = true;
            }
            yield return 0;
        }
        SetLoadingPercentage(1);
        yield return new WaitForSeconds(1f);
        op.allowSceneActivation = true;

    }

    //更新界面中显示的进度条的数值
    private void SetLoadingPercentage(float value)
    {
        progressTxt.text = (value * 100).ToString() + "%";
        progressImg.fillAmount = value;
    }
}
