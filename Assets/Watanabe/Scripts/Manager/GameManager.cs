using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField]
    private GameObject _player = default;
    [SerializeField]
    private Transform _goal = default;
    [ReadOnly]
    [SerializeField]
    private float _time = 0f;

    [Header("Debug")]
    [SerializeField]
    private bool _debugMode = true;
    [SerializeField]
    private bool _isGoal = false;

    private bool _isInitialized = false;
    private Transform _playerTransform = default;

    protected bool IsGoal
    {
        get
        {
            if (_debugMode) { return _isGoal; }

            //参照がない場合
            if (_player == null || _goal == null) { return false; }

            return _playerTransform.position.y >= _goal.position.y;
        }
    }

    protected override bool DontDestroyOnLoad => false;

    private void Start()
    {
        if (_player == null)
        {
            _player = GameObject.Find("Player");
        }
        _playerTransform = _player.transform;
        _time = 0f;

        PlayerPrefs.DeleteAll();
        Fade.Instance.StartFadeIn(() =>
        {
            AudioManager.Instance.PlayBGM(BGMType.InGame);
            _isInitialized = true;
        });

    }

    private void Update()
    {
        if (!_isInitialized) { return; }

        _time += Time.deltaTime;
        if (IsGoal)
        {
            //ここでクリアしたデータを保存する
            PlayerPrefs.SetString("ClearData", "Clear");
            PlayerPrefs.SetInt("Score", (int)(_time * 10));

            SceneLoader.FadeLoad(SceneName.Result);
        }
    }

    /// <summary> PlayerのHPがなくなったときに呼ばれる関数 </summary>
    public void PlayerDead()
    {
        PlayerPrefs.SetString("ClearData", "Failed");
        PlayerPrefs.SetInt("Score", 0);
        SceneLoader.FadeLoad(SceneName.Result);
    }
}
