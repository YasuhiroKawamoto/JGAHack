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
        _time += Time.deltaTime * 5.0f * _speed;
        color.a = Mathf.Sin(_time) * 0.5f + 0.5f;
        return color;
    }

}
