using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Extensions;


public class KeyUI : MonoBehaviour {


    public enum GUID_ID
    {
        LockON,
        Move,
        Copy,
        Paste,
        ChangeLock
    }


    [SerializeField, ReadOnly]
    Text _text;
    [SerializeField, ReadOnly]
    Image _icon;

    [SerializeField]
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
    void SetMessage(string text)
    {
        _text.text = text;
    }

    //顔アイコンの変更
    void SetFaceImage(Sprite sprite)
    {
        _icon.sprite = sprite;
    }



    public void ControllUISet(GUID_ID id)
    {
        switch (id)
        {
            case GUID_ID.Move:

                SetMessage("移動");

                SetFaceImage(_images[(int)GUID_ID.Move]);
                break;

            case GUID_ID.LockON:

                SetMessage("ロックオン");

                SetFaceImage(_images[(int)GUID_ID.LockON]);
                break;

            case GUID_ID.ChangeLock:

                SetMessage("ロックオン切り替え");

                SetFaceImage(_images[(int)GUID_ID.ChangeLock]);
                break;

            case GUID_ID.Copy:

                SetMessage("コピー");

                SetFaceImage(_images[(int)GUID_ID.Copy]);
                break;

            case GUID_ID.Paste:

                SetMessage("ペースト");

                SetFaceImage(_images[(int)GUID_ID.Paste]);
                break;
        }     
    }  
}
