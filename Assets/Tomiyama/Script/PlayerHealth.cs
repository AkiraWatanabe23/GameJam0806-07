using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField, Header("�_���[�W���󂯂����̃p�[�e�B�N��")]
    GameObject _damageParticle;
    [SerializeField, Header("�_���[�W��̖��G����")]
    private float _invincibleTime = 2;
    private float _timer = default;
    [SerializeField, Header("�v���C���[�̗̑�")]
    private int _maxHp = 5;
    /// <summary>�v���C���[�̗̑�</summary>
    private int _hp = 0;
    public int Hp { get => _hp; set => _hp = value; }
    Animator _anim = default;

    [SerializeField]HitPointUI hitPointUI;

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
            if (_hp - 1 == 0)
            {
                GameManager.Instance.PlayerDead();
            }
            else
            {
                _hp--;
                hitPointUI.Damage();
                Debug.Log($"Damage Taken (Current HP:{_hp}");
                Instantiate(_damageParticle, collision.ClosestPoint(transform.position), Quaternion.identity, transform);
                _timer = _invincibleTime;
            }
        }
    }
}
