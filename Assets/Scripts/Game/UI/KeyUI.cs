using System.Collections;
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

    //表示文字
    [SerializeField, ReadOnly]
    Text _text;
    //表示アイコン
    [SerializeField, ReadOnly]
    Image _icon;
    //アイコン集
    [SerializeField,ReadOnly]
    Sprite[] _images;


    // Use this for initialization
    void Start () {
        //テキスト取得
        _text = transform.Find("Text").GetComponent<Text>();
        //アイコン取得
        _icon = transform.Find("Icon").GetComponent<Image>();
        //リソースからアイコンゲット
        _images =  Resources.LoadAll<Sprite>("Icons");
    }
	
	
    //メッセージの変更
    void SetGuidText(string text)
    {
        _text.text = text;
    }

    //顔アイコンの変更
    void SetIcon(Sprite sprite)
    {
        _icon.sprite = sprite;
    }

   
    //ガイド内容のセット
    public void GuidUISet(GUID_ID id)
    {
        this.gameObject.SetActive(true);   
        switch (id)
        {
            case GUID_ID.Move:

                SetGuidText("移動");
                SetIcon(_images[(int)GUID_ID.Move]);
                break;

            case GUID_ID.LockON:

                SetGuidText("ロックオン");
                SetIcon(_images[(int)GUID_ID.LockON]);
                break;

            case GUID_ID.ChangeLock:

                SetGuidText("ロックオン切り替え");
                SetIcon(_images[(int)GUID_ID.ChangeLock]);
                break;

            case GUID_ID.Copy:

                SetGuidText("コピー");
                SetIcon(_images[(int)GUID_ID.Copy]);
                break;

            case GUID_ID.Paste:

                SetGuidText("ペースト");
                SetIcon(_images[(int)GUID_ID.Paste]);
                break;
        }     
    }  
}
