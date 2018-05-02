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

       
    

        //リジットボディ
        private Rigidbody2D _rigitBody2d;

        /// <summary>
        /// 初期化
        /// </summary>
        public override void Initialize()
        {
            _rigitBody2d = GetComponent<Rigidbody2D>();

            _speed = _moveAmount / _requiredTime;

            

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
                _rigitBody2d.velocity = new Vector3(-addX, addY, 0.0f);
            }
            else
            {
                _rigitBody2d.velocity = new Vector3(addX, addY, 0.0f);
            }



           



        }


      



        

    }


}