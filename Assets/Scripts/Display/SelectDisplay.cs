using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util.Display
{
    public sealed class SelectDisplay : DisplayBase
    {
        // タイトルディスプレイ
        [SerializeField]
        private TitleDisplay _titleDisplay = null;

        public override IEnumerator Enter()
        {
            yield return null;
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

        }
    }
}