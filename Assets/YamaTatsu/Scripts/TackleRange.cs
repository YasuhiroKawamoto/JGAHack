using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TackleRange : MonoBehaviour {

    private float _time = 0.0f;

    private float _intervalTime = 1.0f;

    private bool _flag = true;

    [SerializeField]
    private float _cnt;

    GameObject _tackle;
    SpriteRenderer _spriteRenderer;

   
    // Use this for initialization
    void Start () {

        _tackle = transform.Find("TackleRange").gameObject;

        _spriteRenderer = transform.Find("TackleRange").GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {

        _time += Time.deltaTime;

        if(_time > 1.0f)
        {
            _intervalTime += Time.deltaTime;
            if(_intervalTime >=_cnt)
            {
                _intervalTime = 0.0f;
                _flag = !_flag;

                if(_flag)
                {
                    _tackle.SetActive(true);
                }
                else
                {
                    _tackle.SetActive(false);
                }

            }
        }
        
    }
    
}
