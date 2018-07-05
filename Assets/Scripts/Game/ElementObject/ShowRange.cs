using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Play
{
    //判定視覚化用スクリプト

    public class ShowRange : MonoBehaviour
    {
        //可視範囲用画像
        [SerializeField]
        private Sprite _rangeSprite;

        private BoxCollider2D col;

        private SpriteRenderer spr;


        void Start()
        {
            //表示
            Show();
        }


        public void Show()
        {
            //判定用オブジェクト生成
            GameObject Range = new GameObject("Renge");
            //判定オブジェクトをゲームオブジェクトの子供に設定
            Range.transform.SetParent(gameObject.transform);
            //判定オブジェクトにSpriteRendererを取り付け
            Range.gameObject.AddComponent<SpriteRenderer>();
            //画像の設定
            var spr = Range.gameObject.GetComponent<SpriteRenderer>();
            //スプライト設定
            spr.sprite = _rangeSprite;
            //カラー設定
            spr.color = new Vector4(1, 0, 0, 0.2f);
            //表示優先度
            spr.sortingOrder = 10;
            //当たり判定取得
            col = gameObject.GetComponent<BoxCollider2D>();
            //判定オブジェクトの位置調整
            Range.transform.localPosition = col.transform.localPosition + new Vector3(col.offset.x, col.offset.y,0);
            //判定オブジェクトのサイズ調整
            Range.transform.localScale = new Vector3 (col.size.x+ col.bounds.extents.x+0.2f, col.size.y+ col.bounds.extents.y + 0.2f, 0);
                      
        }
    }
}