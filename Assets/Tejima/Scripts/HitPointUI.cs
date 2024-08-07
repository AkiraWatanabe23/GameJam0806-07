using UnityEngine;

public class HitPointUI : MonoBehaviour
{
    [SerializeField] GameObject[] hearts = new GameObject[5];
    [SerializeField] bool isdamage = false;//�_���[�W���󂯂����ǂ���
    int damageCount = 1;//�_���[�W���󂯂���

    void Start()
    {

    }


    void Update()
    {
        if (isdamage == true && damageCount < 6)
        {
            hearts[hearts.Length - damageCount].SetActive(false);
            isdamage = false;
            damageCount++;
        }
    }

    public void Damage()
    {
        isdamage = true;
    }
}
