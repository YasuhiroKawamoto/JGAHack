using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Play.Element;
using Extensions;
using Play.LockOn;
using Util.Sound;

using System.Reflection;

namespace Play
{
    // 要素オブジェクトの選択と要素の移動用クラス
    public class ElementSelector : MonoBehaviour
    {
        enum TargetChoice
        {
            None,
            Next,
            Front
        }

        // ターゲットしているオブジェクト
        [SerializeField, ReadOnly]
        protected ElementObject _targetObject = null;

        // ターゲットしているオブジェクト
        [SerializeField, ReadOnly]
        private GameObject _console = null;

        // 選択している要素コンテナ
        [SerializeField, ReadOnly]
        private ElementContainer _container = null;

        // ロックオン
        private LockOn.LockOn _lockOn = null;


        private GameObject _dataPanel;

        public LockOn.LockOn LockOnObj
        {
            get { return _lockOn; }
        }

        void Start()
        {
            // コンテナ取得
            _container = GetComponent<ElementContainer>();

            // ロックオン関連の初期化
            _lockOn = this.gameObject.AddComponent<LockOn.LockOn>();


            if (!_dataPanel)
            {
                _dataPanel = GameObject.Find("DataPanel");
            }
        }

        void Update()
        {
            if (!InGameManager.IsInstance()) return;

            if (InGameManager.Instance.GameState != InGameManager.State.Play) return;

            // 選択
            var con = GameController.Instance;

            var isTarget = TargetChoice.None;
            bool isSelect = false;
            bool isChange = false;

            if (con.GetConnectFlag())
            {
                isTarget = con.ButtonDown(Button.R1) ? TargetChoice.Front :
                            con.ButtonDown(Button.L1) ? TargetChoice.Next : TargetChoice.None;

                isSelect = con.ButtonDown(Button.A);
                isChange = con.ButtonDown(Button.B);
            }
            else
            {
                isTarget = Input.GetKeyDown(KeyCode.Space) ? TargetChoice.Next : TargetChoice.None;
                isSelect = Input.GetKeyDown(KeyCode.C);
                isChange = Input.GetKeyDown(KeyCode.V);
            }

            if (isTarget != TargetChoice.None)
            {
                // カメラにエレメントオブジェクトが移っているとき探す
                if (LockOnObj.CheckOnScreenAll())
                {
                    // 次のターゲットオブジェクトを取得
                    if (isTarget != TargetChoice.Next)
                    {
                        // 選択
                        TargetObject(LockOnObj.GetTarget(1));
                    }
                    else if (isTarget != TargetChoice.Front)
                    {
                        TargetObject(LockOnObj.GetTarget(-1));
                    }
                }
            }

            // 要素吸出し
            if (isSelect)
            {
                if (_targetObject)
                {
                    //コピー時エフェクト
                    CopyEffect();
                    //Dataパネル更新
                    DataPanelUpDate(_targetObject);
                    SelectObject();
                }
            }

            // 要素を移す
            if (isChange)
            {
                if (_targetObject)
                {

                    MoveElement(_targetObject);

                }
            }

            if (_targetObject)
            {
                //正気に戻ったらConsole破棄
                if (_targetObject.Stats == ElementObject.ElementStates.Remember)
                {
                    TargetUIRelease();
                }
            }
        }

        /// <summary>
        /// オブジェクトをターゲット
        /// </summary>
        virtual protected void TargetObject(ElementObject obj)
        {
            if (obj == null)
            {
                return;
            }

            // 要素をターゲット
            TargetElementObject(obj);

            // Console更新
            ConsoleUpDate(obj);
            //操作ガイドの変更
            GuidUI.Instance.GetComponent<GuidUI>().ChangeGuid(GuidUI.GUID_STEP.Lockon);

            // SE
            SoundManager.Instance.PlayOneShot(AudioKey.in_play_lock_on);
        }

        //Console更新
        private void ConsoleUpDate(ElementObject obj)
        {

            if (obj.GetComponent<ElementObject>().ElementList == null) return;

            //古いConsoleを破棄
            if (_console)
            {
                _console.GetComponent<ConsoleCon>().SetIsOpen(false);
            }

            //新しいConsole設定
            _console = EffectManager.Instance.CreateEffect(EffectID.Console);
            //Consoleをオブジェクトにセット
            _console.GetComponent<UISet>().SetTransform(obj.transform);


            if (obj.GetComponent<ElementObject>().ElementList.Length != 0)
            {
                //ムーブ,ディレクション画像
                for (int i = 0; i < obj.ElementList.Length; i++)
                {
                    if (obj.ElementList[i])
                    {

                        if (obj.ElementList[i].Type == ElementType.Action)
                        {
                            var element = obj.ElementList[i].GetType().Name;
                            ChangeConsoleIcon(0, element, obj);
                        }

                        if (obj.ElementList[i].Type == ElementType.Move)
                        {
                            var element = obj.ElementList[i].GetType().Name;
                            ChangeConsoleIcon(1, element, obj);
                        }

                        if (obj.ElementList[i].Type == ElementType.Direction)
                        {
                            var element = obj.ElementList[i].GetType().Name;
                            ChangeConsoleIcon(2, element, obj);
                        }
                    }
                }
            }
        }

