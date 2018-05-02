﻿using System.Collections;
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
            get { return _type; }
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// 終了処理
        /// </summary>
        public abstract void Discard();
    }
}