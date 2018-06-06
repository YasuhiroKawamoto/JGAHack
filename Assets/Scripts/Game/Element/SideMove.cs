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
        [SerializeField, ReadOnly]
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
        //初期反転判定
        [SerializeField, ReadOnly]
        private bool _isSetRevers;
        //移動開始座標
        [SerializeField, ReadOnly]
        private Vector3 _basePos;
        //右終点座標
        [SerializeField, ReadOnly]
        private Vector3 _RightEndPos;
        //左終点座標
        [SerializeField, ReadOnly]
        private Vector3 _LeftEndPos;
        //反転識別用移動カウント
        [SerializeField, ReadOnly]
        private float _moveCount = 0.0f;
        //リジットボディ
        private Rigidbody2D _rigitBody2d;
        

        void Awake()
        {
            _type = ElementType.Move;
            //初期反転判定のセット
            _isSetRevers = _reversFlag;

        }
        /// <summary>
        /// 初期化
        /// </summary>
        public override void Initialize()
        {
            //リジットボディ取得
            _rigitBody2d = transform.parent.GetComponent<Rigidbody2D>();
            //移動可能時間のセット
            _moveCount = _requiredTime;
            //速度の決定
            _speed = _moveAmount / _requiredTime;
            //移動開始座標の取得
            _basePos = gameObject.transform.position;
            //右終点座標の設定
            _RightEndPos = _basePos + new Vector3(_moveAmount, 0, 0);
            //左終点座標の設定
            _LeftEndPos = _basePos - new Vector3(_moveAmount, 0, 0);
            //反転判定を再設定
            _reversFlag = _isSetRevers;

        }

        /// <summary>
        /// 更新　左右移動
        /// </summary>
        private void Update()
        {
            //移動速度設定
            var addX = _speed;
            var addY = 0;

            //移動量加算（反転対応）
            if (_reversFlag)
            {
                _rigitBody2d.MovePosition(transform.position + new Vector3(-addX, addY, 0) * Time.deltaTime);
            }
            else
            {
                _rigitBody2d.MovePosition(transform.position + new Vector3(addX, addY, 0) * Time.deltaTime);
            }
            //移動量チェック
            CheckMovement();
        }


        private void CheckMovement()
        {
            //反転識別用移動カウントダウン
            _moveCount -= Time.deltaTime;



            //移動時間での反転(壁に引っかかるなど停滞時用)
            if (_moveCount <= 0)
            {
                //反転フラグの切り替え
                _reversFlag = !_reversFlag;
                //反転識別用移動カウントリセット
                _moveCount = _requiredTime * 2;
            }
            else
            {
                //移動上限位置に到達で反転
                if (gameObject.transform.position.x >= _RightEndPos.x)
                {
                    //反転フラグの切り替え
                    _reversFlag = true;
                    //反転識別用移動カウントリセット
                    _moveCount = _requiredTime * 2;
                }
                else if (gameObject.transform.position.x <= _LeftEndPos.x)
                {
                    //反転フラグの切り替え
                    _reversFlag = false;
                    //反転識別用移動カウントリセット
                    _moveCount = _requiredTime * 2;
                }
            }
        }
    }
}