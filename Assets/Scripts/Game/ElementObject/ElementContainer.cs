using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

namespace Play.Element
{
    [System.Serializable]
    public class ElementContainer : MonoBehaviourEx
    {

        private List<ElementBase> _list = null;

        private GameObject _dataPanel;

        public List<ElementBase> List
        {
            get { return _list; }
        }

        /// <summary>
        /// 要素をすべて受け取る
        /// </summary>
        public bool ReceiveAllElement(ElementBase[] receiveList)
        { 

            // 新規作成
            _list = new List<ElementBase>();

            if (!_dataPanel)
            {
                _dataPanel = GameObject.Find("DataPanel");
            }
               

            // 要素のコピー移動
            foreach (var element in receiveList)
            {
                if (element)
                {
                    var copy = this.CopyComponent(element);
                    copy.enabled = false;
                    _list.Add(copy);
                }
            }

            _dataPanel.GetComponent<PlayerDataPanel>().SetIcon(0, _list[0].GetType().Name,GetComponent<DiectionTest>().GetDir());
            _dataPanel.GetComponent<PlayerDataPanel>().SetIcon(1, _list[1].GetType().Name, GetComponent<DiectionTest>().GetDir());
     

            return true;
        }

        /// <summary>
        /// コンテナのすべての要素を破棄
        /// </summary>
        public void AllDelete()
        {
            if (_list == null) return;

            foreach (var element in _list)
            {
                Destroy(element);
            }
            _list = null;
            _dataPanel.GetComponent<PlayerDataPanel>().IconReset() ;
        }
    }
}