using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField]
    private Button _returnTitleButton = default;
    [SerializeField]
    private Image _backGround = default;
    [SerializeField]
    private Sprite _clearImage = default;
    [SerializeField]
    private Sprite _failedImage = default;

    private void Start()
    {
        InitializeUI();
        Fade.Instance.StartFadeIn();
    }

    private void InitializeUI()
    {
        if (_returnTitleButton != null) { _returnTitleButton.onClick.AddListener(() => SceneLoader.FadeLoad(SceneName.Title)); }

        if (!PlayerPrefs.HasKey("ClearData")) { return; }

        var clearData = PlayerPrefs.GetString("ClearData");
        if (clearData == "Clear")
        {
            _backGround.sprite = _clearImage;
            AudioManager.Instance.PlayBGM(BGMType.Clear);
        }
        else if (clearData == "Failed")
        {
            _backGround.sprite = _failedImage;
            AudioManager.Instance.PlayBGM(BGMType.Failed);
        }
    }
}
