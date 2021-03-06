﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play.Element
{
	public class DiectionTest : ElementBase
	{
		[SerializeField]
		private Direction _direction = Direction.Front;

		private Direction _tmpDirection;


		private EnemyAnimController anim;

        private ChangeSprite spr;

        public override void TypeSet()
		{
			// 初期化でタイプを設定する
			_type = ElementType.Direction;
		}

		/// <summary>
		/// 初期化
		/// </summary>
		public override void Initialize()
		{
			anim = GetComponentInParent<EnemyAnimController>();

            spr = GetComponentInParent<ChangeSprite>();
            if (anim)
            {
                anim.ChangeAnim(_direction);
            }
            else if(spr)
            {
                spr.ChangeImage(_direction);
            }
            _tmpDirection = _direction;
		}


		private void Update()
		{
			if (_direction != _tmpDirection)
			{
                if (anim)
                {
                    anim.ChangeAnim(_direction);
                }
                else if(spr)
                {
                    spr.ChangeImage(_direction);
                }
				_tmpDirection = _direction;
				if (GetComponent<Tackle>())
				{
					GetComponent<Tackle>().ChangeDirection(_direction);
				}

			}
		}

		/// <summary>
		/// 終了処理
		/// </summary>
		public override void Discard()
		{
			// 終了時の処理
			transform.eulerAngles = Vector3.zero;
		}


		public Direction GetDir()
		{
			return _direction;
		}

		public Direction GetTmpDir()
		{
			return _tmpDirection;
		}
	}
}