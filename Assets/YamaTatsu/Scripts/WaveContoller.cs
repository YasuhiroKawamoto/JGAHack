using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveContoller : MonoBehaviour {

    //ターゲットの座標
    [SerializeField]
    private Vector3 _targetPos = new Vector3(0, 0, 0);

    //スピード
    [SerializeField]
    private float _speed = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        MoveWave();
	}

    //ターゲットの座標まで進む関数
    void MoveWave()
    {
        //計算
        Vector3 pos = _targetPos - transform.position;
        //正規化
        transform.position += pos.normalized * _speed;

    }

    //pPos プレイヤーの座標　tPos ターゲットの座標
    public void setVelocity(Vector3 tPos)
    {
        _targetPos = tPos;
    }

}
