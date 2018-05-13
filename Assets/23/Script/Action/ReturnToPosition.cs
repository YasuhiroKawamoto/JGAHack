using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;



namespace Play.Action
{



    public class ReturnToPosition : MonoBehaviour
    {
        //移動速度
        [SerializeField]
        private float _speed;

        //所要時間
        [SerializeField]
        private float _returnTime;
        //リジットボディ
        Rigidbody2D _rigidBody2d;
        //復帰開始位置
        [SerializeField, ReadOnly]
        private Vector3 _startPosition;
        //帰るべき場所
        [SerializeField, ReadOnly]
        private Vector3 _returnPosition;




        // Use this for initialization
        void Start()
        {
            _rigidBody2d = gameObject.GetComponent<Rigidbody2D>();


        }

        // Update is called once per frame
        void Update()
        {

        }




        public void SetReturnMove(Vector3 returnPos)
        {
            // Debug.Log("回帰セットぉ");
            //速度セット
            _speed = 1.0f;
            //開始位置セット

            //変えるべき場所セット
            _returnPosition = returnPos;

        }

        public void ReturnMove()
        {
            //目的位置に向かって一定速度で移動
            // Debug.Log("移動中");
            _rigidBody2d.MovePosition(Vector3.MoveTowards(transform.position, _returnPosition, Time.deltaTime * _speed));

        }
    }
}