using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util.Display
{
    public sealed class TitleDisplay : DisplayBase
    {
        // ステージセレクトディスプレイ
        [SerializeField]
        private SelectDisplay _selectDisplay = null;

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