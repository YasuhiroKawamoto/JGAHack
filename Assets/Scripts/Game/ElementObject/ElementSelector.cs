using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Play.Element;
using Extensions;
using Play.LockOn;

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

		// ターゲットしているオブジェクト
		[SerializeField, ReadOnly]
		protected ElementObject _targetObject = null;

		// 選択している要素コンテナ
		[SerializeField, ReadOnly]
		private ElementContainer _container = null;

		// TODO:選択したオブジェクトの要素テキスト
		[SerializeField]
		public Text _elementText;
		[SerializeField, ReadOnly]
		private Text[] _textList = null;

		// ロックオン
		private LockOn.LockOn _lockOn = null;

		public LockOn.LockOn LockOnObj
		{
			get { return _lockOn; }
		}

		void Start()
		{
			// コンテナ取得
			_container = GetComponent<ElementContainer>();

			// TODO: テキストリスト作成
			int num = (int)ElementType.length;
			_textList = new Text[num];

			// ロックオン関連の初期化
			_lockOn = this.gameObject.AddComponent<LockOn.LockOn>();
		}

		void Update()
		{
			// 選択
			var con = GameController.Instance;

			var isTarget = TargetChoice.None;
			bool isSelect = false;
			bool isChange = false;

			if (con.GetConnectFlag())
			{
				isTarget = con.ButtonDown(Button.R1) ? TargetChoice.Front :
							con.ButtonDown(Button.L1) ? TargetChoice.Next : TargetChoice.None;

				isSelect = con.ButtonDown(Button.A);
				isChange = con.ButtonDown(Button.X);
			}
			else
			{
				isTarget = Input.GetKeyDown(KeyCode.Space) ? TargetChoice.Next : TargetChoice.None;
				isSelect = Input.GetKeyDown(KeyCode.Z);
				isChange = Input.GetKeyDown(KeyCode.C);
			}

			if (isTarget != TargetChoice.None)
			{
				// カメラにエレメントオブジェクトが移っているとき探す
				if (LockOnObj.CheckOnScreenAll())
				{
					// 次のターゲットオブジェクトを取得
					if (isTarget != TargetChoice.Next)
					{
						// 選択
						TargetObject(LockOnObj.GetTarget(1));
					}
					else if (isTarget != TargetChoice.Front)
					{
						TargetObject(LockOnObj.GetTarget(-1));
					}
				}
			}

			// 要素吸出し
			if (isSelect)
			{
				if (_targetObject)
				{
					SelectObject();
				}
			}

			// 要素を移す
			if (isChange)
			{
				if (_targetObject)
				{
					MoveElement(_targetObject);
				}
			}
		}

		/// <summary>
		/// オブジェクトをターゲット
		/// </summary>
		virtual protected void TargetObject(ElementObject obj)
		{
			if (obj == null)
			{
				return;
			}

			// 要素をターゲット
			TargetElementObject(obj);
		}

		/// <summary>
		/// 要素オブジェクトをターゲットしたときの処理
		/// </summary>
		/// <param name="elementObj"></param>
		private void TargetElementObject(ElementObject elementObj)
		{
			// ターゲット解除
			TargetUIRelease();

			// ターゲット
			_targetObject = elementObj;

			var target = _targetObject.gameObject.AddComponent<TargetObject>();
			target.SetSelector(this);

		}

		/// <summary>
		/// オブジェクトへのターゲットを解除
		/// </summary>
		public void TargetRelease()
		{
			_targetObject = null;
		}
		public void TargetUIRelease()
		{
			if (_targetObject == null)
			{
				return;
			}

			// タゲUIなど解除
			var target = _targetObject.GetComponent<TargetObject>();

			if (target)
			{
				target.Release();
			}
		}

		/// <summary>
		/// オブジェクトを選択
		/// </summary>
		virtual protected void SelectObject()
		{
			SelectRelease();
			_container.ReceiveAllElement(_targetObject.ElementList);
			// ターゲット解除
			TargetUIRelease();
			// TODO: テキスト追加
			AddText(_container.List.ToArray());
		}

		/// <summary>
		/// オブジェクトへの選択を解除
		/// </summary>
		private void SelectRelease()
		{
			foreach (var text in _textList)
			{
				if (text)
				{
					GameObject.Destroy(text.gameObject);
				}
			}
			_container.AllDelete();
		}

		/// <summary>
		/// 次の選択できる要素を取得
		/// </summary>
		/// <param name="elObj"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		private int SearchSelectElement(ElementObject elObj, int index)
		{
			int select = index;
			int listLength = elObj.ElementList.Length;
			if (0 < listLength)
			{
				select++;
				if (listLength <= select)
				{
					select = 0;
				}

				if (elObj.ElementList[select] == null)
				{
					// 再起して取得する
					select = SearchSelectElement(elObj, select);
				}
			}
			return select;
		}

		/// <summary>
		/// 要素の移動
		/// </summary>
		/// <param name="selectObj"></param>
		virtual protected void MoveElement(ElementObject selectObj)
		{
			// リストを記憶していない場合は移動しない
			if (_container.List == null) return;

			// すべての要素を移動
			selectObj.ReceiveAllElement(_container.List.ToArray());

			// ターゲット解除
			TargetUIRelease();
		}
		// TODO: 要素テキスト追加
		private void AddText(ElementBase[] elements)
		{
			// テキスト削除
			float y = 0.0f;
			foreach (var element in elements)
			{
				if (element == null)
				{
					continue;
				}

				var type = element.Type;

				// 子に要素追加
				var pos = new Vector3(-430.0f, -50.0f + y, 0.0f);
				var text = GameObject.Instantiate(_elementText);

				// UIルート取得
				var root = InGameManager.Instance.UIRoot;
				root.gameObject.transform.SetChild(text.gameObject);

				text.transform.localPosition = pos;

				// テキスト変更
				text.text = type.ToString();

				_textList[(int)type] = text;

				y -= 30.0f;
			}
		}

		/// <summary>
		/// TODO:左クリックしたオブジェクトを取得 
		/// </summary>
		/// <returns></returns>
		private ElementObject GetClickObject()
		{
			ElementObject result = null;

			// 左クリックされた場所のオブジェクトを取得
			if (Input.GetMouseButtonDown(0))
			{
				Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Collider2D collition2d = Physics2D.OverlapPoint(tapPoint);
				if (collition2d)
				{
					result = collition2d.GetComponent<ElementObject>();
				}
			}
			return result;
		}
	}
}