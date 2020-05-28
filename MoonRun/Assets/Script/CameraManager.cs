using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    public GameObject target;
    public float height;
    public float distance;
    Vector3 pos;
    bool isShaking = false;
    public static CameraManager instance;

	// Use this for initialization
	void Start () {
        instance = this;
        pos = transform.position;
	}

    public void CameraShake()
    {
        if (!isShaking)
            StartCoroutine(ShakeCoroutine());
    }

    /// <summary>
    /// 碰撞时的相机抖动
    /// </summary>
    /// <returns></returns>
    IEnumerator ShakeCoroutine()
    {
        isShaking = true;
        float time = 0.5f;
        while (time > 0)
        {
            transform.position = new Vector3(
                target.transform.position.x + Random.Range(-0.3f, 0.3f),
                target.transform.position.y + height,
                target.transform.position.z - distance);
            time -= Time.deltaTime;
            yield return null;
        }
        isShaking = false;
    }

    void LateUpdate()
    {
        //相机跟随
        if (!isShaking && GameController.instance.isPlay && !GameController.instance.isPause)
        {
            pos.x = target.transform.position.x;
            pos.y = Mathf.Lerp(pos.y, target.transform.position.y + height, Time.deltaTime * 5);
            //pos.z = Mathf.Lerp(pos.z, target.transform.position.z - distance, Time.deltaTime * 5);
            pos.z = target.transform.position.z - distance;
            transform.position = pos;
        }
    }
}