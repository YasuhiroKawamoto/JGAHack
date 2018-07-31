using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play.Tutrial
{
    public class TutrialPlayer : Player
    {
        override protected Vector3 TouchControl()
        {
            Vector3 tryMove = Vector3.zero;
            var tutrial = TutrialManager.Instance;

            if (tutrial.CanMove())
            {
                _playerController.KeyInput();

                tryMove = _playerController.Velocity;
            }
            else
            {
                _playerController.EndPunipuni();
                _playerController.Velocity = Vector3.zero;
            }

            return tryMove;
        }
    }
}