using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField, Header("�W�����v��")] 
    float _jumpPower = default;
    [SerializeField, Header("�W�����v�́i�󒆎��j")] 
    float _midJumpPower = default;
    [SerializeField, Header("�v���C���[���x")] 
    float _speed = default;
    [SerializeField, Header("�~������gravitiScale")] 
    float _fallingGravityScale = default;
    float _defaultGravityScale = default;
    float _h = default;
    Rigidbody2D _rb = default;
    Animator _anim = default;
    /// <summary>�c��W�����v��</summary>
    private int _jumpCount = 2;
    public int JumpCount { get => _jumpCount; set => _jumpCount = value; }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _defaultGravityScale = _rb.gravityScale;
    }

    void Update()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _rb.velocity = new Vector2(_h * _speed, _rb.velocity.y);
        _anim.SetBool("IsMoving", (_h != 0));
        if (_h != 0)
        {
            transform.localScale = new Vector3(-_h, 1, 1);
        }
        if (Input.GetButtonDown("Jump") && _jumpCount > 0)
        {
            Jump();
        }
        _rb.gravityScale = _rb.velocity.y < 0 ? _fallingGravityScale : _defaultGravityScale;
    }
    public void Jump()
    {
        Debug.Log("Jump");
        _anim.SetBool("IsGrounded", false);
        _rb.velocity = (_jumpCount == 2) ? new Vector2(_rb.velocity.x, _jumpPower) : new Vector2(_rb.velocity.x, _midJumpPower);
        _jumpCount--;
    }
}
