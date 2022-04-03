using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseHP : MonoBehaviour
{
    [SerializeField] int baseHP = 100;
    [SerializeField] TextMeshProUGUI hpUI;
    [SerializeField] TextMeshProUGUI finalScore;


    [SerializeField] Canvas mainUI;
    [SerializeField] Canvas gameOverUI;
    GameManager gm;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
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
            finalScore.text = "Score: " + gm.Score;
            Time.timeScale = 0;
            Debug.Log("Player loses!");
        }
    }
}
