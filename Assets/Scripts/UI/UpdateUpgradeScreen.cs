using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUpgradeScreen : MonoBehaviour
{
    public Text attackSpeedText;
    public Text healthText;
    public Text damageText;
    public Text speedText;
    public Text energyText;
    private void Update()
    {
        PlayerData maxPossibleStats = GameManager.gameManager.GetMaxStats();
        PlayerData stats = GameManager.gameManager.GetPlayerStats();

        PopulateButtonInformation(stats.maxHealth, maxPossibleStats.maxHealth, healthText, UpgradePrices.Stat.health);
        PopulateButtonInformation(stats.maxEnergy, maxPossibleStats.maxEnergy, energyText, UpgradePrices.Stat.energy);
        PopulateButtonInformation(stats.damageModifier, maxPossibleStats.damageModifier, damageText, UpgradePrices.Stat.damage);
        PopulateButtonInformation(stats.attackSpeedModifier, maxPossibleStats.attackSpeedModifier, attackSpeedText, UpgradePrices.Stat.attackSpeed);
        PopulateButtonInformation(stats.speedModifier, maxPossibleStats.speedModifier, speedText, UpgradePrices.Stat.speed);
    }

    void PopulateButtonInformation(float playerStat, float maxStat, Text text, UpgradePrices.Stat stat)
    {
        UpgradePrices upgradePrices = GameManager.gameManager.GetUpgradePrices();
        if (playerStat < maxStat && MoneyManager.moneyManager.CanPurchase(upgradePrices.GetPriceFromStat(playerStat, maxStat, stat)))
            text.text = "Buy:   $" + upgradePrices.GetPriceFromStat(playerStat, maxStat, stat);
        else if (playerStat < maxStat && !MoneyManager.moneyManager.CanPurchase(upgradePrices.GetPriceFromStat(playerStat, maxStat, stat)))
            text.text = "Buy:   $" + upgradePrices.GetPriceFromStat(playerStat, maxStat, stat);
        else
            text.text = "Max";
    }
}
