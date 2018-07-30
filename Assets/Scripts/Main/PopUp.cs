using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Extensions;


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
	private UnityEngine.UI.Button _yes = null;

	[SerializeField]
	private UnityEngine.UI.Button _no = null;

	private UnityEngine.UI.Button[] _select = new UnityEngine.UI.Button[(int)Select.length];

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

		Time.timeScale = 0.0f;

		// 拡大
		var scaler = this.gameObject.GetComponent<CanvasScaler>();

		_yes.onClick.AddListener(() =>
		{
			_selectNum = 0;
			_push = true;
		});

		_no.onClick.AddListener(() =>
		{
			_push = true;
			_selectNum = 1;
		});

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

		bool deth = false;
		this.gameObject.AddOnDestroyCallback(() => deth = true);

		Destroy(this.gameObject);

		yield return new WaitUntil(() => deth);
	}

	private float GetleftSidePos(Text textObj)
	{
		var size = textObj.rectTransform.sizeDelta;
		var pos = textObj.transform.localPosition;

		return pos.x - size.x;
	}
}
