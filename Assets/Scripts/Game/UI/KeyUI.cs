using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Extensions;

namespace Play
{

    public class KeyUI : MonoBehaviour
    {

        //ガイドID
        public enum GUID_ID
        {
            LockON,
            Move,
            Move2,
            Copy,
            Paste,
            ChangeLock
        }

        //アイコンID(controller)
        public enum CONTROLLER_ICON_ID
        {

            Copy,
            LockOnL,
            LockOnR,
            Move,
            Paste,

        }

        //アイコンID(keyboard)
        public enum KEYBOARD_ICON_ID
        {
            ArrowDown,
            ArrowLeft,
            ArrowRight,
            ArrowUp,
            Copy,
            LockOn,
            Paste
        }

        //表示文字
        [SerializeField, ReadOnly]
        Text _text;
        //表示アイコン
        [SerializeField, ReadOnly]
        Image _icon;
        //表示アイコン2
        [SerializeField, ReadOnly]
        Image _icon2;
        //アイコン集
        [SerializeField, ReadOnly]
        Sprite[] _controllerImages;

        [SerializeField, ReadOnly]
        Sprite[] _keybordImages;


        // Use this for initialization
        void Awake()
        {
            //テキスト取得
            _text = transform.Find("Text").GetComponent<Text>();
            //アイコン取得
            _icon = transform.Find("Icon").GetComponent<Image>();
            //アイコン取得
            _icon2 = transform.Find("Icon2").GetComponent<Image>();
            //リソースからアイコンゲット
            _controllerImages = EffectManager.Instance.GetControllerIcons();
            //リソースからアイコンゲット
            _keybordImages = EffectManager.Instance.GetKeyboardIcons();
        }


        //メッセージの変更
        void SetGuidText(string text)
        {
            _text.text = text;
        }

        //アイコンの変更
        public virtual void SetIcon(Sprite sprite)
        {
            _icon.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(205, 0, 0);
            _icon.sprite = sprite;
            _icon2.gameObject.SetActive(false);
        }

        //アイコンの変更(二枚左右出し)
        public virtual void SetIcon(Sprite sprite, Sprite sprite2)
        {
            _icon.sprite = sprite;
            _icon.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(145, 0, 0);

            _icon2.gameObject.SetActive(true);
            _icon2.sprite = sprite2;
            _icon2.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(245, 0, 0);
        }


        //ガイド内容のセット
        public void GuidUISet(GUID_ID id,bool isContoroller)
        {
           
            if (isContoroller)
            {
                //controller
                switch (id)
                {
                    case GUID_ID.Move:
                        //テキスト変更
                        SetGuidText("移動");
                        //アイコン変更
                        SetIcon(_controllerImages[(int)CONTROLLER_ICON_ID.Move]);
                        break;

                    case GUID_ID.LockON:
                        //テキスト変更
                        SetGuidText("ロックオン");
                        //アイコン変更
                        SetIcon(_controllerImages[(int)CONTROLLER_ICON_ID.LockOnL],_controllerImages[(int)CONTROLLER_ICON_ID.LockOnR]);
                        break;

                    case GUID_ID.ChangeLock:
                        //テキスト変更
                        SetGuidText("ロックオン切り替え");
                        //アイコン変更
                        SetIcon(_controllerImages[(int)CONTROLLER_ICON_ID.LockOnL], _controllerImages[(int)CONTROLLER_ICON_ID.LockOnR]);
                        break;

                    case GUID_ID.Copy:
                        //テキスト変更
                        SetGuidText("コピー");
                        //アイコン変更
                        SetIcon(_controllerImages[(int)CONTROLLER_ICON_ID.Copy]);
                        break;

                    case GUID_ID.Paste:
                        //テキスト変更
                        SetGuidText("ペースト");
                        //アイコン変更
                        SetIcon(_controllerImages[(int)CONTROLLER_ICON_ID.Paste]);
                        break;
                }
            }
            else
            {
                //keyboard
                switch (id)
                {
                    case GUID_ID.Move:
                        //テキスト変更
                        SetGuidText("上下移動");
                        //アイコン変更
                        SetIcon(_keybordImages[(int)KEYBOARD_ICON_ID.ArrowUp], _keybordImages[(int)KEYBOARD_ICON_ID.ArrowDown]);
                        break;
                    case GUID_ID.Move2:
                        //テキスト変更
                        SetGuidText("左右移動");
                        //アイコン変更
                        SetIcon(_keybordImages[(int)KEYBOARD_ICON_ID.ArrowLeft], _keybordImages[(int)KEYBOARD_ICON_ID.ArrowRight]);
                        break;

                    case GUID_ID.LockON:
                        //テキスト変更
                        SetGuidText("ロックオン");
                        //アイコン変更
                        SetIcon(_keybordImages[(int)KEYBOARD_ICON_ID.LockOn]);
                        break;

                    case GUID_ID.ChangeLock:
                        //テキスト変更
                        SetGuidText("ロックオン切り替え");
                        //アイコン変更
                        SetIcon(_keybordImages[(int)KEYBOARD_ICON_ID.LockOn]);
                        break;

                    case GUID_ID.Copy:
                        //テキスト変更
                        SetGuidText("コピー");
                        //アイコン変更
                        SetIcon(_keybordImages[(int)KEYBOARD_ICON_ID.Copy]);
                        break;

                    case GUID_ID.Paste:
                        //テキスト変更
                        SetGuidText("ペースト");
                        //アイコン変更
                        SetIcon(_keybordImages[(int)KEYBOARD_ICON_ID.Paste]);
                        break;
                }
            }
                
        }
    }
}