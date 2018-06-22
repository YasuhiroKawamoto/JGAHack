﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using DG.Tweening;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Main
{
    public class MainSceneChanger : MonoBehaviour
    {
        [SerializeField]
        private BackStage _backStage = null;

        private void Awake()
        {
            // Display 取得
            var name = TakeOverData.Instance.DisplayName;
            var obj = transform.Find(name);
            var display = obj.GetComponent<Util.Display.DisplayBase>();

            // 初期ディスプレイ
            Util.Display.DisplayManager.Instance.ChangeDisplay(display);

            // ステージタイムデータ読み込み
            StageTimeData.Instance.Initialize();
        }

        void Start()
        {
            // ステージをランダムにロード
            int num = 10;
            var rand = Random.Range(1, num + 1);
            _backStage.ChangeStage(rand);

            // BGM開始
            Util.Sound.SoundManager.Instance.Play(AudioKey.MainBGM);

            // カメラの左右移動
            var camera = Camera.main;
            var pos = camera.transform.localPosition;
            pos.x = -7.0f;
            camera.transform.localPosition = pos;
            camera.transform.DOLocalMoveX(pos.x + 10.0f, 5.0f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
    }
}