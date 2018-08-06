using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitlePanel : MonoBehaviour
{
    public Main.PhoneScreen Screen = null;

    public System.Action<int> SelectFunc = null;

    private int _stage = 0;

    private Sprite _defaultImage = null;
    [SerializeField]
    private Sprite _selectImage = null;

    private UnityEngine.UI.Image _image = null;
    public Image Image
    {
        get { return _image = _image ? _image : this.GetComponent<UnityEngine.UI.Image>(); }
    }

    public int Stage
    {
        get { return _stage; }
        set { _stage = value; }
    }

    public Sprite DefaultImage
    {
        get { return _defaultImage; }
        set { _defaultImage = _defaultImage ? _defaultImage : value; }
    }

    void Start()
    {
        // 初期Sprite記憶
        DefaultImage = Image.sprite;

        var button = this.GetComponent<UnityEngine.UI.Button>();
        button.onClick.AddListener(() =>
        {
            Play.InGameManager.Destroy();

            var index = _stage;

            if (index == Main.TakeOverData.Instance.StageNum)
            {
                Main.TakeOverData.Instance.StageNum += 1;
                // 呼び出しはこれ
                Util.Scene.SceneManager.Instance.ChangeSceneFadeInOut("Game");
                // SE
                Util.Sound.SoundManager.Instance.PlayOneShot(AudioKey.sy_enter);
            }
            else
            {
                // 選択
                Image.sprite = _selectImage;
                Main.TakeOverData.Instance.StageNum = index;
                SelectFunc(Main.TakeOverData.Instance.StageNum);
            }
        });
    }

    public void Select()
    {
        // 初期Sprite記憶
        DefaultImage = Image.sprite;
        Image.sprite = _selectImage;
    }

    public void UnSelect()
    {
        Image.sprite = DefaultImage;
    }
}
