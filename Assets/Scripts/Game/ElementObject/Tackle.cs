using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;


namespace Play.Element
{
    //タックルアクション
    public class Tackle : ElementBase
    {
        //タックルの速度
        [SerializeField]
        private float _speed;
        //タックル開始判定範囲
        [SerializeField]
        private float _range;
        //タックル開始までの時間
        [SerializeField]
        private float _waitTime;
        [SerializeField, ReadOnly]
        private Direction _dir;


        private Direction _tmpDir;
        //タックル開始地点
        [SerializeField, ReadOnly]
        private Vector3 _startPos;
        //タックル終了位置
        [SerializeField, ReadOnly]
        private Vector3 _endPos;
        //移動用剛体
        Rigidbody2D _rigidBody2d;
        [SerializeField, ReadOnly]
        private bool _isFound = false;


        //可視範囲用画像
        [SerializeField]
        private Sprite _rangeSprite;

        private GameObject Range;

        private BoxCollider2D col;

        private SpriteRenderer spr;

        void Awake()
        {
            _type = ElementType.Action;
        }


        public override void  Initialize()
        {
            //初期位置首都機
            _startPos = transform.position;
            //剛体取得
            _rigidBody2d = GetComponentInParent<Rigidbody2D>();
            //向き取得
            _dir = gameObject.GetComponentInChildren<Play.Element.DiectionTest>().GetDir();

            _tmpDir = _dir;
            //タックル用当たり判定生成
            SetCollider();
        }


        private void Update()
        {
            _dir = GetEnemyDirection();

            if (_dir != _tmpDir)
            {

                ChangeDirection(_dir);

                _tmpDir = _dir;
            }
        }


        private void OnDisable()
        {
            //非アクティブ時にコライダー破棄
            Destroy(GetComponent<BoxCollider2D>());
        }

        //プレイヤー発見用のコライダーを作成。
        private void SetCollider()
        {
            //// オブジェクトにBoxCollider2Dを貼り付ける
            gameObject.AddComponent<BoxCollider2D>();
            //新しく作成した空のオブジェクトに自分をはっつける
            gameObject.transform.parent = transform;
            //トリガーに設定
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            //当たり判定のセット
            switch (_dir)
            {
                case Direction.Front:
                    //コライダーオフセットの作成
                    gameObject.GetComponent<BoxCollider2D>().offset = new Vector3(0, 1, 0);
                    //コライダーサイズの設定
                    gameObject.GetComponent<BoxCollider2D>().size = new Vector3(1, _range, 1);
                    break;
                case Direction.Back:
                    //コライダーオフセットの作成
                    gameObject.GetComponent<BoxCollider2D>().offset = new Vector3(0, -1, 0);
                    //コライダーサイズの設定
                    gameObject.GetComponent<BoxCollider2D>().size = new Vector3(1, _range, 1);
                    break;
                case Direction.Left:
                    //コライダーオフセットの作成
                    gameObject.GetComponent<BoxCollider2D>().offset = new Vector3(-1, 0, 0);
                    //コライダーサイズの設定
                    gameObject.GetComponent<BoxCollider2D>().size = new Vector3(_range, 1, 1);
                    break;
                case Direction.Right:
                    //コライダーオフセットの作成
                    gameObject.GetComponent<BoxCollider2D>().offset = new Vector3(1, 0, 0);
                    //コライダーサイズの設定
                    gameObject.GetComponent<BoxCollider2D>().size = new Vector3(_range, 1, 1);
                    break;
            }

            Show();
        }



