using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePanel : MonoBehaviour
{
	public Main.PhoneScreen Screen = null;

	public System.Action<int> SelectFunc = null;

	private int _stage = 0;

	private Sprite _defaultImage = null;
	[SerializeField]
	private Sprite _selectImage = null;

	public int Stage
	{
		get { return _stage; }
		set { _stage = value; }
	}

	void Start()
	{
		// 初期Sprite記憶
		var image = this.GetComponent<UnityEngine.UI.Image>();
		_defaultImage = image.sprite;

		var button = this.GetComponent<UnityEngine.UI.Button>();
		button.onClick.AddListener(() =>
		{
			Play.InGameManager.Destroy();

			var index = _stage;

			if (index == Main.TakeOverData.Instance.StageNum)
			{
				// 呼び出しはこれ
				Util.Scene.SceneManager.Instance.ChangeSceneFadeInOut("Game");
				// SE
				Util.Sound.SoundManager.Instance.PlayOneShot(AudioKey.sy_enter);
			}
			else
			{
				// 選択
				Main.TakeOverData.Instance.StageNum = index;
				SelectFunc(Main.TakeOverData.Instance.StageNum);
			}
		});
	}

	public void UnSelect()
	{

	}
}
