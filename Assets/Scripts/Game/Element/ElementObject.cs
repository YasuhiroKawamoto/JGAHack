using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play.Element
{
	// 要素を持つオブジェクトクラス
	public class ElementObject : Extensions.MonoBehaviourEx
	{
		[SerializeField, Extensions.ReadOnly]
		private List<ElementBase> _elementList = null;

		public List<ElementBase> ElementList
		{
			get { return _elementList; }
			set { _elementList = value; }
		}

		private void Start()
		{
			ElementUpdate();
		}

		public void ElementUpdate()
		{
			ElementList = new List<ElementBase>();
			var array = this.GetComponents<ElementBase>();
			ElementList.AddRange(array);

			foreach (var element in ElementList)
			{
				element.Initialize();
			}
		}
	}
}