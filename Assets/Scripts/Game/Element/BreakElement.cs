using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;


namespace Play.Element
{
    public class BreakElement : MonoBehaviour
    {
        //壊れる性質

        //復活できるか？
        [SerializeField]
        private bool _canRebirth = false;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            //TODO 接触物判定
            if (collision.gameObject)
            {
                //復活可能なら
                if (_canRebirth)
                {
                    //再生を司るものに情報を送る
                    //Debug.Log("わが魂をうけとれぇ");
                    //オブジェクトを非アクティブにする
                    gameObject.SetActive(false);
                    return;
                }
                //Debug.Log("壊れるぅ");
                Destroy(gameObject);    
            }
        }
    }
}