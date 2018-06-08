using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    private bool _isUseAxis = false;

    private Direction _prevDir;

    // Use this for initialization
    void Start()
    {
        CheckConnect();
    }

    public bool GetConnectFlag()
    {
        CheckConnect();
        CheckUseAxis();

        return _connected;
    }

    /// <summary>
    /// コントローラの名前取得を利用した接続確認
    /// </summary>
    private void CheckConnect()
    {
        var names = Input.GetJoystickNames();
        string controllerNames = string.Empty;
        if (names.Any())
        {
            controllerNames = names[0];
        }

        if (controllerNames.Length > 0)
        {
            _connected = true;
        }
        else
        {
            _connected = false;
        }
    }

    private void CheckUseAxis()
    {
        if (!Move(_prevDir))
        {
            _isUseAxis = false;
        }
    }

    /// <summary>
    /// 指定されたボタンが押されているか
    /// </summary>
    /// <param name="b">判定したいボタン</param>
    /// <returns>true:押されている false:押されていない</returns>
    public bool ButtonDown(Button b)
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
                if (Input.GetAxis("StickVertical") >= 0.25f || Input.GetAxis("CrossButtonVertical") >= 0.25f)
                {
                    return true;
                }
                break;

            case Direction.Back:
                if (Input.GetAxis("StickVertical") <= -0.25f || Input.GetAxis("CrossButtonVertical") <= -0.25f)
                {
                    return true;
                }
                break;

            case Direction.Left:
                if (Input.GetAxis("StickHorizontal") <= -0.25f || Input.GetAxis("CrossButtonHorizontal") <= -0.25f)
                {
                    return true;
                }
                break;

            case Direction.Right:
                if (Input.GetAxis("StickHorizontal") >= 0.25f || Input.GetAxis("CrossButtonHorizontal") >= 0.25f)
                {
                    return true;
                }
                break;

            default:
                break;
        }
        return false;
    }

    public bool MoveDown(Direction d)
    {
        if (!_isUseAxis && Move(d))
        {
            _isUseAxis = true;
            _prevDir = d;
            return true;
        }

        return false;
    }
}