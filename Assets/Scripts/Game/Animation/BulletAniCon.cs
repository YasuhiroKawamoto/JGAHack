using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAniCon : MonoBehaviour {


    SimpleAnimation _anim;
    // Use this for initialization
 
    private void OnEnable()
    {
        _anim = GetComponent<SimpleAnimation>();
    }

    public void ChangeAnim(Direction dir)
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
