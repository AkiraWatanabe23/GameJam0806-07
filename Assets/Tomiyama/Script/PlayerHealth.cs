using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField, Header("ダメージ後の無敵時間")]
    private float _invinsibleTime = 2;
    private float _timer;
    [SerializeField, Header("プレイヤーの体力")]
    private int _maxHp = 5;
    private int _hp = 0;
    private void Awake()
    {
        _hp = _maxHp;
    }
    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Enemy") && _timer <= 0)
        {
            if (_hp - 1 == 0)
            {
                GameManager.Instance.PlayerDead();
            }
            else
            {
                Debug.Log("Damage Taken");
                _hp--;
                _timer = _invinsibleTime;
            }
        }
    }
}
