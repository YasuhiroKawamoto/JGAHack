using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class PlayerAnimController : MonoBehaviour
{



    [SerializeField, ReadOnly]
    SimpleAnimation _anim;

    void Awake()
    {
        //アニメ切り替えスクリプト切り替え
        _anim = gameObject.GetComponent<SimpleAnimation>();
    }

    public void ChangeAnim(Vector3 vec)
    {
       
        Vector3 direction = vec;

        if (0.4f >= Mathf.Abs(direction.y))
        {
            //左
            if (-0.4f >= direction.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                _anim.CrossFade("Side", 0);
            }
            else if (0.4f <= direction.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                _anim.CrossFade("Side", 0);
            }
        }
        else if (0.4f <= direction.y)
        {
            //上
            if (0.4f >= Mathf.Abs(direction.x))
            {
                transform.localScale = new Vector3(1, 1, 1);
                _anim.CrossFade("Front", 0);
            }
            else if (-0.4f >= direction.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                _anim.CrossFade("SideFront", 0);

            }
            else if (0.4f <= direction.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                _anim.CrossFade("SideFront", 0);
            }
        }
        else if (-0.4f >= direction.y)
        {

            //下
            if (0.4f >= Mathf.Abs(direction.x))
            {
                transform.localScale = new Vector3(1, 1, 1);
                _anim.CrossFade("Back", 0);
            }
            else if (-0.4f >= direction.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                _anim.CrossFade("SideBack", 0);
            }
            else if (0.4f <= direction.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                _anim.CrossFade("SideBack", 0);
            }
        }
    }

    //アニメーション終了を判定するコルーチン
    public IEnumerator WaitAnimationEnd(string animatorName)
    {
        bool finish = false;
        while (!finish)
        {
            AnimatorStateInfo nowState = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            if (nowState.IsName(animatorName))
            {
                finish = true;
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
