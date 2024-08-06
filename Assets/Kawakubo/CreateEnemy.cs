using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    [SerializeField] GameObject Enemy;
    private GameObject m_Enemy;
    private bool IsKilled = false;
    private Coroutine enumerator;
    private Vector3 centar = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        centar = this.transform.position;
        m_Enemy = Instantiate(Enemy,centar,Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_Enemy == null && !IsKilled)
        {
            enumerator = StartCoroutine(ReCreateEnemy());
            IsKilled = true;
        }
    }

    private IEnumerator ReCreateEnemy()
    {
        yield return new WaitForSeconds(5);
        if(IsKilled )
        {
            m_Enemy = Instantiate(Enemy,centar,Quaternion.identity);
            IsKilled = false;
        }
        StopCoroutine(enumerator);
    }
}
