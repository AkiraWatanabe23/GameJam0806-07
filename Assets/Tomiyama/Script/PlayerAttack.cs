using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField, Header("攻撃持続時間")] float _attackDuration;
    [SerializeField, Header("攻撃後のインターバル")] float _attackInterval;
    Collider2D _collider;
    PlayerMove _playerMove;
    bool _canAttack = true;
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
            _playerMove.Jump();
        }
    }
}
