using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play.Tutrial
{
    public class TutrialPlayer : Player
    {
        override protected Vector3 ControllerControl(GameController con)
        {
            var tutrial = TutrialManager.Instance;
            if (tutrial.CanMove())
            {
                return base.ControllerControl(con);
            }
            return Vector3.zero;
        }

        override protected Vector3 KeyboardControl()
        {
            var tutrial = TutrialManager.Instance;
            if (tutrial.CanMove())
            {
                return base.KeyboardControl();
            }
            return Vector3.zero;
        }
    }
}