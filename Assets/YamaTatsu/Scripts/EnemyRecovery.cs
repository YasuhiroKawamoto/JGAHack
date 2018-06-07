using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyRecovery : MonoBehaviour {

    private Image gage;
   
    //カウント設定
    [SerializeField]
    private float _timeMax = 0;

	// Use this for initialization
	void Start () {

        gage = transform.Find("gage").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {

        gage.fillAmount += Time.deltaTime / _timeMax;
        //Debug.Log(gage.fillAmount);
		
	}
}
