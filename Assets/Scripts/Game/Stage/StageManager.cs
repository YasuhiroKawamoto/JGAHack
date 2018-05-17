using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play.Stage
{
    public class StageManager : MonoBehaviour
    {
        // プレイヤー
        [SerializeField]
        private Player _player = null;

        public Player Player
        {
            get { return _player; }
        }

        // ゴール
        [SerializeField]
        private Play.MapEvent.GoalEvent _goal;
        public Play.MapEvent.GoalEvent Goal
        {
            get { return _goal; }
        }

        private Vector3 _startPos;

        /// <summary>
        /// 初期化
        /// </summary>
        /// <returns></returns>
        private void Awake()
        {
            _startPos = Player.transform.position;
        }

        /// <summary>
        /// リトライ
        /// </summary>
        public void ReTry()
        {
            // 初期位置に移動
            Player.transform.position = _startPos;
        }

        public Vector3 GetStartPos()
        {
            return _startPos;
        }

        /// <summary>
        /// 初期位置の更新
        /// </summary>
        /// <param name="pos"></param>
        public void UpdateCheckPoint(CheckPointEvent check)
        {
            _startPos = check.transform.position;
        }
    }
}