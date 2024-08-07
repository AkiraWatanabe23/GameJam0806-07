using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebController : MonoBehaviour
{
    void Start()
    {
        transform.up = -(FindAnyObjectByType<PlayerMove>().transform.position - transform.position);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, 18.77f));
    }
}
