using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class EffectState : MonoBehaviour {

   
    //活動時間
    [SerializeField]
    private float _ActivTime = 0;

    public float GetIsActTime()
    {
        return _ActivTime;
    }
}
