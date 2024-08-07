using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpiderController : MonoBehaviour, IEnemyAttackable
{
    [SerializeField] GameObject defeatEffect, attackObject;
    [SerializeField] Transform attackOffset, effectOffset;
    [SerializeField] float rotationSpeed, amplitude, attackInterval, attackSpeed;
    [SerializeField] bool stopRotateWhenDefeated, canAttack, playerDetectFromDistance;
    [SerializeField] float detectDistance, destroyTimeSinceDefeated;
    Rigidbody2D rb;
    bool isDefeated;
    Animator animator;
    float t, startTime;
    Transform player;

    [SerializeField] bool isPaused;
    float pausedTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startTime = Time.time;
        animator = GetComponent<Animator>();
        player = FindAnyObjectByType<PlayerMove>().transform;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isPaused)
            {
                pausedTime = Time.time;
                rb.simulated = false;
                animator.speed = 0;
                isPaused = true;
            }
            else
            {
                startTime += Time.time - pausedTime;
                rb.simulated = true;
                animator.speed = 1;
                isPaused = false;
            }
        }

        if (!isDefeated && !isPaused)
        {
            transform.localRotation = Quaternion.Euler(0, 0, Mathf.Sin((Time.time - startTime) * rotationSpeed) * amplitude / 2);

            if (playerDetectFromDistance)
            {
                SetCanAttack(detectDistance * detectDistance > Vector2.SqrMagnitude(player.position - attackOffset.position));
            }

            if (canAttack)
            {
                t += Time.deltaTime;
                if (t > attackInterval)
                {
                    t = 0;
                    animator.Play("ShootWeb");
                }
            }
        }
    }

    void ShootWeb()
    {
        var player = FindAnyObjectByType<PlayerMove>();
        if (player != null)
        {
            var dir = (player.transform.position - attackOffset.position).normalized;
            var attack = Instantiate(attackObject);
            attack.transform.position = attackOffset.position;
            attack.GetComponent<Rigidbody2D>().velocity = dir * attackSpeed;
            attack.GetComponent<Rigidbody2D>().angularVelocity = 10;
            attack.GetComponent<AutoDestroyer>().SetTimer(destroyTimeSinceDefeated);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //エネミーの挙動的に新しく撃破後オブジェクトを作るより楽そうなので独自に実装してます（後で消すと思う）
    {
        if (collision.CompareTag("Weapon"))
        {
            var colliders = GetComponentsInChildren<Collider2D>();
            foreach (var collider in colliders)
            {
                collider.enabled = false;
            }

            if (!isDefeated)
            {
                isDefeated = true;
                AudioManager.Instance.PlaySE(SEType.EnemyDead);
                animator.Play("Defeat");
                rb.gravityScale = 1;
                if (stopRotateWhenDefeated) rb.angularVelocity = 0;
                rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
                if (defeatEffect != null)
                {
                    var effect = Instantiate(defeatEffect);
                    if (effectOffset)
                    {
                        effect.transform.position = effectOffset.position;
                    }
                    else
                    {
                        effect.transform.position = collision.ClosestPoint(transform.position);
                    }
                }
                Destroy(gameObject, 3);
            }
        }
    }

    public void SetCanAttack(bool canAttack)
    {
        this.canAttack = canAttack;
    }
}

public interface IEnemyAttackable
{
    void SetCanAttack(bool canAttack);
}
