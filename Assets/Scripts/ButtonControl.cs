using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    private GameObject UpgradesCanvas;
    public void Start()
    {
        if (GameObject.Find("Upgrades Canvas") != null)
        {
            UpgradesCanvas = GameObject.Find("Upgrades Canvas").gameObject;
            UpgradesCanvas.SetActive(false);
        }

        if (GameManager.gameManager.isOnUpgrade())
        {
            ContinueToUpgrade();
        }
    }
    public void NewGame()
    {
        Debug.Log("Loading New Game");
        GameManager.gameManager.NewGame();
    }
    public void LoadGame()
    {
        Debug.Log("Loading Saved Game");
        GameManager.gameManager.Load();
    }
    public void QuitGame()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }
    public void ContinueToUpgrade()
    {
        if (GameObject.Find("Results Canvas") != null)
        {
            GameObject.Find("Results Canvas").gameObject.SetActive(false);
            UpgradesCanvas.SetActive(true);
        }
    }
    public bool CanAfford(float price)
    {
        if (GameManager.gameManager.stats.totalMoney >= price)
            return true;
        else
            return false;
    }
    public void UpgradeHat()
    {
        float price = 10 * GameManager.gameManager.stats.attackSpeedModifier;
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
    public void UpgradeClothes()
    {
        float price = 1 * GameManager.gameManager.stats.maxHealth;
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
    public void UpgradeShoes()
    {
        float price = 10 * GameManager.gameManager.stats.speedModifier;
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
    public void UpgradeWeapon()
    {
        float price = 10 * GameManager.gameManager.stats.damageModifier;
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
    public void UpgradeFood()
    {
        float price = 1 * GameManager.gameManager.stats.maxEnergy;
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
