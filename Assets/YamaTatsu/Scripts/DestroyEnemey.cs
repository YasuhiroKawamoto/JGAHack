using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemey : MonoBehaviour {

    //
    [SerializeField]
    private float _time = 1.0f;
    //カウント
    private float _timeCount = 0.0f;

	// Use this for initialization
	void Start () {
		
        //


	}
	
	// Update is called once per frame
	void Update () {

        _timeCount += Time.deltaTime;

        //
        if(_timeCount > _time)
        {
            Destroy(this.gameObject);
        }


	}
}
