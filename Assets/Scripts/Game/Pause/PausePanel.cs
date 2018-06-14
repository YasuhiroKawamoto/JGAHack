using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PausePanel : MonoBehaviour
{

	[SerializeField]
	private Image _testPhone = null;

	[SerializeField]
	private Play.PauseScreen _phoneScreen = null;

    [SerializeField]
    GameObject _dataPanel;

	// trans
	private Vector3 _initPos = Vector3.zero;
	private Vector3 _initRotate = Vector3.zero;
	private Vector3 _initScale = Vector3.zero;

	[SerializeField]
	private float _transTime = 1.0f;

	private bool _move = false;

	public bool Move
	{
		get { return _move; }
	}

	public void Show()
	{
		if (_move) return;
		_move = true;

		_initPos = _testPhone.transform.localPosition;
		_initRotate = _testPhone.transform.localEulerAngles;
		_initScale = _testPhone.transform.localScale;

		StartCoroutine(ShowCorutine());

	}
	IEnumerator ShowCorutine()
	{
		Time.timeScale = 0.0f;
        _dataPanel.gameObject.SetActive(false);

        _testPhone.transform.DOLocalMove(new Vector3(0.0f, 0.0f, 0.0f), _transTime).SetEase(Ease.OutElastic).SetUpdate(true);
		var tween = _testPhone.transform.DOLocalRotate(new Vector3(0.0f, 0.0f, 0.0f), _transTime).SetEase(Ease.OutElastic).SetUpdate(true);

		yield return new WaitWhile(() => tween.IsPlaying());

        // 携帯画面にステージパネルを出す

        
        _phoneScreen.gameObject.SetActive(true);
		_phoneScreen.SetUp();

		_move = false;
	}

	public void Hide()
	{
		if (Move) return;
		_move = true;

		StartCoroutine(HideCorutine());
	}
	IEnumerator HideCorutine()
	{
		_phoneScreen.gameObject.SetActive(false);

		_testPhone.transform.DOLocalMove(_initPos, _transTime).SetEase(Ease.OutElastic).SetUpdate(true);
		var tween = _testPhone.transform.DOLocalRotate(_initRotate, _transTime).SetEase(Ease.OutElastic).SetUpdate(true);

		yield return new WaitWhile(() => tween.IsPlaying());

		Time.timeScale = 1.0f;

        _dataPanel.gameObject.SetActive(true);

        gameObject.SetActive(false);
		_move = false;
	}
}
