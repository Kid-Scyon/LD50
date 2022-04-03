using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseHP : MonoBehaviour
{
    [SerializeField] int maxHP = 100;
    [SerializeField] int baseHP = 100;
    [SerializeField] int healAmount = 10;
    [SerializeField] int healCost = 100;
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

    public void Heal()
    {
        //If you have the money, you can heal back lost HP (no overheal though)
        if(gm.GetComponent<Bank>().CurBalance >= healCost && (baseHP + healAmount) <= maxHP)
        {
            gm.GetComponent<Bank>().ChangeBalance(-healCost);
            baseHP += healAmount;
            hpUI.text = baseHP + " HP";
        }
    }
}
