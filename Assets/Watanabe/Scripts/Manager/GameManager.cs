using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField]
    private GameObject _player = default;
    [SerializeField]
    private Transform _goal = default;

    [Header("Debug")]
    [SerializeField]
    private bool _debugMode = true;
    [SerializeField]
    private bool _isGoal = false;

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

        PlayerPrefs.DeleteAll();
    }

    private void Update()
    {
        if (IsGoal)
        {
            //ここでクリアしたデータを保存する
            PlayerPrefs.SetString("ClearData", "Clear");

            SceneLoader.FadeLoad(SceneName.Result);
        }
    }

    /// <summary> PlayerのHPがなくなったときに呼ばれる関数 </summary>
    public void PlayerDead()
    {
        PlayerPrefs.SetString("ClearData", "Failed");
        SceneLoader.FadeLoad(SceneName.Result);
    }
}
