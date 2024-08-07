using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField, Header("UŒ‚Ž‘±ŽžŠÔ")] float _attackDuration = default;
    [SerializeField, Header("UŒ‚Œã‚ÌƒCƒ“ƒ^[ƒoƒ‹")] float _attackInterval = default;
    [SerializeField, Header("“GƒqƒbƒgŽž‚ÌƒWƒƒƒ“ƒv—Í")] float _jumpPowerWhenAttackedToEnemy = default;
    Collider2D _collider = default;
    PlayerMove _playerMove = default;
    Animator _anim = default;
    bool _canAttack = true;
    public bool CanAttack { get => _canAttack; set => _canAttack = value; }

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _playerMove = FindObjectOfType<PlayerMove>();
        _anim = GetComponentInParent<Animator>();
        _collider.enabled = false;
    }
    void Update()
    {
        if (!_playerMove.IsPaused)
        {
            if (Input.GetMouseButtonDown(0) && _canAttack)
            {
                StartCoroutine(Attack());
            }
        }
    }
    IEnumerator Attack()
    {
        Debug.Log("Attack");
        _anim.SetTrigger("Attack"); 
        AudioManager.Instance.PlaySE(SEType.Attack);
        _canAttack = false;
        _collider.enabled = true;
        yield return new WaitForSeconds(_attackDuration);
        _collider.enabled = false;
        yield return new WaitForSeconds(_attackInterval);
        _canAttack = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _playerMove.JumpCount = 2;
            _playerMove.JumpWhenAttackedAtEnemy(_jumpPowerWhenAttackedToEnemy);
        }
    }
}
