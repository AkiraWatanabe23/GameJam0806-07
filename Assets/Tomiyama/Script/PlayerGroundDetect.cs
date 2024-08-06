using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class PlayerGroundDetect : MonoBehaviour
{
    PlayerMove _playerMove = default;
    Animator _anim = default;
    void Start()
    {
        _playerMove = FindObjectOfType<PlayerMove>();
        _anim = GetComponentInParent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Ground"))
        {
            _anim.SetBool("IsGrounded", true);
            _playerMove.JumpCount = 2;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _playerMove.JumpCount = 1;
    }
}
