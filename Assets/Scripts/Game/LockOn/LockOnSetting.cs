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
    }
}