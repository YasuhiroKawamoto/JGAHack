using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play.Element
{
	// 要素の基底クラス

	[System.Serializable]
	public abstract class ElementBase : Extensions.MonoBehaviourEx
	{
		/// <summary>
		/// 初期化
		/// </summary>
		public abstract void Initialize();
	}
}