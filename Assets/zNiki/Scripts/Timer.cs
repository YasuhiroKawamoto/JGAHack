using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Play
{
    public class Timer : MonoBehaviour
    {
        // 経過時間カウント（ミリ秒）
        private float _msecondCount;
        // 計測中か
        private bool _isCounting = false;

        //時間計測開始
        public void StartTimer()
        {
            if (_isCounting) return;

            _isCounting = true;
            ResetTimer();

            StartCoroutine(TimerCorutine());
        }
        private IEnumerator TimerCorutine()
        {
            yield return new WaitWhile(() =>
            {
                if (!_isCounting) return _isCounting;
                _msecondCount += Time.deltaTime;
                return _isCounting;
            });
        }

        //時間計測停止
        public void StopTimer()
        {
            _isCounting = false;
        }

        //時間計測リセット
        public void ResetTimer()
        {
            _msecondCount = 0;
        }

        // テキストに表示
        static public string DisplayTime(float mScecond)
        {
            var time = mScecond;
            int minute = 0;
            while (time >= 60f)
            {
                minute++;
                time = time - 60;
            }
            return minute.ToString("00") + ":" + time.ToString("00.00");
        }


        // タイム取得（ミリ秒）
        public float GetMillisecond()
        {
            return _msecondCount;
        }
    }
}