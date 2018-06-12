using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play
{
    //�{�^���̏��
    public enum STATE
    {
        UP,
        DOWN
    }


    public class Doorkey : SwitchKeyEvent
    {
        //�{�^���摜
        [SerializeField]
        Sprite[] _buttonImage;

        //�{�^���摜�����ւ�
        public void ChangeImage(STATE id)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = _buttonImage[(int)id];
        }

    }
}