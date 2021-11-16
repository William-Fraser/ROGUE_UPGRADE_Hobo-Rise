using UnityEngine;
using UnityEngine.UI;

public class UpdateUpgradeScreen : MonoBehaviour
{
    public Text hatButtonText;
    public Text clothesButtonText;
    public Text weaponButtonText;
    public Text shoesButtonText;
    public Text energyButtonText;

    public Button hatButton;
    public Button clothesButton;
    public Button weaponButton;
    public Button shoesButton;
    public Button energyButton;

    private void Update()
    {
        if (GameManager.gameManager.stats.attackSpeedModifier < GameManager.gameManager.maxPossibleStats.attackSpeedModifier && GameManager.gameManager.CanPurchase((int)(10 * GameManager.gameManager.stats.attackSpeedModifier)))
            hatButtonText.text = "Buy:   $" + (10 * GameManager.gameManager.stats.attackSpeedModifier);
        else if (GameManager.gameManager.stats.attackSpeedModifier < GameManager.gameManager.maxPossibleStats.attackSpeedModifier && !GameManager.gameManager.CanPurchase((int)(10 * GameManager.gameManager.stats.attackSpeedModifier)))
        {
            hatButtonText.text = "Buy:   $" + (10 * GameManager.gameManager.stats.attackSpeedModifier);
            hatButton.image.color = new Color(0, 0, 0, 55);
        }
        else
        {
            hatButtonText.text = "Max";
            hatButton.enabled = false;
        }

        if (GameManager.gameManager.stats.maxHealth < GameManager.gameManager.maxPossibleStats.maxHealth && GameManager.gameManager.CanPurchase((int)(GameManager.gameManager.stats.maxHealth)))
            clothesButtonText.text = "Buy:   $" + (1 * GameManager.gameManager.stats.maxHealth);
        else if (GameManager.gameManager.stats.maxHealth < GameManager.gameManager.maxPossibleStats.maxHealth && !GameManager.gameManager.CanPurchase((int)(GameManager.gameManager.stats.maxHealth)))
        {
            clothesButtonText.text = "Buy:   $" + (GameManager.gameManager.stats.maxHealth);
            clothesButton.image.color = new Color(0, 0, 0, 55);
        }
        else
        {
            clothesButtonText.text = "Max";
            clothesButton.enabled = false;
        }

        if (GameManager.gameManager.stats.damageModifier < GameManager.gameManager.maxPossibleStats.damageModifier && GameManager.gameManager.CanPurchase((int)(10 * GameManager.gameManager.stats.damageModifier)))
            weaponButtonText.text = "Buy:   $" + (10 * GameManager.gameManager.stats.damageModifier);
        else if (GameManager.gameManager.stats.damageModifier < GameManager.gameManager.maxPossibleStats.damageModifier && !GameManager.gameManager.CanPurchase((int)(10 * GameManager.gameManager.stats.damageModifier)))
        {
            weaponButtonText.text = "Buy:   $" + (10 * GameManager.gameManager.stats.damageModifier);
            weaponButton.image.color = new Color(0, 0, 0, 55);
        }
        else
        {
            weaponButtonText.text = "Max";
            weaponButton.enabled = false;
        }

        if (GameManager.gameManager.stats.speedModifier < GameManager.gameManager.maxPossibleStats.speedModifier && GameManager.gameManager.CanPurchase((int)(10 * GameManager.gameManager.stats.speedModifier)))
            shoesButtonText.text = "Buy:   $" + (10 * GameManager.gameManager.stats.speedModifier);
        else if (GameManager.gameManager.stats.speedModifier < GameManager.gameManager.maxPossibleStats.speedModifier && !GameManager.gameManager.CanPurchase((int)(10 * GameManager.gameManager.stats.speedModifier)))
        {
            shoesButtonText.text = "Buy:   $" + (10 * GameManager.gameManager.stats.speedModifier);
            shoesButton.image.color = new Color(0, 0, 0, 55);
        }
        else
        {
            shoesButtonText.text = "Max";
            shoesButton.enabled = false;
        }

        if (GameManager.gameManager.stats.maxEnergy < GameManager.gameManager.maxPossibleStats.maxEnergy && GameManager.gameManager.CanPurchase((int)(GameManager.gameManager.stats.maxEnergy)))
            energyButtonText.text = "Buy:   $" + (1 * GameManager.gameManager.stats.maxEnergy);
        else if (GameManager.gameManager.stats.maxEnergy < GameManager.gameManager.maxPossibleStats.maxEnergy && !GameManager.gameManager.CanPurchase((int)(GameManager.gameManager.stats.maxEnergy)))
        {
            energyButtonText.text = "Buy:   $" + (1 * GameManager.gameManager.stats.maxEnergy);
            energyButton.image.color = new Color(0, 0, 0, 55);
        }
        else
        {
            energyButtonText.text = "Max";
            energyButton.enabled = false;
        }
    }
}
