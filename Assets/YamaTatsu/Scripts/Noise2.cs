using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise2 : MonoBehaviour {

    //シェーダー
    Shader m_shader;
    //マテリアル
    Material m_mat;
    //スライダーを表示し変数変更
    [Range(0, 1)]
    public float horizonValue;

    //source 入力画像
    //destination 結果
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //m_matがnullの場合
        if(m_mat == null)
        {
            //Hidden/Noise2を見つける
            m_shader = Shader.Find("Custom/Noise2");
            //m_matにMaterialを入れる
            m_mat = new Material(m_shader);
            m_mat.hideFlags = HideFlags.DontSave;
        }

        //ランダムで値を変更し更新すること乱数する
        m_mat.SetInt("_Seed", Time.frameCount);
        //左右にずらす値をセット
        m_mat.SetFloat("_HorizonValue", horizonValue);
        Graphics.Blit(source, destination, m_mat);

    }



}
