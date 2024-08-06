using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class PlayerGroundDetect : MonoBehaviour
{
    PlayerMove _playerMove;
    void Start()
    {
        _playerMove = FindObjectOfType<PlayerMove>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Ground"))
        {
            _playerMove.JumpCount = 2;
        }
    }
}
