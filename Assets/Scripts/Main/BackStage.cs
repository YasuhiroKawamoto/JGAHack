using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Extensions;

namespace Main
{
    public class BackStage : MonoBehaviourEx
    {
        private ResourceRequest _stageAsset;

        public void ChangeStage(int num)
        {
            StartCoroutine(StageLoad(num));
        }

        private IEnumerator StageLoad(int num)
        {
            // アセットのロード
            var stageName = "Stage/Stage_" + num;
            _stageAsset = Resources.LoadAsync(stageName);

            // ロード待ち
            yield return new WaitWhile(() => !_stageAsset.isDone);

            foreach (var obj in this.transform.GetAllChild())
            {
                Destroy(obj);
            }

            var stageObj = _stageAsset.asset as GameObject;
            var stage = Instantiate(stageObj);
            this.transform.SetChild(stage);
        }
    }
}
