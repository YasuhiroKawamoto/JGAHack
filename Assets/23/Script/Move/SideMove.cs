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
        //移動速度
        [SerializeField]

        private float _speed;
        //移動量
        [SerializeField]
        private float _moveAmount = 1;
        //所要時間
        [SerializeField]
        private float _requiredTime = 3;
        //反転フラグ
        [SerializeField]
        private bool _reversFlag = false;

        [SerializeField]
        private Vector3 _basePos;

        [SerializeField]
        private Vector3 _RightEndPos;

        [SerializeField]
        private Vector3 _LeftEndPos;
        [SerializeField]
        private float _moveCount = 0.0f;

        //リジットボディ
        private Rigidbody2D _rigitBody2d;

        void Awake()
        {
            _type = ElementType.Move;
        }
        /// <summary>
        /// 初期化
        /// </summary>
        public override void Initialize()
        {

            _rigitBody2d = GetComponent<Rigidbody2D>();


            _speed = _moveAmount / _requiredTime;

            _basePos = gameObject.transform.position;

            _RightEndPos = _basePos - new Vector3(0, -_moveAmount, 0);

            _LeftEndPos = _basePos - new Vector3(0, _moveAmount, 0);




        }

        /// <summary>
        /// 更新　左右移動
        /// </summary>
        private void Update()
        {

            var addX = _speed;
            var addY = 0;


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


            if (Mathf.Abs(gameObject.transform.position.x - _basePos.x) < 0.1f)
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
                if (gameObject.transform.position.x >= _RightEndPos.x)
                {
                    _reversFlag = true;
                    _moveCount = _requiredTime * 2;
                }
                else if (gameObject.transform.position.x <= _LeftEndPos.x)
                {
                    _reversFlag = false;
                    _moveCount = _requiredTime * 2;
                }

            }




        }





    }



}