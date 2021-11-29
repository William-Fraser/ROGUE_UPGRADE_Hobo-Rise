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
        if (MoneyManager.moneyManager.GetMoney() >= price)
            return true;
        else
            return false;
    }
    public void BuyHouse()
    {
        GameManager.gameManager.ButtonPressed();
        if (CanAfford(GameManager.gameManager.GetHousePrice()))
        {
            MoneyManager.moneyManager.RemoveMoney(GameManager.gameManager.GetHousePrice());
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
        PlayerData stats = GameManager.gameManager.GetPlayerStats();
        PlayerData maxStats = GameManager.gameManager.GetMaxStats();
        float price = upgradePrices.GetAttackSpeedPrice(stats.attackSpeedModifier, maxStats.attackSpeedModifier);
        GameManager.gameManager.ButtonPressed();
        if (CanAfford(price))
        {
            Debug.Log("Upgrading Hat");
            MoneyManager.moneyManager.RemoveMoney(price);
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
        PlayerData stats = GameManager.gameManager.GetPlayerStats();
        PlayerData maxStats = GameManager.gameManager.GetMaxStats();
        float price = upgradePrices.GetHealthPrice(stats.maxHealth, maxStats.maxHealth);
        GameManager.gameManager.ButtonPressed();
        if (CanAfford(price))
        {
            Debug.Log("Upgrading Clothes");
            MoneyManager.moneyManager.RemoveMoney(price);
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
        PlayerData stats = GameManager.gameManager.GetPlayerStats();
        PlayerData maxStats = GameManager.gameManager.GetMaxStats();
        float price = upgradePrices.GetSpeedPrice(stats.speedModifier, maxStats.speedModifier);
        GameManager.gameManager.ButtonPressed();
        if (CanAfford(price))
        {
            Debug.Log("Upgrading Shoes");
            MoneyManager.moneyManager.RemoveMoney(price);
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
        PlayerData stats = GameManager.gameManager.GetPlayerStats();
        PlayerData maxStats = GameManager.gameManager.GetMaxStats();
        float price = upgradePrices.GetDamagePrice(stats.damageModifier, maxStats.damageModifier);
        GameManager.gameManager.ButtonPressed();
        if (CanAfford(price))
        {
            Debug.Log("Upgrading Weapon");
            MoneyManager.moneyManager.RemoveMoney(price);
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
        PlayerData stats = GameManager.gameManager.GetPlayerStats();
        PlayerData maxStats = GameManager.gameManager.GetMaxStats();
        float price = upgradePrices.GetEnergyPrice(stats.maxEnergy, maxStats.maxEnergy);
        GameManager.gameManager.ButtonPressed();
        if (CanAfford(price))
        {
            Debug.Log("Upgrading Energy");
            MoneyManager.moneyManager.RemoveMoney(price);
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

    public void ManageCredits()
    {
        GameManager.gameManager.ManageCredits();
    }
}
