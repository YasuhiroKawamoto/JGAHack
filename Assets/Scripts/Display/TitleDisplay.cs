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

        public override IEnumerator Enter()
        {
            _phoneImage.transform.DOLocalMove(new Vector3(0.0f, 0.0f, 0.0f), _transTime).SetEase(Ease.OutElastic);
            _phoneImage.transform.DOScale(new Vector3(5.0f, 2.0f, 1.0f), _transTime).SetEase(Ease.OutElastic);
            _phoneImage.transform.DOLocalRotate(new Vector3(0.0f, 0.0f, -5.0f), _transTime).SetEase(Ease.OutElastic);

            yield return new WaitForSeconds(_transTime);
        }

        public override void EnterComplete()
        {
            base.EnterComplete();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void KeyInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DisplayManager.Instance.ChangeDisplay(_selectDisplay);
            }
        }
    }
}