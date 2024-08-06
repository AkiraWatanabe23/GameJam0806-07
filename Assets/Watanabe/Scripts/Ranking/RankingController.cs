using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary> SpreadSheetを用いたランキング処理 </summary>
public class RankingController : MonoBehaviour
{
    [Tooltip("対象のSpreadSheetのURL")]
    [SerializeField]
    private string _deployedSheetURL = "";
    [SerializeField]
    private ConnectorView _rankingView = default;

    private List<int> _highScores = default;

    private IEnumerator Start()
    {
        _highScores = new();
        //スコアの保存
        yield return SendData(PlayerPrefs.GetInt("Score"));
        //最新ランキングの取得
        yield return GetRanking();
    }

    private IEnumerator SendData(int score)
    {
        var form = new WWWForm();
        form.AddField("address", "address");
        form.AddField("name", "name");
        form.AddField("score", score);

        var request = UnityWebRequest.Post(_deployedSheetURL, form);
        var send = request.SendWebRequest();

        while (!send.isDone) { yield return null; }

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log($"Request Failed : {request.error}");
        }
        else
        {
            var records = JsonUtility.FromJson<ScoreRecords>(request.downloadHandler.text).Records;
            Debug.Log("Request Success");
            foreach (var record in records)
            {
                Debug.Log("Name：" + record.Name + ", Score：" + record.Score);
                _highScores.Add(record.Score);
            }

            if (_highScores.Count < 5)
            {
                for (int i = 0; i < 5 - _highScores.Count; i++) { _highScores.Add(0); }
            }
            _highScores.Sort();
            _rankingView.OnUpdateScore((score / 10f).ToString("F1"));
            _rankingView.OnUpdateRanking(string.Join(',', _highScores));
        }
    }

    private IEnumerator GetRanking()
    {
        var request = UnityWebRequest.Get(_deployedSheetURL);
        var send = request.SendWebRequest();

        while (!send.isDone) { yield return null; }

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log($"Request Failed : {request.error}");
        }
        else
        {
            var records = JsonUtility.FromJson<ScoreRecords>(request.downloadHandler.text).Records;
            Debug.Log("Request Success");
            foreach (var record in records)
            {
                Debug.Log("Name：" + record.Name + ", Score：" + record.Score);
            }
        }
    }
}

[Serializable]
public class ScoreRecords
{
    public ScoreRecord[] Records;
}

[Serializable]
public class ScoreRecord
{
    public string Address;
    public string Name;
    public int Score;
    public string Created;
    public string Updated;
}
