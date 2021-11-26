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
        UpgradePrices upgradePrices = GameManager.gameManager.GetUpgradePrices();
        if (GameManager.gameManager.stats.attackSpeedModifier < GameManager.gameManager.maxPossibleStats.attackSpeedModifier && GameManager.gameManager.CanPurchase((int)(10 * GameManager.gameManager.stats.attackSpeedModifier)))
            hatButtonText.text = "Buy:   $" + upgradePrices.GetAttackSpeedPrice(GameManager.gameManager.GetAttackSpeedUpgrades());
        else if (GameManager.gameManager.stats.attackSpeedModifier < GameManager.gameManager.maxPossibleStats.attackSpeedModifier && !GameManager.gameManager.CanPurchase((int)(10 * GameManager.gameManager.stats.attackSpeedModifier)))
            hatButtonText.text = "Buy:   $" + upgradePrices.GetAttackSpeedPrice(GameManager.gameManager.GetAttackSpeedUpgrades());
        else
            hatButtonText.text = "Max";


        if (GameManager.gameManager.stats.maxHealth < GameManager.gameManager.maxPossibleStats.maxHealth && GameManager.gameManager.CanPurchase((int)(GameManager.gameManager.stats.maxHealth)))
            clothesButtonText.text = "Buy:   $" + upgradePrices.GetHealthPrice(GameManager.gameManager.GetHealthUpgrades());
        else if (GameManager.gameManager.stats.maxHealth < GameManager.gameManager.maxPossibleStats.maxHealth && !GameManager.gameManager.CanPurchase((int)(GameManager.gameManager.stats.maxHealth)))
            clothesButtonText.text = "Buy:   $" + upgradePrices.GetHealthPrice(GameManager.gameManager.GetHealthUpgrades());
        else
            clothesButtonText.text = "Max";

        if (GameManager.gameManager.stats.damageModifier < GameManager.gameManager.maxPossibleStats.damageModifier && GameManager.gameManager.CanPurchase((int)(10 * GameManager.gameManager.stats.damageModifier)))
            weaponButtonText.text = "Buy:   $" + upgradePrices.GetDamagePrice(GameManager.gameManager.GetAttackUpgrades());
        else if (GameManager.gameManager.stats.damageModifier < GameManager.gameManager.maxPossibleStats.damageModifier && !GameManager.gameManager.CanPurchase((int)(10 * GameManager.gameManager.stats.damageModifier)))
            weaponButtonText.text = "Buy:   $" + upgradePrices.GetDamagePrice(GameManager.gameManager.GetAttackUpgrades());
        else
            weaponButtonText.text = "Max";

        if (GameManager.gameManager.stats.speedModifier < GameManager.gameManager.maxPossibleStats.speedModifier && GameManager.gameManager.CanPurchase((int)(10 * GameManager.gameManager.stats.speedModifier)))
            shoesButtonText.text = "Buy:   $" + upgradePrices.GetSpeedPrice(GameManager.gameManager.GetSpeedUpgrades());
        else if (GameManager.gameManager.stats.speedModifier < GameManager.gameManager.maxPossibleStats.speedModifier && !GameManager.gameManager.CanPurchase((int)(10 * GameManager.gameManager.stats.speedModifier)))
            shoesButtonText.text = "Buy:   $" + upgradePrices.GetSpeedPrice(GameManager.gameManager.GetSpeedUpgrades());
        else
            shoesButtonText.text = "Max";

        if (GameManager.gameManager.stats.maxEnergy < GameManager.gameManager.maxPossibleStats.maxEnergy && GameManager.gameManager.CanPurchase((int)(GameManager.gameManager.stats.maxEnergy)))
            energyButtonText.text = "Buy:   $" + upgradePrices.GetEnergyPrice(GameManager.gameManager.GetEnergyUpgrades());
        else if (GameManager.gameManager.stats.maxEnergy < GameManager.gameManager.maxPossibleStats.maxEnergy && !GameManager.gameManager.CanPurchase((int)(GameManager.gameManager.stats.maxEnergy)))
            energyButtonText.text = "Buy:   $" + upgradePrices.GetEnergyPrice(GameManager.gameManager.GetEnergyUpgrades());
        else
            energyButtonText.text = "Max";
    }
}
