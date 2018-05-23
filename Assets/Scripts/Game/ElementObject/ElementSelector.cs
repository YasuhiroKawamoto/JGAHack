﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Play.Element;
using Extensions;

using System.Reflection;

namespace Play
{
    // 要素オブジェクトの選択と要素の移動用クラス
    public class ElementSelector : MonoBehaviour
    {
        enum TargetChoice
        {
            None,
            Next,
            Front
        }

        // ターゲットしているオブジェクト
        [SerializeField, ReadOnly]
        private ElementObject _targetObject = null;

        // 選択している要素コンテナ
        [SerializeField, ReadOnly]
        private ElementContainer _container = null;

        // TODO:選択したオブジェクトの要素テキスト
        [SerializeField]
        private Text _elementText;
        [SerializeField, ReadOnly]
        private Text[] _textList = null;

        private List<ElementObject> _targetList = null;
        private int _targetNum = 0;

        // ターゲットのゲームオブジェクト
        [SerializeField]
        private GameObject _target = null;

        // ロックオン
        private LockOn.LockOn _lockOn = null;

        void Start()
        {
            // コンテナ取得
            _container = GetComponent<ElementContainer>();

            // TODO: テキストリスト作成
            int num = (int)ElementType.length;
            _textList = new Text[num];

            // ロックオン関連の初期化
            _lockOn = new LockOn.LockOn();
            if (_targetList == null)
            {
                _targetList = _lockOn.GetLockOnList();
                _targetNum = _lockOn.GetNearObjOnList();
            }
        }

        void Update()
        {
            // 選択
            // 選択したオブジェクトを取得
            var selectObj = GetClickObject();
            TargetObject(selectObj);
            var con = GameController.Instance;

            var isTarget = TargetChoice.None;
            bool isSelect = false;
            bool isChange = false;

            if (con.GetConnectFlag())
            {
                isTarget = con.ButtonDown(Button.R1) ? TargetChoice.Front :
                            con.ButtonDown(Button.L1) ? TargetChoice.Next : TargetChoice.None;

                isSelect = con.ButtonDown(Button.A);
                isChange = con.ButtonDown(Button.X);
            }
            else
            {
                isTarget = Input.GetKeyDown(KeyCode.Space) ? TargetChoice.Next : TargetChoice.None;
                isSelect = Input.GetKeyDown(KeyCode.Z);
                isChange = Input.GetKeyDown(KeyCode.C);
            }

            if (isTarget != TargetChoice.None)
            {
                // カメラにエレメントオブジェクトが移っているとき探す
                if (_lockOn.CheckOnScreenAll())
                {
                    // 次のターゲットオブジェクトを取得
                    if (isTarget != TargetChoice.Next)
                    {
                        // 選択
                        TargetObject(GetTarget(1));
                    }
                    else if (isTarget != TargetChoice.Front)
                    {
                        TargetObject(GetTarget(-1));
                    }
                }
            }

            // 要素吸出し
            if (isSelect)
            {
                if (_targetObject)
                {
                    SelectObject();
                }
            }

            // 要素を移す
            if (isChange)
            {
                if (_targetObject)
                {
                    MoveElement(_targetObject);
                }
            }
        }

        /// <summary>
        /// ターゲットの取得
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>        
        private ElementObject GetTarget(int num)
        {
            _targetNum += num;

            if (_targetList.Count <= _targetNum)
            {
                _targetNum = 0;
            }
            else if (_targetNum < 0)
            {
                _targetNum = _targetList.Count - 1;
            }

            var obj = _targetList[_targetNum];
            //オブジェクトが「missing」（破壊済み）の場合
            if (obj == null)
            {
                //消すオブジェ
                //該当オブジェクトを排斥対象に
                var exclusionObj = obj;
                //リストから排斥
                _targetList.Remove(exclusionObj);
                //再起呼び出し
                obj = GetTarget(num);
            }
            //オブジェクトのチェックが外れている（再生待機）時
            else if (obj.gameObject.activeInHierarchy == false)
            {
                //再起呼び出し
                obj = GetTarget(num);
            }

            //カメラ内に入っていなければ飛ばし
            if (_lockOn.CheckOnScreen(obj.transform.position) == false)
            {
                //再起呼び出し
                obj = GetTarget(num);
            }

            // 思い出し中はタゲしない
            if (obj.Stats == ElementObject.ElementStates.Remember)
            {
                //再起呼び出し
                obj = GetTarget(num);
            }

            return obj;
        }

        /// <summary>
        /// オブジェクトをターゲット
        /// </summary>
        private void TargetObject(ElementObject obj)
        {
            if (obj)
            {
                // 要素をターゲット
                TargetElementObject(obj);
            }
        }

