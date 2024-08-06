using System.Collections;
using UnityEngine;

public class CameraScrollDemo : MonoBehaviour
{
    [SerializeField]
    private Transform[] _points = default;
    [SerializeField]
    private float _scrollSpeed = 1f;
    [Header("Debug")]
    [SerializeField]
    private int _currentIndex = 0;

    private IEnumerator _autoScroll = default;

    private void Update()
    {
        if (_autoScroll != null)
        {
            if (!_autoScroll.MoveNext()) { _autoScroll = null; }
            return;
        }

        var position = transform.position;

        position.y += Time.deltaTime * _scrollSpeed;
        if (position.y >= _points[_currentIndex].position.y)
        {
            _currentIndex++;
            _autoScroll = AutoScroll();
        }
        transform.position = position;
    }

    private IEnumerator AutoScroll()
    {
        var position = transform.position;
        while (position.y < _points[_currentIndex].position.y)
        {
            Debug.Log("Scroll");
            //仮で移動速度3倍
            position.y += Time.deltaTime * _scrollSpeed * 3f;
            transform.position = position;

            yield return null;
        }
        _currentIndex++;
    }
}
