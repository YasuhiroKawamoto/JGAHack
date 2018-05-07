using System.Collections;
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
        // ターゲットしているオブジェクト
        [SerializeField, ReadOnly]
        private ElementObject _targetObject = null;

        // 選択しているオブジェクト
        [SerializeField, ReadOnly]
        private ElementObject _selectObject = null;

        // TODO:選択したオブジェクトの要素テキスト
        [SerializeField]
        private Text _elementText;
        [SerializeField, ReadOnly]
        private Text[] _textList = null;

        // 選択している要素のインデックス
        private int _selectElement = -1;

        void Start()
        {
            // TODO: テキストリスト作成
            int num = (int)ElementType.length;
            _textList = new Text[num];
        }

        void Update()
        {
            // 選択
            // 選択したオブジェクトを取得
            var selectObj = GetClickObject();
            TargetObject(selectObj);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                var lockOn = new LockOn.LockOn();
                var list = lockOn.GetLockOnList();
                if (0 < list.Count)
                {
                    // 適当に選択
                    var ran = Random.Range(0, list.Count);
                    TargetObject(list[ran]);
                }
            }

            // 要素吸出し
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (_targetObject)
                {
                    SelectObject();
                }
            }

            // 選択要素切り替え
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (_selectObject)
                {
                    // 次の要素を取得
                    ChangeNextElement();
                }
            }

            // 要素を移す
            if (Input.GetKeyDown(KeyCode.C))
            {
                MoveElement(_targetObject);
            }
        }

        /// <summary>
        /// オブジェクトをターゲット
        /// </summary>
        private void TargetObject(GameObject obj)
        {
            if (obj)
            {
                var elementObj = obj.GetComponent<ElementObject>();
                if (elementObj)
                {
                    // 要素をターゲット
                    TargetElementObject(elementObj);
                }
            }
        }

        /// <summary>
        /// 要素オブジェクトをターゲットしたときの処理
        /// </summary>
        /// <param name="elementObj"></param>
        private void TargetElementObject(ElementObject elementObj)
        {
            if (_targetObject)
            {
                // TODO: テキスト削除
                _targetObject.transform.DestroyAllChild();
            }
            // ターゲット
            _targetObject = elementObj;

            // TODO: 仮で選択したオブジェクトにテキストを付与
            // ======================================================
            // 子に要素追加
            var text = GameObject.Instantiate(_elementText);
            _targetObject.transform.SetChild(text.gameObject);
            text.transform.localPosition = Vector3.zero;
            text.gameObject.AddComponent<Canvas>();
            var scaler = text.gameObject.AddComponent<CanvasScaler>();
            scaler.dynamicPixelsPerUnit = 20;
            text.transform.localScale = new Vector3(0.1f, 0.1f, 1.0f);
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
                _targetObject.transform.DestroyAllChild();
            }
            _targetObject = null;
        }

        /// <summary>
        /// オブジェクトを選択
        /// </summary>
        private void SelectObject()
        {
            _selectObject = _targetObject;
            // ターゲット解除
            TargetRelease();
            // TODO:要素UI更新
            ElementUIUpdate();
            // TODO: テキスト追加
            AddText(_selectObject.ElementList);
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
            _selectObject = null;
            _selectElement = -1;
        }

        /// <summary>
        /// 選択要素を次に移動
        /// </summary>
        private void ChangeNextElement()
        {
            // 次の要素を取得
            _selectElement = SearchSelectElement(_selectObject, _selectElement);
            ElementUIUpdate();
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
            var element = _selectObject.ElementList[_selectElement];

            // 要素のコピー移動
            selectObj.CopyComponent(element);

            // 要素の更新
            selectObj.ElementUpdate();

            // ターゲット解除
            TargetRelease();

            // 選択解除
            SelectRelease();
        }

        // TODO: 要素テキスト追加
        private void AddText(ElementBase[] elements)
        {
            // TODO:初期要素を選択状態に
            foreach (var element in elements)
            {
                if (element == null)
                {
                    continue;
                }
                _selectElement = (int)element.Type;
            }

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

            // 選択更新時処理
            ElementUIUpdate();
        }

        // TODO: 選択要素の表示更新
        private void ElementUIUpdate()
        {
            if (_selectElement == -1)
            {
                // 選択されている要素がない場合はなにもしない
                return;
            }

            foreach (var text in _textList)
            {
                if (text == null)
                {
                    continue;
                }
                text.fontSize = 25;
            }

            // TODO: 仮ででかくする
            _textList[_selectElement].fontSize = 30;
        }

        /// <summary>
        /// TODO:左クリックしたオブジェクトを取得 
        /// </summary>
        /// <returns></returns>
        private GameObject GetClickObject()
        {
            GameObject result = null;

            // 左クリックされた場所のオブジェクトを取得
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D collition2d = Physics2D.OverlapPoint(tapPoint);
                if (collition2d)
                {
                    result = collition2d.transform.gameObject;
                }
            }
            return result;
        }
    }
}