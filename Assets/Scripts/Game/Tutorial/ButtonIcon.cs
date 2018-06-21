using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Play.Tutrial
{
	public class ButtonIcon : MonoBehaviour
	{

		private SpriteRenderer _renderer = null;

		public Sprite _targetKey = null;
		public Sprite _copyKey = null;
		public Sprite _pasteKey = null;

		public Sprite _targetButton = null;
		public Sprite _copyButton = null;
		public Sprite _pasteButton = null;

		private Vector3 _initPos = Vector3.zero;

		private Tween _moveTween = null;

		void Awake()
		{
			_renderer = GetComponent<SpriteRenderer>();
			_initPos = transform.localPosition;
		}

		public void Show(TutrialManager.StepData data)
		{
			Hide();
			var con = GameController.Instance;
			if (con.GetConnectFlag())
			{
				// コントローラー時
				if (data.isTarget) _renderer.sprite = _targetButton;
				else if (data.isCopy) _renderer.sprite = _copyButton;
				else if (data.isPaste) _renderer.sprite = _pasteButton;
			}
			else
			{
				// キーボード時
				if (data.isTarget) _renderer.sprite = _targetKey;
				else if (data.isCopy) _renderer.sprite = _copyKey;
				else if (data.isPaste) _renderer.sprite = _pasteKey;
			}

			if (_renderer.sprite == null) return;

			// 初期位置
			transform.localPosition = _initPos;
			if (_moveTween != null)
			{
				_moveTween.Kill();
				_moveTween = null;
			}

			_moveTween = transform.DOLocalMoveY(0.001f, 1.0f).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
		}

		public void Hide()
		{
			_renderer.sprite = null;
		}
	}
}