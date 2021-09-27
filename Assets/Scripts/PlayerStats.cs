using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float speedModifier;
    public int amountOfCoins;
    public int energyAmount;

    public void SetCoins(int value)
    {
        amountOfCoins = value;
        if (amountOfCoins < 0)
            amountOfCoins = 0;
    }
    public void ObtainCoins(int value)
    {
        amountOfCoins += value;
    }
    public void LoseCoins(int value)
    {
        amountOfCoins -= value;
        if (amountOfCoins < 0)
            amountOfCoins = 0;
    }
    public void GainEnergy(int value)
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
}
