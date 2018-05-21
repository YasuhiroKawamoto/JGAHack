using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

namespace Play
{
    //カメラマネージャ
    public class CameraManager : MonoBehaviour
    {
        private const int MUST = 30;
        private const int HI = 15;
        private const int LOW = 10;
        private const int LOWEST = 5;
        //プレイヤー
        [SerializeField]
        private GameObject _player;
        public GameObject Player
        {
            get { return _player; }
            set { _player = value; }
        }

        //ゴール
        [SerializeField]
        private GameObject _goal;
        public GameObject Goal
        {
            get { return _goal; }
            set { _goal = value; }
        }

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
        //ステージカメラ
        [SerializeField, ReadOnly]
        private GameObject _camStage;
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
        //固定カメラかどうか？
        [SerializeField]
        private bool _isFixed = false;
        //演出が終わったか？
        [SerializeField,ReadOnly]
        private bool _isEndProduction = false;




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
            else if (_currentCam == _camB)
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
            _currentCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = HI;
            _camGoal.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = LOWEST;
            
        }

        //カメラの設定変更
        void CameraSetting(GameObject oldCam, GameObject nextCam)
        {
            //フォロー対象をプレイヤーにセット
            nextCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Follow = Player.transform;
            nextCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_LookAt = Player.transform;

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
                //切り替えディレイ
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
        public  IEnumerator InitCamera()
        {
            //メインカメラ取得
            _mainCam = Camera.main.gameObject;
            //子要素の疑似カメラ取得
            Cinemachine.CinemachineVirtualCamera[] _camArray = transform.GetComponentsInChildren<Cinemachine.CinemachineVirtualCamera>();
            //各疑似カメラ取得
            _camA = _camArray[0].gameObject;
            _camB = _camArray[1].gameObject;
            _camGoal = _camArray[2].gameObject;
            _camStage = _camArray[3].gameObject;
            //疑似カメラ詳細設定
            //疑似カメラAセッティング
            _camA.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Follow = Player.transform;
            _camA.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_LookAt = Player.transform;
            //疑似カメラBセッティング
            _camB.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Follow = null;
            _camB.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_LookAt = null;
            //ゴールカメラセッティング
            _camGoal.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Follow = Goal.transform;
            _camGoal.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_LookAt = Goal.transform;

            //固定カメラモードなら
            if (_isFixed)
            {
                //ステージ位置カメラの優先度設定（高い程優先される）
                _camStage.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = MUST;
                //ゴール位置カメラの優先度設定（初期演出用）
                //_camGoal.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = MUST+1;
                //現在カメラのセット
                _currentCam = _camStage;
            }
            else
            {
                //ゴール位置カメラの優先度設定（初期演出用）
                _camGoal.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = MUST;
                //ステージ位置カメラの優先度設定（高い程優先される）
                _camStage.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = LOWEST;
                //現在カメラのセット
                _currentCam = _camA;
            }


            //カメラ切り替え時間の設定
            _camChangeTime = _camChangeTimeLimit;
            //カメラのセット
            CameraSetting(_camB, _camA);
            //カメラの切り替わり所要時間の変更
            _mainCam.GetComponent<Cinemachine.CinemachineBrain>().m_DefaultBlend.m_Time = 3;
            //シェイクカメラリセット
            GetComponent<CameraShake>().CameraReset();

            //開始時のみ呼ばれる
            if (!_isStarted)
            {
                //開始時のカメラ挙動
                StartCamMove();
                _isStarted = true;
            }
            //カメラ遷移時間分待機
             yield return new WaitForSeconds(_mainCam.GetComponent<Cinemachine.CinemachineBrain>().m_DefaultBlend.m_Time);
            //演出終わりフラグON
            _isEndProduction = true;

        }

        //現在の疑似カメラの情報を送る
        public Cinemachine.CinemachineVirtualCamera GetCullentVCam()
        {
            return _currentCam.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        }

        public void ShakeCamera()
        {
            //カメラ揺らし
            GetComponent<Play.CameraShake>().ShakeCamera();
        }


        //初期演出終了フラグ検知
        public bool GetEndProduction()
        {
            return _isEndProduction;
        }

        //カメラ切り替え中か？
        public bool GetChangeCam()
        {
            return _isChanging;
        }
    }
}