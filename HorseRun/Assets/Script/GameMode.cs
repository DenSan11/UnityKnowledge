using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMode : FSingleton.SingletonMono<GameMode> {

    public Transform floorOne;
    public Transform floorTwo;      
    public Transform floorThree;//3个地板
    public Transform player;
    public GameObject testObj;
    public bool isCanRide;          //是否能够骑乘
    public AnimalControl animal;     //当前在圈里的动物
    public Material yellowMater;

    private Vector3 startPoint;         //玩家一开始的起始点 用于计算距离
    public float moveDistance;          //玩家移动的距离
    public Text fractionText;           //用于显示分数的文本
    public Image waringImage;           //警告用显示图片

    private GameObject[] animlObjs;      //生成的动物数组
    private GameObject[] treeObjs; //树数组

    float createTime = 0;

    public Action GameOverAction;

    void Start () {
        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        player = FindObjectOfType<Player>().transform;
        startPoint = player.position;
        waringImage.gameObject.SetActive(false);
        animlObjs = Resources.LoadAll<GameObject>("Prefabs/Animals");
        treeObjs = Resources.LoadAll<GameObject>("Prefabs/Trees");

        StartCoroutine(CreateTrees());
        InitAnimal();
    }

	// Update is called once per frame
	void Update () {
        CheckBackMove();

        createTime += Time.deltaTime;
        if (createTime > 3f)
        {
            createTime = 0;
            CreatAnimal();
        }

        UpdataDistance();
    }

    private void InitAnimal()
    {
        int arrayIndex = UnityEngine.Random.Range(0, animlObjs.Length);
        GameObject tempObj = ObjectPool.GetInstance().RequestCacheGameObejct(animlObjs[arrayIndex]);
        int xVaule = UnityEngine.Random.Range(-13, 13);//原宽度28
        int zVaule = UnityEngine.Random.Range(30, 40);
        tempObj.transform.position = new Vector3(xVaule, 0, zVaule);
        tempObj.transform.Rotate(new Vector3(0, 180, 0));
    }

    int maxVaule = 70;
    int floorIndex = 1;

    /// <summary>
    /// 检测地板是否移动
    /// </summary>
    public void CheckBackMove()
    {
        float zValue = player.position.z;
        if (zValue> maxVaule)
        {
            maxVaule += 100;
            if (floorIndex == 1)
            {
                floorOne.transform.position = new Vector3(0, 0, maxVaule + 130);
                floorIndex = 2;
            }
            else if (floorIndex == 2)
            {
                floorTwo.transform.position = new Vector3(0, 0, maxVaule + 130);
                floorIndex = 3;
            }
            else
            {
                floorThree.transform.position = new Vector3(0, 0, maxVaule + 130);
                floorIndex = 1;
            }

            StartCoroutine(CreateTrees()); 
        }
    }

    /// <summary>
    /// 随机生成动物
    /// </summary>
    private void CreatAnimal()
    {
        int arrayIndex = UnityEngine.Random.Range(0, animlObjs.Length);
        //GameObject tempObj = Instantiate(animlObjs[arrayIndex], Vector3.zero,Quaternion.identity);
        GameObject tempObj = ObjectPool.GetInstance().RequestCacheGameObejct(animlObjs[arrayIndex]);
        int xVaule = UnityEngine.Random.Range(-13, 13);
        int zVaule = (int)UnityEngine.Random.Range((player.transform.position.z + 20), (player.transform.position.z + 50));
        tempObj.transform.position = new Vector3(xVaule,0,zVaule);
        tempObj.transform.Rotate(new Vector3(0, 180, 0));
    }

    int treeNum = 0;

    /// <summary>
    /// 随机生成障碍树
    /// </summary>
    IEnumerator CreateTrees()
    {
        while (treeNum < 3)
        {
            yield return null;
            int arrayIndex = UnityEngine.Random.Range(0, treeObjs.Length);
            GameObject tempObj = ObjectPool.GetInstance().RequestCacheGameObejct(treeObjs[arrayIndex]);                    
            tempObj.transform.position = RandomPoint();
            treeNum++;
        }
        treeNum = 0;
    }


    public Vector3 RandomPoint()
    {
        int xVaule = UnityEngine.Random.Range(-13, 13);//原宽度28
        int zVaule = (int)UnityEngine.Random.Range((player.transform.position.z + 100),(player.transform.position.z + 150));
        Vector3 tempPoint = new Vector3(xVaule, 0, zVaule);
        return tempPoint;
    }

    /// <summary>
    /// 计算移动距离
    /// </summary>
    public void UpdataDistance()
    {
        moveDistance = Vector3.Distance(new Vector3(startPoint.x, player.position.y, startPoint.z), player.position);
        fractionText.text = "当前的奔跑的距离： " + ((int)moveDistance).ToString();
    }

    /// <summary>
    /// 播放警告动画
    /// </summary>
    public void PlayWaringAnimator(Vector3 point)
    {
        StartCoroutine(WaringFunc(point));
    }

    IEnumerator WaringFunc(Vector3 point)
    {
        waringImage.transform.position = point;
        for (int i=1;i<=60;i++)
        {
            if (i%10 == 0 )
            {
                bool isActive = waringImage.gameObject.activeSelf;
                if (isActive)
                {
                    waringImage.gameObject.SetActive(false);
                }
                else
                {
                    waringImage.gameObject.SetActive(true);
                }
            }
            yield return null;
        }

        waringImage.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        Player tempPlayer = player.GetComponent<Player>();
        //tempPlayer.PlayDeath();
        GameOverAction();
    }
}
