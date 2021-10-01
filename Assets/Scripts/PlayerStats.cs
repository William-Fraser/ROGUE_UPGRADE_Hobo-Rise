using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float speedModifier;
    public int amountOfCoins;
    public int collectedCoins;
    public int displayedEnergy;
    public float energyAmount;
    public int health;
    public int maxHealth;
    public bool alive = true;


    private void Update()
    {
        if (health <= 0)
            alive = false;
    }

    #region Set, Obtain, and Lose resources
    public void SetCoins(int value)
    {
        collectedCoins = value;
        if (collectedCoins < 0)
            collectedCoins = 0;
    }
    public void ObtainCoins(int value)
    {
        collectedCoins += value;
    }
    public void LoseCoins(int value)
    {
        collectedCoins -= value;
        if (collectedCoins < 0)
            collectedCoins = 0;
    }
    public void ObtainEnergy(int value)
    {
        energyAmount += value;
    }
    public void LoseEnergy(int value)
    {
        energyAmount -= value;
        if (energyAmount < 0)
            energyAmount = 0;
    }
    public void SetEnergy(int value)
    {
        energyAmount = value;
        if (energyAmount < 0)
            energyAmount = 0;
    }
    public void ObtainHealth(int value)
    {
        health += value;
    }
    public void LoseHealth(int value)
    {
        health -= value;
        if (health < 0)
            health = 0;
    }
    public void SetHealth(int value)
    {
        health = value;
        if (health < 0)
            health = 0;
    }
    #endregion
}
