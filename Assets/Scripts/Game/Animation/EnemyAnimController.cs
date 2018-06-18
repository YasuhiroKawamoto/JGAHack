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
        //現在のアニメーション
        [SerializeField, ReadOnly]
        private ANIMATION_ID _currentAnim;
 

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
                    _currentAnim = ANIMATION_ID.Front;
                    _anim.CrossFade("Front", 0);
                    break;

                case Direction.Left:
                    _currentAnim = ANIMATION_ID.Left;
                    _anim.CrossFade("Left", 0);
                    break;

                case Direction.Right:
                    _currentAnim = ANIMATION_ID.Right;
                    _anim.CrossFade("Right", 0);
                    break;

                case Direction.Back:
                    _currentAnim = ANIMATION_ID.Back;
                    _anim.CrossFade("Back", 0);
                    break;
            }
            
        }

        public virtual void ChangeAnim(ANIMATION_ID id)
        {
            //現在アニメーションを変更
            _currentAnim = id;
            //アニメ切り替え
            _anim.CrossFade(id.ToString(), 0);
        }
    }
}
