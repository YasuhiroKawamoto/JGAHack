using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Extensions;

namespace Play
{
    public class EffectManager : Util.SingletonMonoBehaviour<EffectManager>
    {
        //エフェクト
        [SerializeField, ReadOnly]
        private GameObject[] _effects;
        
        //エフェクト置き場（親設定していない場合の置き場）
        [SerializeField]
        private GameObject _effectPlace;
            
       
        void Awake()
        {
            //リソースからエフェクト群を取得
            _effects = Resources.LoadAll<GameObject>("Effects");
        }

        private void Start()
        {
            //エフェクトの名前
            // List<string> _effectNames = new List<string>();
            //for (int i = 0; i < _effects.Length; i++)
            //{
            //    _effectNames.Add(_effects[i].gameObject.name);
            //}
            //ShowListContentsInTheDebugLog(_effectNames);
        }


        //エフェクト生成（作るだけ）
        public virtual GameObject CreateEffect(EffectID name)
        {
            //空オブジェ作成
            GameObject effectobj;
            //エフェクト番号に変換
            int effectNum = (int)name;
            //エフェクトをリストから複製
            effectobj = Instantiate(_effects[effectNum]);
            //エフェクトがRectTransform仕様でなければ
            if (EffectIDEx.Rect(name) == false)
            {
                //エフェクト置き場に置く
                if (_effectPlace)
                {
                    effectobj.transform.parent = _effectPlace.transform;
                }
            }
            //エフェクトの自動消滅時間がセットされていれば
            if (_effects[effectNum].GetComponent<EffectState>().GetIsActTime() != 0)
            {
                //指定時間後に破壊
                StartCoroutine(DestroyEffect(_effects[effectNum].GetComponent<EffectState>().GetIsActTime(), effectobj));
            }

            return effectobj;
        }

        //エフェクト生成（作るだけ,破壊時間指定）
        public virtual GameObject CreateEffect(EffectID name, float time)
        {
            //空オブジェ作成
            GameObject effectObj;
            //エフェクト番号に変換
            int effectNum = (int)name;
            //エフェクトをリストから複製
            effectObj = Instantiate(_effects[effectNum]);
            //エフェクトがRectTransform仕様でなければ
            if (EffectIDEx.Rect(name) == false)
            {
                //エフェクト置き場に置く
                if (_effectPlace)
                {
                    effectObj.transform.parent = _effectPlace.transform;
                }
            }
            //指定時間後に破壊
            StartCoroutine(DestroyEffect(time, effectObj));

            return effectObj;
        }

        //エフェクト生成（作るだけ,位置指定）
        public virtual GameObject CreateEffect(EffectID name, Vector3 pos)
        {
            //空オブジェ作成
            GameObject effectObj;
            //エフェクト番号に変換
            int effectNum = (int)name;
            //エフェクトをリストから複製
            effectObj = Instantiate(_effects[effectNum]);
            //エフェクトがRectTransform仕様でなければ
            if (EffectIDEx.Rect(name) == false)
            {
                //エフェクト置き場に置く
                if (_effectPlace)
                {
                    effectObj.transform.parent = _effectPlace.transform;
                }
            }
            //エフェクトの位置設定
            effectObj.transform.position = pos;
            //エフェクトの自動消滅時間がセットされていれば
            if (_effects[effectNum].GetComponent<EffectState>().GetIsActTime() != 0)
            {
                //指定時間後に破壊
                StartCoroutine(DestroyEffect(_effects[effectNum].GetComponent<EffectState>().GetIsActTime(), effectObj));
            }

            return effectObj;
        }

        //エフェクト生成（親指定）
        public virtual GameObject CreateEffect(EffectID name, GameObject parent)
        {
            //空オブジェ作成
            GameObject effectObj;
            //エフェクト番号に変換
            int effectNum = (int)name;
            //エフェクトをリストから複製
            effectObj = Instantiate(_effects[effectNum]);
            //指定した親の子供にする
            effectObj.transform.parent = parent.transform;
            //エフェクトの位置設定
            effectObj.transform.position = parent.transform.position;

            return effectObj;
        }

        //エフェクト生成（親指定,オフセット指定）
        public virtual GameObject CreateEffect(EffectID name, GameObject parent, Vector3 offSet)
        {
            //空オブジェ作成
            GameObject effectObj;
            //エフェクト番号に変換
            int effectNum = (int)name;
            //エフェクトをリストから複製
            effectObj = Instantiate(_effects[effectNum]);
            //指定した親の子供にする
            effectObj.transform.parent = parent.transform;
            //エフェクトの位置設定（オフセット分ずらす）
            effectObj.transform.position = parent.transform.position + offSet;
            //エフェクトの自動消滅時間がセットされていれば
            if (_effects[effectNum].GetComponent<EffectState>().GetIsActTime() != 0)
            {
                //指定時間後に破壊
                StartCoroutine(DestroyEffect(_effects[effectNum].GetComponent<EffectState>().GetIsActTime(), effectObj));
            }

            return effectObj;
        }

        //エフェクト生成(指定位置)
        public virtual GameObject CreateEffect(EffectID name, Vector3 pos, float time)
        {
            //空オブジェ作成
            GameObject effectObj;
            //エフェクト番号に変換
            int effectNum = (int)name;
            //エフェクトをリストから複製
            effectObj = Instantiate(_effects[effectNum]);
            //エフェクト置き場があれば
            if (_effectPlace)
            {
                //エフェクト置き場の子供に指定
                effectObj.transform.parent = _effectPlace.transform;
            }
            //エフェクトの位置設定
            effectObj.transform.position = pos;
            //指定時間後に破壊
            StartCoroutine(DestroyEffect(time, effectObj));

            return effectObj;
        }

        //エフェクト生成（親指定）
        public virtual GameObject CreateEffect(EffectID name, GameObject parent, float time)
        {
            //空オブジェ作成
            GameObject effectObj;
            //エフェクト番号に変換
            int effectNum = (int)name;
            //エフェクトをリストから複製
            effectObj = Instantiate(_effects[effectNum]);
            //指定した親の子供にする
            effectObj.transform.parent = parent.transform;
            //エフェクトの位置設定
            effectObj.transform.position = parent.transform.position;
            //指定時間後に破壊
            StartCoroutine(DestroyEffect(time, effectObj));

            return effectObj;
        }

        //エフェクト生成（対象エフェクト,親指定,オフセット指定,生存時間設定）
        public virtual GameObject CreateEffect(EffectID name, GameObject parent, Vector3 offSet, float time)
        {
            //空オブジェ作成
            GameObject effectObj;
            //エフェクト番号に変換
            int effectNum = (int)name;
            //エフェクトをリストから複製
            effectObj = Instantiate(_effects[effectNum]);
            //指定した親の子供にする
            effectObj.transform.parent = parent.transform;
            //オフセット分位置をずらす
            effectObj.transform.position = parent.transform.position + offSet;
            //指定時間後に破壊
            StartCoroutine(DestroyEffect(time, effectObj));

            return effectObj;
        }

        //時間経過を待って削除
        private IEnumerator DestroyEffect(float waitTime, GameObject effectObj)
        {
            yield return new WaitForSeconds(waitTime);
            Destroy(effectObj);
        }

        //テスト用リスト表示
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
    }
}