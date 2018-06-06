using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Play.LockOn
{
    public class LockOnSetting : ScriptableObject
    {
        // ターゲットのゲームオブジェクト
        [SerializeField]
        public GameObject _target = null;

        // TODO:選択したオブジェクトの要素テキスト
        [SerializeField]
        public Text _elementText;
    }
}