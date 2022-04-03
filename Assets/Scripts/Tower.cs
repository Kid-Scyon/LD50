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
            //Offset to ignore collision with the floor
            pos.y += 2;
            Instantiate(tower.gameObject, pos, Quaternion.identity);
            gmBank.ChangeBalance(-cost);
            return true;
        }

        return false;
    }
}
