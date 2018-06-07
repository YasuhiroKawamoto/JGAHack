using System.Collections;
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


	// Use this for initialization
	void Start () {

        gage = transform.Find("gage").GetComponent<Image>();
<<<<<<< HEAD
	}
=======
        gage2 = transform.Find("gage2").GetComponent<Image>();
        exmation = transform.Find("exclamation").GetComponent<Image>();
        gage.enabled = true;
        gage2.enabled = true;

    }
>>>>>>> fb32fab6dc0ac4f5f7de011b362065cc46f5388b
	
	// Update is called once per frame
	void Update () {

        gage.fillAmount += Time.deltaTime / _timeMax;
        //Debug.Log(gage.fillAmount);
		
        if(gage.fillAmount >= 1)
        {
            Show();
        }

        if(_destroyFlag)
        {

            _timeCount += Time.deltaTime;

            if(_timeCount > _timeDestroy)
            {
                Destroy(gameObject);
            }
        }

	}

    void Show()
    {
        //ゲージを非表示
        gage.enabled = false;
        gage2.enabled = false;
        exmation.enabled = true;
        _destroyFlag = true;
    }

}
