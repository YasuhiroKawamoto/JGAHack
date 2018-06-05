using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using DG.Tweening;

namespace Main
{
    public class MainSceneChanger : MonoBehaviour
    {

        // 初期ディスプレイ
        [SerializeField]
        private Util.Display.DisplayBase _initDisplay = null;

        // 遷移する際押すマウスの対応した番号
        [SerializeField]
        private int _button = 0;

        // 遷移するシーンの名前
        [SerializeField]
        private string _sceneName = string.Empty;

        [SerializeField]
        private GameObject _backStage = null;

        private void Awake()
        {
            // 初期ディスプレイ
            Util.Display.DisplayManager.Instance.ChangeDisplay(_initDisplay);

        }

        void Start()
        {
            StartCoroutine(StageLoad());
        }

        private IEnumerator StageLoad()
        {
            // アセットのロード
            var stageAsset = Resources.LoadAsync("Stage/TestGrid");

            // ロード待ち
            yield return new WaitWhile(() => !stageAsset.isDone);

            var stageObj = stageAsset.asset as GameObject;
            var stage = Instantiate(stageObj);
            _backStage.transform.SetChild(stage);

            var pos = stage.transform.localPosition;
            pos.x = -7.0f;
            stage.transform.localPosition = pos;
            stage.transform.DOLocalMoveX(pos.x + 10.0f, 5.0f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(_button))
            {
                Play.InGameManager.Destroy();
                TakeOverData.Instance.StageNum = 1;
                // 呼び出しはこれ
                Util.Scene.SceneManager.Instance.ChangeSceneFadeInOut(_sceneName);
            }
        }
    }
}