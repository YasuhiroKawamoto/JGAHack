using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashing : MonoBehaviour {

    private SpriteRenderer _image;

    //
    public float _speed = 2.0f;

    private float _time = 0.0f;

    // Use this for initialization
    void Start () {

        _image = this.gameObject.GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {

        _image.color = GetAlphaColor(_image.color);

    }

    //Alpha値を更新してColorを返す
    private Color GetAlphaColor(Color color)
    {
        color.a = Mathf.Sin(Time.time * 2) * 1.0f + 0.5f;
        Debug.Log(_time);
        Debug.Log(color.a);
        return color;
    }

}
