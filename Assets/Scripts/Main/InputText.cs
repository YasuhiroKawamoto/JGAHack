using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Main
{
	public class InputText : MonoBehaviour
	{
		[SerializeField, Extensions.ReadOnly]
		private bool _isCon = false;

		[SerializeField]
		private Sprite _A = null;

		[SerializeField]
		private Sprite _B = null;

		[SerializeField]
		private Sprite _C = null;

		[SerializeField]
		private Sprite _V = null;

		[SerializeField]
		private Image _enter = null;

		[SerializeField]
		private Image _return = null;

		void Start()
		{
			_isCon = GameController.Instance.GetConnectFlag();
			ChangeUI(_isCon);
		}

		void Update()
		{
			var flag = GameController.Instance.GetConnectFlag();
			if (_isCon != flag)
			{
				_isCon = flag;
				ChangeUI(_isCon);
			}
		}

		private void ChangeUI(bool con)
		{
			_enter.sprite = con ? _A : _C;
			_return.sprite = con ? _B : _V;
		}
	}
}