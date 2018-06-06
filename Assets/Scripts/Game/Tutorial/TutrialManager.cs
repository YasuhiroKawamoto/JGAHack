using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using Extensions;

namespace Play.Tutrial
{
    public class TutrialManager : SingletonMonoBehaviour<TutrialManager>
    {
        [System.Serializable]
        public struct StepData
        {
            public string text;
            public bool isMove;
            public bool isTarget;
            public bool isCopy;
            public bool isPaste;
        }

        [SerializeField]
        private List<StepData> _stepData = new List<StepData>();

        [SerializeField, ReadOnly]
        private int _step = 0;

        public int Step
        {
            get { return _step; }
        }

        void Start()
        {
            _step = 0;
            StartCoroutine(StartText());
        }

        IEnumerator StartText()
        {
            var manager = InGameManager.Instance;
            var cameraMan = manager.CameraManager;

            yield return new WaitUntil(() => cameraMan.GetEndProduction());

            var messenger = manager.Messenger;
            messenger.SetMessagePanel(_stepData[_step].text);
        }

        /// <summary>
        /// 次のステップに移行
        /// </summary>
        public void NextStep()
        {
            _step++;
            var manager = InGameManager.Instance;
            var messenger = manager.Messenger;
            var data = _stepData[_step];
            messenger.SetMessagePanel(data.text);
        }

        // 確認メソッドたち

        public bool CanTarget()
        {
            return _stepData[_step].isTarget;
        }

        public bool CanCopy()
        {
            return _stepData[_step].isCopy;
        }

        public bool CanMove()
        {
            return _stepData[_step].isMove;
        }

        public bool CanPaste()
        {
            return _stepData[_step].isPaste;
        }
    }
}