using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play.Enemy
{

    public class PlayerDestroyer : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D col)
        {
            //持っているコライダーで処理変更
            if (gameObject.GetComponent<CircleCollider2D>())
            {
                //Circleコライダー（弾）に当たった場合。
                var player = col.GetComponent<Player>();
                if (player && gameObject.GetComponent<CircleCollider2D>().isTrigger)
                {
                    // プレイヤー死亡
                    player.Dead();
                }
            }
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            var player = col.transform.GetComponent<Player>();

            //ボックスコライダー（敵本体）に当たった時の判定
            if (player && !gameObject.GetComponent<BoxCollider2D>().isTrigger)
            {
                player.Dead();
            }
        }
    }

}