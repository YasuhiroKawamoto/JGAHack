using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play
{
    // インゲームの管理クラス
    public class InGameManager : Util.SingletonMonoBehaviour<InGameManager>
    {
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
    }
}