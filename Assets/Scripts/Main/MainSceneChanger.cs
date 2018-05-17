using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private void Awake()
        {
            // 初期ディスプレイ
            Util.Display.DisplayManager.Instance.ChangeDisplay(_initDisplay);
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(_button))
            {
                TakeOverData.Instance.StageNum = 1;
                // 呼び出しはこれ
                Util.Scene.SceneManager.Instance.ChangeSceneFadeInOut(_sceneName);
            }
        }
    }
}