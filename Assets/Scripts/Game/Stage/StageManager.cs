using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play.Stage
{
    public class StageManager : MonoBehaviour
    {

        [SerializeField]
        private Player _player = null;

        private Vector3 _startPos;

        /// <summary>
        /// 初期化
        /// </summary>
        /// <returns></returns>
        private void Awake()
        {
            _startPos = _player.transform.position;
        }

        /// <summary>
        /// リトライ
        /// </summary>
        public void ReTry()
        {
            // 初期位置に移動
            _player.transform.position = _startPos;
        }

        public Vector3 GetStartPos()
        {
            return _startPos;
        }
    }
}