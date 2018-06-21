using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;



namespace Play
{
    public class EnemyAnimController : MonoBehaviour
    {
        //アニメーションID
        public enum ANIMATION_ID
        {
            Front,
            Left,
            Right,
            Back,
        }
        //アニメーションセット
        private SimpleAnimation _anim;
        
 

        void Awake()
        {
            //アニメ切り替えスクリプト切り替え
            _anim = gameObject.GetComponent<SimpleAnimation>();
        }

       
        //アニメーション変更（移動方向で変更）
        public virtual void ChangeAnim(Direction dir)
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

        public virtual void ChangeAnim(ANIMATION_ID id)
        {
           
            //アニメ切り替え
            _anim.CrossFade(id.ToString(), 0);
        }
    }
}
