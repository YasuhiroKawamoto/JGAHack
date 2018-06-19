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
            Left,
            Right,
            Back,
            Wait
        }
        //アニメーションセット
        private SimpleAnimation _anim;
       

        private float _waitTime = 1.0f;

        void Awake()
        {
            //アニメ切り替えスクリプト切り替え
            _anim = gameObject.GetComponent<SimpleAnimation>();
        }

        //アニメーション変更（移動方向で変更）
        public virtual void ChangeAnim(Vector3 vec)
        {
            Vector3 direction = vec;
            if (0.4f >= Mathf.Abs(direction.y))
            {
                //左
                if (-0.4f >= direction.x)
                {
                           
                    _anim.CrossFade("Left", 0);
                }
                else if (0.4f <= direction.x)
                {
                       
                    _anim.CrossFade("Right", 0);
                }
            }
            else if (0.4f <= direction.y)
            {
                //上
                if (0.4f >= Mathf.Abs(direction.x))
                {
                              
                    _anim.CrossFade("Front", 0);
                }
            }
            else if (-0.4f >= direction.y)
            {
                //下
                if (0.4f >= Mathf.Abs(direction.x))
                {
                          
                    _anim.CrossFade("Back", 0);
                }
            }

            //移動量0
            if (direction.x == 0 && direction.y == 0)
            {
                _waitTime = _waitTime -Time.deltaTime;
                if (_waitTime <= 0)
                {
                   
                    _anim.CrossFade("Wait", 0);
                    _waitTime = 1.0f;
                }
            }
            else {

                _waitTime = 1.0f;
            }
        }

        public virtual void ChangeAnim(Direction dir,float time)
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


            if (time >= 1)
            {
               
                _anim.CrossFade("Wait", 0);
            }

        }

        public virtual void ChangeAnim(ANIMATION_ID id)
        {
            
            //アニメ切り替え
            _anim.CrossFade(id.ToString(), 0);
        }
    }
}