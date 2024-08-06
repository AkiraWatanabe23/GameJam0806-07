using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KanabouController : MonoBehaviour
{
    Rigidbody2D rb;

    public void Throw(Transform transform, Transform target, float speed)
    {
        this.transform.position = transform.position;

        rb = GetComponent<Rigidbody2D>();

        var direction = target.position - transform.position;
        direction.Normalize();

        rb.velocity = direction * speed;
    }
}
