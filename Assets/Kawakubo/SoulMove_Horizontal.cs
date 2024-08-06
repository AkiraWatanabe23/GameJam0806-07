using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMove_Horizontal : MonoBehaviour
{
    private float x = 0;
    private float z = 0;
    private float theta = 0;
    public float distance = 1f;
    public float speed = 1f;
    private Vector3 centar = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        centar = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        x = Mathf.Sin(theta) * distance;
        z = Mathf.Cos(theta) * distance;

        this.transform.position = new Vector3(x, 0, z) + centar;
        theta += (speed * Mathf.PI / 360);
    }
}
