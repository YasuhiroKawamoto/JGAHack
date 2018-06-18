using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Extensions;

public class StageTimePanel : MonoBehaviour
{
	// 表示定型文
	[SerializeField, ReadOnly]
	private static readonly string _fixedSentence = "{0}位 {1}";

	[SerializeField]
	private string textHead = string.Empty;

	[SerializeField]
	private GameObject _textsParent = null;

	private List<Text> _texts = null;

	public List<Text> Texts
	{
		get { return _texts == null ? GetTexts() : _texts; }
	}

	List<Text> GetTexts()
	{
		var texts = new List<Text>();
		var childs = _textsParent.transform.GetAllChild();

		foreach (var child in childs)
		{
			var text = child.GetComponent<Text>();
			if (text) texts.Add(text);
		}

		return texts;
	}

	public void UpdateView(int stageNum)
	{
		var times = StageTimeData.Instance.GetStageTimes(stageNum + 1);

		for (int i = 0; i < Texts.Count; i++)
		{
			var text = string.Format(_fixedSentence, i + 1, Play.Timer.DisplayTime(times[i]));
			Texts[i].text = text;
		}
	}

}
