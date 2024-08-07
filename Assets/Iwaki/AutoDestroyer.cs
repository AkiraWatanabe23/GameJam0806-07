using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyer : MonoBehaviour
{
    float timeToDestroy;
    float t;
    bool isPaused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isPaused = !isPaused;
        }

        if (!isPaused)
        {
            t += Time.deltaTime;
            if (t > timeToDestroy)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetTimer(float timeToDestroy)
    {
        this.timeToDestroy = timeToDestroy;
    }
}
