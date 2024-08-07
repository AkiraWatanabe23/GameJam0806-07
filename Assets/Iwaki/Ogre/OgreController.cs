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

    bool isPaused;
    bool playerWasLeft;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindAnyObjectByType<PlayerMove>().transform;

        playerWasLeft = player.position.x <= transform.position.x;
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
                if (playerWasLeft)
                {
                    transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                    playerWasLeft = false;
                }
            }
            else
            {
                if (!playerWasLeft)
                {
                    transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                    playerWasLeft = true;
                }
            }
        }
    }

    void Throw()
    {
        AudioManager.Instance.PlaySE(SEType.Throw);
        var obj = Instantiate(this.kanabou);
        var kanabou = obj.GetComponent<KanabouController>();
        kanabou.Throw(transform, target, throwSpeed);
        kanabou.GetComponent<AutoDestroyer>().SetTimer(kanabouDestroyTime);
    }

    public void SetCanAttack(bool canAttack)
    {
        this.canAttack = canAttack;
    }
}
