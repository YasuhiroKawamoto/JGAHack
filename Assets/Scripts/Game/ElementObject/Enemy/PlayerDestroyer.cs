using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play.Enemy
{

    public class PlayerDestroyer : MonoBehaviour
    {
        [SerializeField]
        GameObject CamMan;


        private void Awake()
        {
            CamMan = GameObject.Find("CameraManager");
        }


        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                //プレイヤー死亡処理
                InGameManager.Instance.StageOver();
                //カメラシェイク
                CamMan.GetComponent<CameraManager>().ShakeCamera();
                //カメラの切り替え
                CamMan.GetComponent<CameraManager>().MainCameraChange();

            }

        }


        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                //プレイヤー死亡処理
                InGameManager.Instance.StageOver();
                //カメラシェイク
                CamMan.GetComponent<CameraManager>().ShakeCamera();
                //カメラの切り替え
                CamMan.GetComponent<CameraManager>().MainCameraChange();

            }

        }
    }
    
}