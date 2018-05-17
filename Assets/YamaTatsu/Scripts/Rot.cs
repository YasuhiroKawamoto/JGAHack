using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rot : MonoBehaviour {

    ParticleSystem[] pss;

	// Use this for initialization
	void Start () {

        //子供のパーティクルをすべて取得
        pss = GetComponentsInChildren<ParticleSystem>();

    }
	
	// Update is called once per frame
	void Update () {

        //回転軸
        Vector3 axis = new Vector3(0, 10, 0);
        //回転の角度
        float angle = 100.0f*Time.deltaTime;

        Quaternion q = Quaternion.AngleAxis(angle, axis);

        transform.rotation = q * this.transform.rotation;
	}
}
