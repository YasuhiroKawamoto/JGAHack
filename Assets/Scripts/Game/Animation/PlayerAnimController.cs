using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class PlayerAnimController : MonoBehaviour
{



    [SerializeField, ReadOnly]
    Animator _anim;

    void Awake()
    {

        _anim = gameObject.GetComponent<Animator>();

    }

    public void ChangeAnim(Vector3 vec)
    {
        Vector3 direction = vec;


        _anim.SetBool("Side", false);
        _anim.SetBool("Front", false);
        _anim.SetBool("Back", false);
        _anim.SetBool("SideFront", false);
        _anim.SetBool("SideBack", false);

        if (0.4f >= Mathf.Abs(direction.y))
        {

            //左
            if (-0.4f >= direction.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                _anim.SetBool("Side", true);
            }
            else if (0.4f <= direction.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                _anim.SetBool("Side", true);
            }


        }
        else if (0.4f <= direction.y)
        {


            //上
            if (0.4f >= Mathf.Abs(direction.x))
            {
                transform.localScale = new Vector3(1, 1, 1);
                _anim.SetBool("Front", true);
            }
            else if (-0.4f >= direction.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                _anim.SetBool("SideFront", true);

            }
            else if (0.4f <= direction.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                _anim.SetBool("SideFront", true);
            }


        }
        else if (-0.4f >= direction.y)
        {



            //下
            if (0.4f >= Mathf.Abs(direction.x))
            {
                transform.localScale = new Vector3(1, 1, 1);
                _anim.SetBool("Back", true);
            }
            else if (-0.4f >= direction.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                _anim.SetBool("SideBack", true);
            }
            else if (0.4f <= direction.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                _anim.SetBool("SideBack", true);
            }

        }





    }


}
