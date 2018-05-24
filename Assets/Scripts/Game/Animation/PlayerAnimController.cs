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
            FrontWait,
            LeftWait,
            RightWait,
            BackWait
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
        public virtual void ChangeAnim(Vector3 vec)
        {
            Vector3 direction = vec;
            if (0.4f >= Mathf.Abs(direction.y))
            {
                //左
                if (-0.4f >= direction.x)
                {
                    _currentAnim = ANIMATION_ID.Left;
                    transform.localScale = new Vector3(1, 1, 1);
                    _anim.CrossFade("Side", 0);
                }
                else if (0.4f <= direction.x)
                {
                    _currentAnim = ANIMATION_ID.Right;
                    transform.localScale = new Vector3(-1, 1, 1);
                    _anim.CrossFade("Side", 0);
                }
            }
            else if (0.4f <= direction.y)
            {
                //上
                if (0.4f >= Mathf.Abs(direction.x))
                {
                    _currentAnim = ANIMATION_ID.Front;
                    transform.localScale = new Vector3(1, 1, 1);
                    _anim.CrossFade("Front", 0);
                }
            }
            else if (-0.4f >= direction.y)
            {
                //下
                if (0.4f >= Mathf.Abs(direction.x))
                {
                    _currentAnim = ANIMATION_ID.Back;
                    transform.localScale = new Vector3(1, 1, 1);
                    _anim.CrossFade("Back", 0);
                }
            }

            //移動量0
            if (direction.x == 0 && direction.y == 0)
            {
                switch (_currentAnim)
                {
                    case ANIMATION_ID.Front:

                        _currentAnim = ANIMATION_ID.FrontWait;
                        break;
                    case ANIMATION_ID.Left:

                        _currentAnim = ANIMATION_ID.LeftWait;
                        break;
                    case ANIMATION_ID.Right:

                        _currentAnim = ANIMATION_ID.RightWait;
                        break;
                    case ANIMATION_ID.Back:

                        _currentAnim = ANIMATION_ID.BackWait;
                        break;

                    default:
                        _currentAnim = ANIMATION_ID.BackWait;
                        break;
                }
            }
        }

        public virtual void ChangeAnim(ANIMATION_ID id)
        {
            //現在アニメーションを変更
            _currentAnim = id;

            switch (id)
            {
                case ANIMATION_ID.Right:
                    Debug.Log("未実装、デザイン校を待て");
                    break;

                case ANIMATION_ID.RightWait:
                    Debug.Log("未実装、デザイン校を待て");
                    break;

                case ANIMATION_ID.LeftWait:
                    Debug.Log("未実装、デザイン校を待て");
                    break;

                case ANIMATION_ID.FrontWait:
                    Debug.Log("未実装、デザイン校を待て");
                    break;

                case ANIMATION_ID.BackWait:
                    Debug.Log("未実装、デザイン校を待て");
                    break;

                default:
                    _currentAnim = id;
                    //アニメ切り替え
                    _anim.CrossFade(id.ToString(), 0);
                    break;
            }
        }
    }
}