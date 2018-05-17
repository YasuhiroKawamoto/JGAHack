using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Noise : MonoBehaviour {

    IEnumerator GeneratePulseNoise()
    {
        for (int i = 0; i <= 180; i += 30)
        {
            //歪みの変更
            GetComponent<Image>().material.SetFloat("_Amount", 0.2f * Mathf.Sin(i * Mathf.Deg2Rad));
            yield return null;
        }

    }

    void Update()
    {
        StartCoroutine(GeneratePulseNoise());
    }

}
