using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Play
{
    // インゲームの管理クラス
    public class InGameManager : Util.SingletonMonoBehaviour<InGameManager>
    {
        // ゲームの状態
        public enum State
        {
            Stand = 0,
            Play,
            Pause,
            Clear,
            Over
        }

        // 状態
        [SerializeField, Extensions.ReadOnly]
        private State _state = State.Stand;
        public State GameState
        {
            get { return _state; }
        }

        // UIRoot
        [SerializeField]
        private GameObject _uiRoot = null;
        public GameObject UIRoot
        {
            get { return _uiRoot; }
        }

        // ElementRoot
        [SerializeField]
        private GameObject _elementObjRoot = null;
        public GameObject ElementObjRoot
        {
            get { return _elementObjRoot; }
        }

        // カメラ
        [SerializeField]
        private Stage.StageManager _stageManager = null;

        void OnGUI()
        {
            if (_state == State.Clear)
            {
                GUI.Label(new Rect(370, 50, 100, 50), "Clear");
                // ボタンを表示する
                if (GUI.Button(new Rect(320, 170, 100, 50), "ReStart"))
                {
                    GameReLoad();
                }
            }
        }

        /// <summary>
        /// ゲームの開始
        /// </summary>
        public void GameStart()
        {
            _state = State.Play;
        }

        /// <summary>
        /// ステージのクリア
        /// </summary>
        public void StageClear()
        {
            _state = State.Clear;
        }

        /// <summary>
        /// ステージの失敗
        /// </summary>
        public void StageOver()
        {
            _stageManager.ReTry();
        }

        public void GameReLoad()
        {
            Scene loadScene = SceneManager.GetActiveScene();
            // Sceneの読み直し
            SceneManager.LoadScene(loadScene.name);
        }

        /// <summary>
        /// ゲームの一時停止
        /// </summary>
        public void GamePause()
        {
            _state = State.Pause;
        }

        public Vector3 GetStartPos()
        {
            return _stageManager.GetStartPos();
        }
    }
}