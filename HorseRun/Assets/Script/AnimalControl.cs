using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalControl: MonoBehaviour {

    public Transform ridePoint;     //玩家的骑乘点
    public float moveSpeed;
    public Material startMater;
    public SkinnedMeshRenderer rendererTest;
    public BoxCollider thisBox;
    public bool isRide;             //现在是否进入骑乘状态
    public Animator animator;       //动物自己的动画播放组件
    public float initSpeed;        //最开始的速度

    private Quaternion startQuat;

    private float waringTime = 6;        //进入危险时间
    public float waringDieTime = 3;     //警告时间
    private float nowTime;
    private bool isWaringState;     //进入危险状态

	void Start () {
        ridePoint = transform.Find("ridePoint");
        rendererTest = transform.GetChild(0).GetComponentInChildren<SkinnedMeshRenderer>();
        thisBox = GetComponent<BoxCollider>();
        startMater = rendererTest.material;
        //moveSpeed = 10.0f;
        initSpeed = moveSpeed;
        startQuat = transform.rotation;
        animator = GetComponent<Animator>();
        waringTime = 3.5f;
    }
	
	// Update is called once per frame
	void Update () {
        Move();

        //超出骑乘时间会落马
        if (isRide)
        {
            RideMove();
            nowTime += Time.deltaTime;
            if (nowTime >= waringTime && !isWaringState)
            {
                isWaringState = true;
                Vector3 scenePoint = Camera.main.WorldToScreenPoint(transform.position);
                GameMode.GetInstance().PlayWaringAnimator(scenePoint);
                animator.SetBool("isWaring", true);
            }
            else if (nowTime >= waringTime + waringDieTime)
            {
                Death();
                GameMode.GetInstance().GameOver();
            }
        }
        else
        {
            nowTime = 0;
            isWaringState = false;
        }
        CheckIsGound();
    }

    float t = 0;

    public void Move()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime*-1;
    }

    public void SwitchColor()
    {
        rendererTest.material = GameMode.GetInstance().yellowMater;
    }

    public void RestColor()
    {
        rendererTest.material = startMater;
    }

    public void ChangSpeed(float speed=0)
    {
        if (speed == 0)
        {
            //moveSpeed = initSpeed;
            moveSpeed = 0;
        }
        else
        {
            moveSpeed = speed;
        }
    }

    /// <summary>
    /// 骑乘状态下的移动  主要是旋转移动方向
    /// </summary>
    public void RideMove()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, -30, 0) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
        }
        else
        {
            transform.rotation = startQuat;
        }
    }
    
    /// <summary>
    /// 设置骑乘状态
    /// </summary>
    /// <param name="isRide"></param>
    public void SetRideState( bool isRide)
    {
        if (isRide)
        {
            animator.SetBool("isRidden", true);
        }
        else
        {
            animator.SetBool("isRidden", false);
        }
        this.isRide = isRide;
    }

    public void Death()
    {
        animator.SetBool("isDeath", true);
        moveSpeed = 0;
        SetRideState(false);
    }

    /// <summary>
    /// 检测是否在地面
    /// </summary>
    public void CheckIsGound()
    {
        if (!Physics.Raycast(transform.position+new Vector3 (0,2,0), Vector3.down,10,~(1<<8))) //~(1<<8) 打开除8之外的所有层
        {
            Debug.Log("动物不在地面");
            //Destroy(gameObject);
            ObjectPool.GetInstance().ReturnCacheGameObejct(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (isWaringState)
        //{
        //    if (!other.CompareTag("floor"))
        //    {
        //        Death();
        //        GameMode.GetInstance().GameOver();
        //    }
        //}
        if (isRide)
        {
            if (other.CompareTag("tree"))
            {
                Debug.Log("撞到树");
                Death();
                GameMode.GetInstance().GameOver();
            }
            else if (other.CompareTag("animal"))
            {
                SetRideState(false);
                GameMode.GetInstance().GameOver();
            }
        }       
    }
}
