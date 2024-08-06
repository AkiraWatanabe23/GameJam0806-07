using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShitakaraController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameManager gameManager;

    private void Start()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMove>())
        {
            gameManager.PlayerDead();
        }
    }
}
