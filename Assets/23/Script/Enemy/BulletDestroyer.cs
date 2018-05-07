using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play.Enemy
{
    //弾の消滅用スクリプト
    public class BulletDestroyer : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D col)
        {
            //何かに当たれば弾消滅
            Destroy(gameObject);

        }
    }
}
