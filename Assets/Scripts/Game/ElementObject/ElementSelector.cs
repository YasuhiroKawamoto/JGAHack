using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Play.Element;
using Extensions;
using Play.LockOn;
using Util.Sound;

using System.Reflection;

namespace Play
{
	// 要素オブジェクトの選択と要素の移動用クラス
	public class ElementSelector : MonoBehaviour
	{
		enum TargetChoice
		{
			None,
			Next,
			Front
		}

		protected bool _modeCopy = true;

		// 選択している要素コンテナ
		[SerializeField, ReadOnly]
		private ElementContainer _container = null;

		// ロックオン
		private LockOn.LockOn _lockOn = null;

		private GameObject _dataPanel;

		public LockOn.LockOn LockOnObj
		{
			get { return _lockOn; }
		}

		public bool IsInput = false;

		void Start()
		{
			// コンテナ取得
			_container = GetComponent<ElementContainer>();

			// ロックオン関連の初期化
			_lockOn = this.gameObject.AddComponent<LockOn.LockOn>();

			if (!_dataPanel)
			{
				_dataPanel = GameObject.Find("DataPanel");
			}

			if (InGameManager.IsInstance())
			{
				var button = InGameManager.Instance._modeButton;
				SetButtonFunc(button);
			}
		}

		protected virtual void SetButtonFunc(UnityEngine.UI.Button button)
		{
			button.onClick.AddListener(() =>
			{
				_modeCopy = !_modeCopy;
			});
		}

		public void KeyInput()
		{
#if UNITY_ANDROID

			var obj = TouchSelectObject();

			if (obj == null) return;
			if (_modeCopy)
			{
				if (TargetObject(obj))
				{
					IsInput = true;
					CopyEffect(obj);

					DataPanelUpDate(obj);
					SelectObject(obj);
				}
			}
			else
			{
				if (MoveElement(TouchSelectObject()))
					IsInput = true;
			}

			if (Input.GetMouseButtonUp(0))
			{
				IsInput = false;
			}
#endif
		}

		/// <summary>
		/// オブジェクトをターゲット
		/// </summary>
		virtual protected bool TargetObject(ElementObject obj)
		{
			if (obj == null)
			{
				return false;
			}

			// 要素をターゲット
			TargetElementObject(obj);

			// Console更新
			//ConsoleUpDate(obj);

			//操作ガイドの変更
#if UNITY_WSA_10_0
            GuidUI.Instance.GetComponent<GuidUI>().ChangeGuid(GuidUI.GUID_STEP.Lockon);
#endif

			// SE
			SoundManager.Instance.PlayOneShot(AudioKey.in_play_lock_on);

			return true;
		}

		private void DataPanelUpDate(ElementObject obj)
		{

			if (obj.GetComponent<ElementObject>().ElementList == null) return;

			if (obj.GetComponent<ElementObject>().ElementList.Length != 0)
			{
				//ムーブ,ディレクション画像
				for (int i = 0; i < obj.GetComponent<ElementObject>().ElementList.Length; i++)
				{
					if (obj.ElementList[i])
					{

						if (obj.ElementList[i].Type == ElementType.Action)
						{

							var element = obj.ElementList[i].GetType().Name;
							DataIconSet(0, element, obj);

						}
						else if (obj.ElementList[i].Type == ElementType.Move)
						{
							var element = obj.ElementList[i].GetType().Name;
							DataIconSet(1, element, obj);
						}
						else if (obj.ElementList[i].Type == ElementType.Direction)
						{
							var element = obj.ElementList[i].GetType().Name;
							DataIconSet(2, element, obj);
						}
					}
				}
			}
		}


		private void DataIconSet(int iconNum, string typeName, ElementObject obj)
		{
			if (typeName == "DiectionTest")
			{
				switch (obj.GetCurrentDirection())
				{
					case Direction.Back:
						_dataPanel.GetComponent<PlayerDataPanel>().SetIcon(iconNum, CONSOLE_ICON_ID.Direction_Down);
						break;

					case Direction.Left:
						_dataPanel.GetComponent<PlayerDataPanel>().SetIcon(iconNum, CONSOLE_ICON_ID.Direction_Left);
						break;

					case Direction.Right:
						_dataPanel.GetComponent<PlayerDataPanel>().SetIcon(iconNum, CONSOLE_ICON_ID.Direction_Right);
						break;

					case Direction.Front:
						_dataPanel.GetComponent<PlayerDataPanel>().SetIcon(iconNum, CONSOLE_ICON_ID.Direction_Up);
						break;

					default:

						break;
				}
			}



			if (typeName == "TestShot")
			{
				_dataPanel.GetComponent<PlayerDataPanel>().SetIcon(iconNum, CONSOLE_ICON_ID.Shot);
			}

			if (typeName == "Tackle")
			{
				_dataPanel.GetComponent<PlayerDataPanel>().SetIcon(iconNum, CONSOLE_ICON_ID.Tackle);
			}

			if (typeName == "RideFloor")
			{
				_dataPanel.GetComponent<PlayerDataPanel>().SetIcon(iconNum, CONSOLE_ICON_ID.RideOn);
			}

			if (typeName == "SideMove")
			{
				_dataPanel.GetComponent<PlayerDataPanel>().SetIcon(iconNum, CONSOLE_ICON_ID.Side);
			}

			if (typeName == "Stay")
			{
				_dataPanel.GetComponent<PlayerDataPanel>().SetIcon(iconNum, CONSOLE_ICON_ID.Stop);
			}

			if (typeName == "UpDownMove")
			{

				_dataPanel.GetComponent<PlayerDataPanel>().SetIcon(iconNum, CONSOLE_ICON_ID.Updown);
			}

			if (typeName == "NoData")
			{
				_dataPanel.GetComponent<PlayerDataPanel>().SetIcon(iconNum, CONSOLE_ICON_ID.Nodata);
			}
		}

