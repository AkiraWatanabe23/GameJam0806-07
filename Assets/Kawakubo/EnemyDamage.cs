using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] ParticleSystem _particles;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var Attacker = collision.GetComponent<PlayerAttack>();
        if (Attacker != null && !Attacker.CanAttack && collision.gameObject.tag == "Weapon")
        {
            AudioManager.Instance.PlaySE(SEType.EnemyDead);
            if(_particles != null)
            {
                Instantiate(_particles,this.gameObject.transform.position,Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
    }
}
