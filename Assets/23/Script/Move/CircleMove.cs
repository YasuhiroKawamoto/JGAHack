using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Extensions;

namespace Play.Element
{
    // 円移動の要素　
    public class CircleMove : ElementBase
    {
        //移動速度
        [SerializeField]

        private float _speed;
        //回転半径
        [SerializeField]
        private float _radius;
        //一回転ぼ所要時間
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

            //円周と周期から回転速度を求める
            _speed =Mathf.Abs( (2.0f * Mathf.PI * _radius) / _requiredTime);
        }



        /// 更新　円移動
        /// </summary>
        private void Update()
        {


            float x = Mathf.Cos((Time.time * (Mathf.PI * 2.0f)) / _requiredTime) * (_radius*2*0.1f);
            float y = Mathf.Sin((Time.time * (Mathf.PI * 2.0f)) / _requiredTime) * (_radius*2*0.1f);

            // _rigitBody2d.velocity =  new Vector3(x,  y, 0);

            _rigitBody2d.MovePosition(transform.position + new Vector3(x, y, 0) * Time.deltaTime);

        }




          

      

         
        



    }
}