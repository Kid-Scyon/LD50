using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] int maxHP = 5;
    [SerializeField] int curHP = 0;

    void OnEnable()
    {
        curHP = maxHP;
    }

    void OnParticleCollision(GameObject other)
    {
        curHP--;

        if(curHP <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
