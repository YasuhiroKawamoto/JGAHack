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

        public enum CONSOLE_ICON
        {
            Direction_Down,
            Direction_Left,
            Direction_Right,
            Direction_Up,
            Nodata,
            RideOn,
            Shot,
            Side,
            Stop,
            Tackle,
            Updown
        }

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

        public void SetIcon(int slotNum, string typeName ,Direction dir)
        {
            if (typeName == "DiectionTest")
            {
                switch (dir)
                {
                    case Direction.Back:
                        _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON.Direction_Down];
                        break;

                    case Direction.Left:
                        _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON.Direction_Left];
                        break;

                    case Direction.Right:
                        _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON.Direction_Right];
                        break;

                    case Direction.Front:
                        _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON.Direction_Up];
                        break;

                    default:

                        break;
                }
            }

            if (typeName == "TestShot")
            {
                _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON.Shot];
            }

            if (typeName == "Tackle")
            {
                _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON.Tackle];
            }

            if (typeName == "RideFloor")
            {
                _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON.RideOn];
            }


            if (typeName == "SideMove")
            {
                _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON.Side];
            }

            if (typeName == "Stay")
            {
                _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON.Stop];
            }

            if (typeName == "UpDownMove")
            {

                _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON.Updown];
            }


            if (typeName == "Nodata")
            {

                _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON.Nodata];
            }
        }

        public void IconReset()
        {
          
            for (int i = 0; i < _icons.Length; i++)
            {
                _icons[i].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON.Nodata];
            }        
        }

    }
}