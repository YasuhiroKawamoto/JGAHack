using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace Play.Tutrial
{
	public class Forcus : MonoBehaviour
	{
		[SerializeField]
		private SpriteRenderer _back = null;

		[SerializeField]
		private SpriteMask _forcus = null;

		[SerializeField]
		private Color _changeColor;

		private GameObject _target = null;
		private Tweener _moveTween = null;

		private float _time = 0.5f;

		void Start()
		{
			Release();
		}

		public IEnumerator SetForcus(GameObject obj)
		{
			_target = obj;

			_back.DOColor(_changeColor, _time);
			_moveTween = _forcus.transform.DOLocalMove(obj.transform.position, _time);

			yield return new WaitForSeconds(_time);
		}

		void Update()
		{
			if (!_target) return;

			if (_moveTween == null) return;

			_moveTween.ChangeEndValue(_target.transform.position, _time, true);
		}

		public void Release()
		{
			var color = _back.color;
			color.a = 0.0f;
			_back.color = color;
		}
	}
}