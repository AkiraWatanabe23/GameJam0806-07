using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
    [SerializeField] GameObject defeatEffect, effectOffset;
    [SerializeField] float rotationSpeed, amplitude;
    [SerializeField] string takenDamageObjectTag;
    [SerializeField] bool stopRotateWhenDefeated;
    Rigidbody2D rb;
    bool isDefeated;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (!isDefeated)
        {
            rb.angularVelocity = Mathf.Cos(Time.time * rotationSpeed) * amplitude;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //�G�l�~�[�̋����I�ɐV�������j��I�u�W�F�N�g�������y�����Ȃ̂œƎ��Ɏ������Ă܂��i��ŏ����Ǝv���j
    {
        if (collision.CompareTag(takenDamageObjectTag))
        {
            var colliders = GetComponents<Collider2D>();
            foreach (var collider in colliders)
            {
                collider.enabled = false;
            }

            if (!isDefeated)
            {
                isDefeated = true;
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
}
