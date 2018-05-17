using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class TakeOverData : Util.SingletonMonoBehaviour<TakeOverData>
    {
        // ステージ番号
        [SerializeField, Extensions.ReadOnly]
        private int _stageNum = 0;

        public int StageNum
        {
            get { return _stageNum; }
            set { _stageNum = value; }
        }
    }
}