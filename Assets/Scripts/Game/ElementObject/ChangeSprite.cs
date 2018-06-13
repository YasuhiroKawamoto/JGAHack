using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class ChangeSprite : MonoBehaviour {
    //切り替え画像集
    [SerializeField]
    Sprite[] _objImages;

    [SerializeField,ReadOnly]
    Direction _dir;

    [SerializeField,ReadOnly]
    Direction _tmpDir;


    private void Start()
    {
        _dir = GetComponentInChildren<Play.Element.DiectionTest>().GetDir();
        _tmpDir = _dir;
        ChangeImage(_dir);
    }

    private void Update()
    {
        _dir = GetComponentInChildren<Play.Element.DiectionTest>().GetDir();

        if (_dir != _tmpDir)
        {
            ChangeImage(_dir);
            _tmpDir = _dir;
        }  
    }

    public void ChangeImage(Direction dir)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = _objImages[(int)dir];
    }

}