        //Consoleのアイコン変更
        private void ChangeConsoleIcon(int iconNum, string typeName, ElementObject obj)
        {


            if (typeName == "DiectionTest")
            {
                switch (obj.GetCurrentDirection())
                {
                    case Direction.Back:
                        _console.GetComponent<ConsoleCon>().SetIcon(iconNum, CONSOLE_ICON_ID.Direction_Down);
                        break;

                    case Direction.Left:
                        _console.GetComponent<ConsoleCon>().SetIcon(iconNum, CONSOLE_ICON_ID.Direction_Left);
                        break;

                    case Direction.Right:
                        _console.GetComponent<ConsoleCon>().SetIcon(iconNum, CONSOLE_ICON_ID.Direction_Right);
                        break;

                    case Direction.Front:
                        _console.GetComponent<ConsoleCon>().SetIcon(iconNum, CONSOLE_ICON_ID.Direction_Up);
                        break;

                    default:

                        break;
                }
            }

            if (typeName == "TestShot")
            {
                _console.GetComponent<ConsoleCon>().SetIcon(iconNum, CONSOLE_ICON_ID.ShotRock);
            }

            if (typeName == "Tackle")
            {
                _console.GetComponent<ConsoleCon>().SetIcon(iconNum, CONSOLE_ICON_ID.TackleRock);
            }

            if (typeName == "RideFloor")
            {
                _console.GetComponent<ConsoleCon>().SetIcon(iconNum, CONSOLE_ICON_ID.RideOnRock);
            }

            if (typeName == "SideMove")
            {
                _console.GetComponent<ConsoleCon>().SetIcon(iconNum, CONSOLE_ICON_ID.Side);
            }

            if (typeName == "Stay")
            {
                _console.GetComponent<ConsoleCon>().SetIcon(iconNum, CONSOLE_ICON_ID.Stop);
            }

            if (typeName == "UpDownMove")
            {

                _console.GetComponent<ConsoleCon>().SetIcon(iconNum, CONSOLE_ICON_ID.Updown);
            }

            if (typeName == "NoData")
            {

                _console.GetComponent<ConsoleCon>().SetIcon(iconNum, CONSOLE_ICON_ID.Nodata);
            }
        }


        private void DataPanelUpDate(ElementObject obj)
        {

            if (obj.GetComponent<ElementObject>().ElementList == null) return;

            if (obj.GetComponent<ElementObject>().ElementList.Length != 0)
            {
                //ムーブ,ディレクション画像
                for (int i = 0; i < obj.GetComponent<ElementObject>().ElementList.Length; i++)
                {
                    if (obj.ElementList[i])
                    {

                        if (obj.ElementList[i].Type == ElementType.Action)
                        {

                            var element = obj.ElementList[i].GetType().Name;
                            DataIconSet(0, element, obj);

                        }
                        else if (obj.ElementList[i].Type == ElementType.Move)
                        {
                            var element = obj.ElementList[i].GetType().Name;
                            DataIconSet(1, element, obj);
                        }
                        else if (obj.ElementList[i].Type == ElementType.Direction)
                        {
                            var element = obj.ElementList[i].GetType().Name;
                            DataIconSet(2, element, obj);
                        }
                    }
                }
            }
        }


