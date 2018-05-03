using System.Collections;
using UnityEngine;

public class Controller : MonoBehaviour
{
    void Update()
    {
        var controllerNames = Input.GetJoystickNames();

        if (controllerNames.Length > 0)
        {
            CheckGamepadInput();
        }
    }

    private void CheckGamepadInput()
    {
        if (Input.GetButtonDown("ChangeDate"))
        {
            // ペーストするデータの切り替え
            Debug.Log("ChangeDate");
        }
        if (Input.GetButtonDown("Copy"))
        {
            // データのコピー
            Debug.Log("Copy");
        }
        if (Input.GetButtonDown("Paste"))
        {
            // データのペースト
            Debug.Log("Paste");
        }

        // ロックオン
        if (Input.GetButtonDown("Lockon CClockwise") && Input.GetButton("Lockon Clockwise") ||
            Input.GetButton("Lockon CClockwise") && Input.GetButtonDown("Lockon Clockwise"))
        {
            // ロックオン解除
            Debug.Log("Release Lockon");
        }
        else if (Input.GetButtonDown("Lockon CClockwise"))
        {
            // ロックオン左回り
            Debug.Log("Lockon CounterClockwise");
        }
        else if (Input.GetButtonDown("Lockon Clockwise"))
        {
            // ロックオン右回り
            Debug.Log("Lockon Clockwise");
        }

        // 左3Dスティック
        if (Input.GetAxis("Axis 1") == 1)
        {
            // 右方向へ移動
            Debug.Log("Move Right");
        }
        if (Input.GetAxis("Axis 1") == -1)
        {
            // 左方向へ移動
            Debug.Log("Move Left");
        }
        if (Input.GetAxis("Axis 2") == 1)
        {
            // 上方向へ移動
            Debug.Log("Move Up");
        }
        if (Input.GetAxis("Axis 2") == -1)
        {
            // 下方向へ移動
            Debug.Log("Move Down");
        }
    }
}