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
        private Vector3 _popOffSet = Vector3.zero;

        // 作成するパネルの数
        public static readonly int PANEL_NUM = 7;

        [SerializeField]
        private List<Image> _panelList = new List<Image>();

        private RectTransform _rectTransform = null;

        public void SetUp()
        {
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

        private void PanelSetup()
        {
            for (int i = 0; i < PANEL_NUM; i++)
            {
                var rectT = _stagePanel.rectTransform.sizeDelta;
                var offSet = _popOffSet;
                var posY = i * (rectT.y + offSet.y);
                var pos = new Vector3(_initPos.x, _initPos.y - posY, _initPos.z);
                var obj = CreatePanel(pos);
            }
        }

    }
}