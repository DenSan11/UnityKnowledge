using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirceColler : MonoBehaviour {

    public float startY;        //开始的Y值
	void Start () {
        startY = transform.position.y;

    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, startY, transform.position.z);   //固定Y值
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("可骑乘");
        if (GameMode.GetInstance().animal != null)  return;
        
        AnimalControl animal = other.GetComponent<AnimalControl>();
        if (animal != null)
        {
            GameMode.GetInstance().animal = animal;
            GameMode.GetInstance().isCanRide = true;
            animal.SwitchColor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("不可骑乘");
        var animal = GameMode.GetInstance().animal;
        if (animal != null)
        {
            AnimalControl tempAnimal = other.GetComponent<AnimalControl>();
            if (tempAnimal == animal)
            {
                GameMode.GetInstance().animal = null;
                GameMode.GetInstance().isCanRide = false;
                animal.RestColor();
            }
        }


    }
}
