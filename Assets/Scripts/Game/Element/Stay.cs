﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Extensions;


namespace Play.Element
{
    //その場にとどまる
    public class Stay : ElementBase
    {
        //滞在位置
        [SerializeField, ReadOnly]
        private Vector3 _stayPos;

        //リジットボディ
        private Rigidbody2D _rigitBody2d;

        public override void Initialize()
        {
            //リジットボディ取得
            _rigitBody2d = transform.GetComponent<Rigidbody2D>();
            //滞在位置取得
            _stayPos = transform.position;

        }


        void Awake()
        {
            _type = ElementType.Move;
        }

        // Update is called once per frame
        void Update()
        {        
            //位置修正
            _rigitBody2d.MovePosition(_stayPos);
        }
    }

}