using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    public GameObject obj1;
    public GameObject obj2;

    // Use this for initialization
    void Start ()
    {
             
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ObjectPool.GetInstance().RequestCacheGameObejct(obj1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ObjectPool.GetInstance().RequestCacheGameObejct(obj2);
        }
    }
}
