using UnityEngine;
using UnityEngine.UI;

namespace Network
{
    public class ConnectorView : MonoBehaviour
    {
        [SerializeField]
        private Text _scoreText = default;
        [SerializeField]
        private Text[] _rankingTexts = default;

        public void OnUpdateScore(string score) => _scoreText.text = score;

        public void OnUpdateRanking(string ranking)
        {
            var scores = ranking.Split(',');
            //今回は表示するランキングが固定なので
            for (int i = 0; i < 5; i++)
            {
                if (i >= scores.Length) { _rankingTexts[i].text = "0.0"; }
                else { _rankingTexts[i].text = (int.Parse(scores[i]) / 10f).ToString("F1"); }
            }
        }
    }
}