		/// <summary>
		/// 要素オブジェクトをターゲットしたときの処理
		/// </summary>
		/// <param name="elementObj"></param>
		private void TargetElementObject(ElementObject elementObj)
		{
			var target = elementObj.gameObject.AddComponent<TargetObject>();
			target.SetSelector(this);

#if UNITY_WSA_10_0
            GuidUI.Instance.GetComponent<GuidUI>().ChangeGuid(GuidUI.GUID_STEP.Lockon);
#endif
		}

		/// <summary>
		/// オブジェクトを選択
		/// </summary>
		virtual protected void SelectObject(ElementObject selectObj)
		{
			SelectRelease();
			_container.ReceiveAllElement(selectObj.ElementList);

			// SE
			SoundManager.Instance.PlayOneShot(AudioKey.in_play_copy);
		}

		/// <summary>
		/// オブジェクトへの選択を解除
		/// </summary>
		private void SelectRelease()
		{
			_container.AllDelete();
		}

		/// <summary>
		/// 要素の移動
		/// </summary>
		/// <param name="selectObj"></param>
		virtual protected bool MoveElement(ElementObject selectObj)
		{
			// リストを記憶していない場合は移動しない
			if (_container.List == null)
			{
				SoundManager.Instance.PlayOneShot(AudioKey.in_play_lock_off);
				return false;
			}
			if (selectObj == null) return false;

			// すべての要素を移動
			selectObj.ReceiveAllElement(_container.List.ToArray());
			//Console更新
			//ConsoleUpDate(selectObj);

			// SE
			SoundManager.Instance.PlayOneShot(AudioKey.in_play_paste);

			//ペースト時エフェクト
			PasteEffect(selectObj);
			//復帰演出セット
			RecoverSet(selectObj);

			return true;
		}

		//コピー時エフェクト
		void CopyEffect(ElementObject selectObj)
		{
			//コピー時エフェクト
			GameObject effect = EffectManager.Instance.CreateEffect(EffectID.Wave, selectObj.transform.position);
			effect.GetComponent<WaveContoller>().setVelocity(gameObject.transform.parent.transform);
		}

		//ペースト時エフェクト
		void PasteEffect(ElementObject selectObj)
		{
			//送信エフェクト
			GameObject effect = EffectManager.Instance.CreateEffect(EffectID.Wave, gameObject.transform.parent.position);
			effect.GetComponent<WaveContoller>().setVelocity(selectObj.transform);
		}

		//復帰演出セット
		void RecoverSet(ElementObject selectObj)
		{
			//復帰演出セット＆開始
			GameObject recover = EffectManager.Instance.CreateEffect(EffectID.EnemyRecovery, selectObj.transform.position);
			recover.GetComponent<UISet>().SetTransform(selectObj.transform);
			recover.GetComponent<EnemyRecovery>().SetTime(selectObj.GetComponent<ElementObject>().GetReturnTime());
			selectObj.GetComponent<ElementObject>().EffectUpDate(recover);
		}

		// 左クリックしたオブジェクトを取得する関数(3D)
		public ElementObject TouchSelectObject()
		{
			ElementObject result = null;
			// 左クリックされた場所のオブジェクトを取得
			if (Input.GetMouseButtonDown(0))
			{
				Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				int layerMask = LayerMask.GetMask(new string[] { "Gimic", "Object", "Enemy" });
				Collider2D collition2d = Physics2D.OverlapPoint(tapPoint, layerMask);
				Debug.Log(collition2d);
				if (collition2d)
				{
					var obj = collition2d.transform.gameObject;
					result = obj.GetComponent<ElementObject>();

					if (result == null)
						result = obj.GetComponentInChildren<ElementObject>();
				}
			}
			return result;
		}
	}
}