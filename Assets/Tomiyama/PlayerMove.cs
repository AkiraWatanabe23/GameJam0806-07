using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField, Header("ジャンプ力")] float _jumpPower = default;
    [SerializeField, Header("プレイヤー速度")] float _speed = default;
    [SerializeField, Header("降下中のgravitiScale")] float _fallingGravityScale = default;
    Rigidbody2D _rb = default;
    SpriteRenderer _sr = default;
    float _h = default;
    float _defaultGravityScale = default;
    /// <summary>ジャンプしたかどうか</summary>
    private bool _canJump = true;
    public bool CanJump { get => _canJump; set => _canJump = value; }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _defaultGravityScale = _rb.gravityScale;
    }

    void Update()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _rb.velocity = new Vector2(_h * _speed, _rb.velocity.y);
        if (_h != 0)
        {
            _sr.flipX = (_h == -1) ? true : false;
        }
        if (Input.GetButtonDown("Jump") && _canJump)
        {
            Jump();
        }
        _rb.gravityScale = _rb.velocity.y < 0 ? _fallingGravityScale : _defaultGravityScale;
    }
    private void Jump()
    {
        _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        _canJump = false;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Ground"))
        {
            _canJump = true;
        }
    }
}
