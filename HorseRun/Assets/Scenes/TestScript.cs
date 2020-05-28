using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour {

    public Transform floor1;
    public Transform floor2;
    public Transform floor3;

    public Transform player;

    public Text lookText;

    public RectTransform point;
    private RectTransform rectTransform;
    void Start () {
        rectTransform = player as RectTransform;
        StartCoroutine(StartLook());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator StartLook()
    {
        lookText.text = "现在玩家在起点（0，0），站在地板一上";
        yield return new WaitForSeconds(2.5f);
        lookText.text = @"现在玩家向右移动50，处在地板一与地板二之间
地板不进行移动";
        rectTransform.anchoredPosition3D = new Vector3(-150, 0, 0);
        yield return new WaitForSeconds(1.5f);
        lookText.text = @"现在玩家在向右移动20,到达第一个触发点<color=red>(70,0,0)</color>的位置
处在地板二之上";
        rectTransform.anchoredPosition3D = new Vector3(-90, 0, 0);
        yield return new WaitForSeconds(1.5f);
        lookText.text = "现在完全退出了地板一，此时需要移动地板一进行复用";
        yield return new WaitForSeconds(1.5f);
        lookText.text = @"由于地板长100,需要将复用的的地板移动到道路最前方
只需要在自己的位置基础上在移动300即可";
        yield return new WaitForSeconds(2.5f);
        lookText.text = "这样地板一的新的坐标就是（300，0，0）";
        ((RectTransform)floor1).anchoredPosition3D = new Vector3(600, 0, 0);
        yield return new WaitForSeconds(1.5f);
        lookText.text = @"这个时候下一个触发的的坐标就这个原先触发点基础上增加100
到达<color=red>(170,0,0)</color>";
        point.anchoredPosition3D = new Vector3(210, 0, 0);
        yield return new WaitForSeconds(1.5f);
        lookText.text = @"由于<color=red>新的触发点位置总是离新的坐标距离为130</color>,
我们这使用的是通过新触发点位置来计算新的坐标，<color=red>还请注意！</color>";
        yield return null;
    }
}
