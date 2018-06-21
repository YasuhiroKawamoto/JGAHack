using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{

    //　メッセージUI
    [SerializeField]
    private Text _messageText;
    //　表示するメッセージ
    private string _message;
    //　1回のメッセージの最大文字数
    [SerializeField]
    private int _maxTextLength = 90;
    //　1回のメッセージの現在の文字数
    private int _textLength = 0;
    //　メッセージの最大行数
    [SerializeField]
    private int _maxLine = 3;
    //　現在の行
    private int _nowLine = 0;
    //　テキストスピード
    [SerializeField]
    private float _textSpeed = 0.05f;
    //　経過時間
    private float _elapsedTime = 0f;
    //　今見ている文字番号
    private int _nowTextNum = 0;
    //　マウスクリックを促すアイコン
    [SerializeField]
    private Image _clickIcon;
    //　キャラクターの顔
    [SerializeField]
    private Image _faceIcon;

    //　クリックアイコンの点滅秒数
    [SerializeField]
    private float _clickFlashTime = 0.2f;
    //　1回分のメッセージを表示したかどうか
    private bool _isOneMessage = false;
    //　メッセージをすべて表示したかどうか
    private bool _isEndMessage = false;

    // UIのプレハブ
    [SerializeField]
    private GameObject _ui = null;

    /// <summary>
    /// ウインドウの表示
    /// </summary>
    public void ShowWindow()
    {
        _ui.SetActive(true);
    }

    /// <summary>
    /// ウインドウの非表示
    /// </summary>
    public void HideWindow()
    {
        _ui.SetActive(false);
    }

    void Start()
    {
        _clickIcon.enabled = false;
        HideWindow();
        _messageText.text = "";
        SetMessage("我が名は「小野妹子」!!\n");
    }

    void Update()
    {
        //　メッセージが終わっていない、または設定されている
        if (_isEndMessage || _message == null)
        {
            return;
        }

        //　1回に表示するメッセージを表示していない	
        if (!_isOneMessage)
        {

            //　テキスト表示時間を経過したら
            if (_elapsedTime >= _textSpeed)
            {
                _messageText.text += _message[_nowTextNum];
                ////　改行文字だったら行数を足す
                //if (message [nowTextNum] == '\n') {
                //	nowLine++;
                //}

                //「。」でページ変更（クリック促し）
                if (_message[_nowTextNum] == '\n')
                {
                    _isOneMessage = true;
                }
                _nowTextNum++;
                _textLength++;
                _elapsedTime = 0f;

                //　メッセージを全部表示、または行数が最大数表示された
                if (_nowTextNum >= _message.Length || _textLength >= _maxTextLength || _nowLine >= _maxLine)
                {
                    _isOneMessage = true;
                }
            }
            _elapsedTime += Time.deltaTime;

            //　メッセージ表示中にマウスの左ボタンを押したら一括表示
            if (Input.GetMouseButtonDown(0))
            {
                //　ここまでに表示しているテキストを代入
                var allText = _messageText.text;

                //　表示するメッセージ文繰り返す
                for (var i = _nowTextNum; i < _message.Length; i++)
                {
                    allText += _message[i];

                    if (_message[i] == '\n')
                    {
                        _nowLine++;
                    }


                    _nowTextNum++;
                    _textLength++;

                    //　メッセージがすべて表示される、または１回表示限度を超えた時
                    if (_nowTextNum >= _message.Length || _textLength >= _maxTextLength || _nowLine >= _maxLine)
                    {
                        _messageText.text = allText;
                        _isOneMessage = true;
                        break;
                    }
                }
            }
            //　1回に表示するメッセージを表示した
        }
        else
        {
            _elapsedTime += Time.deltaTime;

            //　クリックアイコンを点滅する時間を超えた時、反転させる
            if (_elapsedTime >= _clickFlashTime)
            {
                _clickIcon.enabled = !_clickIcon.enabled;
                _elapsedTime = 0f;
            }
        }
    }

    //メッセージの変更
    void SetMessage(string message)
    {
        this._message = message;
    }

    //顔アイコンの変更
    void SetFaceImage(Sprite sprite)
    {
        this._faceIcon.sprite = sprite;
    }

    //　他のスクリプトから新しいメッセージを設定
    public virtual void SetMessagePanel(string message)
    {
        //リセット
        ResetMessage();
        // UI表示
        ShowWindow();
        //文章の設定
        SetMessage(message);
        //文章のアクティブ化
        transform.GetChild(0).gameObject.SetActive(true);
        //文章はまだ終わらない...
        _isEndMessage = false;
    }

    //　他のスクリプトから新しいメッセージを設定(顔変更)
    public virtual void SetMessagePanel(string message, Sprite sprite)
    {
        //顔アイコンの変更
        SetFaceImage(sprite);
        // 新規メッセージ設定
        SetMessagePanel(message);
    }

    void ResetMessage()
    {
        //メッセージを空にする
        _messageText.text = "";
        //現在行数リセット
        _nowLine = 0;
        //クリック状態リセット
        _clickIcon.enabled = false;
        //経過時間リセット
        _elapsedTime = 0f;
        //文字列の長さリセット
        _textLength = 0;
        //一括表示フラグリセット
        _isOneMessage = false;
        //現在何文字目かのリセット     
        _nowTextNum = 0;
    }

}
