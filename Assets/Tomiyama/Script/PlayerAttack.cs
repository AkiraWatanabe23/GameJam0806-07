using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField, Header("UŒ‚Ž‘±ŽžŠÔ")] float _attackDuration;
    [SerializeField, Header("UŒ‚Œã‚ÌƒCƒ“ƒ^[ƒoƒ‹")] float _attackInterval;
    Collider2D _collider;
    PlayerMove _playerMove;
    bool _canAttack = true;
    public bool CanAttack { get => _canAttack; set => _canAttack = value; }

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _playerMove = FindObjectOfType<PlayerMove>();
        _collider.enabled = false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _canAttack)
        {
            StartCoroutine(Attack());
        }
    }
    IEnumerator Attack()
    {
        Debug.Log("Attack");
        _canAttack = false;
        _collider.enabled = true;
        yield return new WaitForSeconds(_attackDuration);
        _collider.enabled = false;
        yield return new WaitForSeconds(_attackInterval);
        _canAttack = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Enemy"))
        {
            _playerMove.JumpCount = 2;
            _playerMove.Jump();
        }
    }
}
