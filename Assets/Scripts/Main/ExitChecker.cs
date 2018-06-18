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
        var controller = GameController.Instance;

        if (controller.GetConnectFlag())
        {
            if (controller.ButtonDown(Button.BACK))
            {
                StartCoroutine(Exit());
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StartCoroutine(Exit());
            }
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

        yield return StartCoroutine(pop.ShowPopUp("ゲームを終了しますか？", (flag) => result = flag,
        () =>
        {
            var con = GameController.Instance;
            if (con.GetConnectFlag())
            {
                if (con.ButtonDown(Button.BACK)) return true;
            }
            else if (Input.GetKeyDown(KeyCode.Escape)) return true;
            return false;
        }));
        Time.timeScale = 1.0f;

        if (result) Application.Quit();
#if UNITY_EDITOR
        if (result) EditorApplication.isPlaying = false;
#endif
    }
}
