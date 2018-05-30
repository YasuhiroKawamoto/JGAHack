using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleCon : MonoBehaviour {

    enum PATTERN
    {
        Left,       //左
        Middle,     //真ん中
        Right,      //右
        MiddleDown  //下
    }

    private RectTransform rect;
    private Image image;
    //α値
    [SerializeField,Range(0,255)]
    private float _alfa = 0;

    //スピード
    [SerializeField]
    private float _speed = 0.02f;

    //trueなら真ん中からfalseなら左下
    [SerializeField]
    private PATTERN _state = PATTERN.Left;
    //falseなら閉じる
    [SerializeField]
    private bool _flag = true;

    [SerializeField]
    private Vector3 pos;




    // Use this for initialization
    void Start () {

        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        rect.localScale = new Vector3(0, 0, 0);

        image.color = new Color(255, 255, 255, _alfa);

        switch (_state)
        {
            case PATTERN.Left:
                rect.pivot = new Vector2(0, 0);
                break;
            case PATTERN.Middle:
                rect.pivot = new Vector2(0.5f, 0);
                break;
            case PATTERN.Right:
                rect.pivot = new Vector2(1, 0);
                break;
            case PATTERN.MiddleDown:
                rect.pivot = new Vector2(0.5f, 1);
                break;
            default:
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {

        image.color = new Color(255, 255, 255, _alfa);

        //flagがtrueなら
        if (_flag)
        {
            //拡大
            if (rect.localScale.x <= 1)
            {
                rect.localScale += new Vector3(_speed, _speed, 1);
                _alfa += 0.02f;
            }
        }
        else
        {
            //0より大きければ小さくする
            if (rect.localScale.x >= 0)
            {
                rect.localScale += new Vector3(-_speed, -_speed, 1);
                _alfa -= 0.02f;
            }
        }
    }

    //フラグのセット
    void setFlag(bool flag)
    {
        _flag = flag;
    }

}
