using System.Collections;
using System.Collections.Generic;
using UnityEngine;




//カメラマネージャ



public class CameraManager : MonoBehaviour
{
    //プレイヤー状態確認用
    [SerializeField]
    private GameObject _player;
    //メインのプレイヤー追従カメラ
    [SerializeField]
    private GameObject _mainCam;
    //ステージ開始時のゴール→プレイヤー誘導カメラ
    [SerializeField]
    private GameObject _startCam;
    //プレイヤー死亡時のプレイヤー死亡地点→スタート地点カメラ
    [SerializeField]
    private GameObject _deadCam;





    // Use this for initialization
    void Start()
    {
        //プレイヤー取得
        _player = GameObject.Find("Player");
        //ステージ開始カメラ起動
        StageStartCameraMove();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO　プレイヤー追従カメラの起動テスト
        if (Input.GetMouseButtonDown(0))
        {
            MainCameraMove();
        }

        //TODO　死亡時カメラの起動テスト
        if (Input.GetMouseButtonDown(1))
        {
            DeadCameraMove();
        }

    }

    //ステージ開始時演出用カメラ起動
    public void StageStartCameraMove()
    {
        //メイン追従カメラのOFF
        _mainCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().enabled = false;
        //ゴールからプレイヤーまでの移動カメラON
        _startCam.GetComponent<Cinemachine.CinemachineBlendListCamera>().enabled = true;
        //死亡位置からスタート地点カメラのOFF
        _deadCam.GetComponent<Cinemachine.CinemachineBlendListCamera>().enabled = false;
    }

    //メインのプレイヤー追従カメラの起動
    public void MainCameraMove()
    {
        //メイン追従カメラのOFF
        _mainCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().enabled = true;
        //ゴールからプレイヤーまでの移動カメラON
        _startCam.GetComponent<Cinemachine.CinemachineBlendListCamera>().enabled = false;
        //死亡位置からスタート地点カメラのOFF
        _deadCam.GetComponent<Cinemachine.CinemachineBlendListCamera>().enabled = false;
    }

    //死亡時演出用カメラの起動
    public void DeadCameraMove()
    {
        //メイン追従カメラのOFF
        _mainCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().enabled = false;
        //ゴールからプレイヤーまでの移動カメラON
        _startCam.GetComponent<Cinemachine.CinemachineBlendListCamera>().enabled = false;
        //死亡位置からスタート地点カメラのOFF
        _deadCam.GetComponent<Cinemachine.CinemachineBlendListCamera>().enabled = true;
    }

}
