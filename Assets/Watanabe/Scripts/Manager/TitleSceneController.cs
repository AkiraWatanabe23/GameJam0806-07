using UnityEngine;
using UnityEngine.UI;

public class TitleSceneController : MonoBehaviour
{
    [SerializeField]
    private Button _startButton = default;
    [SerializeField]
    private Button _easyModeButton = default;
    [SerializeField]
    private Button _hardModeButton = default;
    [SerializeField]
    private GameObject _creditPanel = default;

    private bool _isOpenCredit = false;

    private void Start()
    {
        _startButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySE(SEType.Click);
        });
        _easyModeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySE(SEType.Click);
            SceneLoader.FadeLoad(SceneName.Stage1);
        });
        _hardModeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySE(SEType.Click);
            SceneLoader.FadeLoad(SceneName.Stage2);
        });

        Fade.Instance.StartFadeIn(() => AudioManager.Instance.PlayBGM(BGMType.Title));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { OpenCredit(); }
    }

    private void OpenCredit()
    {
        if (_isOpenCredit) { _isOpenCredit = false; }
        else { _isOpenCredit = true; }

        _creditPanel.SetActive(_isOpenCredit);
    }
}
