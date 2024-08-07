using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField, Header("ジャンプ力")] 
    float _jumpPower = default;
    [SerializeField, Header("ジャンプ力（空中時）")] 
    float _midJumpPower = default;
    [SerializeField, Header("プレイヤー速度")] 
    float _speed = default;
    [SerializeField, Header("降下中のgravitiScale")] 
    float _fallingGravityScale = default;
    [SerializeField, Header("跳躍時のエフェクト")]
    GameObject _jumpEffect;
    float _defaultGravityScale = default;
    float _h = default;
    bool _isPaused;
    public bool IsPaused { get => _isPaused; set => _isPaused = value; }
    Rigidbody2D _rb = default;
    Animator _anim = default;
    Vector3 _velocityBuffer;
    /// <summary>残りジャンプ回数</summary>
    private int _jumpCount = 2;
    public int JumpCount { get => _jumpCount; set => _jumpCount = value; }

    Vector3 _defaultScale;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _defaultGravityScale = _rb.gravityScale;
        _defaultScale = transform.localScale;
    }

    void Update()
    {
        if (!_isPaused)
        {
            _h = Input.GetAxisRaw("Horizontal");
            _rb.velocity = new Vector2(_h * _speed, _rb.velocity.y);
            _anim.SetBool("IsMoving", (_h != 0));
            _anim.SetFloat("MoveY", _rb.velocity.y);
            if (_h != 0)
            {
                transform.localScale = new Vector3(-_h * _defaultScale.x, _defaultScale.y, _defaultScale.z);
            }
            if (Input.GetButtonDown("Jump") && _jumpCount > 0)
            {
                Jump();
            }
            _rb.gravityScale = _rb.velocity.y < 0 ? _fallingGravityScale : _defaultGravityScale;
        }
        else
        {
            _rb.gravityScale = 0;
            _rb.velocity = Vector3.zero;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!_isPaused)
            {
                _isPaused = true;
                _velocityBuffer = _rb.velocity;
                _anim.speed = 0;
            }
            else if (_isPaused)
            {
                _isPaused = false;
                _rb.velocity = _velocityBuffer;
                _anim.speed = 1;
            }
        }
    }
    void Jump()
    {
        Debug.Log("Jump");
        _anim.SetBool("IsGrounded", false);
        _rb.velocity = (_jumpCount == 2) ? new Vector2(_rb.velocity.x, _jumpPower) : new Vector2(_rb.velocity.x, _midJumpPower);
        _jumpCount--;
        Instantiate(_jumpEffect,transform);
    }
    public void JumpWhenAttackedAtEnemy(float jumpPower)
    {
        Debug.Log("Hit by enemy Jump");
        _anim.SetBool("IsGrounded", false);
        _rb.velocity = new Vector2(_rb.velocity.x, jumpPower);
        _jumpCount--;
        Instantiate(_jumpEffect, transform);
    }
}
