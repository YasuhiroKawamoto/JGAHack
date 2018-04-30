using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Extensions;

namespace Play.Element
{
	// 円移動の要素　(仮なので当たり判定など考慮していない)
	public class CircleElement : ElementBase
	{
		[SerializeField]
		private float _speed = 1.0f;

		[SerializeField]
		private float _radius = 1.0f;

		// 前回加えた移動量
		private Vector3 _addSpeed = Vector3.zero;

		/// <summary>
		/// 初期化
		/// </summary>
		public override void Initialize()
		{
		}

		/// <summary>
		/// 更新　円移動
		/// </summary>
		private void Update()
		{
			var position = transform.position;

			// 前回移動した分を減らす
			position -= _addSpeed;

			var addX = _radius * Mathf.Sin(Time.time * _speed);
			var addY = _radius * Mathf.Cos(Time.time * _speed);

			_addSpeed = new Vector3(addX, addY, 0.0f);

			this.PosX = position.x + addX;
			this.PosY = position.y + addY;
		}
	}
}