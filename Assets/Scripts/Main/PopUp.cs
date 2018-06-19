using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopUp : MonoBehaviour
{

	enum Select
	{
		Yes = 0,
		No = 1,
		length = 2
	}

	[SerializeField]
	private Text _text = null;

	[SerializeField]
	private Arrow _arrow = null;

	[SerializeField]
	private Text _yes = null;

	[SerializeField]
	private Text _no = null;

	private Text[] _select = new Text[(int)Select.length];

	[SerializeField, Extensions.ReadOnly]
	private int _selectNum = 0;

	private bool _push = false;

	System.Func<bool> _inputBack = null;

	public IEnumerator ShowPopUp(string text, System.Action<bool> action, System.Func<bool> input = null)
	{
		// SE 
		Util.Sound.SoundManager.Instance.PlayOneShot(AudioKey.sy_pop_show);

		_text.text = text;

		_select[0] = _yes;
		_select[1] = _no;

		_inputBack = input;

		// 矢印の設定
		SetArrow();

		Time.timeScale = 0.0f;

		// 拡大
		var scaler = this.gameObject.GetComponent<CanvasScaler>();

		var scale = scaler.scaleFactor;
		yield return new WaitWhile(() =>
		{
			scale += 0.1f;
			scaler.scaleFactor = scale;
			if (scale < 1.0f)
			{
				return true;
			}
			return false;
		});

		yield return new WaitWhile(() => !_push);

		//Time.timeScale = 1.0f;

		bool flag = false;

		if (_selectNum == 0)
		{
			Util.Sound.SoundManager.Instance.PlayOneShot(AudioKey.sy_pop_enter);
			flag = true;
		}
		else
		{
			Util.Sound.SoundManager.Instance.PlayOneShot(AudioKey.sy_pop_cancel);
		}

		action(flag);

		scale = scaler.scaleFactor;
		yield return new WaitWhile(() =>
		{
			scale -= 0.1f;
			scaler.scaleFactor = scale;
			if (0.1f <= scale)
			{
				return true;
			}
			return false;
		});

		Destroy(this.gameObject);
	}

	void Update()
	{
		KeyInput();
	}

	void KeyInput()
	{
		var controller = GameController.Instance;

		if (controller.GetConnectFlag())
		{
			if (controller.MoveDown(Direction.Right) ||
				(controller.MoveDown(Direction.Left)))
			{
				_selectNum++;
				if ((int)Select.length <= _selectNum) _selectNum = 0;

				SetArrow();
			}

			if (controller.ButtonDown(Button.A))
			{
				_push = true;
			}
			if (controller.ButtonDown(Button.B))
			{
				_selectNum = 1;
				_push = true;
			}
		}
		else
		{
			if ((Input.GetKeyDown(KeyCode.RightArrow)) ||
			(Input.GetKeyDown(KeyCode.LeftArrow)))
			{
				_selectNum++;
				if ((int)Select.length <= _selectNum) _selectNum = 0;

				SetArrow();
			}

			if (Input.GetKeyDown(KeyCode.C))
			{
				_push = true;
			}

			if (Input.GetKeyDown(KeyCode.V))
			{
				_selectNum = 1;
				_push = true;
			}
		}

		if (_inputBack == null) return;

		if (_inputBack())
		{
			_selectNum = 1;
			_push = true;
		}
	}

	private void SetArrow()
	{
		var pos = _arrow.transform.localPosition;
		var arrowPos = pos;
		arrowPos.x = GetleftSidePos(_select[_selectNum]);

		_arrow.SetPos(arrowPos);
	}

	private float GetleftSidePos(Text textObj)
	{
		var size = textObj.rectTransform.sizeDelta;
		var pos = textObj.transform.localPosition;

		return pos.x - size.x;
	}
}
