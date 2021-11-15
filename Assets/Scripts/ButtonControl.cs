using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    private GameObject replacementCanvas;
    public void Start()
    {
        if (GameObject.Find("Upgrades Canvas") != null)
        {
            replacementCanvas = GameObject.Find("Upgrades Canvas").gameObject;
            replacementCanvas.SetActive(false);
        }
        else if (GameObject.Find("Instructions Canvas") != null)
        {
            replacementCanvas = GameObject.Find("Instructions Canvas").gameObject;
            replacementCanvas.SetActive(false);
        }

        if (GameManager.gameManager.isOnUpgrade())
        {
            ContinueToUpgrade();
        }
    }
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
    public void ContinueToUpgrade()
    {
        GameManager.gameManager.ButtonPressed();
        if (GameObject.Find("Results Canvas") != null)
        {
            GameObject.Find("Results Canvas").gameObject.SetActive(false);
            replacementCanvas.SetActive(true);
        }
    }
    public void ContinueToInstructions()
    {
        if (GameObject.Find("Menu Canvas") != null)
        {
            GameObject.Find("Menu Canvas").gameObject.SetActive(false);
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
        float price = 10 * GameManager.gameManager.stats.attackSpeedModifier;
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
        float price = 1 * GameManager.gameManager.stats.maxHealth;
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
        float price = 10 * GameManager.gameManager.stats.speedModifier;
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
        float price = 10 * GameManager.gameManager.stats.damageModifier;
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
        float price = 1 * GameManager.gameManager.stats.maxEnergy;
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
