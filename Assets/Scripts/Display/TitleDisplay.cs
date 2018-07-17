using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

using Util.Display;

namespace Main
{
    public sealed class TitleDisplay : DisplayBase
    {
        // ステージセレクトディスプレイ
        [SerializeField]
        private SelectDisplay _selectDisplay = null;

        // 携帯UI
        [SerializeField]
        private Image _phoneImage = null;

        [SerializeField]
        private float _transTime = 1.0f;

        [SerializeField]
        private Text _pressStart = null;
        private Tween _startTween = null;

        public override IEnumerator Enter()
        {
            //マウスカーソル非表示
            Cursor.visible = false;
            //強制フルスクリーン
            Screen.SetResolution(1920, 1080, Screen.fullScreen);
            _phoneImage.transform.DOLocalMove(new Vector3(0.0f, 0.0f, 0.0f), _transTime).SetEase(Ease.OutElastic);
            _phoneImage.transform.DOScale(new Vector3(1.0f, 1.2f, 1.0f), _transTime).SetEase(Ease.OutElastic);
            _phoneImage.transform.DOLocalRotate(new Vector3(0.0f, 0.0f, 85.0f), _transTime).SetEase(Ease.OutElastic);

            var button = _phoneImage.transform.Find("Button");
            button.transform.DOScale(new Vector3(1.2f, 1.0f, 1.0f), _transTime);

            yield return new WaitForSeconds(_transTime);

            // pressStart点滅
            TextFlashing();
        }

        public override void EnterComplete()
        {
            base.EnterComplete();
        }

        public override void Exit()
        {
            base.Exit();
        }

        /// <summary>
        /// 入力
        /// </summary>
        public override void KeyInput()
        {
#if UNITY_WSA_10_0
			var controller = GameController.Instance;

			if (controller.GetConnectFlag())
			{
				if (controller.ButtonDown(Button.START) ||
					controller.ButtonDown(Button.A))
				{
					StartCoroutine(Change());
				}
			}
			else
			{
				if (Input.GetKeyDown(KeyCode.C))
				{
					StartCoroutine(Change());
				}
			}
#endif
#if UNITY_ANDROID
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(Change());
            }
#endif
        }

        /// <summary>
        /// テキストの点滅
        /// </summary>
        /// <param name="text"></param>
        private void TextFlashing()
        {
            var color = _pressStart.color;
            color.a = 0.1f;
            if (_startTween == null)
            {
                _startTween = _pressStart.DOColor(color, 1.0f).SetLoops(-1, LoopType.Yoyo);
            }
        }

        private IEnumerator Change()
        {
            // SE
            Util.Sound.SoundManager.Instance.PlayOneShot(AudioKey.sy_enter);

#if UNITY_WSA_10_0
            var load = Resources.LoadAsync("PopUp");
            yield return new WaitWhile(() => !load.isDone);

            var obj = load.asset as GameObject;
            var popObj = Instantiate(obj);
            var pop = popObj.GetComponent<PopUp>();

            bool result = false;

            yield return StartCoroutine(pop.ShowPopUp("　　チュートリアルを　　 プレイしますか？", (flag) => result = flag));

            Time.timeScale = 1.0f;

            if (result)
            {
                Play.InGameManager.Destroy();
                TakeOverData.Instance.StageNum = 0;
                // 呼び出しはこれ
                Util.Scene.SceneManager.Instance.ChangeSceneFadeInOut("Game");

                yield break;
            }
#endif
            yield return null;
            DisplayManager.Instance.ChangeDisplay(_selectDisplay);
        }
    }
}