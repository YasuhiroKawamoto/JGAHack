using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Play.Timer
{
    public class PlayTimer : MonoBehaviour
    {
        //分数カウント
        private int _minuteCount;
        //経過時間カウント
        private float _secondCount;
        //経過時間表示用テキスト
        [SerializeField]
        Text _timeText;
        //計測中か
        [SerializeField]
        private bool _isCounting;

        // Use this for initialization
        void Start()
        {
            //分数初期化
            _minuteCount = 0;
            //秒数初期化
            _secondCount = 0;
            _isCounting = true;
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
        }

        //タイマー更新
        void UpdateTimer()
        {
            //計測中なら
            if (_isCounting)
            {
                //時間加算
                _secondCount += Time.deltaTime;

                if (_secondCount >= 60.0f)
                {
                    _minuteCount++;
                    _secondCount -= 60.0f;
                }

            }
            //テキスト更新
            TextUpdate();
        }

        //テキスト更新
        void TextUpdate()
        {
            //秒数によって変更
            if (_secondCount < 10)
            {
                _timeText.text = "Time:" + _minuteCount.ToString("00") + ":0" + _secondCount.ToString("F2");
            }
            else
            {
                _timeText.text = "Time:" + _minuteCount.ToString("00") + ":" + _secondCount.ToString("F2");
            }

        }
    }
}