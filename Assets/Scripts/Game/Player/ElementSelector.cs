using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Play.Element;
using Extensions;

using System.Reflection;

namespace Play
{
	// 要素オブジェクトの選択と要素の移動用クラス
	public class ElementSelector : MonoBehaviour
	{
		// 選択しているオブジェクト
		[SerializeField, ReadOnly]
		private ElementObject _selectObject = null;

		void Start()
		{

		}

		void Update()
		{
			// 選択
			SelectObject();

			// 解除
			ObjectRelease();
		}

		/// <summary>
		/// オブジェクトを選択
		/// </summary>
		private void SelectObject()
		{
			// オブジェクトを選択していない
			var select = GetClickObject();
			if (select)
			{
				// 選択したオブジェクトがある
				var elementObj = select.GetComponent<ElementObject>();
				if (elementObj)
				{
					SelectElementObject(elementObj);
				}
			}
		}

		/// <summary>
		/// 要素オブジェクトを選択したときの処理
		/// </summary>
		/// <param name="elementObj"></param>
		private void SelectElementObject(ElementObject elementObj)
		{
			if (elementObj == null)
			{
				// エレメントオブジェクトがない場合は何もしない
				return;
			}

			if (_selectObject == null)
			{
				// 既に選択されていないときは選択
				_selectObject = elementObj;
			}
			else
			{
				// されている場合は要素の移動
				MoveElement(elementObj);
			}

		}

		/// <summary>
		/// 要素の移動
		/// </summary>
		/// <param name="selectObj"></param>
		private void MoveElement(ElementObject selectObj)
		{
			foreach (var element in _selectObject.ElementList)
			{
				var com = selectObj.MoveComponent(element);
			}

			selectObj.ElementUpdate();

			// 選択解除
			ObjectRelease();
		}

		/// <summary>
		/// 選択したオブジェクトを解除
		/// </summary>
		private void ObjectRelease()
		{
			if (Input.GetMouseButtonDown(1))
			{
				_selectObject = null;
			}
		}

		/// <summary>
		/// 左クリックしたオブジェクトを取得 
		/// </summary>
		/// <returns></returns>
		private GameObject GetClickObject()
		{
			GameObject result = null;

			// 左クリックされた場所のオブジェクトを取得
			if (Input.GetMouseButtonDown(0))
			{
				Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Collider2D collition2d = Physics2D.OverlapPoint(tapPoint);
				if (collition2d)
				{
					result = collition2d.transform.gameObject;
				}
			}
			return result;
		}
	}
}