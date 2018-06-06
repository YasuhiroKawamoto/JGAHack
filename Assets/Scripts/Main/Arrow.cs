using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Arrow : MonoBehaviour
{

	private RectTransform _rectTransform;

	private Tween _tween = null;

	void Awake()
	{
		_rectTransform = GetComponent<RectTransform>();

		SetPos(_rectTransform.localPosition);
	}

	public void SetPos(Vector3 pos)
	{
		_rectTransform.localPosition = pos;

		pos.x -= 10.0f;
		if (_tween != null)
		{
			_tween.Kill();
			_tween = null;
		}

		_tween = _rectTransform.DOLocalMove(pos, 0.2f).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
	}
}
