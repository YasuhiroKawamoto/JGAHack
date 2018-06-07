using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

namespace Play
{
    public class GuidUI : Util.SingletonMonoBehaviour<GuidUI>
    {

        //案内状況
        public enum GUID_STEP
        {
            Normal,
            Lockon
        }
        //表示するUI（こども）
        [SerializeField, ReadOnly]
        private GameObject[] _uiSet;

        void Start()
        {
            //全行取得
            _uiSet = transform.GetAllChild();
            //非表示
            HideAll();
        }

   
        //非表示
        public void HideAll()
        {
            for (int i = 0; i < _uiSet.Length; i++)
            {
                _uiSet[i].SetActive(false);
            }
        }

        //表示
        public void ShowAll()
        {
            for (int i = 0; i < _uiSet.Length; i++)
            {
                _uiSet[i].SetActive(true);
            }
        }


        //案内状況に応じて表示変化
        public void ChangeGuid(GUID_STEP step)
        {

            //表示
            ShowAll();

            var controller = GameController.Instance;
            bool isController = controller.GetConnectFlag();
            if (isController)
            {
                switch (step)
                {

                    case GUID_STEP.Normal:
                        _uiSet[0].GetComponent<KeyUI>().GuidUISet(KeyUI.GUID_ID.Move, isController);
                        _uiSet[1].GetComponent<KeyUI>().GuidUISet(KeyUI.GUID_ID.LockON, isController);
                        _uiSet[2].SetActive(false);
                        break;

                    case GUID_STEP.Lockon:
                        _uiSet[0].GetComponent<KeyUI>().GuidUISet(KeyUI.GUID_ID.ChangeLock, isController);
                        _uiSet[1].GetComponent<KeyUI>().GuidUISet(KeyUI.GUID_ID.Copy, isController);
                        _uiSet[2].GetComponent<KeyUI>().GuidUISet(KeyUI.GUID_ID.Paste, isController);
                        break;
                }
            }
            else
            {
                switch (step)
                {

                    case GUID_STEP.Normal:
                        _uiSet[0].GetComponent<KeyUI>().GuidUISet(KeyUI.GUID_ID.Move, isController);
                        _uiSet[1].GetComponent<KeyUI>().GuidUISet(KeyUI.GUID_ID.Move2, isController);
                        _uiSet[2].GetComponent<KeyUI>().GuidUISet(KeyUI.GUID_ID.LockON, isController);
                        break;

                    case GUID_STEP.Lockon:
                        _uiSet[0].GetComponent<KeyUI>().GuidUISet(KeyUI.GUID_ID.ChangeLock, isController);
                        _uiSet[1].GetComponent<KeyUI>().GuidUISet(KeyUI.GUID_ID.Copy, isController);
                        _uiSet[2].GetComponent<KeyUI>().GuidUISet(KeyUI.GUID_ID.Paste, isController);
                        break;
                }
            }     
        }
    }
}