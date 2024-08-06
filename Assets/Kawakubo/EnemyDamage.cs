using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var Attacker = collision.GetComponentInParent<PlayerAttack>();
        if (Attacker != null && !Attacker.CanAttack && collision.tag == "Weapon")
        {
            Destroy(this.gameObject);
        }
    }
}
