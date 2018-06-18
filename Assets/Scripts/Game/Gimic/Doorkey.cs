using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play
{
    //ボタンの状態
    public enum STATE
    {
        UP,
        DOWN
    }


    public class Doorkey : SwitchKeyEvent
    {
        //ボタン画像
        [SerializeField]
        Sprite[] _buttonImage;

        //ボタン画像差し替え
        public void ChangeImage(STATE id)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = _buttonImage[(int)id];
        }

    }
}