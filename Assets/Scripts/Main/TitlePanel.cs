using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePanel : MonoBehaviour
{
    private int _stage = 0;

    public int Stage
    {
        get { return _stage; }
        set { _stage = value; }
    }

    void Start()
    {
        var button = this.GetComponent<UnityEngine.UI.Button>();
        button.onClick.AddListener(() =>
        {
            Play.InGameManager.Destroy();

            var index = _stage;
            Main.TakeOverData.Instance.StageNum = index + 1;

            // 呼び出しはこれ
            Util.Scene.SceneManager.Instance.ChangeSceneFadeInOut("Game");
            // SE
            Util.Sound.SoundManager.Instance.PlayOneShot(AudioKey.sy_enter);
        });
    }
}
