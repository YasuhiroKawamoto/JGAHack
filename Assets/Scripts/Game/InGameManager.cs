﻿using System.Collections;
using System.Collections.Generic;
using Play.Stage;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Play
{
	// インゲームの管理クラス
	public class InGameManager : Util.SingletonMonoBehaviour<InGameManager>
	{
		// ゲームの状態
		public enum State
		{
			Stand = 0,
			Play,
			Pause,
			Clear,
			Over
		}

		// 状態
		[SerializeField, Extensions.ReadOnly]
		private State _state = State.Stand;
		public State GameState
		{
			get { return _state; }
		}

		// UIRoot
		[SerializeField]
		private GameObject _uiRoot = null;
		public GameObject UIRoot
		{
			get { return _uiRoot; }
		}

		// ElementRoot
		[SerializeField]
		private GameObject _elementObjRoot = null;
		public GameObject ElementObjRoot
		{
			get { return _elementObjRoot; }
		}

		// ステージ管理
		[SerializeField]
		private Stage.StageManager _stageManager = null;
		public StageManager StageManager
		{
			get { return _stageManager; }
		}

		// カメラ管理
		[SerializeField]
		private CameraManager _cameraManager = null;

		public CameraManager CameraManager
		{
			get { return _cameraManager; }
		}

		// Pause Panel
		[SerializeField]
		private PausePanel _pausePlane = null;

		[SerializeField]
		private ClearPanel _clearPlane = null;

		// 復活管理システム
		[SerializeField]
		private RebornManager _rebornManager = null;

		// message
		[SerializeField]
		private Message _messenger = null;
		public Message Messenger
		{
			get { return _messenger; }
		}

		// TimeCounter
		[SerializeField]
		private TimeCounter _counter = null;

		[SerializeField]
		private GameObject _dataPhone = null;

		public UnityEngine.UI.Button _modeButton = null;

		public UnityEngine.UI.Button _pauseButton = null;

		void Start()
		{
			// ゲームの設定
			StartCoroutine(StartSetUp());
		}

		private IEnumerator StartSetUp()
		{
			// BGM再生
			Util.Sound.SoundManager.Instance.Play(AudioKey.PlayBGM);

			// 復活マネージャーの取得
			_rebornManager = this.GetComponent<RebornManager>();

			// ステージプレハブの設定
			yield return LoadStage();

			// カメラに必要な要素を設定
			CameraManager.Player = StageManager.Player.gameObject;
			CameraManager.Goal = StageManager.Goal.gameObject;

			//カメラ初期化
			StartCoroutine(CameraManager.InitCamera());

			// カメラ遷移終了待ち
			yield return new WaitUntil(() => _cameraManager.GetEndProduction());

#if UNITY_WSA_10_0
            GuidUI.Instance.GetComponent<GuidUI>().ChangeGuid(GuidUI.GUID_STEP.Normal);
#endif
			_state = State.Play;

			// タイムカウント開始
			_counter.StartTimer();
		}

		/// <summary>
		/// ステージの読み込み
		/// </summary>
		/// <returns></returns>
		private IEnumerator LoadStage()
		{
			// アセットのロード
			var stageNum = Main.TakeOverData.Instance.StageNum;
			var stageAsset = Resources.LoadAsync("Stage/Stage_" + stageNum);

			// ロード待ち
			yield return new WaitWhile(() => !stageAsset.isDone);

			var stageObj = stageAsset.asset as GameObject;
			var stage = Instantiate(stageObj);

			var manager = stage.GetComponent<StageManager>();

			// ステージマネージャーの設定
			_stageManager = manager;
		}

		/// <summary>
		/// ステージのクリア
		/// </summary>
		public void StageClear()
		{
			_stageManager.Player.PuniHide();

			_state = State.Clear;

			// プレイヤーのゴール処理
			StageManager.Player.Goal();

			// SE
			Util.Sound.SoundManager.Instance.PlayOneShot(AudioKey.in_clear);

			// タイムカウント終了
			var time = _counter.EndTimer();

			StageTimeData.Instance.SetTime(Main.TakeOverData.Instance.StageNum, time);
			StageTimeData.Instance.Save();

			//ガイドUIの非表示
#if UNITY_WSA_10_0
            GuidUI.Instance.HideAll();
#endif

			_dataPhone.SetActive(false);
			_clearPlane.gameObject.SetActive(true);
			_clearPlane.Show(time);
			_modeButton.gameObject.SetActive(false);
			_pauseButton.gameObject.SetActive(false);
		}

		/// <summary>
		/// ステージの失敗
		/// </summary>
		public void StageOver()
		{
			if (_state == State.Play)
			{
				StartCoroutine(StageManager.ReTry(_cameraManager));
			}
		}

		public void GameReLoad()
		{
			Scene loadScene = SceneManager.GetActiveScene();
			// Sceneの読み直し
			SceneManager.LoadScene(loadScene.name);
		}

		/// <summary>
		/// ゲームの一時停止
		/// </summary>
		public void GamePause(bool active)
		{
			if (_pausePlane.Move) return;

			if (_pausePlane.IsPopUp()) return;


			if (active)
			{
				if (Time.timeScale == 0.0f) return;

				_pausePlane.gameObject.SetActive(true);
				_pausePlane.Show();
				_state = State.Pause;
			}
			else
			{
				_pausePlane.Hide();
				_state = State.Play;
			}
		}

		/// <summary>
		/// メイン画面に戻る
		/// </summary>
		public void BackMain(string displayName)
		{
			Main.TakeOverData.Instance.DisplayName = displayName;
			Util.Scene.SceneManager.Instance.ChangeSceneFadeInOut("Main");
		}

		public Vector3 GetStartPos()
		{
			return StageManager.GetStartPos();
		}

		/// <summary>
		/// 復活のセット
		/// </summary>
		/// <param name="bObj"></param>
		public void RebornSet(Element.BreakElement bObj)
		{
			if (_rebornManager)
			{
				_rebornManager.RebornSet(bObj);
			}
		}

		/// <summary>
		/// ゲームポーズの切り替え
		/// </summary>
		public void ChangePause()
		{
			if (_state == InGameManager.State.Pause)
			{
				GamePause(false);
			}
			else if (_state == InGameManager.State.Play)
			{
				GamePause(true);
			}
		}

		/// <summary>
		/// タイマーの停止
		/// </summary>
		public void TimerStop()
		{
			var time = _counter.EndTimer();
		}
	}
}