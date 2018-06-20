using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;



namespace Play.Element
{

    //簡易射撃テストスクリプト
    public class TestShot : ElementBase
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
        // レンダラー
        SpriteRenderer _renderer = null;

        Direction _dir;

        [SerializeField]
        bool _canShot;

        [SerializeField]
         LayerMask _layerMask;
        //射撃可能判定用レイ情報
        Vector2 _rayDirection;
        //TODO レイの距離(今は大体１マス分)
        float _rayDistance = 1.0f;

        void Awake()
        {
            _type = ElementType.Action;
        }


        // Use this for initialization
        public override void Initialize()
        {
            //マスク設定
            _layerMask = LayerMask.GetMask(new string[] { "Default", "Immortal" });
        }


        public void OnEnable()
        {
            //マスク設定
            _layerMask = LayerMask.GetMask(new string[] { "Default", "Immortal" });
            //弾置き場探し
            _bulletPlace = GameObject.Find("BulletPlace");
            //発射カウントのリセット
            _shotCount = _shotInterval;
            _renderer = GetComponentInParent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            //発射カウントダウン
            _shotCount -= Time.deltaTime;

            //発射カウント0時
            if (_shotCount <= 0)
            {
                // 向きの取得
                 _dir = GetEnemyDirection();

                switch (_dir)
                {
                    case Direction.Front:
                        //弾の方向設定（正面）
                        _bulletVel = new Vector3(0, 1, 0);
                        //オフセットの設定(テストなので後日修正)
                        _shotOffset = new Vector3(0, _renderer.bounds.size.y, 0);

                        break;
                    case Direction.Back:
                        //弾の方向設定（正面）
                        _bulletVel = new Vector3(0, -1, 0);
                        //オフセットの設定(テストなので後日修正)
                        _shotOffset = new Vector3(0, -_renderer.bounds.size.y, 0);

                        break;
                    case Direction.Left:
                        //弾の方向設定（正面）
                        _bulletVel = new Vector3(-1, 0, 0);
                        //オフセットの設定(テストなので後日修正)
                        _shotOffset = new Vector3(-_renderer.bounds.size.x, 0, 0);

                        break;
                    case Direction.Right:
                        //弾の方向設定（正面）
                        _bulletVel = new Vector3(1, 0, 0);
                        //オフセットの設定(テストなので後日修正)
                        _shotOffset = new Vector3(_renderer.bounds.size.x, 0, 0);

                        break;
                }

                //レイによる射撃可能判定
                RaycastHit2D hitInfo;
                //レイの向き
                _rayDirection = _bulletVel;
                //レイの作成
                hitInfo = Physics2D.Raycast(gameObject.transform.position, _rayDirection, _rayDistance, _layerMask);
                //レイの当たり判定
                if (hitInfo.collider != null)
                {                 
                    _canShot = false;
                }
                else
                {                
                    _canShot = true;
                }

                //打てるなら打つ
                if (_canShot)
                {
                    // 弾丸の複製
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
                    bullets.GetComponent<BulletAniCon>().ChangeAnim(_dir);
                }

                //発車時間の再設定
                _shotCount = _shotInterval;
            }
        }

        /// <summary>
        /// 現在の向きを取得
        /// </summary>
        /// <returns></returns>
        private Direction GetEnemyDirection()
        {
            var dirs = GetComponents<Play.Element.DiectionTest>();
            Play.Element.DiectionTest dir = null;
            foreach (var d in dirs)
            {
                if (d.enabled)
                {
                    dir = d;
                }
            }

            if (dir == null)
            {
                // 設定されていない場合正面
                return gameObject.GetComponent<ElementObject>().GetTmpDirection();
            }

            // 向きを返す
            return dir.GetDir();
        }
    }
}