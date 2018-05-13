using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play.Element
{
    // 要素を持つオブジェクトクラス
    public class ElementObject : Extensions.MonoBehaviourEx
    {
        // 付与されている要素たち
        [SerializeField, Extensions.ReadOnly]
        private ElementBase[] _elementList = null;

        public ElementBase[] ElementList
        {
            get { return _elementList; }
            set { _elementList = value; }
        }

        // 忘れない要素
        private ElementBase[] _rememberList = null;

        // 初期位置
        [SerializeField]
        private Vector3 _initPos = Vector3.zero;

        // 上書き時の位置リスト
        [SerializeField]
        private List<Vector3> _overwritePosList = new List<Vector3>();

        // 我に戻る時間(秒)
        static readonly float _returnTime = 5.0f;

        /// <summary>
        /// 初期化
        /// </summary>
        private void Awake()
        {
            _initPos = transform.position;
        }
        private void Start()
        {
            ElementUpdate();
        }

        /// <summary>
        /// アタッチされている要素を検出
        /// </summary>
        public void ElementUpdate()
        {
            int index = (int)ElementType.length;
            _elementList = new ElementBase[index];

            var array = this.GetComponents<ElementBase>();

            foreach (var element in array)
            {
                int typeIndex = (int)element.Type;

                if (typeIndex < 0)
                {
                    // タイプがない場合は削除
                    Object.Destroy(element);
                }

                // 実行されていないときはスキップ
                if (element.enabled == false)
                {
                    continue;
                }

                if (_elementList[typeIndex])
                {
                    // タイプがかぶっている場合後半を反映
                    _elementList[typeIndex].Discard();
                    Object.Destroy(_elementList[typeIndex]);
                }

                _elementList[typeIndex] = element;
                element.Initialize();
            }
        }

        /// <summary>
        /// 要素をすべて受け取る
        /// </summary>
        public bool ReceiveAllElement(ElementBase[] receiveList)
        {
            // 忘れてはいけないものがある…
            if (_rememberList != null)
            {
                return false;
            }

            int index = (int)ElementType.length;
            _rememberList = new ElementBase[index];

            // 現在の要素を止める
            for (int i = 0; i < _elementList.Length - 1; i++)
            {
                if (_elementList[i])
                {
                    _elementList[i].enabled = false;
                    _rememberList[i] = _elementList[i];
                }
            }

            // 要素のコピー移動
            foreach (var element in receiveList)
            {
                if (element)
                {
                    this.CopyComponent(element);

                    // 要素の更新
                    this.ElementUpdate();
                }
               
            }

            //上書き時の位置を保存
            _overwritePosList.Add(transform.position);
           

            // n秒後思い出すコルーチン
            StartCoroutine(WaitSanity());

            return true;
        }

        /// <summary>
        /// 正気に戻るのを待つ
        /// </summary>
        /// <returns></returns>
        private IEnumerator WaitSanity()
        {
            // 待つ
            yield return new WaitForSeconds(_returnTime);

            // 正気になる
            ReturnToSanity();
        }

        /// <summary>
        /// 正気に戻る
        /// </summary>
        public void ReturnToSanity()
        {
            StartCoroutine(ReturnToSanityCorutine());
        }
        private IEnumerator ReturnToSanityCorutine()
        {
            // 今の要素を忘れる
            ForgetAllElement();
            
            // 元の位置に戻る
            yield return ReturnToInitPos();

            // 要素を思い出す
            ReCallElement();
        }

        /// <summary>
        /// 初期位置に戻る
        /// </summary>
        private IEnumerator ReturnToInitPos()
        {
            //リスト内要素逆回し用のカウント
            int Count = _overwritePosList.Count-1;
            //上書き時の位置をセット
            gameObject.GetComponent<Play.Action.ReturnToPosition>().SetReturnMove(_overwritePosList[Count]);

            //ループ処理
            while (true)
            {
                //上書き位置に戻れば
                if (transform.position == _overwritePosList[Count])
                {
                    //Debug.Log("我戻れり");
                    if (0 < Count)
                    {
                        Count--;
                        //上書き時の位置をセット
                        gameObject.GetComponent<Play.Action.ReturnToPosition>().SetReturnMove(_overwritePosList[Count]);
                    }
                    else
                    {
                        //初期位置をセット
                        gameObject.GetComponent<Play.Action.ReturnToPosition>().SetReturnMove(_initPos);
                    }            
                }
                   
                //元の位置に戻れば
                if (transform.position == _initPos)
                {
                    
                    //Debug.Log("我完全に戻れり");
                    //上書き位置リストのクリア
                    _overwritePosList.Clear();
                    //コルーチン終わり
                    yield break;
                }
                else
                {
                    // TODO: 元の位置に向かって移動
                    gameObject.GetComponent<Play.Action.ReturnToPosition>().ReturnMove();
                }

                // 毎フレームループ
                yield return null; 
            }   
        }

        // 現在の要素をすべて忘れる
        private void ForgetAllElement()
        {
            foreach (var element in _elementList)
            {
                if (element)
                {
                    Destroy(element);
                }
            }
            _elementList = null;
        }

        /// <summary>
        /// 要素を思い出す
        /// </summary>
        private void ReCallElement()
        {
            foreach (var element in _rememberList)
            {
                if (element)
                {
                    element.enabled = true;
                }
            }
            // 更新
            ElementUpdate();
        }
    }
}