using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashing : MonoBehaviour {

    private Image _image;

    //
    public float _speed = 2.0f;

    private float _time = 0.0f;

    [SerializeField]
    private float _setWidth = 1.0f;

    private RectTransform rect;

    // Use this for initialization
    void Start () {

        _image = this.gameObject.GetComponent<Image>();

        rect = this.gameObject.GetComponent<RectTransform>();

        rect.sizeDelta = new Vector2(100 * _setWidth, 100);

    }
	
	// Update is called once per frame
	void Update () {

        _image.color = GetAlphaColor(_image.color);

        rect.sizeDelta = new Vector2(100 * _setWidth, 100);

    }

    //Alpha値を更新してColorを返す
    private Color GetAlphaColor(Color color)
    {
        _time += Time.deltaTime * 5.0f * _speed;
        color.a = Mathf.Sin(_time) * 0.5f + 0.5f;

        return color;
    }

    //横幅の変更
    public void SetWidth(float width)
    {
        _setWidth = width;
    }

}
