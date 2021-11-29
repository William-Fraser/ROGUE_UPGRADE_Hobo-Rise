using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager moneyManager;
    private int collectedMoney;
    void Start()
    {
        moneyManager = this;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Backspace))
        {
            collectedMoney = 1;
            AddMoneyToTotal();
        }
    }
    public void CollectMoney(int value, Vector3 pos)
    {
        collectedMoney += value;
        GameManager.gameManager.DisplayGUIPopup("+$" + value, pos, Color.yellow);
    }
    public void AddMoneyToTotal()
    {
        GameManager.gameManager.AddToMoneyTotal(collectedMoney);
        collectedMoney = 0;
    }
    public float GetMoney()
    {
        PlayerData stats = GameManager.gameManager.GetPlayerStats();
        return stats.totalMoney;
    }
    public float GetCollectedMoney()
    {
        return collectedMoney;
    }

    public void RemoveMoney(float moneyToRemove)
    {
        GameManager.gameManager.RemoveMoney(moneyToRemove);
    }
    public bool CanPurchase(int amountToCheckAgainst)
    {
        return GetMoney() >= amountToCheckAgainst;
    }
}
