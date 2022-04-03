using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] int maxHP = 5;
    [SerializeField] int curHP = 0;
    [SerializeField] int scoreValue = 10;
    GameManager gm;

    void OnEnable()
    {
        curHP = maxHP;
        gm = FindObjectOfType<GameManager>();
    }

    void OnParticleCollision(GameObject other)
    {
        curHP--;

        if(curHP <= 0)
        {
            gameObject.GetComponent<AudioSource>().Play();
            gameObject.SetActive(false);
            gm.IncreaseScore(scoreValue);
        }
    }
}
