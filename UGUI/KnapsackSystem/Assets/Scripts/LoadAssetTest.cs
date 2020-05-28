using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAssetTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        WuQiSerializableSet wuqi = Resources.Load<WuQiSerializableSet>("Asset/weapon");
        Debug.Log(wuqi.WuQiConfigs[1].description); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
