using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    IEnemyAttackable attack;

    private void Update()
    {
        if (enemy == null)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetInterface();
        if (collision.GetComponent<PlayerMove>() != null) attack.SetAttackable(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GetInterface();
        if (collision.GetComponent<PlayerMove>() != null) attack.SetAttackable(false);
    }

    void GetInterface()
    {
        if (enemy != null)
        {
            var com = enemy.GetComponents<MonoBehaviour>();
            foreach (var c in com)
            {
                if (c is IEnemyAttackable)
                {
                    attack = c as IEnemyAttackable;
                    break;
                }
            }
        }
    }
}
