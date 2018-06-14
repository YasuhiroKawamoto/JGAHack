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
        //Consoleアイコン
        [SerializeField, ReadOnly]
        private Sprite[] _iconImages;
        //操作アイコン集（コントローラー）
        [SerializeField, ReadOnly]
        Sprite[] _controllerImages;
        //操作アイコン集（キーボード）
        [SerializeField, ReadOnly]
        Sprite[] _keybordImages;
        //エフェクト置き場（親設定していない場合の置き場）
        [SerializeField]
        private GameObject _effectPlace;
            
        void Awake()
        {
            //リソースからエフェクト群を取得
            _effects = Resources.LoadAll<GameObject>("Effects");
            //リソースからアイコンゲット
            _iconImages = Resources.LoadAll<Sprite>("Icons/ConsoleIcon");
            //リソースから操作アイコン（コントローラー）ゲット
            _controllerImages = Resources.LoadAll<Sprite>("Icons/ControllerIcon");
            //リソースから操作アイコン（キーボード）ゲット
            _keybordImages = Resources.LoadAll<Sprite>("Icons/KeyboardIcon");
        }

        

        //エフェクト生成（作るだけ）
        public virtual GameObject CreateEffect(EffectID name)
        {
            //空オブジェ作成
            GameObject effectObj;
            //エフェクト番号に変換
            int effectNum = (int)name;
            //エフェクトをリストから複製
            effectObj = Instantiate(_effects[effectNum]);
            //親の設定
            SetParent(effectObj, name);
            //自己破壊セット
            SetDestroy(effectObj);

            return effectObj;
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
            //親の設定
            SetParent(effectObj, name);
            //自己破壊セット
            SetDestroy(effectObj,time);

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
            effectObj.SetActive(false);
            //親の設定
            SetParent(effectObj, name);
            //エフェクトの位置設定
            effectObj.transform.position = pos;
            //自己破壊セット
            SetDestroy(effectObj);

            effectObj.SetActive(true);

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
            //親の設定
            SetParent(effectObj, name,parent);
            //エフェクトの位置設定
            effectObj.transform.position = parent.transform.position;
            //自己破壊セット
            SetDestroy(effectObj);

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
            //親の設定
            SetParent(effectObj, name, parent);
            //エフェクトの位置設定（オフセット分ずらす）
            effectObj.transform.position = parent.transform.position + offSet;
            //自動破壊セット
            SetDestroy(effectObj);

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

            effectObj.SetActive(false);
            //親の設定
            SetParent(effectObj, name);
            //エフェクトの位置設定
            effectObj.transform.position = pos;
            //自動破壊セット
            SetDestroy(effectObj,time);

            effectObj.SetActive(true);

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
            //親の設定
            SetParent(effectObj, name, parent);
            //エフェクトの位置設定
            effectObj.transform.position = parent.transform.position;
            //自動破壊セット
            SetDestroy(effectObj,time);

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
            //親の設定
            SetParent(effectObj, name, parent);
            //オフセット分位置をずらす
            effectObj.transform.position = parent.transform.position + offSet;
            //自動破壊セット
            SetDestroy(effectObj);

            return effectObj;
        }


        //親の設定（子供になるオブジェクト,セットするエフェクトID）
        public virtual void SetParent(GameObject obj, EffectID name)
        {
            //エフェクトがRectTransform仕様でなければ
            if (name.Rect() == false)
            {
                //エフェクト置き場に置く
                if (_effectPlace)
                {
                    obj.transform.SetParent(_effectPlace.transform);
                    //ヒエラルキーの一番上に置く
                    obj.transform.SetAsFirstSibling();
                }
            }
            else
            {
                //UIRootに置く
                obj.transform.SetParent(InGameManager.Instance.UIRoot.gameObject.transform);
                //ヒエラルキーの一番上に置く（最背面描画）
                obj.transform.SetAsFirstSibling();
            }
        }

        //親の設定（子供になるオブジェクト,セットするエフェクトID,親になるオブジェクト）
        public virtual void SetParent(GameObject obj, EffectID name, GameObject parent)
        {
            //エフェクトがRectTransform仕様でなければ
            if (name.Rect() == false)
            {
                //指定した親の子供にする
                obj.transform.SetParent(parent.transform);
                //ヒエラルキーの一番上に置く
                obj.transform.SetAsFirstSibling();
            }
            else
            {
                //UIRootに置く
                obj.transform.SetParent(InGameManager.Instance.UIRoot.gameObject.transform);
                //ヒエラルキーの一番上に置く（最背面描画）
                obj.transform.SetAsFirstSibling();
            }
        }

        //自動消滅セット
        public virtual void SetDestroy(GameObject obj)
        {
            //エフェクトの自動消滅時間がセットされていれば
            if (obj.GetComponent<EffectState>().GetIsActTime() != 0)
            {
                //指定時間後に破壊
                StartCoroutine(DestroyEffect(obj.GetComponent<EffectState>().GetIsActTime(), obj));
            }
        }

        //自動消滅セット(時間指定あり)
        public virtual void SetDestroy(GameObject obj,float destroyTime)
        {          
             //指定時間後に破壊
             StartCoroutine(DestroyEffect(destroyTime, obj));        
        }

        //時間経過を待って削除
        private IEnumerator DestroyEffect(float waitTime, GameObject effectObj)
        {
            yield return new WaitForSeconds(waitTime);
            Destroy(effectObj);
        }

        //Console関連画像取得
        public Sprite[] GetConsoleIcons()
        {
            return _iconImages;
        }

        //Console関連画像取得
        public Sprite[] GetControllerIcons()
        {
            return _controllerImages;
        }

        //Console関連画像取得
        public Sprite[] GetKeyboardIcons()
        {
            return _keybordImages;
        }
    }
}