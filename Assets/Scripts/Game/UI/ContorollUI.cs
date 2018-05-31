using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class ContorollUI : MonoBehaviour {

    [SerializeField, ReadOnly]
    private GameObject[] _uiSet;
    

	// Use this for initialization
	void Start () {
        _uiSet = transform.GetAllChild();
        Show();

    }



    private void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {

            _uiSet[0].GetComponent<KeyUI>().ControllUISet(KeyUI.GUID_ID.LockON);

        }


        if (Input.GetMouseButtonDown(1))
        {

            _uiSet[0].GetComponent<KeyUI>().ControllUISet(KeyUI.GUID_ID.Copy);

        }
    }


    //非表示
    public void Hide()
    {
        for (int i = 0; i < _uiSet.Length; i++)
        {
            _uiSet[i].SetActive(false);

        }

    }

    //表示
    public void Show()
    {
        for (int i = 0; i < _uiSet.Length; i++)
        {
            _uiSet[i].SetActive(true);
        }
    }




}
