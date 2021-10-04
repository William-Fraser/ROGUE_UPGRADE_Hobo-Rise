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
    public void UpgradeHat()
    {
        Debug.Log("Upgrading Hat");
        GameManager.gameManager.UpgradeAttackSpeed();
    }
    public void UpgradeClothes()
    {
        Debug.Log("Upgrading Clothes");
        GameManager.gameManager.UpgradeHealth();
    }
    public void UpgradeShoes()
    {
        Debug.Log("Upgrading Shoes");
        GameManager.gameManager.UpgradeSpeed();
    }
    public void UpgradeWeapon()
    {
        Debug.Log("Upgrading Weapon");
        GameManager.gameManager.UpgradeDamage();
    }
    public void UpgradeFood()
    {
        Debug.Log("Upgrading Food");
        GameManager.gameManager.UpgradeEnergy();
    }
}
