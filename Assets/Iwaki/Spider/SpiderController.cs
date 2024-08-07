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
            rb.angularVelocity = Mathf.Cos((Time.time - startTime) * rotationSpeed) * amplitude;

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
            attack.transform.up = dir;
            attack.GetComponent<Rigidbody2D>().velocity = dir * attackSpeed;
            attack.GetComponent<AutoDestroyer>().SetTimer(destroyTimeSinceDefeated);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //�G�l�~�[�̋����I�ɐV�������j��I�u�W�F�N�g�������y�����Ȃ̂œƎ��Ɏ������Ă܂��i��ŏ����Ǝv���j
    {
        if (collision.CompareTag("Weapon"))
        {
            animator.Play("Defeat");
            var colliders = GetComponentsInChildren<Collider2D>();
            foreach (var collider in colliders)
            {
                collider.enabled = false;
            }

            if (!isDefeated)
            {
                isDefeated = true;
                //animator.Play("Defeat");
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