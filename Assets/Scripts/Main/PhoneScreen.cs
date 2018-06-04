using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    public class PhoneScreen : MonoBehaviour
    {
        [SerializeField]
        private Image _stagePanel = null;

        [SerializeField]
        private Vector3 _initPos = Vector3.zero;

        [SerializeField]
        private Vector2 _popOffSet = Vector2.zero;

        // 作成するパネルの数
        public static readonly int PANEL_NUM = 7;

        [SerializeField]
        private List<Image> _panelList = new List<Image>();

        private RectTransform _rectTransform = null;

        private Vector2 _panelRect = Vector2.zero;

        private int _selectIndex = 0;

        public void SetUp()
        {
            _selectIndex = Mathf.CeilToInt(PANEL_NUM / 2.0f) - 1;
            _panelRect = _stagePanel.rectTransform.sizeDelta;

            _rectTransform = GetComponent<RectTransform>();

            PanelSetup();
        }

        /// <summary>
        /// パネルの作成
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private Image CreatePanel(Vector3 pos)
        {
            var obj = Instantiate(_stagePanel, pos, Quaternion.identity);
            obj.rectTransform.SetParent(_rectTransform);
            _panelList.Add(obj);

            obj.rectTransform.localScale = Vector3.one;
            obj.rectTransform.localPosition = pos;

            return obj;
        }

        private Image MovePanel(Vector3 pos, int index)
        {
            var obj = _panelList[index];

            obj.rectTransform.localScale = Vector3.one;
            obj.rectTransform.localPosition = pos;

            return obj;
        }

        private void PanelSetup()
        {
            bool create = false;
            if (_panelList.Count == 0)
            {
                create = true;
            }

            for (int i = 0; i < PANEL_NUM; i++)
            {
                var offSet = _popOffSet;
                var posX = 0.0f;

                if (i == _selectIndex)
                {
                    posX += offSet.x;
                }

                var posY = i * (_panelRect.y + offSet.y);
                var pos = new Vector3(_initPos.x - posX, _initPos.y - posY, _initPos.z);

                if (create) CreatePanel(pos);
                else MovePanel(pos, i);
            }
        }

        public void PanelNext()
        {
            _selectIndex++;
            PanelSetup();
        }

        public void PanelBefore()
        {
            _selectIndex--;
            PanelSetup();
        }

    }
}