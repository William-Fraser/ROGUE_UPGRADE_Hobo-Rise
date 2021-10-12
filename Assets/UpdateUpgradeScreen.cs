using System.Collections;
using System.Collections.Generic;
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
        if (GameManager.gameManager.stats.attackSpeedModifier < GameManager.gameManager.maxPossibleStats.attackSpeedModifier)
            hatButtonText.text = "Attack Speed:   $" + (10 * GameManager.gameManager.stats.attackSpeedModifier);
        else
        {
            hatButtonText.text = "Attack Speed:   Max";
            hatButton.enabled = false;
        }

        if (GameManager.gameManager.stats.maxHealth < GameManager.gameManager.maxPossibleStats.maxHealth)
            clothesButtonText.text = "Health:   $" + (1 * GameManager.gameManager.stats.maxHealth);
        else
        {
            clothesButtonText.text = "Health:   Max";
            clothesButton.enabled = false;
        }

        if (GameManager.gameManager.stats.damageModifier < GameManager.gameManager.maxPossibleStats.damageModifier)
            weaponButtonText.text = "Damage:   $" + (10 * GameManager.gameManager.stats.damageModifier);
        else
        {
            weaponButtonText.text = "Damage:   Max";
            weaponButton.enabled = false;
        }

        if (GameManager.gameManager.stats.speedModifier < GameManager.gameManager.maxPossibleStats.speedModifier)
            shoesButtonText.text = "Speed:   $" + (10 * GameManager.gameManager.stats.speedModifier);
        else
        {
            shoesButtonText.text = "Speed:   Max";
            shoesButton.enabled = false;
        }

        if (GameManager.gameManager.stats.maxEnergy < GameManager.gameManager.maxPossibleStats.maxEnergy)
            energyButtonText.text = "Energy:   $" + (1 * GameManager.gameManager.stats.maxEnergy);
        else
        {
            energyButtonText.text = "Energy:   Max";
            energyButton.enabled = false;
        }
    }
}
