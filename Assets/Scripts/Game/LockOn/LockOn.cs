using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Play.LockOn
{
    //ロックオン用スクリプト
    public class LockOn
    {
        //ロックオンリスト
        [SerializeField]
        public List<GameObject> _lockOnList = new List<GameObject>();

        // Use this for initialization
        void Start()
        {
            //カメラに写ってるオブジェクト取得
            GetTargetOnScreen();
        }

        // Update is called once per frame
        void Update()
        {

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

            //ロックオンリストの内容をtransform.position.xでソート(昇順)
            _lockOnList.Sort(ComparePosXAsc);
            ////TODO リスト内オブジェクトの表示
            //ShowListContentsInTheDebugLog(_lockOnList);
            ////TODO 一番近いリスト内オブジェクトのリスト番号を取得表示
            //Debug.Log(GetNearObjOnList());

        }

        //リスト内にmissingがあれば排斥
        void ListCheck()
        {
            //消すオブジェ
            GameObject exclusionObj = null;
            //リスト内のチェック
            foreach (GameObject obj in _lockOnList)
            {
                //missing or null だった場合
                if (!obj)
                {
                    //該当オブジェクトを排斥対象に
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
        private bool CheckOnScreen(Vector3 _pos)
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

        //リスト内の要素を比較してソート（昇順）
        public static int ComparePosXAsc(GameObject a, GameObject b)
        {
            // nullチェック  
            if (a == null)
            {
                if (b == null)
                {
                    return 0;
                }
                return -1;
            }
            else
            {
                if (b == null)
                {
                    return 1;
                }

                // aとbの比較 （ポジションのXで比較） 
                return a.transform.position.x.CompareTo(b.transform.position.x);
            }
        }


        //リスト内の要素をDebug.Logに表示する
        public void ShowListContentsInTheDebugLog<T>(List<T> list)
        {
            string log = "";

            foreach (var content in list.Select((val, idx) => new { val, idx }))
            {
                if (content.idx == list.Count - 1)
                    log += content.val.ToString();
                else
                    log += content.val.ToString() + ", ";
            }

            Debug.Log(log);
        }





        //指定されたオブジェクトに最も近いオブジェクトをリストから取得しその要素番号を返す。
        public int GetNearObjOnList()
        {
            //TODO　Playerセット（テスト）
            GameObject nowObj = GameObject.Find("Player");
            //距離用一時変数
            float tmpDis = 0;
            //最も近いオブジェクトの距離      
            float nearDis = 0;
            //リストカウント（何番目か）
            int count = 0;
            //一番近いオブジェクトのリスト内番号
            int nearObjNum = 0;

            //リスト内のオブジェクトをプレイヤーとの距離で比較
            foreach (GameObject obs in _lockOnList)
            {
                //自身と取得したオブジェクトの距離を取得]
                if (obs.transform.parent != null)
                {
                    tmpDis = Vector3.Distance(obs.transform.parent.position, nowObj.transform.parent.position);
                }
                else
                {
                    tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.parent.position);
                }
               
                  

                //オブジェクトの距離が近いか、距離0であればオブジェクトを取得
                //一時変数に距離を格納
                if (nearDis == 0 || nearDis > tmpDis)
                {
                    //比較用一の更新
                    nearDis = tmpDis;
                    //現状一番近いオブジェクトのリスト番号を記憶
                    nearObjNum = count;
                }
                //カウントアップ
                count++;

            }
            //最も近かったオブジェクトのリスト内番号を返す
            return nearObjNum;
        }


    }




}