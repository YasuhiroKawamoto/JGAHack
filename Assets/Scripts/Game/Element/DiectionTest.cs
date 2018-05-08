﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play.Element
{
    public class DiectionTest : ElementBase
    {
        [SerializeField]
        private Direction _direction = Direction.Front;

        private void Awake()
        {
            // 初期化でタイプを設定する
            _type = ElementType.Direction;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public override void Initialize()
        {
            transform.eulerAngles = _direction.Vector();
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public override void Discard()
        {
            // 終了時の処理
            transform.eulerAngles = Vector3.zero;
        }
    }
}