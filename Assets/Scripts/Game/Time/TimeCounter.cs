using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Play
{
	public class TimeCounter : MonoBehaviour
	{
		[SerializeField]
		private Text _text;

		[SerializeField]
		private Timer _timer = null;

		void Awake()
		{
			_timer = gameObject.AddComponent<Timer>();
			_text = gameObject.GetComponent<Text>();
		}

		public void StartTimer()
		{
			_timer.StartTimer();
		}

		public float EndTimer()
		{
			_timer.StopTimer();
			return _timer.GetMillisecond();
		}

		void Update()
		{
			var time = _timer.GetMillisecond();
			_text.text = Timer.DisplayTime(time);
		}
	}
}