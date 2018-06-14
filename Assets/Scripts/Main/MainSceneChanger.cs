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
            _backStage.ChangeStage(1);
        }
    }
}