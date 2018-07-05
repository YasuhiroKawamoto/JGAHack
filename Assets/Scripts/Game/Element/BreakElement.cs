﻿using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;


namespace Play.Element
{
	public class BreakElement : MonoBehaviour
	{
		//壊れる性質

		//復活できるか？
		[SerializeField]
		private bool _canRebirth = false;

		// 自らの要素オブジェクト
		private Element.ElementObject _elementObj = null;
		public ElementObject ElementObj
		{
			get { return _elementObj; }
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (InGameManager.IsInstance() == false) return;

			//TODO 接触物判定
			if (LayerMask.LayerToName(collision.gameObject.layer) == "Bullet")
			{

				EffectManager.Instance.CreateEffect(EffectID.DestoryEnemy, gameObject.transform.position);
				//復活可能なら
				if (_canRebirth)
				{
					_elementObj = GetComponentInChildren<ElementObject>();

					//再生を司るものに情報を送る
					InGameManager.Instance.RebornSet(this);
                    //敵破壊サウンド
                    if (InGameManager.IsInstance())
                    {
                        if (InGameManager.Instance.GameState == InGameManager.State.Play)
                        {
                            Util.Sound.SoundManager.Instance.PlayOneShot(AudioKey.in_death);
                        }
                    }
                    // 非表示
                    gameObject.SetActive(false);

					return;
				}
				//Debug.Log("壊れるぅ");
				Destroy(gameObject);
			}
		}
	}
}