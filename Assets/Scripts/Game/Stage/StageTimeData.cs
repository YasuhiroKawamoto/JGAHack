using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Util.Save;

public class StageTimeData : SaveSingleton<StageTimeData>
{
    static public readonly int SAVE_NUM = 3;

    static public readonly int STAGE_NUM = 11;

    [SerializeField]
    private Serialization<string, float> _stageTime;

    public Serialization<string, float> StageTime
    {
        get { return _stageTime; }
    }

    override public void Save(string json = "")
    {
        json = JsonUtility.ToJson(_stageTime);
        base.Save(json);
    }

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        // セーブデータの読み込み
        var load = Load((json) =>
        {
            _instance = new StageTimeData();
            var dic = JsonUtility.FromJson<Serialization<string, float>>(json);
            _instance._stageTime = dic;
        });

        if (load == false)
        {
            _stageTime = new Serialization<string, float>(new Dictionary<string, float>());
            for (int i = 0; i < STAGE_NUM; i++)
            {
                // 20ステージ
                var dic = _stageTime.ToDictionary();
                for (int j = 0; j < SAVE_NUM; j++)
                {
                    // ベスト3まで計測
                    string key = GetKey(i, j);
                    dic.Add(key, 0.0f);
                }
            }
        }
    }

    private string GetKey(int stage, int rank)
    {
        if (SAVE_NUM <= rank) return string.Empty;

        return stage.ToString() + "_" + rank.ToString();
    }

    /// <summary>
    /// ステージのスコア取得
    /// </summary>
    /// <param name="stage"></param>
    /// <returns></returns>
    public float[] GetStageTimes(int stage)
    {
        float[] times = new float[SAVE_NUM];

        var dic = _stageTime.ToDictionary();

        for (int i = 0; i < SAVE_NUM; i++)
        {
            var key = GetKey(stage, i);
            times[i] = dic[key];
        }

        return times;
    }

    /// <summary>
    /// 時間の設定
    /// </summary>
    /// <param name="stage"></param>
    /// <param name="time"></param>
    /// <returns>更新した場合更新したindex</returns>
    public int SetTime(int stage, float time)
    {
        var dic = _stageTime.ToDictionary();

        int i = 0;
        for (i = 0; i < SAVE_NUM; i++)
        {
            var key = GetKey(stage, i);
            var data = dic[key];

            // 更新しているか？
            if (data <= 0.0f) break;

            if (time < data) break;

        }
        // 更新がなかったら return 
        if (SAVE_NUM <= i) return -1;

        // ランクをずらしてタイムを更新
        int j = 0;
        for (j = SAVE_NUM - 1; i <= j; j--)
        {
            var key = GetKey(stage, j);
            var bKey = GetKey(stage, j + 1);
            if (bKey != string.Empty)
            {
                dic[bKey] = dic[key];
            }
        }

        dic[GetKey(stage, i)] = time;

        return j;
    }
}