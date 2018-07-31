using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Play.Element;

namespace Play.Tutrial
{
    public class TutrialSelector : ElementSelector
    {

        /// <summary>
        /// オブジェクトをターゲット
        /// </summary>
        override protected bool TargetObject(ElementObject obj)
        {
            var manager = InGameManager.Instance;
            var messenger = manager.Messenger;

            var tutrial = TutrialManager.Instance;

            if (tutrial.IsChange) return false;

            if (tutrial.CanTarget())
            {
                var targetObj = tutrial.GetTargetObj();
                if (targetObj == null) return base.TargetObject(obj);
                else
                {
                    base.TargetObject(targetObj);
                    tutrial.NextStep();
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// オブジェクトを選択
        /// </summary>
        override protected void SelectObject(ElementObject obj)
        {
            var manager = InGameManager.Instance;
            var messenger = manager.Messenger;

            var tutrial = TutrialManager.Instance;

            if (tutrial.IsChange) return;

            if (tutrial.CanCopy())
            {
                base.SelectObject(obj);
                // 次に移行
                tutrial.NextStep();
            }
        }

        /// <summary>
        /// 要素の移動
        /// </summary>
        /// <param name="selectObj"></param>
        override protected bool MoveElement(ElementObject selectObj)
        {
            var manager = InGameManager.Instance;
            var messenger = manager.Messenger;

            var tutrial = TutrialManager.Instance;

            if (tutrial.IsChange) return false;

            if (tutrial.CanPaste())
            {
                base.MoveElement(selectObj);
                // 次に移行
                tutrial.NextStep();
                return true;
            }
            return false;
        }
    }
}