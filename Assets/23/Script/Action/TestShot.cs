using System.Collections;
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

        // Use this for initialization
        void Start()
        {
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
                //方向決定と弾生成時オフセット設定
                switch (GetComponent<EnemyStates>().direction)
                {
                    case Play.Enemy.EnemyStates.Direction.Up:
                        _bulletVel = new Vector3(0, _bulletSpeed, 0);
                        _shotOffset = new Vector3(0, gameObject.GetComponent<SpriteRenderer>().bounds.size.y, 0);
                        break;

                    case Play.Enemy.EnemyStates.Direction.Down:
                        _bulletVel = new Vector3(0, -_bulletSpeed, 0);
                        _shotOffset = new Vector3(0, -gameObject.GetComponent<SpriteRenderer>().bounds.size.y, 0);
                        break;

                    case Play.Enemy.EnemyStates.Direction.Left:
                        _bulletVel = new Vector3(-_bulletSpeed, 0, 0);
                        _shotOffset = new Vector3(-gameObject.GetComponent<SpriteRenderer>().bounds.size.x, 0, 0);
                        break;

                    case Play.Enemy.EnemyStates.Direction.Right:
                        _bulletVel = new Vector3(_bulletSpeed, 0, 0);
                        _shotOffset = new Vector3 (gameObject.GetComponent<SpriteRenderer>().bounds.size.x, 0, 0);
                        break;
                }

                // 弾丸の複製
                GameObject bullets = GameObject.Instantiate(_bullet) as GameObject;
                // 弾速設定
                bullets.GetComponent<Rigidbody2D>().velocity = _bulletVel ;
                // 弾丸の位置を調整
                bullets.transform.position = transform.position +_shotOffset;

                //発車時間の再設定
                _shotCount = _shotInterval;
            }
        }
    }
}