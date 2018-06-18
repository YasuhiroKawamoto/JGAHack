using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Play.Element;

namespace Play.Tutrial
{
	public class TutrialSelector : ElementSelector
	{

		/// <summary>
		/// オブジェクトをターゲット
		/// </summary>
		override protected void TargetObject(ElementObject obj)
		{
			var manager = InGameManager.Instance;
			var messenger = manager.Messenger;

			var tutrial = TutrialManager.Instance;
			if (tutrial.CanTarget())
			{
				var targetObj = tutrial.GetTargetObj();
				if (targetObj == null) base.TargetObject(obj);
				else
				{
					base.TargetObject(targetObj);
					tutrial.NextStep();
				}
			}
		}

		/// <summary>
		/// オブジェクトを選択
		/// </summary>
		override protected void SelectObject()
		{
			var manager = InGameManager.Instance;
			var messenger = manager.Messenger;

			var tutrial = TutrialManager.Instance;

			if (tutrial.CanCopy())
			{
				base.SelectObject();
				// 次に移行
				tutrial.NextStep();
			}
		}

		/// <summary>
		/// 要素の移動
		/// </summary>
		/// <param name="selectObj"></param>
		override protected void MoveElement(ElementObject selectObj)
		{
			var manager = InGameManager.Instance;
			var messenger = manager.Messenger;

			var tutrial = TutrialManager.Instance;

			if (tutrial.CanPaste())
			{
				base.MoveElement(selectObj);
				// 次に移行
				tutrial.NextStep();
			}
		}
	}
}