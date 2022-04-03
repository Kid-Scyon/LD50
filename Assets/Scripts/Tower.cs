using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 125;

    Bank gmBank;

    public bool CreateTower(Tower tower, Vector3 pos)
    {
        gmBank = FindObjectOfType<GameManager>().GetComponent<Bank>();

        if(gmBank.CurBalance >= cost)
        {
            Instantiate(tower.gameObject, pos, Quaternion.identity);
            gmBank.changeBalance(-cost);
            return true;
        }

        return false;
    }
}
