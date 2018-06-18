using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Extensions;
using System.Linq;


namespace Play
{
    public class PlayerDataPanel :MonoBehaviour
    {

        //アイコンセット
        [SerializeField, ReadOnly]
        private GameObject[] _icons;
        //変更用アイコンリスト
        [SerializeField, ReadOnly]
        private Sprite[] _iconImages;

        private void Awake()
        {
            //子供のセット
            _icons = transform.GetAllChild();
            //リソースからアイコンゲット
            _iconImages = EffectManager.Instance.GetConsoleIcons();
        }
        // Use this for initialization
        void Start()
        {
            IconReset();
        }

        public void SetIcon(int slotNum, CONSOLE_ICON_ID id)
        {
            _icons[slotNum].GetComponent<Image>().color = Color.white;
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
                    //_icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.Nodata];
                    _icons[slotNum].GetComponent<Image>().color = Color.clear;
                    _icons[3].GetComponent<Image>().color = Color.clear;
                    break;

                case CONSOLE_ICON_ID.RideOn:
                    _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.RideOn];
                    _icons[3].GetComponent<Image>().color = Color.white;
                    break;

                case CONSOLE_ICON_ID.RideOnRock:
                    _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.RideOnRock];
                    break;

                case CONSOLE_ICON_ID.Shot:
                    _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.Shot];
                    _icons[3].GetComponent<Image>().color = Color.white;
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
                    _icons[3].GetComponent<Image>().color = Color.white;
                    break;

                case CONSOLE_ICON_ID.TackleRock:
                    _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.TackleRock];
                    break;

                case CONSOLE_ICON_ID.Updown:
                    _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.Updown];
                    break;

                default:
                    _icons[slotNum].GetComponent<Image>().sprite = null;
                    break;
            }
        }

        public void IconReset()
        {         
            for (int i = 0; i < _icons.Length; i++)
            {
                _icons[i].GetComponent<Image>().color = Color.clear;
            }        
        }

        public void ShowSlot(int iconNam ,bool flag)
        {
            if (flag == true)
            {
                _icons[iconNam].GetComponent<Image>().color = Color.white;
            }
            else
            {

                _icons[iconNam].GetComponent<Image>().color = Color.clear;
            }           
        }
    }
}