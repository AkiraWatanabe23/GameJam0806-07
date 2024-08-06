﻿using UnityEngine;
using UnityEngine.UI;

public class TitleSceneController : MonoBehaviour
{
    [SerializeField]
    private Button _startButton = default;

    private void Start()
    {
        if (_startButton != null) { _startButton.onClick.AddListener(() => SceneLoader.FadeLoad(SceneName.InGame)); }

        Fade.Instance.StartFadeIn(() => AudioManager.Instance.PlayBGM(BGMType.Title));
    }
}