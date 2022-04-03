using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseHP : MonoBehaviour
{
    [SerializeField] int baseHP = 100;
    [SerializeField] TextMeshProUGUI hpUI;

    [SerializeField] Canvas mainUI;
    [SerializeField] Canvas gameOverUI;

    private void Awake()
    {
        gameOverUI.enabled = false;
        hpUI.text = baseHP + " HP";
    }

    public void reduceHP(int dmg)
    {
        baseHP -= dmg;
        hpUI.text = baseHP + " HP";
        if(baseHP <= 0)
        {
            //TODO: Loss state
            mainUI.enabled = false;
            gameOverUI.enabled = true;
            Time.timeScale = 0;
            Debug.Log("Player loses!");
        }
    }
}
