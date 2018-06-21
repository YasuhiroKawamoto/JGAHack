using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseContollor2 : MonoBehaviour {

    [SerializeField]
    private float _num = 0.1f;

    [SerializeField]
    private int _speed = 30;

    // Use this for initialization
    IEnumerator GeneratePulseNoise()
    {
        for (int i = 0; i <= 180; i += _speed)
        {
            //歪みの変更
            GetComponent<Image>().material.SetFloat("_Amount", _num * Mathf.Sin(i * Mathf.Deg2Rad));
            yield return null;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(GeneratePulseNoise());
        }

    }
}
