using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

using Util.Display;

namespace Main
{
	public sealed class SelectDisplay : DisplayBase
	{
		// タイトルディスプレイ
		[SerializeField]
		private TitleDisplay _titleDisplay = null;

		// 携帯UI
		[SerializeField]
		private Image _phoneImage = null;

		[SerializeField]
		private float _transTime = 1.0f;

		[SerializeField]
		private Main.PhoneScreen _phoneScreen = null;

		private Text _stageName = null;

		[SerializeField]
		private StageTimePanel _timePanel = null;

		[SerializeField]
		private BackStage _backStage = null;

		public override IEnumerator Enter()
		{
			_phoneImage.transform.DOLocalMove(new Vector3(100.0f, -3.0f, 0.0f), _transTime).SetEase(Ease.OutElastic);
			_phoneImage.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), _transTime).SetEase(Ease.OutElastic);
			_phoneImage.transform.DOLocalRotate(new Vector3(0.0f, 0.0f, 0.0f), _transTime).SetEase(Ease.OutElastic);

			var button = _phoneImage.transform.Find("Button");
			button.transform.DOScale(Vector3.one, _transTime);

			yield return new WaitForSeconds(_transTime);

			// 携帯画面にステージパネルを出す
			_phoneScreen.SetUp();

			// ステージ名テキストを取得
			_stageName = this.transform.transform.Find("StageName").GetComponentInChildren<Text>();

			ChangeStageName(_phoneScreen.SelectIndex);

			_timePanel.UpdateView(_phoneScreen.SelectIndex);
		}

		public override void EnterComplete()
		{
			base.EnterComplete();
		}

		public override void Exit()
		{
			base.Exit();
		}

		public override void KeyInput()
		{
			if (Util.Scene.SceneManager.Instance.IsLoading) return;

			var controller = GameController.Instance;

			if (controller.GetConnectFlag())
			{
				// コントローラー
				if (controller.ButtonDown(Button.B))
				{
					DisplayManager.Instance.ChangeDisplay(_titleDisplay);
				}

				if (controller.Move(Direction.Front))
				{
					var index = _phoneScreen.PanelBefore();
					ChangeStageName(index);
					_timePanel.UpdateView(_phoneScreen.SelectIndex);
				}

				if (controller.Move(Direction.Back))
				{
					var index = _phoneScreen.PanelNext();
					ChangeStageName(index);
					_timePanel.UpdateView(_phoneScreen.SelectIndex);
				}

				if (controller.ButtonDown(Button.A))
				{
					Play.InGameManager.Destroy();
					var index = _phoneScreen.SelectIndex;
					TakeOverData.Instance.StageNum = index + 1;
					// 呼び出しはこれ
					Util.Scene.SceneManager.Instance.ChangeSceneFadeInOut("Game");
					// SE
					Util.Sound.SoundManager.Instance.PlayOneShot(AudioKey.sy_enter);
				}
			}
			else
			{
				// キーボード
				if (Input.GetKeyDown(KeyCode.V))
				{
					DisplayManager.Instance.ChangeDisplay(_titleDisplay);
				}

				if (Input.GetKey(KeyCode.UpArrow))
				{
					var index = _phoneScreen.PanelBefore();
					ChangeStageName(index);
					_timePanel.UpdateView(_phoneScreen.SelectIndex);
				}

				if (Input.GetKey(KeyCode.DownArrow))
				{
					var index = _phoneScreen.PanelNext();
					ChangeStageName(index);
					_timePanel.UpdateView(_phoneScreen.SelectIndex);
				}

				if (Input.GetKeyDown(KeyCode.C))
				{
					Play.InGameManager.Destroy();
					var index = _phoneScreen.SelectIndex;
					TakeOverData.Instance.StageNum = index + 1;

					// 呼び出しはこれ
					Util.Scene.SceneManager.Instance.ChangeSceneFadeInOut("Game");
					// SE
					Util.Sound.SoundManager.Instance.PlayOneShot(AudioKey.sy_enter);
				}
			}
		}

		/// <summary>
		/// ステージ名変更
		/// </summary>
		/// <param name="index"></param>
		private void ChangeStageName(int index)
		{
			if (0 <= index)
			{
				var stage = index + 1;
				_stageName.text = "STAGE " + stage;

				_backStage.ChangeStage(stage);
			}
		}
	}
}