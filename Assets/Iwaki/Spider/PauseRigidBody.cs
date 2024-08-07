using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseRigidBody : MonoBehaviour
{
    Rigidbody2D rb;
    bool isPaused;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isPaused)
            {
                rb.simulated = false;
                isPaused = true;
            }
            else
            {
                rb.simulated = true;
                isPaused = false;
            }
        }
    }
}
