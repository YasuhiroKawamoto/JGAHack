using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Extensions;
using System.Linq;


namespace Play
{
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
        [SerializeField, Range(0, 255)]
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
        [SerializeField, ReadOnly]
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
            _iconImages = EffectManager.Instance.GetConsoleIcons();
    
    }


        // Use this for initialization
        void Start() {


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
        void Update() {

            image.color = new Color(255, 255, 255, _alfa);

            //flagがtrueなら
            if (_isOpen)
            {
                //拡大
                if (rect.localScale.x <= _windowSize.x)
                {
                    rect.localScale += new Vector3(_speed, 0, 0);

                }

                if (rect.localScale.y <= _windowSize.y)
                {
                    rect.localScale += new Vector3(0, _speed, 0);

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


        //Consoleのアイコン変更



        //アイコン変更
        public void SetIcon(int slotNum, CONSOLE_ICON_ID id)
        {
            switch (id)
            {
                case CONSOLE_ICON_ID.Direction_Down:
                    _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.Direction_Down];
                    break;

                case CONSOLE_ICON_ID.Direction_Left:
                    _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.Direction_Left];
                    break;

                case CONSOLE_ICON_ID.Direction_Right:
                    _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.Direction_Right];
                    break;

                case CONSOLE_ICON_ID.Direction_Up:
                    _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.Direction_Up];
                    break;

                case CONSOLE_ICON_ID.Nodata:
                    _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.Nodata];
                    break;

                case CONSOLE_ICON_ID.RideOn:
                    _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.RideOn];
                    break;

                case CONSOLE_ICON_ID.RideOnRock:
                    _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.RideOnRock];
                    break;

                case CONSOLE_ICON_ID.Shot:
                    _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.Shot];
                    break;

                case CONSOLE_ICON_ID.ShotRock:
                    _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.ShotRock];
                    break;

                case CONSOLE_ICON_ID.Side:
                    _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.Side];
                    break;

                case CONSOLE_ICON_ID.Stop:
                    _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.Stop];
                    break;

                case CONSOLE_ICON_ID.Tackle:
                    _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.Tackle];
                    break;

                case CONSOLE_ICON_ID.TackleRock:
                    _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.TackleRock];
                    break;

                case CONSOLE_ICON_ID.Updown:
                    _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.Updown];
                    break;

                default:
                    _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.Nodata];
                    break;
            }
        }
    }
}