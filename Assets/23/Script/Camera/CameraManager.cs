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

        //プレイヤー
        [SerializeField]
        private GameObject _player;
        //ゴール
        [SerializeField]
        private GameObject _goal;
        //カメラ本体
        [SerializeField, ReadOnly]
        private GameObject _mainCam;
        //疑似カメラA
        [SerializeField, ReadOnly]
        private GameObject _camA;
        //疑似カメラB
        [SerializeField, ReadOnly]
        private GameObject _camB;
        //ゴールカメラ
        [SerializeField, ReadOnly]
        private GameObject _camGoal;
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
        [SerializeField, ReadOnly]
        private bool _isChanging = false;
        //開始フラグ（初期演出用）
        private bool _isStarted = false;

        private void Awake()
        {
            //カメラの初期設定
            InitCamera();
        }
        // Use this for initialization
        void Start()
        {
            //現在のカメラの設定
            _currentCam = _camA;
            //カメラ切り替え時間の設定
            _camChangeTime = _camChangeTimeLimit;
            //カメラのセット
            CameraSetting(_camB, _camA);
            //カメラの切り替わり所要時間の変更
            _mainCam.GetComponent<Cinemachine.CinemachineBrain>().m_DefaultBlend.m_Time = 3;
        }

        // Update is called once per frame
        void Update()
        {
            //カメラの状態チェック
            CamCheck();
        }

        //遅れて呼び出し（演出の動作の安定性のため）
        void LateUpdate()
        {
            //開始時のみ呼ばれる
            if (!_isStarted)
            {
                //開始時のカメラ挙動
                StartCamMove();
                _isStarted = true;
            }
        }

        //メインカメラの切り替え
        public void MainCameraChange()
        {
            //カメラの切り替わり所要時間の変更
            _mainCam.GetComponent<Cinemachine.CinemachineBrain>().m_DefaultBlend.m_Time = 2;

            //現カメラのフォロー解除
            _currentCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Follow = null;
            _currentCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_LookAt = null;

            if (_currentCam == _camA)
            {
                //カメラの優先度切り替え
                _camA.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = LOW;
                _camB.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = HI;
                //現状のカメラ切り替え
                _currentCam = _camB;
                //切り替えフラグON
                _isChanging = true;

            }
            else
            {
                //カメラの優先度切り替え
                _camA.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = HI;
                _camB.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = LOW;
                //現状のカメラ切り替え
                _currentCam = _camA;
                //切り替えフラグON
                _isChanging = true;
            }

        }

        //ゴール表示カメラの優先度変更
        void StartCamMove()
        {
            _camGoal.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = 5;
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
            //カメラ切り替え中にセッティング変更
            if (_isChanging)
            {
                _camChangeTime -= Time.deltaTime;

                if (_camChangeTime <= 0)
                {
                    if (_currentCam == _camB)
                    {
                        //疑似カメラの設定
                        CameraSetting(_camA, _camB);
                    }
                    else
                    {
                        //疑似カメラの設定
                        CameraSetting(_camB, _camA);
                    }
                    _isChanging = false;
                    _camChangeTime = _camChangeTimeLimit;
                }
            }
        }

        //カメラの初期設定
        private void InitCamera()
        {
            //メインカメラ取得
            _mainCam = Camera.main.gameObject;
            //子要素の疑似カメラ取得
            Cinemachine.CinemachineVirtualCamera[] _camArray = transform.GetComponentsInChildren<Cinemachine.CinemachineVirtualCamera>();
            //各疑似カメラ取得
            _camA = _camArray[0].gameObject;
            _camB = _camArray[1].gameObject;
            _camGoal = _camArray[2].gameObject;
            //疑似カメラ詳細設定
            //疑似カメラAセッティング
            _camA.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Follow = _player.transform;
            _camA.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_LookAt = _player.transform;
            //疑似カメラBセッティング
            _camB.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Follow = null;
            _camB.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_LookAt = null;
            //ゴールカメラセッティング
            _camGoal.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Follow = _goal.transform;
            _camGoal.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_LookAt = _goal.transform;
            //ゴール位置カメラの優先度設定（高い程優先される）
            _camGoal.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = 30;
        }
    }
}