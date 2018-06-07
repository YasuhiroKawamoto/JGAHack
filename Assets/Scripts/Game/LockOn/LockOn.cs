using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Play.Element;

namespace Play.LockOn
{
    //ロックオン用スクリプト
    public class LockOn : MonoBehaviour
    {
        //ロックオンリスト
        [SerializeField]
        public List<ElementObject> _lockOnList = null;
        private int _targetNum = 0;

        void Awake()
        {
            _lockOnList = GetLockOnList();
            _targetNum = GetNearObjOnList();
        }

        //ステージ内のすべての「Element」タグオブジェをリストに収納(シーン開始時に呼ぶ)
        void CreateLockonList()
        {
            if (_lockOnList == null)
            {
                // リスト作成
                _lockOnList = new List<ElementObject>();

                //リストのクリア
                _lockOnList.Clear();

                //指定したタグのオブジェクトを全て引っ張ってくる
                foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Element"))
                {
                    var element = obj.GetComponent<ElementObject>();
                    if (element)
                    {
                        //ロックオンリストに追加
                        _lockOnList.Add(element);
                    }
                }
            }

            //ロックオンリストの内容をtransform.position.xでソート(昇順)
            _lockOnList.Sort(ComparePosXAsc);
        }

        /// <summary>
        /// ターゲットできるオブジェクトが１つでもあるか？
        /// </summary>
        /// <returns></returns>
        public bool CheckOnScreenAll()
        {
            foreach (var obj in _lockOnList)
            {
                if (obj == null)
                {
                    continue;
                }

                if (CheckOnScreen(obj.transform.position))
                {
                    if (obj.Stats == ElementObject.ElementStates.Remember)
                    {
                        continue;
                    }
                    return true;
                }
            }
            return false;
        }

        //カメラ範囲内に映ってるか？（対象の位置を参照）
        public bool CheckOnScreen(Vector3 _pos)
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
        public List<ElementObject> GetLockOnList()
        {
            //TODO　ロックオンリスト作成
            CreateLockonList();
            //ロックオンリストを返す
            return _lockOnList;
        }

        //リスト内の要素を比較してソート（昇順）
        public static int ComparePosXAsc(ElementObject a, ElementObject b)
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


        //リスト内の要素をDebug.Logに表示する(デバッグ用)
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
            Debug.Log(log + "要素数" + list.Count);
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
            foreach (var obs in _lockOnList)
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

        /// <summary>
        /// ターゲットの取得
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>        
        public ElementObject GetTarget(int num)
        {
            _targetNum += num;

            if (_lockOnList.Count <= _targetNum)
            {
                _targetNum = 0;
            }
            else if (_targetNum < 0)
            {
                _targetNum = _lockOnList.Count - 1;
            }

            var obj = _lockOnList[_targetNum];
            //オブジェクトが「missing」（破壊済み）の場合
            if (obj == null)
            {
                //消すオブジェ
                //該当オブジェクトを排斥対象に
                var exclusionObj = obj;
                //リストから排斥
                _lockOnList.Remove(exclusionObj);
                //再起呼び出し
                obj = GetTarget(num);
            }
            //オブジェクトのチェックが外れている（再生待機）時
            else if (obj.gameObject.activeInHierarchy == false)
            {
                //再起呼び出し
                obj = GetTarget(num);
            }

            //カメラ内に入っていなければ飛ばし
            if (CheckOnScreen(obj.transform.position) == false)
            {
                //再起呼び出し
                obj = GetTarget(num);
            }

            // 思い出し中はタゲしない
            if (obj.Stats == ElementObject.ElementStates.Remember)
            {
                //再起呼び出し
                obj = GetTarget(num);
            }

            return obj;
        }
    }
}