using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Play
{
    public class PauseScreen : ScreenBase
    {
        #region item

        public enum Item
        {
            Resume = 0,
            Reset,
            Title,
            length,
        }

        struct ItemData
        {
            public System.Action func;
            public string text;
            public bool isPopup;

            public ItemData(string text, System.Action func, bool pop = false)
            {
                this.func = func;
                this.text = text;
                this.isPopup = pop;
            }
        }

        private ItemData[] _itemData = null;

        private void SetItemFunc()
        {
            // データ作成
            _itemData = new ItemData[(int)Item.length];

            _itemData[(int)Item.Resume] = new ItemData("", () =>
            {
                Play.InGameManager.Instance.GamePause(false);
            });

            _itemData[(int)Item.Reset] = new ItemData("ステージをリセットしますか？", () =>
            {
                // リロード
                Time.timeScale = 1.0f;
                Play.InGameManager.Instance.GameReLoad();
            }, true);


            _itemData[(int)Item.Title] = new ItemData("タイトルに戻りますか？", () =>
            {
                Time.timeScale = 1.0f;
                InGameManager.Instance.BackMain("Select");
            }, true);
        }

        private IEnumerator ItemCorutine(ItemData data)
        {
            if (_popUP) yield break;

            _popUP = true;

            bool result = true;

            if (data.isPopup)
            {
                result = false;

                var load = Resources.LoadAsync("PopUp");

                yield return new WaitWhile(() => !load.isDone);

                var obj = load.asset as GameObject;
                var popObj = Instantiate(obj);
                var pop = popObj.GetComponent<PopUp>();

                yield return StartCoroutine(pop.ShowPopUp(data.text, (flag) => result = flag));
            }

            if (result)
            {
                // 処理
                data.func();
            }

            _popUP = false;
        }

        #endregion

        [SerializeField]
        private Arrow _arrow = null;

        private bool _popUP = false;

        /// <summary>
        /// 設定
        /// </summary>
        public void SetUp()
        {
            base.SetUp();

            if (!IsCreated()) return;

            for (int i = 0; i < (int)Item.length; i++)
            {
                var offSet = _popOffSet;
                var posX = 0.0f;

                var posY = i * (_panelRect.y + offSet.y);
                var pos = new Vector3(_initPos.x - posX, _initPos.y - posY, _initPos.z);

                var obj = CreatePanel(pos, i);

                // テキスト変更
                var text = obj.GetComponentInChildren<Text>();
                text.text = Enum.ToObject(typeof(Item), i).ToString();
            }

            // 初期選択項目
            _selectIndex = 0;
            SelectChange(0);

            // 処理の設定
            SetItemFunc();
        }

        /// <summary>
        /// 更新
        /// </summary>
        void Update()
        {
            if (_popUP) return;

            var controller = GameController.Instance;
            if (controller.GetConnectFlag()) ControllerInput(controller);
            else KeyInput();
        }

        /// <summary>
        /// キー入力
        /// </summary>
        private void KeyInput()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                SelectChange(-1);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                SelectChange(1);
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                StartCoroutine(ItemCorutine(_itemData[_selectIndex]));
            }
        }

        /// <summary>
        /// コントローラー入力
        /// </summary>
        /// <param name="con"></param>
        private void ControllerInput(GameController con)
        {
            if (con.MoveDown(Direction.Front))
            {
                SelectChange(-1);
            }

            if (con.MoveDown(Direction.Back))
            {
                SelectChange(1);
            }

            if (con.ButtonDown(Button.A))
            {
                StartCoroutine(ItemCorutine(_itemData[_selectIndex]));
            }
        }

        private void SelectChange(int num)
        {
            var max = _panelList.Count - 1;
            _selectIndex += num;
            if (_selectIndex < 0) _selectIndex = max;
            else if (max < _selectIndex) _selectIndex = 0;

            var image = _panelList[_selectIndex];
            var pos = _arrow.Initpos;
            pos.y = image.transform.localPosition.y;
            _arrow.SetPos(pos);
        }
    }
}