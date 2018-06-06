using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Play.Element;

namespace Play.Tutrial
{
    public class TutrialSelector : ElementSelector
    {
        [SerializeField]
        private ElementObject _step2Object = null;
        [SerializeField]
        private ElementObject _step4Object = null;
        [SerializeField]
        private ElementObject _step7Object = null;
        [SerializeField]
        private ElementObject _step10Object = null;

        /// <summary>
        /// オブジェクトをターゲット
        /// </summary>
        override protected void TargetObject(ElementObject obj)
        {
            var manager = InGameManager.Instance;
            var messenger = manager.Messenger;

            var tutrial = TutrialManager.Instance;
            if (tutrial.CanTarget())
            {
                var step = tutrial.Step;
                switch (step)
                {
                    case 1:
                        base.TargetObject(_step2Object);
                        tutrial.NextStep();
                        break;
                    case 3:
                        base.TargetObject(_step4Object);
                        tutrial.NextStep();
                        break;
                    case 6:
                        base.TargetObject(_step7Object);
                        tutrial.NextStep();
                        break;
                    case 9:
                        base.TargetObject(_step10Object);
                        tutrial.NextStep();
                        break;
                }
            }
        }

        /// <summary>
        /// オブジェクトを選択
        /// </summary>
        override protected void SelectObject()
        {
            var manager = InGameManager.Instance;
            var messenger = manager.Messenger;

            var tutrial = TutrialManager.Instance;

            if (tutrial.CanCopy())
            {
                base.SelectObject();
                // 次に移行
                tutrial.NextStep();
            }
        }

        /// <summary>
        /// 要素の移動
        /// </summary>
        /// <param name="selectObj"></param>
        override protected void MoveElement(ElementObject selectObj)
        {
            var manager = InGameManager.Instance;
            var messenger = manager.Messenger;

            var tutrial = TutrialManager.Instance;

            if (tutrial.CanPaste())
            {
                base.MoveElement(selectObj);
                // 次に移行
                tutrial.NextStep();
            }
        }

    }
}