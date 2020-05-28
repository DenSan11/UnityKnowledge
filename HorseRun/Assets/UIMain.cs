using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMain : MonoBehaviour {

    private GameObject panelEnd;
    private Text distanceTxt;
    private Button againButton;
    private Button exitButton;

    private void Awake()
    {
        panelEnd = this.transform.Find("PanelEnd").gameObject;
        distanceTxt = this.transform.Find("PanelEnd/Distance").GetComponent<Text>();
        againButton = this.transform.Find("PanelEnd/ButtonAgain").GetComponent<Button>();
        exitButton = this.transform.Find("PanelEnd/ButtonExit").GetComponent<Button>();
    }

    // Use this for initialization
    void Start () {
        ShowHidePanel(false);
        GameMode.GetInstance().GameOverAction += GameEnd;
        againButton.onClick.AddListener(GameAgain);
        exitButton.onClick.AddListener(GameExit);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameEnd()
    {
        Debug.Log("游戏结束");
        distanceTxt.text = ((int)(GameMode.GetInstance().moveDistance)).ToString();
        ShowHidePanel(true);
    }

    private void ShowHidePanel(bool isShow)
    {
        panelEnd.SetActive(isShow);
    }

    private void GameAgain()
    {
        SceneManager.LoadScene("Main");
    }

    private void GameExit()
    {
        PlayerPrefs.SetString("nextscene", "Start");
        FadeTransition.GetInstance().TransitionScene(PlayerPrefs.GetString("nextscene"), 3f, 3f);
    }
}
