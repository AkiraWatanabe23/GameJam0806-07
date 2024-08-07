using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMove_Circle : MonoBehaviour
{
    private float x = 0;
    private float z = 0;
    private float theta = 0;
    public float radius = 1f;
    public float speed = 1f;
    private bool Pause = false;
    private Vector3 centar = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        centar = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        x = Mathf.Sin(theta) * radius;
        z = Mathf.Cos(theta) * radius;

        this.transform.position = new Vector3(x, z, 0) + centar;
        if(!Pause)
        {
            AudioManager.Instance.PlaySE(SEType.Soul);
            theta += (speed * Mathf.PI / 360);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OnPause();
        }
    }

    public void OnPause()
    {
        Pause = !Pause;
    }
}
