using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;



namespace Play
{
    //カメラマネージャ


    public class CameraManager : MonoBehaviour
    {
        private const int HI = 15;

        private const int LOW = 10;

        //プレイヤー状態確認用
        [SerializeField]
        private GameObject _player;
        //カメラ本体
        [SerializeField]
        private GameObject _mainCam;
        //疑似カメラA
        [SerializeField]
        private GameObject _CamA;
        //疑似カメラB
        [SerializeField]
        private GameObject _CamB;
        //ゴールカメラ
        [SerializeField]
        private GameObject _CamGoal;
        //現在のカメラ
        [SerializeField, ReadOnly]
        private GameObject _currentCam;
        //カメラ切り替え所要時間
        [SerializeField, ReadOnly]
        private float _camChangeTime;
        //カメラ切り替え所要時間
        [SerializeField]
        private float _camChangeTimeLimit = 2.0f;
        //カメラ切り替え中か？
        [SerializeField,ReadOnly]
        private bool _isChanging = false;

        [SerializeField, ReadOnly]
        private bool _isStarted = false;

        // Use this for initialization
        void Start()
        {
            //ゴール位置カメラの優先度設定（高い程優先される）
            _CamGoal.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = 30;
            //プレイヤー取得
            _player = GameObject.Find("Player");
           
            //現在のカメラの設定
            _currentCam = _CamA;
            //カメラ切り替え時間の設定
            _camChangeTime = _camChangeTimeLimit;
            //カメラのセット
            CameraSetting(_CamB, _CamA);
            //カメラの切り替わり所要時間の変更
            _mainCam.GetComponent<Cinemachine.CinemachineBrain>().m_DefaultBlend.m_Time = 3;




        }

        // Update is called once per frame
        void Update()
        {
           
            //カメラの状態チェック
            CamCheck();

        }

        //メインカメラの切り替え
        public void MainCameraChange()
        {
            //カメラの切り替わり所要時間の変更
            _mainCam.GetComponent<Cinemachine.CinemachineBrain>().m_DefaultBlend.m_Time = 2;

           
            //現カメラのフォロー解除
            _currentCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Follow = null;
            _currentCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_LookAt = null;

            if (_currentCam == _CamA)
            {
                //カメラの優先度切り替え
                _CamA.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = LOW;
                _CamB.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = HI;
                //現状のカメラ切り替え
                _currentCam = _CamB;
                //切り替えフラグON
                _isChanging = true;
              
            }
            else
            {
                //カメラの優先度切り替え
                _CamA.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = HI;
                _CamB.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = LOW;
                //現状のカメラ切り替え
                _currentCam = _CamA;
                //切り替えフラグON
                _isChanging = true;
               
            }

        }

        //ゴール表示カメラの優先度変更
        void StartCamMove()
        {
            _CamGoal.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = 5;
        }

        //カメラの設定変更
        void CameraSetting(GameObject oldCam, GameObject nextCam)
        {
            //フォロー対象をプレイヤーにセット
            nextCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Follow = _player.transform;
            nextCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_LookAt = _player.transform;
     
            //プレイヤー復活地点を取得
            Vector3 resetPos = InGameManager.Instance.GetStartPos();
            resetPos.z = -10.0f;
            //旧カメラの位置を開始地点に移動
            oldCam.transform.position = resetPos;

        }


        //カメラの状態チェック
        void CamCheck()
        {
            //開始時のみ呼ばれる
            if (!_isStarted)
            {
                //開始時のカメラ挙動
                StartCamMove();
            }
            
            //カメラ切り替え中にセッティング変更
            if (_isChanging)
            {
                _camChangeTime -= Time.deltaTime;

                if (_camChangeTime <= 0)
                {
                    if (_currentCam == _CamB)
                    {
                        //疑似カメラの設定
                        CameraSetting(_CamA, _CamB);
                    }
                    else
                    {
                        //疑似カメラの設定
                        CameraSetting(_CamB, _CamA);
                    }
                    _isChanging = false;
                    _camChangeTime = _camChangeTimeLimit; 
                }
            }
        }
    }
}