using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class WaveContoller : MonoBehaviour {

    private Transform myTrans;

    public Transform _target;

    private Tweener tweener;

    // 起動時
    void Start()
    {
        myTrans = transform;

        StartCoroutine(start());
    }

    private IEnumerator start()
    {
        // 待つ
        yield return new WaitForSeconds(1);

        Sequence seq = DOTween.Sequence();
        // バウンド
        seq.Append(myTrans.DOLocalMoveY(0.1f, 1f).SetEase(Ease.OutBounce).OnComplete(callback).SetRelative());
        seq.Join(myTrans.DOLocalMoveX(0, 1f).SetRelative());
    }

    // 更新
    void Update()
    {
        
        // バウンド終了後から監視する
        if (tweener != null)
        {
            // ある程度近づいたら消す
            if (Vector3.Distance(myTrans.position, _target.position) < 0.05f) {
                // 消す
                gameObject.SetActive(false);
            }
            // 終点を更新
            tweener.ChangeEndValue(_target.position, 0.2f, true);
        }
    }

    // バウンド終了後に呼ばれる
    private void callback()
    {
        tweener = myTrans.DOMove(_target.position, 0.5f);
    }

    //pPos プレイヤーの座標　tPos ターゲットの座標
    public void setVelocity(Transform target)
    {
        _target = target;
    }

}
