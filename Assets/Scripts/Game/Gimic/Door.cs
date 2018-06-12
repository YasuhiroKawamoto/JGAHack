
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

namespace Play
{
    public class Door : SwitchEvent
    {
        [SerializeField]
        private GameObject _player;

        // 対応しているキー
        public Doorkey _key;
       
        //コライダー
        private Collider2D _collider;

        //アニメーションラグ
        [SerializeField]
        private float _rugTime　= 0.8f;
        //動作中か？
        [SerializeField, ReadOnly]
        private bool _inAction = false;
        //閉じようとしているか？
        [SerializeField, ReadOnly]
        private bool _isCloseing = false;
        //閉じ切っているか？
        [SerializeField, ReadOnly]
        private bool _isClosed = true;

        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");

            _collider = this.GetComponent<BoxCollider2D>();
        }

        public override void KeyStayed()
        {
            //動作中でなく、閉じようとしていなく、閉じ切っているなら
            if (!_inAction && !_isCloseing && _isClosed)
            {
                //開く
                StartCoroutine(DoorOpen());
            }        
        }

        private void Update()
        {  
            //閉じようとしている
            if (_isCloseing)
            {         
                //プレイヤーが上に載ってなければ（ｙ軸判定）
                if (Mathf.Abs(_player.transform.position.y - gameObject.transform.position.y) > 0.8f )
                {
                    //閉じる
                    StartCoroutine(DoorClose());
                }
            }
        }

        public override void KeyCanceled()
        {
            //閉じようとする
            _isCloseing = true;          
        }

        //開きコルーチン
        private IEnumerator DoorOpen()
        {
            //閉じようとしていない
            _isCloseing = false;
            //スイッチ画像差し替え
            _key.GetComponent<Doorkey>().ChangeImage(STATE.DOWN);
            //開きアニメーション
            gameObject.GetComponent<SimpleAnimation>().CrossFade("Open", 0);
            //開閉時間分待機
            yield return new WaitForSeconds(_rugTime);
            if (_inAction)
            {
                yield break;
            }     
            //当たり判定復活解除
            _collider.enabled = false;
            //動作中ではない
            _inAction = false;      
            //閉じてはいない
            _isClosed = false;
        }


        //閉じるコルーチン
        private IEnumerator DoorClose()
        {
            //閉じようとしていない
            _isCloseing = false;
            //スイッチ画像差し替え
            _key.GetComponent<Doorkey>().ChangeImage(STATE.UP);
            //動作中
            _inAction = true;
            //閉じアニメーション
            gameObject.GetComponent<SimpleAnimation>().CrossFade("Close", 0);
            //当たり判定復活
            _collider.enabled = true;
            //開閉時間分待機
            yield return new WaitForSeconds(_rugTime);
            //動作中ではない
            _inAction = false;
            //閉じ切った
            _isClosed = true;
        }
    }
}