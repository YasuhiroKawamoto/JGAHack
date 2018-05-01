using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Play.Enemy
{
    //敵用ステータス
    public class EnemyStates : MonoBehaviour
    {

        //体力
        [SerializeField]
        int HP = 3;

        //現在方向(初期では↓)
        [SerializeField]
        Direction _direction = Direction.Down;
        public Direction direction {
            get { return _direction; }
            set { _direction = value; }
        }

        //方向の定義
        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }



        void OnTriggerEnter2D(Collider2D col)
        {
            
            
                //被ダメ
                HP--;
               
                //体力が0になったら消滅
                if (HP <= 0)
                {
                    Destroy(this.gameObject);
                }

            


        }
    }
}