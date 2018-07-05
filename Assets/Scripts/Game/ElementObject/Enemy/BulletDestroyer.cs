﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play.Enemy
{
    //弾の消滅用スクリプト
    public class BulletDestroyer : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D col)
        {
           // Debug.Log("イリ");
            //何かに当たれば弾消滅
            Destroy(gameObject);

        }

        void OnTriggerStay2D(Collider2D col)
        {
            //Debug.Log("待機");
            //何かに当たれば弾消滅
            Destroy(gameObject);

        }

        void OnTriggerExit2D(Collider2D col)
        {
            //Debug.Log("ヌケ");
            //何かに当たれば弾消滅
            Destroy(gameObject);

        }
    }
}
