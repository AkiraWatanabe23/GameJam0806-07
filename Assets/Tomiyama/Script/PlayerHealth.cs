using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField, Header("ダメージを受けた時のパーティクル")]
    GameObject _damageParticle;
    [SerializeField, Header("ダメージ後の無敵時間")]
    private float _invincibleTime = 2;
    private float _timer = default;
    [SerializeField, Header("プレイヤーの体力")]
    private int _maxHp = 5;
    /// <summary>プレイヤーの体力</summary>
    private int _hp = 0;
    public int Hp { get => _hp; set => _hp = value; }
    Animator _anim = default;
    [SerializeField]HitPointUI hitPointManager;

    private void Awake()
    {
        _hp = _maxHp;
    }
    private void Start()
    {
        _anim = GetComponentInParent<Animator>();
    }
    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            _anim.SetBool("IsInvincible", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && _timer <= 0)
        {
            _anim.SetBool("IsInvincible", true);
            AudioManager.Instance.PlaySE(SEType.Damaged);
            if (_hp - 1 == 0)
            {
                GameManager.Instance.PlayerDead();
            }
            else
            {
                _hp--;
                hitPointManager.Damage();
                Debug.Log($"Damage Taken (Current HP:{_hp}");
                Instantiate(_damageParticle, collision.ClosestPoint(transform.position), Quaternion.identity, transform);
                _timer = _invincibleTime;
            }
        }
    }
}
