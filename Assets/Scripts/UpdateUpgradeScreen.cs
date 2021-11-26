using UnityEngine;
using UnityEngine.UI;

public class UpdateUpgradeScreen : MonoBehaviour
{
    public Text hatButtonText;
    public Text clothesButtonText;
    public Text weaponButtonText;
    public Text shoesButtonText;
    public Text energyButtonText;
    private void Update()
    {
        GameManager gameManager = GameManager.gameManager;
        UpgradePrices upgradePrices = gameManager.GetUpgradePrices();
        PlayerData maxPossibleStats = gameManager.GetMaxStats();
        PlayerData stats = gameManager.GetPlayerStats();
        if (stats.attackSpeedModifier < maxPossibleStats.attackSpeedModifier && gameManager.CanPurchase(upgradePrices.GetAttackSpeedPrice(stats.attackSpeedModifier, maxPossibleStats.attackSpeedModifier)))
            hatButtonText.text = "Buy:   $" + upgradePrices.GetAttackSpeedPrice(stats.attackSpeedModifier, maxPossibleStats.attackSpeedModifier);
        if (stats.attackSpeedModifier < maxPossibleStats.attackSpeedModifier && !gameManager.CanPurchase(upgradePrices.GetAttackSpeedPrice(stats.attackSpeedModifier, maxPossibleStats.attackSpeedModifier)))
            hatButtonText.text = "Buy:   $" + upgradePrices.GetAttackSpeedPrice(stats.attackSpeedModifier, maxPossibleStats.attackSpeedModifier);
        else
            hatButtonText.text = "Max";


        if (stats.maxHealth < maxPossibleStats.maxHealth && gameManager.CanPurchase(upgradePrices.GetHealthPrice(stats.maxHealth, maxPossibleStats.maxHealth)))
            clothesButtonText.text = "Buy:   $" + upgradePrices.GetHealthPrice(stats.attackSpeedModifier, maxPossibleStats.maxHealth);
        else if (stats.maxHealth < maxPossibleStats.maxHealth && !gameManager.CanPurchase(upgradePrices.GetHealthPrice(stats.maxHealth, maxPossibleStats.maxHealth)))
            clothesButtonText.text = "Buy:   $" + upgradePrices.GetHealthPrice(stats.attackSpeedModifier, maxPossibleStats.maxHealth);
        else
            clothesButtonText.text = "Max";

        if (stats.damageModifier < maxPossibleStats.damageModifier && gameManager.CanPurchase(upgradePrices.GetDamagePrice(stats.damageModifier, maxPossibleStats.damageModifier)))
            weaponButtonText.text = "Buy:   $" + upgradePrices.GetDamagePrice(stats.attackSpeedModifier, maxPossibleStats.damageModifier);
        else if (stats.damageModifier < maxPossibleStats.damageModifier && !gameManager.CanPurchase(upgradePrices.GetDamagePrice(stats.damageModifier, maxPossibleStats.damageModifier)))
            weaponButtonText.text = "Buy:   $" + upgradePrices.GetDamagePrice(stats.damageModifier, maxPossibleStats.damageModifier);
        else
            weaponButtonText.text = "Max";

        if (stats.speedModifier < maxPossibleStats.speedModifier && gameManager.CanPurchase(upgradePrices.GetSpeedPrice(stats.speedModifier, maxPossibleStats.speedModifier)))
            shoesButtonText.text = "Buy:   $" + upgradePrices.GetSpeedPrice(stats.speedModifier, maxPossibleStats.speedModifier);
        else if (stats.speedModifier < maxPossibleStats.speedModifier && !gameManager.CanPurchase(upgradePrices.GetSpeedPrice(stats.speedModifier, maxPossibleStats.speedModifier)))
            shoesButtonText.text = "Buy:   $" + upgradePrices.GetSpeedPrice(stats.speedModifier, maxPossibleStats.speedModifier);
        else
            shoesButtonText.text = "Max";

        if (stats.maxEnergy < maxPossibleStats.maxEnergy && gameManager.CanPurchase(upgradePrices.GetEnergyPrice(stats.maxEnergy, maxPossibleStats.maxEnergy)))
            energyButtonText.text = "Buy:   $" + upgradePrices.GetEnergyPrice(stats.maxEnergy, maxPossibleStats.maxEnergy);
        else if (stats.maxEnergy < maxPossibleStats.maxEnergy && !gameManager.CanPurchase(upgradePrices.GetEnergyPrice(stats.maxEnergy, maxPossibleStats.maxEnergy)))
            energyButtonText.text = "Buy:   $" + upgradePrices.GetEnergyPrice(stats.maxEnergy, maxPossibleStats.maxEnergy);
        else
            energyButtonText.text = "Max";
    }
}
