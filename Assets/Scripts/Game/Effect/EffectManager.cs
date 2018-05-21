using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Extensions;

namespace Play.Effect
{

    public enum EffectName
    {
        Cicle,
        Destory,
        Effect,
        Ling,
        Ling2,
        LookOn,
        Numparent,
        obj,
        Recovery,
        Respown,
        Wave,
    }

    public class EffectManager : MonoBehaviour
    {

        //エフェクト
        [SerializeField]
        private GameObject[] _effects;
        //エフェクトの名前
        [SerializeField, ReadOnly]
        private List<string> _effectNames;
        //エフェクト置き場（親設定していない場合の置き場）
        [SerializeField]
        private GameObject _effectPlace;
        //呼び出すエフェクト番号
        [SerializeField]
        private EffectName _effectNum;
        //エフェクト作成位置
        [SerializeField]
        private Vector3 _createPos;
        //親オブジェ（テスト用）
        [SerializeField]
        GameObject _parentGameObj;


        // Use this for initialization
        void Start()
        {
            //リソースからエフェクト群を取得
            _effects = Resources.LoadAll<GameObject>("Effects");

            //for (int i = 0; i < _effects.Length; i++)
            //{
            //    _effectNames.Add(_effects[i].gameObject.name);
            //}

            //ShowListContentsInTheDebugLog(_effectNames);
        }

        // Update is called once per frame
        void Update()
        {
            //動作テスト（指定位置）
            if (Input.GetMouseButtonDown(1))
            {
                CreateEffect(_effectNum, _createPos,2);
            }

            //動作テスト（親指定）
            if (Input.GetMouseButtonDown(0))
            {
                CreateEffect(_effectNum, _parentGameObj,2);
            }

        }

        //エフェクト生成（作るだけ）
        public virtual void CreateEffect(EffectName name)
        {
            //空オブジェ作成
            GameObject effectobj;
            //エフェクト番号に変換
            int effectNum = (int)name;
            //エフェクトをリストから複製
            effectobj = Instantiate(_effects[effectNum]);
            //エフェクトがRectTransform仕様でなければ
            if (!_effects[effectNum].GetComponent<EffectState>().GetIsRect())
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
            
        }

        //エフェクト生成（作るだけ,破壊時間指定）
        public virtual void CreateEffect(EffectName name,float time)
        {
            //空オブジェ作成
            GameObject effectobj;
            //エフェクト番号に変換
            int effectNum = (int)name;
            //エフェクトをリストから複製
            effectobj = Instantiate(_effects[effectNum]);
            //エフェクトがRectTransform仕様でなければ
            if (!_effects[effectNum].GetComponent<EffectState>().GetIsRect())
            {
                //エフェクト置き場に置く
                if (_effectPlace)
                {
                    effectobj.transform.parent = _effectPlace.transform;
                }
            }

            //指定時間後に破壊
            StartCoroutine(DestroyEffect(time, effectobj));

        }


        //エフェクト生成（作るだけ,位置指定）
        public virtual void CreateEffect(EffectName name, Vector3 pos)
        {
            //空オブジェ作成
            GameObject effectobj;
            //エフェクト番号に変換
            int effectNum = (int)name;
            //エフェクトをリストから複製
            effectobj = Instantiate(_effects[effectNum]);
            //エフェクトがRectTransform仕様でなければ
            if (!_effects[effectNum].GetComponent<EffectState>().GetIsRect())
            {
                //エフェクト置き場に置く
                if (_effectPlace)
                {
                    effectobj.transform.parent = _effectPlace.transform;
                }
            }
            //エフェクトの位置設定
            effectobj.transform.position = pos;
        }

        //エフェクト生成（親指定）
        public virtual void CreateEffect(EffectName name, GameObject parent)
        {
            //空オブジェ作成
            GameObject effectobj;
            //エフェクト番号に変換
            int effectNum = (int)name;
            //エフェクトをリストから複製
            effectobj = Instantiate(_effects[effectNum]);
            //指定した親の子供にする
            effectobj.transform.parent = parent.transform;
            //エフェクトの位置設定
            effectobj.transform.position = parent.transform.position;
        }

        //エフェクト生成（親指定,オフセット指定）
        public virtual void CreateEffect(EffectName name, GameObject parent, Vector3 offSet)
        {
            //空オブジェ作成
            GameObject effectobj;
            //エフェクト番号に変換
            int effectNum = (int)name;
            //エフェクトをリストから複製
            effectobj = Instantiate(_effects[effectNum]);
            //指定した親の子供にする
            effectobj.transform.parent = parent.transform;
            //エフェクトの位置設定（オフセット分ずらす）
            effectobj.transform.position = parent.transform.position + offSet;
        }



        //エフェクト生成(指定位置)
        public virtual void CreateEffect(EffectName name, Vector3 pos, float time)
        {
            //空オブジェ作成
            GameObject effectobj;
            //エフェクト番号に変換
            int effectNum = (int)name;
            //エフェクトをリストから複製
            effectobj = Instantiate(_effects[effectNum]);
            //エフェクト置き場があれば
            if (_effectPlace)
            {
                //エフェクト置き場の子供に指定
                effectobj.transform.parent = _effectPlace.transform;
            }
            effectobj.transform.position = pos;
            //指定時間後に破壊
            StartCoroutine(DestroyEffect(time, effectobj));

        }

        //エフェクト生成（親指定）
        public virtual void CreateEffect(EffectName name, GameObject parent, float time)
        {
            //空オブジェ作成
            GameObject effectobj;
            //エフェクト番号に変換
            int effectNum = (int)name;
            //エフェクトをリストから複製
            effectobj = Instantiate(_effects[effectNum]);
            //指定した親の子供にする
            effectobj.transform.parent = parent.transform;
            //エフェクトの位置設定
            effectobj.transform.position = parent.transform.position;
            //指定時間後に破壊
            StartCoroutine(DestroyEffect(time, effectobj));
        }




        //エフェクト生成（対象エフェクト,親指定,オフセット指定,生存時間設定）
        public virtual void CreateEffect(EffectName name, GameObject parent, Vector3 offSet, float time)
        {
            //空オブジェ作成
            GameObject effectobj;
            //エフェクト番号に変換
            int effectNum = (int)name;
            //エフェクトをリストから複製
            effectobj = Instantiate(_effects[effectNum]);
            //指定した親の子供にする
            effectobj.transform.parent = parent.transform;
            //オフセット分位置をずらす
            effectobj.transform.position = parent.transform.position + offSet;
            //指定時間後に破壊
            StartCoroutine(DestroyEffect(time, effectobj));
        }



        //時間経過を待って削除
        private IEnumerator DestroyEffect(float waitTime, GameObject effectObj)
        {
            yield return new WaitForSeconds(waitTime);
            Destroy(effectObj);
        }




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