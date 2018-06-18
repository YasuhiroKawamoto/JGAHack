using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenBase : MonoBehaviour
{
	[SerializeField]
	protected Image _panel = null;

	protected RectTransform _rectTransform = null;

	[SerializeField]
	protected List<Image> _panelList = new List<Image>();

	protected Vector2 _panelRect = Vector2.zero;

	[SerializeField]
	protected Vector3 _initPos = Vector3.zero;

	[SerializeField]
	protected Vector2 _popOffSet = Vector2.zero;

	protected int _selectIndex = 0;
	public int SelectIndex
	{
		get { return _selectIndex; }
	}

	/// <summary>
	/// 設定
	/// </summary>
	public void SetUp()
	{
		_panelRect = _panel.rectTransform.sizeDelta;

		_rectTransform = GetComponent<RectTransform>();

	}

	/// <summary>
	/// パネルの作成
	/// </summary>
	/// <param name="pos"></param>
	/// <returns></returns>
	protected Image CreatePanel(Vector3 pos, int index)
	{
		var obj = Instantiate(_panel, pos, Quaternion.identity);
		obj.rectTransform.SetParent(_rectTransform);
		_panelList.Add(obj);

		obj.rectTransform.localScale = Vector3.one;
		obj.rectTransform.localPosition = pos;

		return obj;
	}

	protected bool IsCreated()
	{
		if (_panelList.Count == 0)
		{
			return true;
		}
		return false;
	}
}
