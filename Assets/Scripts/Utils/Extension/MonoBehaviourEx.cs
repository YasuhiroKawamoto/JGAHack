using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Reflection;

namespace Extensions
{
	public class MonoBehaviourEx : MonoBehaviour
	{

		/// <summary>
		/// X座標
		/// </summary>
		public float PosX
		{
			get { return transform.position.x; }
			set
			{
				Vector3 pos = transform.position;
				pos.x = value;
				transform.position = pos;
			}
		}

		/// <summary>
		/// Y座標
		/// </summary>
		public float PosY
		{
			get { return transform.position.y; }
			set
			{
				Vector3 pos = transform.position;
				pos.y = value;
				transform.position = pos;
			}
		}

		/// <summary>
		/// Z座標
		/// </summary>
		public float PosZ
		{
			get { return transform.position.z; }
			set
			{
				Vector3 pos = transform.position;
				pos.z = value;
				transform.position = pos;
			}
		}

		/// <summary>
		/// Xローカル座標
		/// </summary>
		public float LocalPosX
		{
			get { return transform.localPosition.x; }
			set
			{
				Vector3 pos = transform.localPosition;
				pos.x = value;
				transform.localPosition = pos;
			}
		}

		/// <summary>
		/// Yローカル座標
		/// </summary>
		public float LocalPosY
		{
			get { return transform.localPosition.y; }
			set
			{
				Vector3 pos = transform.localPosition;
				pos.y = value;
				transform.localPosition = pos;
			}
		}

		/// <summary>
		/// Zローカル座標
		/// </summary>
		public float LocalPosZ
		{
			get { return transform.localPosition.z; }
			set
			{
				Vector3 pos = transform.localPosition;
				pos.z = value;
				transform.localPosition = pos;
			}
		}

		/// <summary>
		/// Xスケール
		/// </summary>
		public float ScaleX
		{
			get { return transform.localScale.x; }
			set
			{
				Vector3 scale = transform.localScale;
				scale.x = value;
				transform.localScale = scale;
			}
		}

		/// <summary>
		/// Yスケール
		/// </summary>
		public float ScaleY
		{
			get { return transform.localScale.y; }
			set
			{
				Vector3 scale = transform.localScale;
				scale.y = value;
				transform.localScale = scale;
			}
		}

		/// <summary>
		/// コンポーネントの取得 ない場合はアタッチする
		/// </summary>
		/// <typeparam name="T">取得したいコンポーネント</typeparam>
		/// <returns>コンポーネント</returns>
		public T GetComponentAttach<T>() where T : Component
		{
			return this.gameObject.GetComponent<T>() ?? this.gameObject.AddComponent<T>();
		}

		/// <summary>
		/// コンポーネントのコピー
		/// </summary>
		/// <param name="orgComponent"></param>
		/// <returns></returns>
		public T CopyComponent<T>(T orgComponent) where T : Component
		{
			var type = orgComponent.GetType();
			var com = gameObject.AddComponent(type);

			// Target
			BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;

			// プロパティ取得
			PropertyInfo[] propInfoList = type.GetProperties(flags);
			foreach (var propInfo in propInfoList)
			{
				if (propInfo.CanWrite)
				{
					propInfo.SetValue(com, propInfo.GetValue(orgComponent, null), null);

				}
			}

			// フィールドの属性を取得し、フィールドのメタデータにアクセス
			FieldInfo[] finfos = type.GetFields(flags);
			foreach (var finfo in finfos)
			{
				// 値の設定
				finfo.SetValue(com, finfo.GetValue(orgComponent));
			}
			return com as T;
		}

		/// <summary>
		/// コンポーネントの移動
		/// </summary>
		/// <param name="toMoveComponent"></param>
		/// <returns></returns>
		public T MoveComponent<T>(T toMoveComponent) where T : Component
		{
			// オリジナルを元にコピーする
			var com = this.CopyComponent(toMoveComponent);

			// 移動後は削除する
			UnityEngine.Component.Destroy(toMoveComponent);

			return com as T;
		}
	}
}