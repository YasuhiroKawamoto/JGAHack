using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Extensions;
using System.Linq;

public class ConsoleCon : MonoBehaviour {

    enum PATTERN
    {
        Left,       //左
        Middle,     //真ん中
        Right,      //右
        MiddleDown  //下
    }

   public enum CONSOLE_ICON
    {
        Direction_Down,
        Direction_Left,
        Direction_Right,
        Direction_Up,
        Shot,
        Side,
        Stop,
        Updown
    }

    private RectTransform rect;
    private Image image;
    //α値
    [SerializeField,Range(0,255)]
    private float _alfa = 0;
    //拡縮スピード
    [SerializeField]
    private float _speed = 0.02f;
    [SerializeField]
    private Vector3 _windowSize;

    //trueなら真ん中からfalseなら左下
    [SerializeField]
    private PATTERN _state = PATTERN.Left;
    //開けているかどうか？
    [SerializeField]
    private bool _isOpen = true;
    //アイコンセット
    [SerializeField,ReadOnly]
    private GameObject[] _icons;
    //変更用アイコンリスト
    [SerializeField, ReadOnly]
    private Sprite[] _iconImages;


    private void Awake()
    {

        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        //子供のセット
        _icons = transform.GetAllChild();
        //リソースからアイコンゲット
        _iconImages = Resources.LoadAll<Sprite>("Icons/ConsoleIcon");

    }


    // Use this for initialization
    void Start () {

        ////アイコンの名前
        // List<string> _iconNames = new List<string>();
        //for (int i = 0; i < _iconImages.Length; i++)
        //{
        //    _iconNames.Add(_iconImages[i].name);
        //}
        //ShowListContentsInTheDebugLog(_iconNames);

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
        if (_isOpen)
        {
            //拡大
            if (rect.localScale.x <= _windowSize.x)
            {
                rect.localScale += new Vector3(_speed,0,0);
                
            }

            if (rect.localScale.y <= _windowSize.y)
            {
                rect.localScale += new Vector3(0,_speed, 0);

            }


            _alfa += 0.02f;
        }
        else
        {
            //0より大きければ小さくする
            if (rect.localScale.x >= 0)
            {
                rect.localScale += new Vector3(-_speed, -_speed, 1);
                _alfa -= 0.02f;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    //フラグのセット
    public void SetIsOpen(bool flag)
    {
        _isOpen = flag;
    }


    //アイコン変更
    public void SetIcon(int slotNum, CONSOLE_ICON id)
    {
        switch (id)
        {
            case CONSOLE_ICON.Direction_Down:
                _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON.Direction_Down];
                break;

            case CONSOLE_ICON.Direction_Left:
                _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON.Direction_Left];
                break;

            case CONSOLE_ICON.Direction_Right:
                _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON.Direction_Right];
                break;

            case CONSOLE_ICON.Direction_Up:
                _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON.Direction_Up];
                break;


            case CONSOLE_ICON.Shot:
                _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON.Shot];
                break;

            case CONSOLE_ICON.Side:
                _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON.Side];
                break;

            case CONSOLE_ICON.Stop:
                _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON.Stop];
                break;

            case CONSOLE_ICON.Updown:
                _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON.Updown];
                break;

            default:

                break;
        }
    }

   


    ////テスト用リスト表示
    //public void ShowListContentsInTheDebugLog<T>(List<T> list)
    //{
    //    string log = "";

    //    foreach (var content in list.Select((val, idx) => new { val, idx }))
    //    {
    //        if (content.idx == list.Count - 1)
    //            log += content.val.ToString();
    //        else
    //            log += content.val.ToString() + ", ";
    //    }
    //    Debug.Log(log + "要素数" + list.Count);
    //}

}
