using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] int maxHP = 5;
    [SerializeField] int curHP = 0;
    [SerializeField] int statIncrease = 2;


    [SerializeField] int scoreValue = 10;
    [SerializeField] int cashReward = 5;

    GameManager gm;

    void OnEnable()
    {
        curHP = maxHP;
        gm = FindObjectOfType<GameManager>();
    }

    void OnParticleCollision(GameObject other)
    {
        curHP -= gm.Damage;

        if(curHP <= 0)
        {
            gameObject.GetComponent<AudioSource>().Play();
            gameObject.SetActive(false);
            gm.IncreaseScore(scoreValue);
            IncreaseStats();
            gm.GetComponent<Bank>().ChangeBalance(cashReward);
        }
    }

    void IncreaseStats()
    {
        maxHP += statIncrease;
        scoreValue += statIncrease;
        cashReward += statIncrease;
    }
}
