﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyRecovery : MonoBehaviour {

    
    private Image gage;
    private Image gage2;

    //
    private Image exmation;

    //カウント設定
    [SerializeField]
    private float _timeMax = 0;

    //デストロイのカウント
    [SerializeField]
    private float _timeDestroy = 2.0f;
    private float _timeCount = 0.0f;
    //デストロイフラグ
    private bool _destroyFlag = false;
    //スケール
    private Vector3 _scale = new Vector3(0.1f, 0.1f, 0.1f);

    //スケールs
    private Vector3 _scale = new Vector3(0.1f, 0.1f, 0.1f);

<<<<<<< HEAD
    //カラー
    //private Cach


    // Use this for initialization
    void Start ()
    {
=======
    //赤色→緑
    IEnumerator ChangeGageColor()
    {
        while(true)
        {
            //yield return StartCoroutine(ChangeColor(Color.red, 2.5f));
            //yield return StartCoroutine(ChangeColor(Color.green, 2.5f));
        }
        yield break;
    }

    IEnumerator ChangeColor(Color toColor,float duration)
    {
        Color fromColor = gage.color;
        float startTime = Time.time;
        float endTime = Time.time + duration;
        float marginR = toColor.r - fromColor.r;
        float marginG = toColor.g - fromColor.g;
        float marginB = toColor.b - fromColor.b;

        while(Time.deltaTime < endTime)
        {
            fromColor.r = fromColor.r + (Time.deltaTime / duration) * marginR;
            fromColor.g = fromColor.g + (Time.deltaTime / duration) * marginG;
            fromColor.b = fromColor.b + (Time.deltaTime / duration) * marginB;

            gage.color = fromColor;
            yield return 0;
        }

        gage.color = toColor;
        yield break;

    }

	// Use this for initialization
	void Start () {
>>>>>>> b285b70bef37b3a29488a1ff2b8768786c7f121c

        gage = transform.Find("gage").GetComponent<Image>();
        gage2 = transform.Find("gage2").GetComponent<Image>();
        exmation = transform.Find("exclamation").GetComponent<Image>();
<<<<<<< HEAD

        exmation.transform.localScale = Vector3.zero;

=======
        exmation.transform.localScale = Vector3.zero;
>>>>>>> b285b70bef37b3a29488a1ff2b8768786c7f121c
        gage.enabled = true;
        gage2.enabled = true;

        gage.color = Color.red;

        StartCoroutine(ChangeColor(Color.green, _timeMax));
       
    }

	
	// Update is called once per frame
	void Update () {

        gage.fillAmount += Time.deltaTime / _timeMax;
        //Debug.Log(gage.fillAmount);
		
        if(gage.fillAmount >= 1)
        {
            Show();
        }

        if (_destroyFlag)
        {

            _timeCount += Time.deltaTime;

            if (exmation.transform.localScale.x < 1.0f)
            {
                exmation.transform.localScale += _scale;
<<<<<<< HEAD
                //Debug.Log(exmation.transform.localScale);
=======
>>>>>>> b285b70bef37b3a29488a1ff2b8768786c7f121c
            }

            if (_timeCount > _timeDestroy)
            {
                Destroy(gameObject);
            }
        }

    }

    public void Show()
    {
        //ゲージを非表示
        gage.enabled = false;
        gage2.enabled = false;
        exmation.enabled = true;
        _destroyFlag = true;
    }

    void SetTime(float time)
    {
        _timeMax = time;
    }

}
