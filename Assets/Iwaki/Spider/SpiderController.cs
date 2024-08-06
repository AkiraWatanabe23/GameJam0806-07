using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpiderController : MonoBehaviour
{
    [SerializeField] GameObject defeatEffect, effectOffset, AttackObject;
    [SerializeField] float rotationSpeed, amplitude, attackInterval;
    [SerializeField] string takenDamageObjectTag;
    [SerializeField] bool stopRotateWhenDefeated;
    Rigidbody2D rb;
    bool isDefeated, canAttack;
    Animator animator;
    float t;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (!isDefeated)
        {
            rb.angularVelocity = Mathf.Cos(Time.time * rotationSpeed) * amplitude;
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

    void Attack()
    {
        var player = FindAnyObjectByType<PlayerMove>();
        if (player != null)
        {
            var dir = player.transform.position - transform.position;
            var attack = Instantiate(AttackObject);
            attack.transform.position = transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //エネミーの挙動的に新しく撃破後オブジェクトを作るより楽そうなので独自に実装してます（後で消すと思う）
    {
        if (collision.CompareTag(takenDamageObjectTag))
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
}
