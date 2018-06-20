using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		


	}
	
	// Update is called once per frame
	void Update () {

        //回転軸
        Vector3 axis = new Vector3(0, 0, 10);
        //回転の角度
        float angle = 100.0f * Time.deltaTime;

        Quaternion q = Quaternion.AngleAxis(angle, axis);

        //画像の回転
        transform.rotation = q * this.transform.rotation;

        //カーソルの拡大縮小
        transform.localScale = new Vector3(Mathf.Sin(Time.time * 2), Mathf.Sin(Time.time * 2), 1);

    }
}
