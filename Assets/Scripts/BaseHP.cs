using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseHP : MonoBehaviour
{
    [SerializeField] int baseHP = 100;
    [SerializeField] TextMeshProUGUI hpUI;

    private void Awake()
    {
        hpUI.text = baseHP + " HP";
    }

    public void reduceHP(int dmg)
    {
        baseHP -= dmg;
        hpUI.text = baseHP + " HP";
        if(baseHP >= 0)
        {
            //TODO: Loss state
            Debug.Log("Player loses!");
        }
    }
}
