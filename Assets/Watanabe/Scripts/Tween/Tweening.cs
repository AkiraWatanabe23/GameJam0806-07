using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tweening
{
    private List<IEnumerator> _tweens = default;

    public static Tweening Instance { get; private set; }

    public void Initialize()
    {
        Instance = this;
        _tweens = new();
    }

    public void OnUpdate()
    {
        if (_tweens == null || _tweens.Count <= 0) { return; }

        //各処理の実行部分
        for (int i = _tweens.Count - 1; i >= 0; i--)
        {
            if (_tweens[i] == null) { continue; }

            if (!_tweens[i].MoveNext()) { _tweens.RemoveAt(i); }
            else { continue; }
        }
    }

    public void RegisterTween(IEnumerator target) => _tweens.Add(target);

    public static IEnumerator ValueCount(int start, int end, float duration, Text target)
    {
        var timer = 0f;
        var diff = end - start;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            target.text = (diff * (timer / duration)).ToString("F1");
            yield return null;
        }
        target.text = end.ToString("F1");
        yield return null;
    }

    public static IEnumerator ValueCount(float start, float end, float duration, Text target)
    {
        Debug.Log($"{start} {end}");
        var timer = 0f;
        var diff = end - start;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            target.text = (diff * (timer / duration)).ToString("F1");
            yield return null;
        }
        target.text = end.ToString("F1");
        yield return null;
    }
}
