using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTest : MonoBehaviour
{
    [SerializeField]
    private GameObject controller;


    // Update is called once per frame
    void Update()
    {
        controller.GetComponent<Controller>().CheckInput();
    }
}
