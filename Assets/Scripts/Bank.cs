using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int startBalance = 500;
    [SerializeField] TextMeshProUGUI balanceUI;

    int curBalance = 0;

    public int CurBalance { get { return curBalance; } }

    private void Awake()
    {
        curBalance = startBalance;
        balanceUI.text = "$" + curBalance;
    }

    public void ChangeBalance(int balanceChange)
    {
        curBalance += balanceChange;
        balanceUI.text = "$" + curBalance;
    }
}
