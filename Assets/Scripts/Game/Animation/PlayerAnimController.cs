using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;


namespace Play
{
    public class PlayerAnimController : MonoBehaviour
    {
        //アニメーションID
        public enum ANIMATION_ID
        {
            Front,
            FrontWait,
            Left,
            LeftWait,
            Right,
            RightWait,
            Back,
            BackWait,
            LongWait
        }
        //アニメーションセット
        private SimpleAnimation _anim;
       

        private float _waitTime = 1.5f;

        void Awake()
        {
            //アニメ切り替えスクリプト切り替え
            _anim = gameObject.GetComponent<SimpleAnimation>();
        }

        //アニメーション変更（移動方向で変更）  
        public virtual void ChangeAnim(Direction dir,float time)
        {
            
            //待機時間が1秒を超える
            if (time >= _waitTime)
            {
                _anim.CrossFade("LongWait", 0);
            }
            //１秒未満かつ0.3秒以上
            else if (time >= 0.2f)
            {
                switch (dir)
                {
                    case Direction.Front:
                        _anim.CrossFade("FrontWait", 0);
                        break;

                    case Direction.Left:
                        _anim.CrossFade("LeftWait", 0);
                        break;

                    case Direction.Right:
                        _anim.CrossFade("RightWait", 0);
                        break;

                    case Direction.Back:
                        _anim.CrossFade("BackWait", 0);
                        break;
                }
            }
            //それ未満
            else
            {
                switch (dir)
                {
                    case Direction.Front:
                        _anim.CrossFade("Front", 0);
                        break;

                    case Direction.Left:
                        _anim.CrossFade("Left", 0);
                        break;

                    case Direction.Right:
                        _anim.CrossFade("Right", 0);
                        break;

                    case Direction.Back:
                        _anim.CrossFade("Back", 0);
                        break;
                }
            }
        }

        public virtual void ChangeAnim(ANIMATION_ID id)
        {       
            //アニメ切り替え
            _anim.CrossFade(id.ToString(), 0);
        }
    }
}