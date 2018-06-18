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
						InGameManager.Instance.StageClear();
				}
			};
		}
	}
}