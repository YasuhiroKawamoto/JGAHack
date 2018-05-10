using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play.Enemy
{

    public class PlayerDestroyer : MonoBehaviour
    {

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                //プレイヤー死亡処理
                InGameManager.Instance.StageOver();

            }

        }


        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                //プレイヤー死亡処理
                InGameManager.Instance.StageOver();

            }

        }
    }
    
}