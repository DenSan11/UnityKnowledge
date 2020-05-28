using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    private Transform playerTrans;
    Vector3 offest;

	void Start () {
        playerTrans = FindObjectOfType<Player>().transform;
        offest = transform.position - playerTrans.position;

    }
	
	// Update is called once per frame
	void Update () {
        Move();
    }

    public void Move()
    {
        transform.position = offest + playerTrans.position;
    }
}
