using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tekito : MonoBehaviour {

Vector3 pos;

	// Use this for initialization
	void Start () {
		pos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		pos.x = 9 + Mathf.Sin(Time.time)*1.5f;
		gameObject.transform.position = pos;
	}
}
