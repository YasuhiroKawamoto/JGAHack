using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using DG.Tweening;

namespace Play
{
    public class FallObject : MonoBehaviour
    {
        private BoxCollider2D _collider = null;

        public Collider2D Collider
        {
            get { return _collider ? _collider : _collider = GetComponent<BoxCollider2D>(); }
        }

        private Transform _parent = null;

        private bool _fall = false;
        private Element.RideFloor _ride = null;

        // 親の前フレームの位置
        private Vector3 _old;

        /// <summary>
        /// 初期化
        /// </summary>
        void Awake()
        {
            _parent = transform.parent;
            SetParent(_parent);
        }

        /// <summary>
        /// 判定前の更新
        /// </summary>
        void FixedUpdate()
        {
            // 親の移動量を取得
            var v = transform.parent.transform.position - _old;
            transform.position = transform.position + v * Time.deltaTime;
            _old = transform.parent.transform.position;

            if (_ride)
            {
                // 乗る(親子関係)
                if (_ride.transform != transform.parent)
                {
                    SetParent(_ride.transform.parent.transform);
                }
            }
            else
            {
                if (_parent)
                {
                    SetParent(_parent);
                }
            }

            if (_fall && (!_ride))
            {
                // 落ちた
                StartCoroutine(this.Fall());
            }

            _fall = false;
            _ride = null;
        }

        /// <summary>
        /// 当たっているとき
        /// </summary>
        /// <param name="other"></param>
        void OnTriggerStay2D(Collider2D other)
        {
            // 足元で判定
            var myBounds = Collider.bounds;
            if (other.bounds.Contains(myBounds.center - new Vector3(0.0f, myBounds.extents.y, 0.0f)))
            {
                if (other.tag == "Hole")
                {
                    _fall = true;
                }

                var ride = other.GetComponent<Element.RideFloor>();
                if (ride)
                {
                    _ride = ride;
                    _fall = false;
                }
            }

        }

        /// <summary>
        /// 判定から外れたとき
        /// </summary>
        /// <param name="other"></param>
        void OnTriggerExit2D(Collider2D other)
        {
            // 離れたとき親子関係解除
            var ride = other.GetComponent<Element.RideFloor>();
            if (!ride) return;

            if (ride.transform.parent == transform.parent)
            {
                SetParent(_parent);
            }
        }

        /// <summary>
        /// 親の設定
        /// </summary>
        /// <param name="parent"></param>
        void SetParent(Transform parent)
        {
            transform.parent = parent;
            _old = parent.transform.position;
        }

        /// <summary>
        /// 落ちる
        /// </summary>
        /// <returns></returns>
        private IEnumerator Fall()
        {
            Debug.Log("落ちたな");

            // プレイヤー死亡
            var player = this.GetComponent<Player>();

            if (player)
            {
                if (player.PlayerState == Player.State.Dead)
                {
                    yield break;
                }

                player.Dead(false);

                yield return StartCoroutine(FallStaging(player));

                // サイズを戻す
                this.transform.localScale = Vector3.one;

                // TODO 謎
                player.gameObject.SetActive(false);
                player.gameObject.SetActive(true);

                // リトライ
                Play.InGameManager.Instance.StageOver();
            }

        }
        private IEnumerator FallStaging(Player player)
        {
            bool end = false;

            // 小さくしていく
            this.transform.DOScale(Vector3.zero, 1.0f)
                .OnComplete(() => end = true);

            var pos = player.transform.position;
            switch (player.Dir)
            {
                case Direction.Left:
                    this.transform.DOMoveX(pos.x - 0.5f, 1.0f);
                    break;
                case Direction.Right:
                    this.transform.DOMoveX(pos.x + 0.5f, 1.0f);
                    break;
                case Direction.Back:
                    this.transform.DOMoveY(pos.y + 0.5f, 1.0f);
                    break;
                case Direction.Front:
                    this.transform.DOMoveY(pos.y - 0.5f, 1.0f);
                    break;
            }

            // 終わるまで待つ
            yield return new WaitUntil(() => end);
        }
    }
}