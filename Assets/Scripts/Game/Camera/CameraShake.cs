using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using Cinemachine;
namespace Play
{ 
    public class CameraShake : MonoBehaviour
    {
        //振動に使用するノイズパターン
        [SerializeField]
        NoiseSettings _noise;
        //デフォルト振幅
        [SerializeField]
        private float _defaultShakeAmplitude = 0.5f;
        //デフォルト周波数
        [SerializeField]
        private float _defaultShakeFrequency = 10f;
        //振動時間
        [SerializeField]
        private float _durationTime = 0.2f;
        //対象疑似カメラ
        [SerializeField, ReadOnly]
        private Cinemachine.CinemachineVirtualCamera _virtualCamera;
        //シネマシネノイズ
        [SerializeField,ReadOnly]
        private Cinemachine.CinemachineBasicMultiChannelPerlin _perlin;
       
        /// <summary>
        /// On Start we reset our camera to apply our base amplitude and frequency
        /// </summary>
        void Start()
        {
            //カメラ状態のリセット
            CameraReset();
        }

        //カメラを振動させる
        public virtual void ShakeCamera()
        {
            //ノイズパターンを「Vibration」に変更
            if (_noise)
            {
                _perlin.m_NoiseProfile = _noise;
            }
            else
            {
                Debug.Log("ノイズがセットされていないぞ");
            }      
            //疑似カメラを振動させる
            StartCoroutine(ShakeCameraCo(_defaultShakeAmplitude, _defaultShakeFrequency));
        }
    
        //指定時間経過で振動させる
        IEnumerator ShakeCameraCo( float amplitude, float frequency)
        {
            //振幅と周波数のセット
            _perlin.m_AmplitudeGain = amplitude;
            _perlin.m_FrequencyGain = frequency;
            //設定時間経過まで待機
            yield return new WaitForSeconds(_durationTime);
            //カメラのリセット
            CameraReset();
        }

        //カメラのリセットを行う
        public virtual void CameraReset()
        {
            //現在使用中のカメラがある場合
            if(_virtualCamera)
            {
                //ノイズの停止
                _perlin.m_NoiseProfile = null;
            }
            //使用するカメラの更新
            _virtualCamera = GetComponent<CameraManager>().GetCullentVCam();
            _perlin = _virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }
    }
}
