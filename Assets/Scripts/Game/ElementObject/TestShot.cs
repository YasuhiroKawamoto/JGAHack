﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Play.Enemy
{

	//簡易射撃テストスクリプト
	public class TestShot : MonoBehaviour
	{
		//生成オブジェクト（弾）
		[SerializeField]
		GameObject _bullet;
		//弾の速さ
		[SerializeField]
		float _bulletSpeed;
		//弾のベクトル
		[SerializeField]
		Vector3 _bulletVel;
		//発射時のズレ
		[SerializeField]
		Vector3 _shotOffset;
		//発射間隔
		[SerializeField]
		float _shotInterval = 2;
		//発射カウント
		[SerializeField]
		float _shotCount;
        //弾置き場
        GameObject _bulletPlace;

        // Use this for initialization
        void Start()
        {
            //弾置き場探し
            _bulletPlace = GameObject.Find("BulletPlace");
            //発射カウントのリセット
            _shotCount = _shotInterval;         
        }

        // Update is called once per frame
        void Update()
        {
            //発射カウントダウン
            _shotCount -= Time.deltaTime;
            //発射カウント0時
            if (_shotCount <= 0)
            {

                switch (GetComponent<Play.Element.DiectionTest>().GetDir())
                {
                    case Direction.Front:
                        //弾の方向設定（正面）
                        _bulletVel = new Vector3(0,1,0);
                        //オフセットの設定(テストなので後日修正)
                        _shotOffset = new Vector3(0,GetComponentInChildren<SpriteRenderer>().bounds.size.y / 2, 0);
                        break;

                    case Direction.Back:
                        //弾の方向設定（正面）
                        _bulletVel = new Vector3(0, -1, 0);
                        //オフセットの設定(テストなので後日修正)
                        _shotOffset = new Vector3(0,-GetComponentInChildren<SpriteRenderer>().bounds.size.y / 2, 0);
                        break;

                    case Direction.Left:
                        //弾の方向設定（正面）
                        _bulletVel = new Vector3(-1, 0, 0);
                        //オフセットの設定(テストなので後日修正)
                        _shotOffset = new Vector3(-GetComponentInChildren<SpriteRenderer>().bounds.size.x/2,0,0);
                        break;

                    case Direction.Right:
                        //弾の方向設定（正面）
                        _bulletVel = new Vector3(1, 0, 0);
                        //オフセットの設定(テストなので後日修正)
                        _shotOffset = new Vector3(GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2, 0,0);

                        break;


                }


                
                //// 弾丸の複製
                GameObject bullets = GameObject.Instantiate(_bullet) as GameObject;
                // 弾速設定
                bullets.GetComponent<Rigidbody2D>().velocity = _bulletVel * _bulletSpeed;
                // 弾丸の位置を調整
                bullets.transform.position = transform.position + _shotOffset;
                if (_bulletPlace)
                {
                    //弾丸を弾置き場の子供に設定
                    bullets.transform.parent = _bulletPlace.transform;
                }
                else
                {
                    //弾丸をゲームオブジェクトの子供に設定
                    bullets.transform.parent = transform;
                }
      
                //発車時間の再設定
                _shotCount = _shotInterval;
            }
        }
    }
}