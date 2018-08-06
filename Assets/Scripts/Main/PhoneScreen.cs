﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Main
{
    public class PhoneScreen : ScreenBase
    {
        // 作成するパネルの数
        public int STAGE_NUM = 7;

        private Tween _moveTween = null;

        private System.Action<int> _selectFunc = null;

        public void SetButtonFunc(System.Action<int> func)
        {
            _selectFunc = func;
        }

        private void ButtonFunc(int index)
        {
            // すべてのボタンのUnSelectを呼ぶ
            for (int i = 0; i < _panelList.Count; i++)
            {
                if (i == index) continue;
                var tilePanel = _panelList[i].GetComponent<TitlePanel>();
                tilePanel.UnSelect();
            }

            _selectFunc(index);
        }

        override public void SetUp()
        {
            base.SetUp();

            var num = TakeOverData.Instance.StageNum - 1;
            var stage = 0 < num ? num : 0;

            DestroyPanel();

            PanelSlide(0);

            var tilePanel = _panelList[0].GetComponent<TitlePanel>();
            tilePanel.Select();
        }

        private Image MovePanel(Vector3 pos, int index)
        {
            var obj = _panelList[index];

            obj.rectTransform.localScale = Vector3.one;
            _moveTween = obj.rectTransform.DOLocalMove(pos, 0.15f)
                                .OnComplete(() => _moveTween = null);

            return obj;
        }

        /// <summary>
        /// パネルのセットアップ
        /// </summary>
        private void PanelSetup()
        {
            var create = IsCreated();

            if (create)
            {
                _panelList = new List<Image>();
            }

            float bHeight = _panel.rectTransform.sizeDelta.y;

            RectTransform contentRectTrans = GetComponent<RectTransform>();
            contentRectTrans.sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, bHeight * STAGE_NUM + _popOffSet.y * (STAGE_NUM + 2));

            for (int i = 0; i < STAGE_NUM; i++)
            {
                var offSet = _popOffSet;
                var posX = 0.0f;

                var posY = (i + 1) * (_panelRect.y + offSet.y);
                var pos = new Vector3(_initPos.x - posX, _initPos.y - posY, _initPos.z);

                if (create)
                {
                    var obj = CreatePanel(pos, i);

                    // テキスト変更
                    var text = obj.GetComponentInChildren<Text>();
                    text.text = "STAGE " + (i + 1);

                    var panel = obj.GetComponent<TitlePanel>();
                    panel.Stage = i;
                    panel.Screen = this;
                    panel.SelectFunc = ButtonFunc;
                }
                else MovePanel(pos, i);
            }
        }

        /// <summary>
        /// パネルを次に移動
        /// </summary>
        public int PanelNext()
        {
            return PanelSlide(1);
        }

        /// <summary>
        /// パネルを前に移動
        /// </summary>
        public int PanelBefore()
        {
            return PanelSlide(-1);
        }

        /// <summary>
        /// パネルのスライド移動
        /// </summary>
        /// <param name="c"></param>
        private int PanelSlide(int c)
        {

            if (_moveTween != null)
            {
                return -1;
            }

            // 上下の場合スキップ移動
            var count = STAGE_NUM;
            if (_selectIndex + c < 0)
            {
                c = STAGE_NUM - 1;
            }
            else if (count <= _selectIndex + c)
            {
                c = -STAGE_NUM + 1;
            }

            var margin = _panelRect.y + _popOffSet.y;
            _initPos.y += (margin * c);
            _selectIndex += c;
            PanelSetup();

            return _selectIndex;
        }
    }
}