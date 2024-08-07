using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] ParticleSystem _particles;
    public Transform particlePosition;

    private void Start()
    {
        if(particlePosition == null)
        {
            particlePosition = this.gameObject.transform;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var Attacker = collision.GetComponent<PlayerAttack>();
        if (Attacker != null && !Attacker.CanAttack && collision.gameObject.tag == "Weapon")
        {
            AudioManager.Instance.PlaySE(SEType.EnemyDead);
            if(_particles != null)
            {
                Instantiate(_particles,particlePosition.position,Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
    }
}
