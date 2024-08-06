using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField, Header("ジャンプ力")] float _jumpPower = default;
    [SerializeField, Header("プレイヤー速度")] float _speed = default;
    [SerializeField, Header("降下中のgravitiScale")] float _fallingGravityScale = default;
    Rigidbody2D _rb = default;
    float _h = default;
    float _defaultGravityScale = default;
    /// <summary>ジャンプしたかどうか</summary>
    private bool _canJump = true;
    public bool CanJump { get => _canJump; set => _canJump = value; }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _defaultGravityScale = _rb.gravityScale;
    }

    void Update()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _rb.velocity = new Vector2(_h * _speed, _rb.velocity.y);
        if (_h != 0)
        {
            transform.localScale = new Vector3(-_h, 1, 1);
        }
        if (Input.GetButtonDown("Jump") && _canJump)
        {
            Jump();
        }
        _rb.gravityScale = _rb.velocity.y < 0 ? _fallingGravityScale : _defaultGravityScale;
    }
    public void Jump()
    {
        Debug.Log("Jump");
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpPower);
        _canJump = false;
    }
}
