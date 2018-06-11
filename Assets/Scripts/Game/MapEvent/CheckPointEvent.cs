using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play
{
	public class CheckPointEvent : MapEvent.EventBase
	{
		protected override void ColliderSetting()
		{
			_onEnter = (Collider2D other) =>
			{
				if (other.GetComponent<Player>())
				{
					// チェックポイントの更新
					InGameManager.Instance.StageManager.UpdateCheckPoint(this);
                    //カメラマネージャに現在のチェックポイントを記憶＆カメラ移動
                    CameraManager.Instance.CheckPointUpDate(gameObject.transform);
				}
			};
		}
	}
}