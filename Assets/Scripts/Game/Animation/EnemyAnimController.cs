﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class EnemyAnimController : MonoBehaviour
{
    //向き
    Direction _dir;
    [SerializeField, ReadOnly]
    Animator _anim;

    void Awake()
    {
        _dir = gameObject.GetComponentInChildren<Play.Element.DiectionTest>().GetDir();
        _anim = gameObject.GetComponent<Animator>();
        ChangeAnim(_dir);
    }

    public void ChangeAnim(Direction dir)
    {
        if (dir == Direction.Front)
        {
            _anim.SetBool("Front", true);
        }
        else
        {
            _anim.SetBool("Front", false);
        }

        if (dir == Direction.Back)
        {
            _anim.SetBool("Back", true);
        }
        else
        {
            _anim.SetBool("Back", false);
        }

        if (dir == Direction.Left)
        {
            _anim.SetBool("Left", true);
        }
        else
        {
            _anim.SetBool("Left", false);
        }

        if (dir == Direction.Right)
        {
            _anim.SetBool("Right", true);
        }
        else
        {
            _anim.SetBool("Right", false);
        }

    }

}
