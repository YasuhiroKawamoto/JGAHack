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

    private void Start()
    {
        _dir = GetComponentInChildren<Play.Element.DiectionTest>().GetDir();
        ChangeImage(_dir);
    }


    public void ChangeImage(Direction dir)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = _objImages[(int)dir];
    }

}
