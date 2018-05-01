using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Extensions;

namespace Play.Element
{
    // 上下移動の要素
    public class UpDownMove : ElementBase
    {
        //移動速度
        [SerializeField]
        private float _speed = 1.0f;

        //移動量制限
        [SerializeField]
        private float _moveAmountLimit = 5.0f;
        //移動量
        [SerializeField]
        private float _moveAmount;
        [SerializeField]
        //反転フラグ
        private bool _reversFlag = false;
       
        //リジットボディ
        private Rigidbody2D _rigitBody2d;

        /// <summary>
        /// 初期化
        /// </summary>
        public override void Initialize()
        {
            _rigitBody2d = GetComponent<Rigidbody2D>();
             //移動量の設定初期化
             _moveAmount = _moveAmountLimit;

        }

        /// <summary>
        /// 更新　上下移動
        /// </summary>
        private void Update()
        {

            var addX = 0;
            var addY = _speed;

          
            //移動量加算（反転対応）
            if (_reversFlag)
            {
                _rigitBody2d.velocity = new Vector3(addX, -addY, 0.0f) * Time.deltaTime;
            }
            else
            {
                _rigitBody2d.velocity = new Vector3(addX, addY, 0.0f) * Time.deltaTime;
            }



            //移動量チェック
            CheckMovement();


        }


        private void CheckMovement()
        {
            //移動量の減算
            _moveAmount -= Mathf.Abs(_speed)*Time.deltaTime;
            //移動量リセット
            if (_moveAmount <= 0)
            {
                _reversFlag = !_reversFlag;
                _moveAmount = _moveAmountLimit;
            }
        }

    }
}


