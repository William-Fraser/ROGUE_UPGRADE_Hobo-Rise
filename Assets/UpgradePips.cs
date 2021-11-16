using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePips : MonoBehaviour
{

    public Pips speedPips;
    public Pips attackPips;
    public Pips damagePips;
    public Pips healthPips;
    public Pips energyPips;

    public float testVariable;
    [Serializable]
    public struct Pips
    {
        public Image[] pips;
        public Sprite unObtained;
        public Sprite obtained;
    }
    void Update()
    {
        for (int i = 0; i < 100; i += 10)
        {
            if(i < GameManager.gameManager.GetSpeedUpgrades())
            {
                speedPips.pips[i/10].sprite = speedPips.obtained;
            } else
            {
                speedPips.pips[i/10].sprite = speedPips.unObtained;
            }
        }

        for (int i = 0; i < 100; i += 10)
        {
            if (i < GameManager.gameManager.GetAttackSpeedUpgrades())
            {
                attackPips.pips[i / 10].sprite = attackPips.obtained;
            }
            else
            {
                attackPips.pips[i / 10].sprite = attackPips.unObtained;
            }
        }

        for (int i = 0; i < 100; i += 10)
        {
            if (i < GameManager.gameManager.GetAttackUpgrades())
            {
                damagePips.pips[i / 10].sprite = damagePips.obtained;
            }
            else
            {
                damagePips.pips[i / 10].sprite = damagePips.unObtained;
            }
        }

        for (int i = 0; i < 100; i += 10)
        {
            if (i < GameManager.gameManager.GetEnergyUpgrades())
            {
                energyPips.pips[i / 10].sprite = energyPips.obtained;
            }
            else
            {
                energyPips.pips[i / 10].sprite = energyPips.unObtained;
            }
        }

        for (int i = 0; i < 100; i += 10)
        {
            if (i < GameManager.gameManager.GetHealthUpgrades())
            {
                healthPips.pips[i / 10].sprite = healthPips.obtained;
            }
            else
            {
                healthPips.pips[i / 10].sprite = healthPips.unObtained;
            }
        }
    }
}
