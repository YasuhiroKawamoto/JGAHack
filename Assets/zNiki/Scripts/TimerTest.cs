using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerTest : MonoBehaviour
{

	[SerializeField]
	private Play.Timer timer;

	[SerializeField]
	private Text minuteText;
	[SerializeField]
	private Text secondText;
	[SerializeField]
	private Text millisecondText;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		timer.StartTimer();
		millisecondText.text = "Msec: " + timer.GetMillisecond().ToString();
	}
}
