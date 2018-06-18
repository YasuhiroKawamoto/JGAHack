using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play.Element
{
    // 要素の種類
    public enum ElementType
    {
        None = -1,
        Move = 0,
        Action,
        Direction,
        
        length
    }

    // 要素の基底クラス

    [System.Serializable]
    public abstract class ElementBase : Extensions.MonoBehaviourEx
    {
        // 要素のタイプ
        protected ElementType _type = ElementType.None;

        public ElementType Type
        {
            get
            {
                if (_type == ElementType.None)
                {
                    var typeS = this.GetType().ToString();
                    throw new System.Exception(typeS + "の要素タイプを設定してください。");
                }
                return _type;
            }
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// 終了処理
        /// </summary>
        public virtual void Discard()
        {

        }
    }
}