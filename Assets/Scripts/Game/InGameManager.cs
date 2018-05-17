using System.Collections;
using System.Collections.Generic;
using Play.Stage;
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

        // ステージ管理
        [SerializeField]
        private Stage.StageManager _stageManager = null;
        public StageManager StageManager
        {
            get { return _stageManager; }
        }

        // カメラ管理
        [SerializeField]
        private CameraManager _cameraManager = null;

        public CameraManager CameraManager
        {
            get { return _cameraManager; }
        }

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

        void Start()
        {
            // ゲームの設定
            StartCoroutine(StartSetUp());
        }

        private IEnumerator StartSetUp()
        {
            // ステージプレハブの設定
            yield return LoadStage();

            // カメラに必要な要素を設定
            CameraManager.Player = StageManager.Player.gameObject;
            CameraManager.Goal = StageManager.Goal.gameObject;

            //カメラ初期化
            CameraManager.InitCamera();

            yield return null;
        }

        /// <summary>
        /// ステージの読み込み
        /// </summary>
        /// <returns></returns>
        private IEnumerator LoadStage()
        {
            // アセットのロード
            var stageAsset = Resources.LoadAsync("Stage/TestGrid");

            // ロード待ち
            yield return new WaitWhile(() => !stageAsset.isDone);

            var stageObj = stageAsset.asset as GameObject;
            var stage = Instantiate(stageObj);

            var manager = stage.GetComponent<StageManager>();

            // ステージマネージャーの設定
            _stageManager = manager;
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
            StageManager.ReTry();
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
            return StageManager.GetStartPos();
        }
    }
}