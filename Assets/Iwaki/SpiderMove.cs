using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMove : MonoBehaviour
{
    [SerializeField] GameObject defeatEffect, effectOffset;
    [SerializeField] float rotationSpeed, amplitude;
    [SerializeField] string takenDamageObjectTag;
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

    private void OnTriggerEnter2D(Collider2D collision)
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
                rb.gravityScale = 1;
                rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
                var effect = Instantiate(defeatEffect);
                effect.transform.position = effectOffset.transform.position;
                isDefeated = true;
                Destroy(gameObject, 3);
            }
        }
    }
}
