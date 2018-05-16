using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play
{
    public enum Button
    {
        A,
        B,
        X,
        Y,
        L1,
        R1,
        BACK,
        START,
        L3,
        R3,
    }

    public class GameController : Util.SingletonMonoBehaviour<GameController>
    {
        private bool _connected = false;

        // Use this for initialization
        void Start()
        {
            CheckConnect();
        }

        public bool GetConnectFlag()
        {
            CheckConnect();

            return _connected;
        }

        /// <summary>
        /// コントローラの名前取得を利用した接続確認
        /// </summary>
        private void CheckConnect()
        {
            var controllerNames = Input.GetJoystickNames()[0];

            if (controllerNames.Length > 0)
            {
                _connected = true;
            }
            else
            {
                _connected = false;
            }
        }

        /// <summary>
        /// 指定されたボタンが押されているか
        /// </summary>
        /// <param name="b">判定したいボタン</param>
        /// <returns>true:押されている false:押されていない</returns>
        public bool ButtonDown(Play.Button b)
        {
            string button = b.ToString();

            try
            {
                if (Input.GetButtonDown(button))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        public bool Move(Direction d)
        {

            switch (d)
            {
                case Direction.Front:
                    if (Input.GetAxis("StickVertical") == 1 || Input.GetAxis("CrossButtonVertical") == 1)
                    {
                        return true;
                    }
                    break;

                case Direction.Back:
                    if (Input.GetAxis("StickVertical") == -1 || Input.GetAxis("CrossButtonVertical") == -1)
                    {
                        return true;
                    }
                    break;

                case Direction.Left:
                    if (Input.GetAxis("StickHorizontal") == -1 || Input.GetAxis("CrossButtonHorizontal") == -1)
                    {
                        return true;
                    }
                    break;

                case Direction.Right:
                    if (Input.GetAxis("StickHorizontal") == 1 || Input.GetAxis("CrossButtonHorizontal") == 1)
                    {
                        return true;
                    }
                    break;

                default:
                    break;
            }
            return false;
        }
    }
}