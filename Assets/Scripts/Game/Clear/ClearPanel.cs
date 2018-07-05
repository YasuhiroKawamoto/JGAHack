using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ClearPanel : MonoBehaviour
{
	[SerializeField]
	private Image _testPhone = null;

	[SerializeField]
	private Text _title = null;

	[SerializeField]
	private Text _time = null;

	[SerializeField]
	private float _transTime = 1.0f;

	/// <summary>
	/// 初期設定
	/// </summary>
	public void Show(float time)
	{
		_time.text = Play.Timer.DisplayTime(time);

		StartCoroutine(ShowCorutine());
	}

	IEnumerator ShowCorutine()
	{
		_testPhone.transform.DOLocalMove(new Vector3(0.0f, 0.0f, 0.0f), _transTime).SetEase(Ease.OutElastic).SetUpdate(true);
		_testPhone.transform.DOScale(new Vector3(3.0f, 3.6f, 1.0f), _transTime).SetEase(Ease.OutElastic).SetUpdate(true);
		var tween = _testPhone.transform.DOLocalRotate(new Vector3(0.0f, 0.0f, 90.0f), _transTime).SetEase(Ease.OutElastic).SetUpdate(true);

		yield return new WaitWhile(() => tween.IsPlaying());

		yield return new WaitWhile(() =>
		{
			var tileColor = _title.color;
			tileColor.a += 0.02f;
			_title.color = tileColor;
			return _title.color.a <= 1.0f;
		});

		yield return new WaitWhile(() =>
		{
			var timeColor = _time.color;
			timeColor.a += 0.01f;
			_time.color = timeColor;
			return _time.color.a <= 1.0f;
		});

		var time = Time.time;
		yield return new WaitWhile(() =>
		{
			// クリア演出の若干スキップ
			var con = GameController.Instance;

			if (Input.GetKeyDown(KeyCode.C)) return false;
			if (con.GetConnectFlag() && con.ButtonDown(Button.A)) return false;

			if (time + 5.0f < Time.time) return false;

			return true;
		});

		Play.InGameManager.Instance.BackMain("Select");

	}

}
