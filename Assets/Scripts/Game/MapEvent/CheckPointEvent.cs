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
                    //インゲームマネージャチェック
					if (InGameManager.IsInstance() == false) return;
                    //チェックポイント更新時のみ以下処理
                    if (InGameManager.Instance.GetStartPos() == gameObject.transform.position) return;
                    
                    //エフェクト発生
                    EffectManager.Instance.CreateEffect(EffectID.CheckPoint, gameObject.transform.position, 1.0f);
					// チェックポイントの更新
					InGameManager.Instance.StageManager.UpdateCheckPoint(this);
					//カメラマネージャに現在のチェックポイントを記憶＆カメラ移動
					CameraManager.Instance.CheckPointUpDate(gameObject.transform);
                    // SE
                    if (InGameManager.IsInstance())
                    {
                        if (InGameManager.Instance.GameState == InGameManager.State.Play)
                        {
                            Util.Sound.SoundManager.Instance.PlayOneShot(AudioKey.in_check_point);
                        }
                    }
				}
			};
		}
	}
}