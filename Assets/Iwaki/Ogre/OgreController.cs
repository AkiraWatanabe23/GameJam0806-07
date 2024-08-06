using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class OgreController : MonoBehaviour, IEnemyAttackable
{
    [SerializeField] GameObject kanabou;
    [SerializeField] Transform target;
    [SerializeField] float interval, throwSpeed, throwAnimationOffset;
    [SerializeField] bool canAttack;
    Rigidbody2D rb;
    Animator animator;
    float t;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (canAttack)
        {
            t += Time.deltaTime;

            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Throw") && t + throwAnimationOffset > interval)
            {
                animator.Play("Throw");
            }

            if (t > interval)
            {
                t = 0;
                Throw();
            }
        }
    }

    void Throw()
    {
        var obj = Instantiate(this.kanabou);
        var kanabou = obj.GetComponent<KanabouController>();
        kanabou.Throw(transform, target, throwSpeed);
        Destroy(obj, 5);
    }

    public void SetAttackable(bool canAttack)
    {
        this.canAttack = canAttack;
    }
}
