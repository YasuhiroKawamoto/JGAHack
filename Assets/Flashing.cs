using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashing : MonoBehaviour {

    private SpriteRenderer _image;

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
        color.a = Mathf.Sin(Time.time * 5)+0.8f;
        return color;
    }

}
