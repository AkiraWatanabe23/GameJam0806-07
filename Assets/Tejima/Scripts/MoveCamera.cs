using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    private Transform _player = default;
    [SerializeField]
    private HeightUI _heightUI = default;

    /// <summary> 現在のステージ上でのカメラの高さ </summary>
    private int _currentHeightPoint = 0;

    private const float CameraMoveSpace = 10.8f;

    private void Start()
    {
        if (_player == null) { _player = FindObjectOfType<PlayerMove>().gameObject.transform; }

        var position = transform.position;
        position.y = CameraMoveSpace * _currentHeightPoint;

        //transform.position = position;
    }

    private void Update()
    {
        //次の位置の半分を境にしてどれくらい位置が変化したか
        if (_player.position.y >= (CameraMoveSpace * (_currentHeightPoint + 1)) - 5.4f)
        {
            _currentHeightPoint++;
            var position = transform.position;
            position.y += CameraMoveSpace;

            transform.position = position;
            _heightUI.Climb();
        }
        else if (_player.position.y <= (CameraMoveSpace * _currentHeightPoint) - 5.4f && _currentHeightPoint > 0)
        {
            _currentHeightPoint--;
            var position = transform.position;
            position.y -= CameraMoveSpace;

            transform.position = position;
            _heightUI.Drop();
        }
    }
}