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

        private float _speed;
        //移動量
        [SerializeField]
        private float _moveAmount;
        //所要時間
        [SerializeField]
        private float _requiredTime;
        //反転フラグ
        [SerializeField]
        private bool _reversFlag = false;

        [SerializeField]
        private Vector3 _basePos;

        [SerializeField]
        private Vector3 _UpEndPos;

        [SerializeField]
        private Vector3 _DownEndPos;
        [SerializeField]
        private float _moveCount = 0.0f;

        //リジットボディ
        private Rigidbody2D _rigitBody2d;




        /// <summary>
        /// 初期化
        /// </summary>
        public override void Initialize()
        {
            _rigitBody2d = GetComponent<Rigidbody2D>();


            _speed = _moveAmount / _requiredTime;

            _basePos = gameObject.transform.position;

            _UpEndPos = _basePos - new Vector3(0, -_moveAmount, 0);

            _DownEndPos = _basePos - new Vector3(0, _moveAmount, 0);

          

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
                _rigitBody2d.velocity = new Vector3(addX, -addY, 0.0f);
            }
            else
            {
                _rigitBody2d.velocity = new Vector3(addX, addY, 0.0f);

            }



            //移動量チェック
            CheckMovement();


        }


        private void CheckMovement()
        {

            _moveCount -= Time.deltaTime;


            if (Mathf.Abs(gameObject.transform.position.y - _basePos.y) < 0.1f)
            {
                _moveCount = _requiredTime;

            }


            //移動時間での反転
            if (_moveCount <= 0)
            {


                _reversFlag = !_reversFlag;
                _moveCount = _requiredTime * 2;



            }
            else
            {


                //移動距離に移動上限位置に到達で反転
                if (gameObject.transform.position.y >= _UpEndPos.y)
                {
                    _reversFlag = true;
                    _moveCount = _requiredTime * 2;
                }
                else if (gameObject.transform.position.y <= _DownEndPos.y)
                {
                    _reversFlag = false;
                    _moveCount = _requiredTime * 2;
                }

            }




        }

    }
}


