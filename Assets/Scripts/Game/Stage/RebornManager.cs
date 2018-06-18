using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play
{
	public class RebornManager : MonoBehaviour
	{

		/// <summary>
		/// 復活するオブジェクトのセット
		/// </summary>
		/// <param name="bObj"></param>
		public void RebornSet(Element.BreakElement bObj)
		{
			StartCoroutine(RebornCorutine(bObj, 10.0f));
		}

		/// <summary>
		/// 復活コルーチン
		/// </summary>
		/// <param name="bObj"></param>
		/// <param name="time"></param>
		/// <returns></returns>
		private IEnumerator RebornCorutine(Element.BreakElement bObj, float time)
		{
			yield return new WaitForSeconds(time);

			bObj.ElementObj.Reborn();
		}
	}
}