using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] int curHP;
    [SerializeField] int curScore;
    [SerializeField] int curValue;

    GameManager gm;

    void OnEnable()
    {
        gm = FindObjectOfType<GameManager>();
        curHP = gm.enemyMaxHP;
        curScore = gm.scoreValue;
        curValue = gm.cashReward;
    }

    void OnParticleCollision(GameObject other)
    {
        curHP -= gm.Damage;

        if(curHP <= 0)
        {
            gameObject.GetComponent<AudioSource>().Play();
            gameObject.SetActive(false);
            gm.IncreaseScore(curScore);
            gm.GetComponent<Bank>().ChangeBalance(curValue);
        }
    }
}
