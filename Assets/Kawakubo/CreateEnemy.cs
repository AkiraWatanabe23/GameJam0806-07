using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    [SerializeField] GameObject Enemy;
    private GameObject m_Enemy;
    private bool IsKilled = false;
    private Coroutine enumerator;
    // Start is called before the first frame update
    void Start()
    {
        m_Enemy = Instantiate(Enemy);
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
            m_Enemy = Instantiate(Enemy);
            IsKilled = false;
        }
        StopCoroutine(enumerator);
    }
}
