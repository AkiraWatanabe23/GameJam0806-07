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

        private Tweening _tweening = default;

        private void Start()
        {
            _tweening = new();
            _tweening.Initialize();
        }

        private void Update()
        {
            if (_tweening == null) { return; }

            _tweening.OnUpdate();
        }

        public void OnUpdateScore(float score)
        {
            _tweening.RegisterTween(Tweening.ValueCount(0f, score, 1f, _scoreText));
        }

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
