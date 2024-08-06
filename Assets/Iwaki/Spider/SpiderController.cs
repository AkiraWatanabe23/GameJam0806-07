using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpiderController : MonoBehaviour, IEnemyAttackable
{
    [SerializeField] GameObject defeatEffect, effectOffset, attackObject;
    [SerializeField] Transform attackOffset;
    [SerializeField] float rotationSpeed, amplitude, attackInterval, attackSpeed;
    [SerializeField] bool stopRotateWhenDefeated, canAttack, playerDetectFromDistance;
    [SerializeField] float detectDistance;
    Rigidbody2D rb;
    bool isDefeated;
    Animator animator;
    float t;
    Transform player;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = FindAnyObjectByType<PlayerMove>().transform;
    }
    void Update()
    {
        if (!isDefeated)
        {
            rb.angularVelocity = Mathf.Cos(Time.time * rotationSpeed) * amplitude;

            if (playerDetectFromDistance)
            {
                SetAttackable(detectDistance * detectDistance > Vector2.SqrMagnitude(player.position - attackOffset.position));
            }

            if (canAttack)
            {
                t += Time.deltaTime;
                if (t > attackInterval)
                {
                    t = 0;
                    Attack();
                }
            }
        }
    }

    void Attack()
    {
        var player = FindAnyObjectByType<PlayerMove>();
        if (player != null)
        {
            var dir = (player.transform.position - attackOffset.position).normalized;
            var attack = Instantiate(attackObject);
            attack.transform.position = attackOffset.position;
            attack.transform.up = dir;
            attack.GetComponent<Rigidbody2D>().velocity = dir * attackSpeed;
            Destroy(attack, 3);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //エネミーの挙動的に新しく撃破後オブジェクトを作るより楽そうなので独自に実装してます（後で消すと思う）
    {
        if (collision.CompareTag("Weapon"))
        {
            var colliders = GetComponents<Collider2D>();
            foreach (var collider in colliders)
            {
                collider.enabled = false;
            }

            if (!isDefeated)
            {
                isDefeated = true;
                animator.Play("Defeat");
                rb.gravityScale = 1;
                if (stopRotateWhenDefeated) rb.angularVelocity = 0;
                rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
                if (defeatEffect != null)
                {
                    var effect = Instantiate(defeatEffect);
                    effect.transform.position = transform.position;
                }
                Destroy(gameObject, 3);
            }
        }
    }

    public void SetAttackable(bool canAttack)
    {
        this.canAttack = canAttack;
    }
}

public interface IEnemyAttackable
{
    void SetAttackable(bool canAttack);
}
