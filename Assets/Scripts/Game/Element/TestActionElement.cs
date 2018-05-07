using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Extensions;

namespace Play.Element
{
    // 円移動の要素　(仮なので当たり判定など考慮していない)
    public class TestActionElement : ElementBase
    {
        private void Awake()
        {
            // 初期化でタイプを設定する
            _type = ElementType.Action;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public override void Initialize()
        {
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public override void Discard()
        {
            // 終了時の処理
        }

    }
}