        /// <summary>
        /// 要素オブジェクトをターゲットしたときの処理
        /// </summary>
        /// <param name="elementObj"></param>
        private void TargetElementObject(ElementObject elementObj)
        {
            // ターゲット解除
            TargetRelease();

            // ターゲット
            _targetObject = elementObj;

            // TODO: 仮で選択したオブジェクトにテキストを付与
            // ======================================================
            // 子に要素追加
            var text = GameObject.Instantiate(_elementText);
            _targetObject.transform.SetChild(text.gameObject);
            // ターゲットマーカー作成
            var obj = Instantiate(_target);
            text.transform.SetChild(obj);
            text.transform.localPosition = Vector3.zero;
            text.gameObject.AddComponent<Canvas>();
            var scaler = text.gameObject.AddComponent<CanvasScaler>();
            scaler.dynamicPixelsPerUnit = 20;
            text.transform.localPosition = new Vector3(0.0f, 0.5f, 0.0f);
            text.transform.localScale = new Vector3(0.3f, 0.3f, 1.0f);
            text.fontSize = 1;
            text.alignment = TextAnchor.MiddleLeft;

            // テキスト変更

            text.text = string.Empty;
            foreach (var element in _targetObject.ElementList)
            {
                if (element)
                {
                    text.text += element.Type.ToString() + "\n";
                }
            }
            if (text.text == string.Empty)
            {
                text.text = "NoneElement";
            }
            // ======================================================
        }

        /// <summary>
        /// オブジェクトへのターゲットを解除
        /// </summary>
        private void TargetRelease()
        {
            // TODO: 追加したテキスト削除
            if (_targetObject)
            {
                var childs = _targetObject.transform.GetAllChild();
                foreach (var c in childs)
                {
                    if (c.name == "ElementText(Clone)")
                    {
                        Destroy(c.gameObject);
                    }
                }
            }
            _targetObject = null;
        }

        /// <summary>
        /// オブジェクトを選択
        /// </summary>
        private void SelectObject()
        {
            SelectRelease();
            _container.ReceiveAllElement(_targetObject.ElementList);
            // ターゲット解除
            TargetRelease();
            // TODO: テキスト追加
            AddText(_container.List.ToArray());
        }

        /// <summary>
        /// オブジェクトへの選択を解除
        /// </summary>
        private void SelectRelease()
        {
            foreach (var text in _textList)
            {
                if (text)
                {
                    GameObject.Destroy(text.gameObject);
                }
            }
            _container.AllDelete();
        }

        /// <summary>
        /// 次の選択できる要素を取得
        /// </summary>
        /// <param name="elObj"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private int SearchSelectElement(ElementObject elObj, int index)
        {
            int select = index;
            int listLength = elObj.ElementList.Length;
            if (0 < listLength)
            {
                select++;
                if (listLength <= select)
                {
                    select = 0;
                }

                if (elObj.ElementList[select] == null)
                {
                    // 再起して取得する
                    select = SearchSelectElement(elObj, select);
                }
            }
            return select;
        }

        /// <summary>
        /// 要素の移動
        /// </summary>
        /// <param name="selectObj"></param>
        private void MoveElement(ElementObject selectObj)
        {
            // リストを記憶していない場合は移動しない
            if (_container.List == null) return;

            // すべての要素を移動
            selectObj.ReceiveAllElement(_container.List.ToArray());

            // ターゲット解除
            TargetRelease();
        }
        // TODO: 要素テキスト追加
        private void AddText(ElementBase[] elements)
        {
            // テキスト削除
            float y = 0.0f;
            foreach (var element in elements)
            {
                if (element == null)
                {
                    continue;
                }

                var type = element.Type;

                // 子に要素追加
                var pos = new Vector3(-430.0f, -50.0f + y, 0.0f);
                var text = GameObject.Instantiate(_elementText);

                // UIルート取得
                var root = InGameManager.Instance.UIRoot;
                root.gameObject.transform.SetChild(text.gameObject);

                text.transform.localPosition = pos;

                // テキスト変更
                text.text = type.ToString();

                _textList[(int)type] = text;

                y -= 30.0f;
            }
        }

        /// <summary>
        /// TODO:左クリックしたオブジェクトを取得 
        /// </summary>
        /// <returns></returns>
        private ElementObject GetClickObject()
        {
            ElementObject result = null;

            // 左クリックされた場所のオブジェクトを取得
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D collition2d = Physics2D.OverlapPoint(tapPoint);
                if (collition2d)
                {
                    result = collition2d.GetComponent<ElementObject>();
                }
            }
            return result;
        }
    }
}