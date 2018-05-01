using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//弾の消滅用スクリプト

namespace Play.Enemy
{
    public class BulletDestroyer : MonoBehaviour
    {

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

            Destroy(gameObject);

        }
    }

}
