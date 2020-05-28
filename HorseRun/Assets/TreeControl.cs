using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeControl : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update() {
        CheckIsGound();
    }

    private void CheckIsGound()
    {        
        if (!Physics.Raycast(transform.position + new Vector3(0,3,0), Vector3.down, 10, 1)) //中心点太低，适当调高
        {
            Debug.Log("树不在地面");
            ObjectPool.GetInstance().ReturnCacheGameObejct(this.gameObject);
        }
    }
}
