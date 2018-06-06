using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Extensions;
using Play.Element;

namespace Play.LockOn
{
    public class TargetObject : MonoBehaviourEx
    {
        // 参照しているセレクター
        private ElementSelector _selector = null;
        public ElementSelector Selector
        {
            get { return _selector; }
        }

        // 要素オブジェクト
        private Element.ElementObject _elementObj = null;

        public ElementObject ElementObj
        {
            get
            { return _elementObj; }
        }

        private LockOnSetting _setting = null;
        public LockOnSetting Setting
        {
            get { return _setting ? _setting : _setting = Resources.Load("Settings\\LockOnSetting") as LockOnSetting; }
        }

        /// <summary>
        /// 初期化
        /// </summary>
        void InitSetting()
        {
            _elementObj = GetComponent<Element.ElementObject>();

            // TODO: 仮で選択したオブジェクトにテキストを付与
            // ======================================================
            // 子に要素追加
            var setting = Setting;
            var text = GameObject.Instantiate(setting._elementText);
            transform.SetChild(text.gameObject);
            // ターゲットマーカー作成
            var obj = Instantiate(setting._target);
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
            foreach (var element in ElementObj.ElementList)
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
        /// セレクターの設定
        /// </summary>
        /// <param name="selector"></param>
        public void SetSelector(ElementSelector selector)
        {
            _selector = selector;

            InitSetting();
        }

        void Update()
        {
            var lockOn = Selector.LockOnObj;
            if (!lockOn.CheckOnScreen(transform.position))
            {
                // 画面外に出たらターゲット解除
                Release();
            }
        }

        public void Release()
        {
            // 選択解除
            var childs = transform.GetAllChild();
            foreach (var c in childs)
            {
                if (c.name == "ElementText(Clone)")
                {
                    Destroy(c.gameObject);
                }
            }
            Destroy(this);

            _selector.TargetRelease();
        }
    }
}