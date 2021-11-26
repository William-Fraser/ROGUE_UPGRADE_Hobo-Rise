using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    private readonly GameObject replacementCanvas;
    public void NextRound()
    {
        GameManager.gameManager.NextRound();
        GameManager.gameManager.ButtonPressed();
    }
    public void NewGame()
    {
        Debug.Log("Loading New Game");
        GameManager.gameManager.NewGame();
        GameManager.gameManager.ButtonPressed();
    }
    public void LoadGame()
    {
        Debug.Log("Loading Saved Game");
        GameManager.gameManager.Load();
        GameManager.gameManager.ButtonPressed();
    }
    public void QuitGame()
    {
        Debug.Log("Exiting Game");
        GameManager.gameManager.ButtonPressed();
        Application.Quit();
    }
    public void ContinueToInstructions()
    {
        GameObject canvas = GameObject.Find("Menu Canvas");
        if (canvas != null)
        {
            canvas.SetActive(false);
            replacementCanvas.SetActive(true);
        }
    }
    public bool CanAfford(float price)
    {
        if (GameManager.gameManager.stats.totalMoney >= price)
            return true;
        else
            return false;
    }
    public void BuyHouse()
    {
        GameManager.gameManager.ButtonPressed();
        if (CanAfford(GameManager.gameManager.GetHousePrice()))
        {
            GameManager.gameManager.RemoveMoney(GameManager.gameManager.GetHousePrice());
            GameManager.gameManager.BuyHouse();
        }
        else
        {
            Debug.Log("Cannot afford house, price is set to: " + (GameManager.gameManager.GetHousePrice()));
        }
    }
    public void UpgradeAttackSpeed()
    {
        UpgradePrices upgradePrices = GameManager.gameManager.GetUpgradePrices();
        float price = upgradePrices.GetAttackSpeedPrice(GameManager.gameManager.GetAttackSpeedUpgrades());
        GameManager.gameManager.ButtonPressed();
        if (CanAfford(price))
        {
            Debug.Log("Upgrading Hat");
            GameManager.gameManager.RemoveMoney(price);
            GameManager.gameManager.UpgradeAttackSpeed();
        }
        else
        {
            Debug.Log("Cannot afford hat, price is set to: " + (price));
        }
    }
    public void UpgradeHealth()
    {
        UpgradePrices upgradePrices = GameManager.gameManager.GetUpgradePrices();
        float price = upgradePrices.GetHealthPrice(GameManager.gameManager.GetHealthUpgrades());
        GameManager.gameManager.ButtonPressed();
        if (CanAfford(price))
        {
            Debug.Log("Upgrading Clothes");
            GameManager.gameManager.RemoveMoney(price);
            GameManager.gameManager.UpgradeHealth();
        }
        else
        {
            Debug.Log("Cannot afford clothes, price is set to: " + (price));
        }
    }
    public void UpgradeSpeed()
    {
        UpgradePrices upgradePrices = GameManager.gameManager.GetUpgradePrices();
        float price = upgradePrices.GetSpeedPrice(GameManager.gameManager.GetSpeedUpgrades());
        GameManager.gameManager.ButtonPressed();
        if (CanAfford(price))
        {
            Debug.Log("Upgrading Shoes");
            GameManager.gameManager.RemoveMoney(price);
            GameManager.gameManager.UpgradeSpeed();
        }
        else
        {
            Debug.Log("Cannot afford shoes, price is set to: " + (price));
        }
    }
    public void UpgradeDamage()
    {
        UpgradePrices upgradePrices = GameManager.gameManager.GetUpgradePrices();
        float price = upgradePrices.GetDamagePrice(GameManager.gameManager.GetAttackUpgrades());
        GameManager.gameManager.ButtonPressed();
        if (CanAfford(price))
        {
            Debug.Log("Upgrading Weapon");
            GameManager.gameManager.RemoveMoney(price);
            GameManager.gameManager.UpgradeDamage();
        }
        else
        {
            Debug.Log("Cannot afford weapon, price is set to: " + (price));
        }
    }
    public void UpgradeEnergy()
    {
        UpgradePrices upgradePrices = GameManager.gameManager.GetUpgradePrices();
        float price = upgradePrices.GetEnergyPrice(GameManager.gameManager.GetEnergyUpgrades());
        GameManager.gameManager.ButtonPressed();
        if (CanAfford(price))
        {
            Debug.Log("Upgrading Energy");
            GameManager.gameManager.RemoveMoney(price);
            GameManager.gameManager.UpgradeEnergy();
        }
        else
        {
            Debug.Log("Cannot afford Energy, price is set to: " + (price));
        }
    }
    public void Save()
    {
        GameManager.gameManager.AttemptSave();
    }
    public void Load()
    {
        GameManager.gameManager.Load();
    }
}
