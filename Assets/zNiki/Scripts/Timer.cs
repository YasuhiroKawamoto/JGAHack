using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Play
{
    public class Timer : MonoBehaviour
    {
        // 分数カウント
        private int _minuteCount;
        // 秒数カウント
        private float _secondCount;
        // 経過時間カウント（ミリ秒）
        private float _msecondCount;
        // 計測中か
        private bool _isCounting;
        // タイム表示テキスト
        private Text _timerText;

        // Use this for initialization
        void Start()
        {
            //分数初期化
            _minuteCount = 0;
            //秒数初期化
            _secondCount = 0;
            // 経過時間初期化
            _msecondCount = 0;

            _isCounting = false;

            _timerText = this.GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            //タイマー更新
            UpdateTimer();
        }

        //時間計測開始
        public void StartTimer()
        {
            _isCounting = true;

        }

        //時間計測停止
        public void StopTimer()
        {
            _isCounting = false;
        }

        //時間計測リセット
        public void ResetTimer()
        {
            _minuteCount = 0;
            _secondCount = 0;
            _msecondCount = 0;
        }

        //タイマー更新
        private void UpdateTimer()
        {
            //計測中なら
            if (_isCounting)
            {
                //時間加算
                _secondCount += Time.deltaTime;
                _msecondCount += Time.deltaTime;

                if (_secondCount >= 60.0f)
                {
                    _minuteCount++;
                    _secondCount -= 60.0f;
                }

                DisplayTime();
            }
        }

        // テキストに表示
        private void DisplayTime()
        {
            _timerText.text = "Time: " + _minuteCount.ToString("00") + ":" + _secondCount.ToString("00.00");
        }

        // タイム取得（分）
        public int GetMinute()
        {
            return _minuteCount;
        }

        // タイム取得（秒）
        public float GetSecond()
        {
            return _secondCount;
        }
        
        // タイム取得（ミリ秒）
        public float GetMillisecond()
        {
            return _msecondCount;
        }
    }
}