﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class WaveContoller : MonoBehaviour {

    //ターゲットのトランスフォーム
    [SerializeField]
    private Transform _target;

    private Vector3 pos = Vector3.zero;

    //スピード
    [SerializeField]
    private float _speed = 0.5f;

    //ターゲットの範囲
    [SerializeField]
    private float _range = 1.0f;
   
    // 起動時
    void Start()
    {
      
    }

    // 更新
    void Update()
    {
        Move();

        if(Mathf.Abs(transform.position.x + _target.transform.position.x) < Mathf.Abs(_target.transform.position.x + _range) &&
           Mathf.Abs(transform.position.y + _target.transform.position.y) < Mathf.Abs(_target.transform.position.y + _range))
        {
            Destroy(gameObject);
        }

    }

    //移動処理
    private void Move()
    {
        pos = (_target.transform.position - transform.position);
        transform.position += pos.normalized * _speed;
    }

   
    //ターゲットのTransformを代入
    public void setVelocity(Transform target)
    {
        _target = target;
    }

}
