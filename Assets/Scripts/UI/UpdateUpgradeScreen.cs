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

        PopulateButtonInformation(stats.maxHealth, maxPossibleStats.maxHealth, healthText);
        PopulateButtonInformation(stats.maxEnergy, maxPossibleStats.maxEnergy, energyText);
        PopulateButtonInformation(stats.damageModifier, maxPossibleStats.damageModifier, damageText);
        PopulateButtonInformation(stats.attackSpeedModifier, maxPossibleStats.attackSpeedModifier, attackSpeedText);
        PopulateButtonInformation(stats.speedModifier, maxPossibleStats.speedModifier, speedText);
    }

    void PopulateButtonInformation(float playerStat, float maxStat, Text text)
    {
        UpgradePrices upgradePrices = GameManager.gameManager.GetUpgradePrices();
        if (playerStat < maxStat && MoneyManager.moneyManager.CanPurchase(upgradePrices.GetEnergyPrice(playerStat, maxStat)))
            text.text = "Buy:   $" + upgradePrices.GetEnergyPrice(playerStat, maxStat);
        else if (playerStat < maxStat && !MoneyManager.moneyManager.CanPurchase(upgradePrices.GetEnergyPrice(playerStat, maxStat)))
            text.text = "Buy:   $" + upgradePrices.GetEnergyPrice(playerStat, maxStat);
        else
            text.text = "Max";
    }
}