        public void ChangeDirection(Direction dir)
        {
            Debug.Log("向き変更");
            //当たり判定のセット
            switch (_dir)
            {
                case Direction.Front:
                    //コライダーオフセットの作成
                    gameObject.GetComponent<BoxCollider2D>().offset = new Vector3(0, 1, 0);
                    //コライダーサイズの設定
                    gameObject.GetComponent<BoxCollider2D>().size = new Vector3(1, _range, 1);
                    break;
                case Direction.Back:
                    //コライダーオフセットの作成
                    gameObject.GetComponent<BoxCollider2D>().offset = new Vector3(0, -1, 0);
                    //コライダーサイズの設定
                    gameObject.GetComponent<BoxCollider2D>().size = new Vector3(1, _range, 1);
                    break;
                case Direction.Left:
                    //コライダーオフセットの作成
                    gameObject.GetComponent<BoxCollider2D>().offset = new Vector3(-1, 0, 0);
                    //コライダーサイズの設定
                    gameObject.GetComponent<BoxCollider2D>().size = new Vector3(_range, 1, 1);
                    break;
                case Direction.Right:
                    //コライダーオフセットの作成
                    gameObject.GetComponent<BoxCollider2D>().offset = new Vector3(1, 0, 0);
                    //コライダーサイズの設定
                    gameObject.GetComponent<BoxCollider2D>().size = new Vector3(_range, 1, 1);
                    break;

             
            }
            Show();
        }


        public void Show()
        {
            if (Range)
            {
                Destroy(Range);
                Range = null;
            }
            //判定用オブジェクト生成
            Range = new GameObject("Renge");
            //判定オブジェクトをゲームオブジェクトの子供に設定
            Range.transform.SetParent(gameObject.transform);
            //判定オブジェクトにSpriteRendererを取り付け
            Range.gameObject.AddComponent<SpriteRenderer>();
            //画像の設定
            var spr = Range.gameObject.GetComponent<SpriteRenderer>();
            //スプライト設定
            spr.sprite = _rangeSprite;
            //カラー設定
            spr.color = new Vector4(1, 0, 0, 0.2f);
            //表示優先度
            spr.sortingOrder = 10;
            //当たり判定取得
            col = gameObject.GetComponent<BoxCollider2D>();
            //判定オブジェクトの位置調整
            Range.transform.localPosition = col.transform.localPosition + new Vector3(col.offset.x, col.offset.y, 0);
            //判定オブジェクトのサイズ調整
            Range.transform.localScale = new Vector3(col.size.x + col.bounds.extents.x + 0.2f, col.size.y + col.bounds.extents.y + 0.2f, 0);

        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            //攻撃判定に触れたのがプレイヤーなら
            if (collision.gameObject.tag == "Player")
            {
                //タックルの設定
                if (!_isFound)
                {
                    StartCoroutine(TackleMove(collision.gameObject));
                }
            }
        }


        private void OnTriggerStay2D(Collider2D collision)
        {
            //攻撃判定に触れたのがプレイヤーなら
            if (collision.gameObject.tag == "Player")
            {
                //タックルの設定
                if (!_isFound)
                {
                    StartCoroutine(TackleMove(collision.gameObject));
                }
            }
        }


        private IEnumerator TackleMove(GameObject obj)
        {
            _isFound = true;

            //タックル終了地点セット
            _endPos = obj.transform.position;

            if (_dir == Direction.Left || _dir == Direction.Right)
            {
                //真横に固定
                _endPos.y = gameObject.transform.position.y;

            }
            else if(_dir == Direction.Front || _dir == Direction.Back)
            {

                //真上or真下に固定
                _endPos.x = gameObject.transform.position.x;
            }
          
            yield return new WaitForSeconds(_waitTime);

            while (transform.position != _endPos)
            {
                // 目標置に向かって移動
                _rigidBody2d.MovePosition(Vector3.MoveTowards(transform.parent.position, _endPos, Time.deltaTime * _speed));
                yield return null;
            }
            //元の位置に戻るコルーチン開始
            yield return StartCoroutine(ReturnStartPos());
        }


        //開始位置に戻る
        private IEnumerator ReturnStartPos()
        {
            while (transform.position != _startPos)
            {
                // TODO: 元の位置に向かって移動
                _rigidBody2d.MovePosition(Vector3.MoveTowards(transform.parent.position, _startPos, Time.deltaTime * _speed));
                yield return null;
            }
            //プレイヤー未発見状態に戻る
            _isFound = false;
        }



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
                return _tmpDir;
            }

            // 向きを返す
            return dir.GetDir();
        }
    }
}