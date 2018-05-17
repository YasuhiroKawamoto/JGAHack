using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimCount : MonoBehaviour {

    //テキストに表示
    public Text countText = null;

    //カウントの初期値
    float count;

    float ranCount = 0;

	// Use this for initialization
	void Start () {

        //Stringでテキストに表示
        countText.text = count.ToString();
		
	}
	
	// Update is called once per frame
	void Update () {

        ranCount += Random.Range(1, 3);

        StartCoroutine(countAnimation(ranCount, 2));

        if(count > 10000)
        {
            count = 0;
        }

    }


    // スコアをアニメーションさせる
    IEnumerator countAnimation(float addCount, float time)
    {
        //前回のスコア
        float befor = count;
        //今回のスコア
        float after = count + addCount;
        //得点加算
        count += addCount;
        //0fを経過時間にする
        float elapsedTime = 0.0f;

        //timeが０になるまでループさせる
        while (elapsedTime < time)
        {
            float rate = elapsedTime / time;
            // テキストの更新
            countText.text = (befor + (after - befor) * rate).ToString("f0");

            elapsedTime += Time.deltaTime;
            // 0.01秒待つ
            yield return new WaitForSeconds(0.01f);
        }
        // 最終的な着地のスコア
        countText.text = after.ToString();
    }
}
