using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISet : MonoBehaviour
{

    //ターゲットに追従するUIにつけるスクリプト
    private RectTransform _rectTransform;
    //ターゲットの情報
    [SerializeField]
    private Transform _targetTransform;
    //オフセットの値
    [SerializeField]
    private Vector3 _offset;

    void Start()
    {
        //自身のRectTransform取得
        _rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        //ターゲットの座標に合わせる
        if (_targetTransform)
        {
            //ワールド座標に変換（オフセット込み）
            _rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, _targetTransform.position + _offset);
        }
    }

    //ターゲットセット
    public void SetTransform(Transform tfm)
    {
        _targetTransform = tfm;
    }
}

