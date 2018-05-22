using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;


namespace Play.Enemy
{
    //タックルアクション
    public class Tackle : MonoBehaviour
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

        //アクティブ時
        void OnEnable()
        {
            //初期位置首都機
            _startPos = transform.position;
            //剛体取得
            _rigidBody2d = GetComponentInParent<Rigidbody2D>();
            //向き取得
            _dir = gameObject.GetComponentInChildren<Play.Element.DiectionTest>().GetDir();
            //タックル用当たり判定生成
            SetCollider();
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


        private IEnumerator TackleMove(GameObject obj)
        {
            _isFound = true;

            yield return new WaitForSeconds(_waitTime);

            //タックル終了地点セット
            _endPos = obj.transform.position;

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
    }
}