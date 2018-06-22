﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using DG.Tweening;

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

		// 上書き時の位置
		[SerializeField]
		private List<Vector3> _overwritePos = new List<Vector3>();

		// 我に戻る時間(秒)
		[SerializeField]
		private float _returnTime = 5.0f;

		//リジットボディ
		private Rigidbody2D _rigidBody2d;

		//現在保有しているエフェクト
		[SerializeField, ReadOnly]
		private GameObject _myEffect;

		//今のの向き
		[SerializeField, ReadOnly]
		private Direction _currentDirection;

		//もともとの向き
		[SerializeField, ReadOnly]
		private Direction _tmpDirection;

		/// <summary>
		/// 初期化
		/// </summary>
		private void Awake()
		{
			//リジットボディ取得
			_rigidBody2d = gameObject.transform.parent.GetComponent<Rigidbody2D>();
			_initPos = _rigidBody2d.transform.position;
			_tmpDirection = gameObject.GetComponent<DiectionTest>().GetDir();
			_currentDirection = _tmpDirection;
		}
		private void Start()
		{
			ElementUpdate();

			// 最初の要素を覚えておく
			SaveRemenberList();
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
		/// 要素を覚えておく
		/// </summary>
		private void SaveRemenberList()
		{
			// 忘れてはいけないものがない…
			if (_rememberList == null)
			{
				int index = (int)ElementType.length;
				_rememberList = new ElementBase[index];

				// 現在の要素を止める
				for (int i = 0; i < _elementList.Length; i++)
				{
					if (_elementList[i])
					{
						if (_elementList[i].Type != ElementType.Action)
						{
							_rememberList[i] = this.CopyComponent(_elementList[i]);
							_rememberList[i].enabled = false;
						}
					}
				}
			}
		}

		/// <summary>
		/// 要素をすべて受け取る
		/// </summary>
		public bool ReceiveAllElement(ElementBase[] receiveList)
		{
			// 要素のコピー移動
			foreach (var element in receiveList)
			{
				if (element)
				{
					if (element.Type != ElementType.Action)
					{
						this.CopyComponent(element);
						//現在の向きを取得
						if (element.Type != ElementType.Direction)
						{
							_currentDirection = element.GetComponent<DiectionTest>().GetDir();
						}
						// 要素の更新
						this.ElementUpdate();
					}
				}
			}

			// 状態の変更
			_stats = ElementStates.Element;

			//上書き時の位置を保存
			_overwritePos.Add(_rigidBody2d.transform.position);

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
			var index = _overwritePos.Count;

			// 待つ
			yield return new WaitForSeconds(_returnTime);

			if (index == _overwritePos.Count)
			{
				// 正気になる
				ReturnToSanity();
				//TODO アニメ切り替え
				//親オブジェのレイヤーが「Enemy」なら
				if (LayerMask.LayerToName(gameObject.transform.parent.gameObject.layer) == "Enemy")
				{
                    //アニメーションを向きに合わせて変更
                    transform.parent.GetComponent<EnemyAnimController>().ChangeAnim(GetComponent<Play.Element.DiectionTest>().GetDir());
				}
				else if (LayerMask.LayerToName(gameObject.transform.parent.gameObject.layer) == "Object")
				{
                    //画像を向きに合わせて変更
                    transform.parent.GetComponent<ChangeSprite>().ChangeImage(GetComponent<Play.Element.DiectionTest>().GetDir());
				}
			}
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
            // SE 
            if (InGameManager.IsInstance())
            {
                if (InGameManager.Instance.GameState == InGameManager.State.Play)
                {
                    Util.Sound.SoundManager.Instance.PlayOneShot(AudioKey.in_sanity);
                }
            }
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
			// 上書き位置まで移動
			foreach (var pos in _overwritePos)
			{
				// 近くのときは戻らない
				if (IsNearPos(this.transform.position, pos, 0.05f) == false)
					yield return StartCoroutine(ReturnMove(pos));
			}

			_overwritePos = new List<Vector3>();

			// 初期位置に移動
			yield return StartCoroutine(ReturnMove(_initPos));
		}

		/// <summary>
		/// 座標が近くか？
		/// </summary>
		/// <param name="pos1"></param>
		/// <param name="pos2"></param>
		/// <returns></returns>
		private bool IsNearPos(Vector3 pos1, Vector3 pos2, float difBase = 0.1f)
		{
			var difX = Mathf.Abs(pos1.x - pos2.x);
			var difY = Mathf.Abs(pos1.y - pos2.y);

			if (difBase < difX) return false;
			if (difBase < difY) return false;

			return true;
		}

		// 現在の要素をすべて忘れる
		private void ForgetAllElement()
		{
			if (_elementList == null) return;

			foreach (var element in _elementList)
			{
				if (element)
				{
					//アクション以外は忘れる
					if (element.Type != ElementType.Action)
					{
						Destroy(element);
					}
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
					var copy = this.CopyComponent(element);
					copy.enabled = true;

					if (copy.Type != ElementType.Direction)
					{
						_currentDirection = copy.GetComponent<DiectionTest>().GetDir();
					}
				}
			}

			// 更新
			ElementUpdate();

			// 状態の変更
			_stats = ElementStates.Default;
		}

		/// <summary>
		/// 指定位置への移動
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		private IEnumerator ReturnMove(Vector3 pos)
		{
			// 目的位置に向かって一定時間で移動 TODO : 変更が入る場合1.0fの部分を変数に
			var tween = _rigidBody2d.transform.DOMove(pos, 1.0f);
			bool finish = false;
			tween.OnComplete(() => finish = true);

			yield return new WaitUntil(() => finish);
		}

		/// <summary>
		/// 復活
		/// </summary>
		public void Reborn()
		{
			// 初期化
			// 初期位置
			_rigidBody2d.gameObject.transform.position = _initPos;

            // SE 
            if (InGameManager.IsInstance())
            {
                if (InGameManager.IsInstance())
                {
                    if (InGameManager.Instance.GameState == InGameManager.State.Play)
                    {
                        Util.Sound.SoundManager.Instance.PlayOneShot(AudioKey.in_reborn);
                    }
                }
            }
			//復活エフェクト
			EffectManager.Instance.CreateEffect(EffectID.EnemyRespown, gameObject.transform.position, 2);

			// 動きリセット
			ElementUpdate();

			// 今の要素を忘れる
			ForgetAllElement();

			// 要素を思い出す
			ReCallElement();

			// 復活
			transform.parent.gameObject.SetActive(true);

			//親オブジェのレイヤーが「Enemy」なら
			if (LayerMask.LayerToName(gameObject.transform.parent.gameObject.layer) == "Enemy")
			{
                //アニメーションを向きに合わせて変更
                transform.parent.GetComponent<EnemyAnimController>().ChangeAnim(GetComponent<Play.Element.DiectionTest>().GetDir());
			}
			else if (LayerMask.LayerToName(gameObject.transform.parent.gameObject.layer) == "Object")
			{
				//画像を向きに合わせて変更
				transform.parent.GetComponent<ChangeSprite>().ChangeImage(GetComponent<Play.Element.DiectionTest>().GetDir());
			}
		}


		//エフェクト更新（旧エフェクト破棄）
		public void EffectUpDate(GameObject newEffect)
		{
			//エフェクトがある場合
			if (_myEffect)
			{
				Destroy(_myEffect);
			}
			_myEffect = newEffect;
		}


		//復帰までの時間取得
		public float GetReturnTime()
		{
			return _returnTime;

		}


		//現在向き取得
		public Direction GetCurrentDirection()
		{
			return _currentDirection;

		}

		//もともとの向き取得
		public Direction GetTmpDirection()
		{
			return _tmpDirection;

		}
	}
}