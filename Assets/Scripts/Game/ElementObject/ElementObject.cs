using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

namespace Play.Element
{
	// 要素を持つオブジェクトクラス
	public class ElementObject : Extensions.MonoBehaviourEx
	{
		// 要素オブジェクトの状態
		public enum ElementStates
		{
			Default,
			Element,
			Remember,
			Dead
		}

		// 現在の状態
		[SerializeField]
		private ElementStates _stats = ElementStates.Default;
		public ElementStates Stats
		{
			get { return _stats; }
		}

		// 付与されている要素たち
		[SerializeField, Extensions.ReadOnly]
		private ElementBase[] _elementList = null;

		public ElementBase[] ElementList
		{
			get { return _elementList; }
			set { _elementList = value; }
		}

		// 忘れない要素
		private ElementBase[] _rememberList = null;

		// 初期位置
		[SerializeField]
		private Vector3 _initPos = Vector3.zero;

		// 上書き時の位置リスト
		[SerializeField]
		private List<Vector3> _overwritePosList = new List<Vector3>();

		// 我に戻る時間(秒)
		static readonly float _returnTime = 5.0f;

		//移動速度
		[SerializeField]
		private float _speed;

		//帰るべき場所
		[SerializeField, ReadOnly]
		private Vector3 _returnPosition;

		//リジットボディ
		private Rigidbody2D _rigidBody2d;

		/// <summary>
		/// 初期化
		/// </summary>
		private void Awake()
		{
			_initPos = transform.position;
			//リジットボディ取得
			_rigidBody2d = gameObject.GetComponentInParent<Rigidbody2D>();
		}
		private void Start()
		{
			ElementUpdate();
		}

		/// <summary>
		/// アタッチされている要素を検出
		/// </summary>
		public void ElementUpdate()
		{
			int index = (int)ElementType.length;
			_elementList = new ElementBase[index];

			var array = this.GetComponents<ElementBase>();

			foreach (var element in array)
			{
				int typeIndex = (int)element.Type;

				if (typeIndex < 0)
				{
					// タイプがない場合は削除
					Object.Destroy(element);
				}

				// 実行されていないときはスキップ
				if (element.enabled == false)
				{
					continue;
				}

				if (_elementList[typeIndex])
				{
					// タイプがかぶっている場合後半を反映
					_elementList[typeIndex].Discard();
					Object.Destroy(_elementList[typeIndex]);
				}

				_elementList[typeIndex] = element;
				element.Initialize();
			}
		}

		/// <summary>
		/// 要素をすべて受け取る
		/// </summary>
		public bool ReceiveAllElement(ElementBase[] receiveList)
		{
			// 忘れてはいけないものがある…
			if (_rememberList != null)
			{
				return false;
			}

			int index = (int)ElementType.length;
			_rememberList = new ElementBase[index];

			// 現在の要素を止める
			for (int i = 0; i < _elementList.Length; i++)
			{
				if (_elementList[i])
				{
					_elementList[i].enabled = false;
					_rememberList[i] = _elementList[i];
				}
			}

			// 要素のコピー移動
			foreach (var element in receiveList)
			{
				if (element)
				{
					this.CopyComponent(element);

					// 要素の更新
					this.ElementUpdate();
				}

			}

			// 状態の変更
			_stats = ElementStates.Element;

			//上書き時の位置を保存
			_overwritePosList.Add(transform.position);

			// n秒後思い出すコルーチン
			StartCoroutine(WaitSanity());

			return true;
		}

		/// <summary>
		/// 正気に戻るのを待つ
		/// </summary>
		/// <returns></returns>
		private IEnumerator WaitSanity()
		{
			// 待つ
			yield return new WaitForSeconds(_returnTime);

			// 正気になる
			ReturnToSanity();
		}

		/// <summary>
		/// 正気に戻る
		/// </summary>
		public void ReturnToSanity()
		{
			StartCoroutine(ReturnToSanityCorutine());
		}
		private IEnumerator ReturnToSanityCorutine()
		{
			// 状態の変更
			_stats = ElementStates.Remember;

			// 今の要素を忘れる
			ForgetAllElement();

			// 元の位置に戻る
			yield return ReturnToInitPos();

			// 要素を思い出す
			ReCallElement();
		}

		/// <summary>
		/// 初期位置に戻る
		/// </summary>
		private IEnumerator ReturnToInitPos()
		{
			//リスト内要素逆回し用のカウント
			int Count = _overwritePosList.Count - 1;
			//上書き時の位置をセット
			SetReturnMove(_overwritePosList[Count]);

			//ループ処理
			while (true)
			{
				//上書き位置に戻れば
				if (transform.position == _overwritePosList[Count])
				{
					//Debug.Log("我戻れり");
					if (0 < Count)
					{
						Count--;
						//上書き時の位置をセット
						SetReturnMove(_overwritePosList[Count]);
					}
					else
					{
						//初期位置をセット
						SetReturnMove(_initPos);
					}
				}

				//元の位置に戻れば
				if (transform.position == _initPos)
				{
					//Debug.Log("我完全に戻れり");
					//上書き位置リストのクリア
					_overwritePosList.Clear();
					//コルーチン終わり
					yield break;
				}
				else
				{
					// TODO: 元の位置に向かって移動
					ReturnMove();
				}

				// 毎フレームループ
				yield return null;
			}
		}

		// 現在の要素をすべて忘れる
		private void ForgetAllElement()
		{
			foreach (var element in _elementList)
			{
				if (element)
				{
					Destroy(element);
				}
			}
			_elementList = null;
		}

		/// <summary>
		/// 要素を思い出す
		/// </summary>
		private void ReCallElement()
		{
			foreach (var element in _rememberList)
			{
				if (element)
				{
					element.enabled = true;
				}
			}

			// 忘れてはいけないものを忘れる…
			_rememberList = null;

			// 更新
			ElementUpdate();

			// 状態の変更
			_stats = ElementStates.Default;
		}

		private void SetReturnMove(Vector3 returnPos)
		{
			// Debug.Log("回帰セットぉ");
			//速度セット
			_speed = 1.0f;
			//帰るべき場所セット
			_returnPosition = returnPos;
		}

		private void ReturnMove()
		{
			//目的位置に向かって一定速度で移動
			// Debug.Log("移動中");
			_rigidBody2d.MovePosition(Vector3.MoveTowards(transform.position, _returnPosition, Time.deltaTime * _speed));

		}

		/// <summary>
		/// 復活
		/// </summary>
		public void Reborn()
		{
			// 初期化
			// 初期位置
			_rigidBody2d.gameObject.transform.position = _initPos;

			// 動きリセット
			ElementUpdate();

			// 復活
			transform.parent.gameObject.SetActive(true);
		}
	}
}