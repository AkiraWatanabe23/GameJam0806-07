using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField]
    private GameObject _player = default;
    [SerializeField]
    private Transform _goal = default;
    [ReadOnly]
    [SerializeField]
    private float _time = 0f;

    [Header("UI")]
    [SerializeField]
    private Text _playTimeText = default;
    [SerializeField]
    private Slider _bgmSlider = default;
    [SerializeField]
    private Slider _seSlider = default;

    [Header("Debug")]
    [SerializeField]
    private bool _debugMode = true;
    [SerializeField]
    private bool _isGoal = false;

    private bool _isInitialized = false;
    private Transform _playerTransform = default;

    protected float PlayTime
    {
        get => _time;
        private set
        {
            _time = value;
            if (_playTimeText != null) { _playTimeText.text = _time.ToString("F1"); }
        }
    }
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
            _player = FindObjectOfType<PlayerMove>().gameObject;
        }
        _playerTransform = _player.transform;
        PlayTime = 0f;

        _bgmSlider.onValueChanged.AddListener(AudioManager.Instance.VolumeSettingBGM);
        _seSlider.onValueChanged.AddListener(AudioManager.Instance.VolumeSettingSE);

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

        PlayTime += Time.deltaTime;
        if (IsGoal)
        {
            //ここでクリアしたデータを保存する
            PlayerPrefs.SetString("ClearData", "Clear");
            PlayerPrefs.SetInt("Score", (int)(PlayTime * 10));

            SceneLoader.FadeLoad(SceneName.ClearResult);
        }
    }

    /// <summary> PlayerのHPがなくなったときに呼ばれる関数 </summary>
    public void PlayerDead()
    {
        PlayerPrefs.SetString("ClearData", "Failed");
        PlayerPrefs.SetInt("Score", 0);
        SceneLoader.FadeLoad(SceneName.FailedResult);
    }
}
