using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ExitChecker : MonoBehaviour
{
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(Exit());
        }
    }

    IEnumerator Exit()
    {
        var load = Resources.LoadAsync("PopUp");
        yield return new WaitWhile(() => !load.isDone);

        var obj = load.asset as GameObject;
        var popObj = Instantiate(obj);
        var pop = popObj.GetComponent<PopUp>();

        bool result = false;

        yield return StartCoroutine(pop.ShowPopUp("ゲームを終了しますか？", (flag) => result = flag));

        if (result) Application.Quit();
#if UNITY_EDITOR
        if (result) EditorApplication.isPlaying = false;
#endif
    }
}
