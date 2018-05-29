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
   //可変式か？
    [SerializeField]
    private bool _isVariable = false;

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
            if (!_isVariable)
            {
                //ワールド座標に変換（オフセット込み）
                _rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, _targetTransform.position + _offset);
            }
            else
            {
                //カメラの中心座標をもとにUIオフセットｙ反転
                if (_targetTransform.position.y > Camera.main.transform.position.y)
                {
                    _offset.y = -Mathf.Abs(_offset.y);
                }
                else
                {
                    _offset.y = Mathf.Abs(_offset.y);
                }

                //ワールド座標に変換（オフセット込み）
                _rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, _targetTransform.position + _offset);

            }        
        }
    }

    //ターゲットセット
    public void SetTransform(Transform tfm)
    {
        _targetTransform = tfm;
    }
}

