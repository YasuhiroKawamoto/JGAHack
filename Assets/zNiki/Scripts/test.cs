using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    GameController con;

    // Use this for initialization
    void Start()
    {
        con = GameController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (con.GetConnectFlag())
        {
            if (con.ButtonDown(Button.A))
            {
                Debug.Log("A");
            }
            if (con.ButtonDown(Button.B))
            {
                Debug.Log("B");
            }
            if (con.ButtonDown(Button.X))
            {
                Debug.Log("X");
            }
            if (con.ButtonDown(Button.Y))
            {
                Debug.Log("Y");
            }
            if (con.ButtonDown(Button.L1))
            {
                Debug.Log("L1");
            }
            if (con.ButtonDown(Button.R1))
            {
                Debug.Log("R1");
            }
            if (con.ButtonDown(Button.BACK))
            {
                Debug.Log("BACK");
            }
            if (con.ButtonDown(Button.START))
            {
                Debug.Log("START");
            }
            if (con.ButtonDown(Button.L3))
            {
                Debug.Log("L3");
            }
            if (con.ButtonDown(Button.R3))
            {
                Debug.Log("R3");
            }

            // バグになる呼び出し
            if (con.ButtonDown(Button.R3 + 1))
            {
                Debug.Log("R3");
            }

            if (con.Move(Direction.Front))
            {
                Debug.Log("UP");
            }
            if (con.Move(Direction.Back))
            {
                Debug.Log("DOWN");
            }
            if (con.Move(Direction.Left))
            {
                Debug.Log("LEFT");
            }
            if (con.Move(Direction.Right))
            {
                Debug.Log("RIGHT");
            }
        }
        else
        {
            Debug.Log("つっかえ！");
        }
    }
}
