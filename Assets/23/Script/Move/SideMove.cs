using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Extensions;

namespace Play.Element
{
    // 左右移動の要素　
    public class SideMove : ElementBase
    {
        //移動速度
        [SerializeField]
        private float _speed = 1.0f;
        //移動量制限
        [SerializeField]
        private float _moveAmountLimit = 100.0f;
        //移動量
        [SerializeField]
        private float _moveAmount;
        //反転フラグ
        private bool _reversFlag = false;
        // 前回加えた移動量
        private Vector3 _addSpeed = Vector3.zero;

        /// <summary>
        /// 初期化
        /// </summary>
        public override void Initialize()
        {
            //移動量の設定初期化
            _moveAmount = _moveAmountLimit;

        }

        /// <summary>
        /// 更新　左右移動
        /// </summary>
        private void Update()
        {
            var position = transform.position;

            // 前回移動した分を減らす(反転対応)
            if (_reversFlag)
            {
                position += _addSpeed;
            }
            else
            {
                position -= _addSpeed;
            }

            var addX = Time.time * _speed;
            var addY = 0;

            _addSpeed = new Vector3(addX, addY, 0.0f);

            //移動量加算（反転対応）
            if (_reversFlag)
            {
                this.PosX = position.x - addX;
                this.PosY = position.y - addY;
            }
            else
            {
                this.PosX = position.x + addX;
                this.PosY = position.y + addY;
            }

            CheckMovement();


        }


        private void CheckMovement()
        {
            //移動量の減算
            _moveAmount -= Mathf.Abs(_speed) * Time.deltaTime;
            //移動量リセット
            if (_moveAmount <= 0)
            {
                _reversFlag = !_reversFlag;
                _moveAmount = _moveAmountLimit;
            }
        }

    }
}