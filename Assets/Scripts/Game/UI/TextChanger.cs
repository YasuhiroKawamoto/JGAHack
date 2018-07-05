using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextChanger : MonoBehaviour {


    [SerializeField]
    GameObject _textPanel;

	// Use this for initialization
	void Start () {
        _textPanel = GameObject.Find("TextPanel");
    }
	
	// Update is called once per frame
	void Update () {


        if (Input.GetMouseButtonDown(1))
        {
            _textPanel.GetComponent<Message>().SetMessagePanel("死ね");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            _textPanel.GetComponent<Message>().SetMessagePanel("生きる！！");
        }

	}
}
