using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseMeterUpdate : MonoBehaviour
{
    public Image meter;
    public Vector2 meterSize;

    private void Awake()
    {
        meterSize = meter.rectTransform.sizeDelta;
    }
    private void Update()
    {
        float meterBaseUnit = meterSize.x / 100;
        if (meterBaseUnit * (MoneyManager.moneyManager.GetMoney() / GameManager.gameManager.GetHousePrice()) * 100 <= meterSize.x)
        {
            meter.rectTransform.sizeDelta = new Vector2(meterBaseUnit * (MoneyManager.moneyManager.GetMoney() / GameManager.gameManager.GetHousePrice()) * 100, meterSize.y);
        } else
        {
            meter.rectTransform.sizeDelta = meterSize;
        }
    }
}
