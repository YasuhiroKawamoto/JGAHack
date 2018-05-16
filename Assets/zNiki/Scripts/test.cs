using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    Play.GameController con;

    // Use this for initialization
    void Start () {
        con = Play.GameController.Instance;
    }
	
	// Update is called once per frame
	void Update () {
        if (con.GetConnectFlag())
        {
            if (con.ButtonDown(Play.Button.A))
            {
                Debug.Log("A");
            }
            if (con.ButtonDown(Play.Button.B))
            {
                Debug.Log("B");
            }
            if (con.ButtonDown(Play.Button.X))
            {
                Debug.Log("X");
            }
            if (con.ButtonDown(Play.Button.Y))
            {
                Debug.Log("Y");
            }
            if (con.ButtonDown(Play.Button.L1))
            {
                Debug.Log("L1");
            }
            if (con.ButtonDown(Play.Button.R1))
            {
                Debug.Log("R1");
            }
            if (con.ButtonDown(Play.Button.BACK))
            {
                Debug.Log("BACK");
            }
            if (con.ButtonDown(Play.Button.START))
            {
                Debug.Log("START");
            }
            if (con.ButtonDown(Play.Button.L3))
            {
                Debug.Log("L3");
            }
            if (con.ButtonDown(Play.Button.R3))
            {
                Debug.Log("R3");
            }

            // バグになる呼び出し
            if (con.ButtonDown(Play.Button.R3 + 1))
            {
                Debug.Log("R3");
            }

            if (con.Move(Play.Direction.UP))
            {
                Debug.Log("UP");
            }
            if (con.Move(Play.Direction.DOWN))
            {
                Debug.Log("DOWN");
            }
            if (con.Move(Play.Direction.LEFT))
            {
                Debug.Log("LEFT");
            }
            if (con.Move(Play.Direction.RIGHT))
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
