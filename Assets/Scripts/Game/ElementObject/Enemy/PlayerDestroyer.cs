using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play.Enemy
{

    public class PlayerDestroyer : MonoBehaviour
    {
        [SerializeField]
        GameObject CamMan;


        private void Awake()
        {
            CamMan = GameObject.Find("CameraManager");
        }


        void OnTriggerEnter2D(Collider2D col)
        {
            //持っているコライダーで処理変更
            if (gameObject.GetComponent<CircleCollider2D>())
            {
                //Circleコライダー（弾）に当たった場合。
                if (col.gameObject.tag == "Player" && gameObject.GetComponent<CircleCollider2D>().isTrigger)
                {
                    //プレイヤー死亡演出
                    col.gameObject.GetComponent<Player>().PlayerDead();
                }
            }
        }


        void OnCollisionEnter2D(Collision2D col)
        {
            //ボックスコライダー（敵本体）に当たった時の判定
            if (col.gameObject.tag == "Player"&&!gameObject.GetComponent<BoxCollider2D>().isTrigger)
            {
                //プレイヤー死亡演出
                col.gameObject.GetComponent<Player>().PlayerDead();

            }
        }
    }
    
}