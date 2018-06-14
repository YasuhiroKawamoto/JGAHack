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

        public void SetUp()
        {
            base.SetUp();

            PanelSlide(TakeOverData.Instance.StageNum - 1);
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

            for (int i = 0; i < STAGE_NUM; i++)
            {
                var offSet = _popOffSet;
                var posX = 0.0f;

                if (i == _selectIndex)
                {
                    posX += offSet.x;
                }

                var posY = i * (_panelRect.y + offSet.y);
                var pos = new Vector3(_initPos.x - posX, _initPos.y - posY, _initPos.z);

                if (create)
                {
                    var obj = CreatePanel(pos, i);

                    // テキスト変更
                    var text = obj.GetComponentInChildren<Text>();
                    text.text = "STAGE " + (i + 1);
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
            if ((_moveTween != null) ||
                (!IndexIsContained(_selectIndex + c)))
            {
                return -1;
            }

            var margin = _panelRect.y + _popOffSet.y;
            _initPos.y += (margin * c);
            _selectIndex += c;
            PanelSetup();

            return _selectIndex;
        }

        private bool IndexIsContained(int index)
        {
            var count = STAGE_NUM;
            if ((index < 0) || (count <= index))
            {
                return false;
            }
            return true;
        }

    }
}