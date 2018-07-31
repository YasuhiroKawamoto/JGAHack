using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Extensions;
using Play.Element;
using Util.Sound;

namespace Play.LockOn
{
	public class TargetObject : MonoBehaviourEx
	{
		// 参照しているセレクター
		private ElementSelector _selector = null;
		public ElementSelector Selector
		{
			get { return _selector; }
		}

		// 要素オブジェクト
		private Element.ElementObject _elementObj = null;

		public ElementObject ElementObj
		{
			get
			{ return _elementObj; }
		}

		private LockOnSetting _setting = null;
		public LockOnSetting Setting
		{
			get { return _setting ? _setting : _setting = Resources.Load("Settings\\LockOnSetting") as LockOnSetting; }
		}

		/// <summary>
		/// 初期化
		/// </summary>
		void InitSetting()
		{
			_elementObj = GetComponent<Element.ElementObject>();

			// TODO: 仮で選択したオブジェクトにテキストを付与
			// ======================================================
			// 子に要素追加
			var setting = Setting;
			var canvas = new GameObject("Canvas");
			transform.SetChild(canvas.gameObject);
			// ターゲットマーカー作成
			var obj = Instantiate(setting._target);
			canvas.transform.SetChild(obj);
			canvas.transform.localPosition = Vector3.zero;
			canvas.gameObject.AddComponent<Canvas>();
			canvas.GetComponent<Canvas>().sortingOrder = 2;
			var scaler = canvas.gameObject.AddComponent<CanvasScaler>();
			scaler.dynamicPixelsPerUnit = 20;
			canvas.transform.localPosition = new Vector3(0.0f, 0.5f, 0.0f);
			canvas.transform.localScale = new Vector3(0.3f, 0.3f, 1.0f);
			// ======================================================
		}

		/// <summary>
		/// セレクターの設定
		/// </summary>
		/// <param name="selector"></param>
		public void SetSelector(ElementSelector selector)
		{
			_selector = selector;

			InitSetting();
		}

		void Update()
		{
			var lockOn = Selector.LockOnObj;
			if (!lockOn.CheckOnScreen(transform.position))
			{
				if (InGameManager.IsInstance())
				{
					if (InGameManager.Instance.GameState == InGameManager.State.Play)
					{
						SoundManager.Instance.PlayOneShot(AudioKey.in_play_lock_off);
					}
				}

			}
		}
	}
}