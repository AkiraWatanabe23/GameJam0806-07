using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgreController : MonoBehaviour
{
    [SerializeField] bool canThrow;
    [SerializeField] GameObject kanabou;
    [SerializeField] Transform target;
    [SerializeField] float interval, throwSpeed;
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
        if (canThrow)
        {
            t += Time.deltaTime;

            if (t > interval)
            {
                t = 0;
                Throw();
            }
        }
    }

    void Throw()
    {
        animator.Play("Throw");
        var obj = Instantiate(this.kanabou);
        var kanabou = obj.GetComponent<KanabouController>();
        kanabou.Throw(transform, target, throwSpeed);
    }

    public void ChangeAttackable(bool canAttack)
    {
        this.canThrow = canAttack;
    }
}
