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

        /// <summary>
        /// 初期化
        /// </summary>
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
            ElementList = new ElementBase[index];

            var array = this.GetComponents<ElementBase>();

            foreach (var element in array)
            {
                int typeIndex = (int)element.Type;

                if (typeIndex < 0)
                {
                    // タイプがない場合は削除
                    Object.Destroy(element);
                }

                if (_elementList[typeIndex] == null)
                {
                    _elementList[typeIndex] = element;
                    element.Initialize();
                }
                else
                {
                    // タイプがかぶっているので2個目からは破棄
                    Object.Destroy(element);
                }
            }
        }
    }
}