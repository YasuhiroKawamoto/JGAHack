﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Extensions;


public class KeyUI : MonoBehaviour {

    //案内の種類
    public enum GUID_ID
    {
        LockON,
        Move,
        Copy,
        Paste,
        ChangeLock
    }


    public enum ICON_ID
    {
        
        Copy,
        LockOnL,
        LockOnR,
        Paste,
     
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
    [SerializeField,ReadOnly]
    Sprite[] _images;


    // Use this for initialization
    void Start () {
        //テキスト取得
        _text = transform.Find("Text").GetComponent<Text>();
        //アイコン取得
        _icon = transform.Find("Icon").GetComponent<Image>();
        //アイコン取得
        _icon2 = transform.Find("Icon2").GetComponent<Image>();
        //リソースからアイコンゲット
        _images =  Resources.LoadAll<Sprite>("Icons");
    }
	
	
    //メッセージの変更
    void SetGuidText(string text)
    {
        _text.text = text;
    }

    //顔アイコンの変更
    public virtual void SetIcon(Sprite sprite)
    {
        _icon.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(205, 0, 0);
        _icon.sprite = sprite;
        _icon2.gameObject.SetActive(false);
    }

    public virtual void SetIcon(Sprite sprite,Sprite sprite2)
    {
        _icon.sprite = sprite;
        _icon.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(145, 0, 0);

        _icon2.gameObject.SetActive(true);
        _icon2.sprite = sprite2;
        _icon2.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(245, 0, 0);
    }


    //ガイド内容のセット
    public void GuidUISet(GUID_ID id)
    {
        this.gameObject.SetActive(true);
        switch (id)
        {
            case GUID_ID.Move:

                SetGuidText("移動");
                SetIcon(_images[(int)ICON_ID.Copy]);
                break;

            case GUID_ID.LockON:

                SetGuidText("ロックオン");
                SetIcon(_images[(int)ICON_ID.LockOnR]);
                break;

            case GUID_ID.ChangeLock:

                SetGuidText("ロックオン切り替え");
                SetIcon(_images[(int)ICON_ID.LockOnL],_images[(int)ICON_ID.LockOnR]);
                break;

            case GUID_ID.Copy:

                SetGuidText("コピー");
                SetIcon(_images[(int)ICON_ID.Copy]);
                break;

            case GUID_ID.Paste:

                SetGuidText("ペースト");
                SetIcon(_images[(int)ICON_ID.Paste]);
                break;
        }     
    }  
}
