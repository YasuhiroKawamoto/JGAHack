using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace Play.LockOn
{
    //ロックオン用スクリプト

    public class LockOn : MonoBehaviour {

        //ロックオンリスト
        [SerializeField]
        public List<GameObject> _lockOnList = new List<GameObject>();


        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

            //右キリックでカメラに映るオブジェクトを取得
            if (Input.GetMouseButtonDown(1))
            {
                GetTargetOnScreen();

            }

        }

        //画面内のTarget対象を取得
        void GetTargetOnScreen()
        {
            //リストのクリア
            _lockOnList.Clear();
            //指定したtypeのオブジェクトを全て引っ張ってくる（今は問答無用にゲームオブジェクト）
            foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
            {
                // シーン上に存在するオブジェクトならば処理.
                if (obj.activeInHierarchy)
                {
                    //カメラ範囲内に映っていた場合に処理
                    if (CheckOnScreen(obj.transform.position))
                    {
                        //ロックオンリストに追加
                        _lockOnList.Add(obj);
                    }

                }
            }

        }

        //カメラ範囲内に映ってるか？（対象の位置を参照）
        bool CheckOnScreen(Vector3 _pos)
        {
            //メインカメラ範囲に対しての対象の座標を参照
            Vector3 view_pos = Camera.main.WorldToViewportPoint(_pos);
            if (view_pos.x < -0.0f ||
               view_pos.x > 1.0f ||
               view_pos.y < -0.0f ||
               view_pos.y > 1.0f)
            {
                // 範囲外 
                return false;
            }
            // 範囲内 
            return true;
        }

        //ロックオンリストの取得
        public List<GameObject> GetLockOnList()
        {
            return _lockOnList;
        }

    }
}