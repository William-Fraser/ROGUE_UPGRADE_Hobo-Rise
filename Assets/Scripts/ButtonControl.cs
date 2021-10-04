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
    }
    public void NewGame()
    {
        Debug.Log("Loading New Game");
        // put new game method from GM here
    }
    public void LoadGame()
    {
        Debug.Log("Loading Saved Game");
        // put load method from GM here
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
    public void UpgradeHat()
    {
        Debug.Log("Upgrading Hat");
    }
    public void UpgradeClothes()
    {
        Debug.Log("Upgrading Clothes");
    }
    public void UpgradeShoes()
    {
        Debug.Log("Upgrading Shoes");
    }
    public void UpgradeWeapon()
    {
        Debug.Log("Upgrading Weapon");
    }
    public void UpgradeFood()
    {
        Debug.Log("Upgrading Food");
    }
}
