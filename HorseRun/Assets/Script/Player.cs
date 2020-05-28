using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public GameObject circleObj;        //地面圆圈物体
    public GameObject lassoObj;         //套索物体
    public float lassoSpeed;            //套索旋转速度
    public Transform lassRootPoint;     //套索的骨骼节点
    Vector3 offestPoint;
    public bool isMove;
    public float timeScale;             //时间缩放值（慢放）
    public Animator playAnimator;
    public float moveSpeed;
    public bool isDeath;
    public AnimalControl animalControl;     //现在骑乘的动物



    void Start () {
        offestPoint = lassoObj.transform.position - lassRootPoint.position;
        playAnimator = GetComponentInChildren<Animator>();
        circleObj.SetActive(false);
        GameMode.GetInstance().GameOverAction += PlayDeath;
        //GameMode.GetInstance().animal = animalRide;
    }
	
	// Update is called once per frame
	void Update () {
        if (isDeath)
        {
            return;
        }

        LassRotate();

        if (Input.GetKeyDown(KeyCode.Space) && !isMove)
        {
            Debug.Log("起跳");
            playAnimator.SetBool("isJump", true);
            playAnimator.SetBool("isLand", false);
            transform.SetParent(null);
            Time.timeScale = timeScale;
            isMove = true;
            circleObj.SetActive(true);
            if (animalControl != null)
            {
                animalControl.ChangSpeed();
                animalControl.SetRideState(false);
                animalControl.thisBox.enabled = false;
            }
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("换马");
            Debug.Log(GameMode.GetInstance().animal);
            //在动物范围内再按一次换骑乘动物
            if (GameMode.GetInstance().animal!=null)
            {
                transform.SetParent(GameMode.GetInstance().animal.ridePoint);
                GameMode.GetInstance().animal.RestColor();
               
                GameMode.GetInstance().animal.ChangSpeed(moveSpeed);
                //GameMode.GetInstance().animal.thisBox.enabled = false;
                GameMode.GetInstance().animal.SetRideState(true);
                StartCoroutine(MoveToTarget());
                isMove = false;
                playAnimator.SetBool("isLand", true);
                playAnimator.SetBool("isJump", false);
                circleObj.SetActive(false);
                animalControl = GameMode.GetInstance().animal;
                GameMode.GetInstance().animal = null;
            }
        }

        if (isMove)
        {
            JumpMove();
        }

        CheckDeath();
    }

    /// <summary>
    /// 套索旋转
    /// </summary>
    public void LassRotate()
    {
        lassoObj.transform.position = offestPoint + lassRootPoint.position;
        lassoObj.transform.Rotate(Vector3.up, lassoSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 起跳移动
    /// </summary>
    public void JumpMove()
    {
        //transform.position += transform.forward * moveSpeed * Time.deltaTime;
        //transform.position += Vector3.down * 9.8f*0.4f * Time.deltaTime;
        transform.position += transform.forward * moveSpeed * 1.3f * Time.deltaTime;
        transform.position += Vector3.down * 9.8f * 0.3f * Time.deltaTime;
    }

    public void CheckDeath()
    {
        if (transform.position.y<0)
        {
            //PlayDeath();
            GameMode.GetInstance().GameOverAction();
        }
    }

    /// <summary>
    /// 玩家死亡
    /// </summary>
    public void PlayDeath()
    {
        Debug.Log("玩家落马");
        isDeath = true;
        Time.timeScale = 1;
        playAnimator.SetBool("isDeath", true);
        lassoObj.SetActive(false);
    }

    IEnumerator MoveToTarget()
    {
        for (int i=0;i<20;i++)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, 0.2f);
            yield return null;
        }

        transform.localPosition = Vector3.zero;
        Time.timeScale = 1;
    }
}
