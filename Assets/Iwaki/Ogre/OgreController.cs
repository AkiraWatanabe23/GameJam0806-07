using UnityEngine;

public class OgreController : MonoBehaviour, IEnemyAttackable
{
    [SerializeField] GameObject kanabou;
    [SerializeField] Transform target;
    [SerializeField] float interval, throwSpeed;
    [SerializeField] bool canAttack;
    [SerializeField] float kanabouDestroyTime;
    Animator animator;
    float t;
    Transform player;
    SpriteRenderer rend;

    bool isPaused;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindAnyObjectByType<PlayerMove>().transform;
        rend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isPaused)
            {
                animator.speed = 0;
                isPaused = true;
            }
            else
            {
                animator.speed = 1;
                isPaused = false;
            }
        }

        if (canAttack && !isPaused)
        {
            t += Time.deltaTime;

            if (t > interval)
            {
                animator.Play("Throw");
                t = 0;
            }

            if (player.position.x > transform.position.x)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                transform.localScale = new Vector2(1, 1);
            }
        }
    }

    void Throw()
    {
        var obj = Instantiate(this.kanabou);
        var kanabou = obj.GetComponent<KanabouController>();
        kanabou.Throw(transform, target, throwSpeed);
        kanabou.GetComponent<AutoDestroyer>().SetTimer(kanabouDestroyTime);
    }

    public void SetAttackable(bool canAttack)
    {
        this.canAttack = canAttack;
    }
}
