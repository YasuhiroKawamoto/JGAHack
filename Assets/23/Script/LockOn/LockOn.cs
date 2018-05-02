using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace Play.LockOn
{
    //ロックオン用スクリプト

    public class LockOn : MonoBehaviour
    {

        //ロックオンリスト
        [SerializeField]
        public List<GameObject> _lockOnList = new List<GameObject>();


        // Use this for initialization
        void Start() {
            //カメラに写ってるオブジェクト取得
            GetTargetOnScreen();
        }

        // Update is called once per frame
        void Update() {

            ////右キリックでカメラに映るオブジェクトを取得
            //if (Input.GetMouseButtonDown(1))
            //{
            //    GetTargetOnScreen();

            //}

            //ListCheck();

        }

        //画面内のTarget対象を取得
        void GetTargetOnScreen()
        {
            //リストのクリア
            _lockOnList.Clear();
            //指定したタグのオブジェクトを全て引っ張ってくる
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Element"))
            {
                
                    //カメラ範囲内に映っていた場合に処理
                    if (CheckOnScreen(obj.transform.position))
                    {
                        //ロックオンリストに追加
                        _lockOnList.Add(obj);
                    }

                
            }
        }


        //リスト内にmissingがあれば排斥
        void ListCheck()
        {
            //消すオブジェ
            GameObject　exclusionObj = null;

            //リスト内のチェック
            foreach (GameObject obj in _lockOnList)
            {
                //missing or null だった場合
                if (!obj)
                {
                    exclusionObj = obj;
                   
                }

            }
            //排斥対象があれば
            if (exclusionObj)
            {
                //リストから排斥
                _lockOnList.Remove(exclusionObj);
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
            GetTargetOnScreen();

            return _lockOnList;
        }

    }
}