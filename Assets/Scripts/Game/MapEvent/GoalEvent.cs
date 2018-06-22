using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play.MapEvent
{
	public class GoalEvent : EventBase
	{
		protected override void ColliderSetting()
		{
			_onEnter += (Collider2D other) =>
			{
				if (other.GetComponent<Player>())
				{
					if (InGameManager.IsInstance())
					{
						InGameManager.Instance.StageClear();
                        //クリアエフェクトのセット
                        EffectManager.Instance.CreateEffect(EffectID.ClearEffect,other.gameObject,new Vector3(0,-3,0));
                        InGameManager.Instance.Messenger.HideWindow();
						Util.Sound.SoundManager.Instance.Stop(AudioKey.PlayBGM);
					}
				}
			};
		}
	}
}