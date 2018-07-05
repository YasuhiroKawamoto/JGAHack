using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
	public class TakeOverData : Util.SingletonMonoBehaviour<TakeOverData>
	{
		// ステージ番号
		[SerializeField]
		private int _stageNum = 0;

		public int StageNum
		{
			get { return _stageNum; }
			set { _stageNum = value; }
		}

		// 移動先ディスプレイ名
		[SerializeField, Extensions.ReadOnly]
		private string _displayName = "Title";

		public string DisplayName
		{
			get { return _displayName; }
			set { _displayName = value; }
		}
	}
}