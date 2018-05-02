using UnityEngine;

public class Controller : MonoBehaviour
{

    public void CheckInput () {
        
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Press Fire1 Button");
        }
        if (Input.GetButtonDown("Fire3"))
        {
            Debug.Log("Press Fire3 Button");
        }
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Press Jump Button");
        }

        // ロックオン
        if (Input.GetButtonDown("LockOnLeft"))
        {
            Debug.Log("Press LockOnLeft Button");
        }
        if (Input.GetButtonDown("LockOnRight"))
        {
            Debug.Log("Press LockOnRight Button");
        }

        // 左3Dスティック
        if (Input.GetAxis("Axis 1") == 1)
        {
            Debug.Log("Move Right");
        }
        if (Input.GetAxis("Axis 1") == -1)
        {
            Debug.Log("Move Left");
        }
        if (Input.GetAxis("Axis 2") == 1)
        {
            Debug.Log("Move Up");
        }
        if (Input.GetAxis("Axis 2") == -1)
        {
            Debug.Log("Move Down");
        }

        // 左十字キー
        if (Input.GetAxis("Axis 5") == 1)
        {
            Debug.Log("Move Right");
        }
        if (Input.GetAxis("Axis 5") == -1)
        {
            Debug.Log("Move Left");
        }
        if (Input.GetAxis("Axis 6") == 1)
        {
            Debug.Log("Move Up");
        }
        if (Input.GetAxis("Axis 6") == -1)
        {
            Debug.Log("Move Down");
        }
    }
}
