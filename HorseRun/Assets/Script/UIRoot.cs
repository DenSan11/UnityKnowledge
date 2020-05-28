using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIRoot : MonoBehaviour {

    private Button startBtn;
    private Button quitBtn;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Use this for initialization
    void Start ()
    {
        InitUI();
	}
	
    private void InitUI()
    {
        startBtn = this.transform.Find("ButtonStart").GetComponent<Button>();
        quitBtn = this.transform.Find("ButtonQuit").GetComponent<Button>();

        startBtn.onClick.AddListener(StartBtnClick);
        quitBtn.onClick.AddListener(QuitBtnClick);
    }

    public void StartBtnClick()
    {
        PlayerPrefs.SetString("nextscene","Main");
        //SceneManager.LoadScene("Loading");
        FadeTransition.GetInstance().TransitionScene(PlayerPrefs.GetString("nextscene"), 3f,3f);
    }

    public void QuitBtnClick()
    {
        Application.Quit();
    }
}