        private void DataIconSet(int iconNum, string typeName, ElementObject obj)
        {
            if (typeName == "DiectionTest")
            {
                switch (obj.GetCurrentDirection())
                {
                    case Direction.Back:
                        _dataPanel.GetComponent<PlayerDataPanel>().SetIcon(iconNum, CONSOLE_ICON_ID.Direction_Down);
                        break;

                    case Direction.Left:
                        _dataPanel.GetComponent<PlayerDataPanel>().SetIcon(iconNum, CONSOLE_ICON_ID.Direction_Left);
                        break;

                    case Direction.Right:
                        _dataPanel.GetComponent<PlayerDataPanel>().SetIcon(iconNum, CONSOLE_ICON_ID.Direction_Right);
                        break;

                    case Direction.Front:
                        _dataPanel.GetComponent<PlayerDataPanel>().SetIcon(iconNum, CONSOLE_ICON_ID.Direction_Up);
                        break;

                    default:

                        break;
                }
            }



            if (typeName == "TestShot")
            {
                _dataPanel.GetComponent<PlayerDataPanel>().SetIcon(iconNum, CONSOLE_ICON_ID.Shot);
            }

            if (typeName == "Tackle")
            {
                _dataPanel.GetComponent<PlayerDataPanel>().SetIcon(iconNum, CONSOLE_ICON_ID.Tackle);
            }

            if (typeName == "RideFloor")
            {
                _dataPanel.GetComponent<PlayerDataPanel>().SetIcon(iconNum, CONSOLE_ICON_ID.RideOn);
            }

            if (typeName == "SideMove")
            {
                _dataPanel.GetComponent<PlayerDataPanel>().SetIcon(iconNum, CONSOLE_ICON_ID.Side);
            }

            if (typeName == "Stay")
            {
                _dataPanel.GetComponent<PlayerDataPanel>().SetIcon(iconNum, CONSOLE_ICON_ID.Stop);
            }

            if (typeName == "UpDownMove")
            {

                _dataPanel.GetComponent<PlayerDataPanel>().SetIcon(iconNum, CONSOLE_ICON_ID.Updown);
            }

            if (typeName == "NoData")
            {
                _dataPanel.GetComponent<PlayerDataPanel>().SetIcon(iconNum, CONSOLE_ICON_ID.Nodata);
            }
        }

        private void ConsoleOut()
        {
            if (_console)
            {
                _console.GetComponent<ConsoleCon>().SetIsOpen(false);
            }
        }

        /// <summary>
        /// 要素オブジェクトをターゲットしたときの処理
        /// </summary>
        /// <param name="elementObj"></param>
        private void TargetElementObject(ElementObject elementObj)
        {
            // ターゲット解除
            TargetUIRelease();

            // ターゲット
            _targetObject = elementObj;

            var target = _targetObject.gameObject.AddComponent<TargetObject>();
            target.SetSelector(this);

            GuidUI.Instance.GetComponent<GuidUI>().ChangeGuid(GuidUI.GUID_STEP.Lockon);

        }

        /// <summary>
        /// オブジェクトへのターゲットを解除
        /// </summary>
        public void TargetRelease()
        {
            _targetObject = null;
            ConsoleOut();
        }
        public void TargetUIRelease()
        {
            if (_targetObject == null)
            {
                return;
            }

            // タゲUIなど解除
            var target = _targetObject.GetComponent<TargetObject>();

            if (target)
            {
                target.Release();
                //Console削除
                ConsoleOut();
                GuidUI.Instance.GetComponent<GuidUI>().ChangeGuid(GuidUI.GUID_STEP.Normal);
            }
        }

        /// <summary>
        /// オブジェクトを選択
        /// </summary>
        virtual protected void SelectObject()
        {
            SelectRelease();
            _container.ReceiveAllElement(_targetObject.ElementList);

            // SE
            SoundManager.Instance.PlayOneShot(AudioKey.in_play_copy);

            // ターゲット解除
            TargetUIRelease();
        }

        /// <summary>
        /// オブジェクトへの選択を解除
        /// </summary>
        private void SelectRelease()
        {
            _container.AllDelete();
        }

        /// <summary>
        /// 要素の移動
        /// </summary>
        /// <param name="selectObj"></param>
        virtual protected void MoveElement(ElementObject selectObj)
        {
            // リストを記憶していない場合は移動しない
            if (_container.List == null) return;
            // すべての要素を移動
            selectObj.ReceiveAllElement(_container.List.ToArray());
            //Console更新
            ConsoleUpDate(selectObj);

            // SE
            SoundManager.Instance.PlayOneShot(AudioKey.in_play_paste);

            //ペースト時エフェクト
            PasteEffect();
            //復帰演出セット
            RecoverSet();
            // ターゲット解除
            TargetUIRelease();
        }

        //コピー時エフェクト
        void CopyEffect()
        {
            //コピー時エフェクト
            GameObject effect = EffectManager.Instance.CreateEffect(EffectID.Wave, _targetObject.transform.position);
            effect.GetComponent<WaveContoller>().setVelocity(gameObject.transform.parent.transform);
        }

        //ペースト時エフェクト
        void PasteEffect()
        {
            //送信エフェクト
            GameObject effect = EffectManager.Instance.CreateEffect(EffectID.Wave, gameObject.transform.parent.position);
            effect.GetComponent<WaveContoller>().setVelocity(_targetObject.transform);
        }

        //復帰演出セット
        void RecoverSet()
        {
            //復帰演出セット＆開始
            GameObject recover = EffectManager.Instance.CreateEffect(EffectID.EnemyRecovery, _targetObject.transform.position);
            recover.GetComponent<UISet>().SetTransform(_targetObject.transform);
            recover.GetComponent<EnemyRecovery>().SetTime(_targetObject.GetComponent<ElementObject>().GetReturnTime());
            _targetObject.GetComponent<ElementObject>().EffectUpDate(recover);
        }
    }
}