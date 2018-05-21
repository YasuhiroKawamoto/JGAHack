using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class EffectState : MonoBehaviour {

    //RectTransformか？
    [SerializeField]
    private bool _isRect = false;
    //活動時間
    [SerializeField]
    private float _ActivTime;


    public bool GetIsRect()
    {
        return _isRect;
    }


    public float GetIsActTime()
    {
        return _ActivTime;
    }


}
