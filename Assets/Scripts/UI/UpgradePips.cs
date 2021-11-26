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

    private GameManager gameManager;
    [Serializable]
    public struct Pips
    {
        public Image[] pips;
        public Sprite unObtained;
        public Sprite obtained;
    }
    private void Awake()
    {
        gameManager = GameManager.gameManager;
    }
    void Update()
    {
        PlayerData stats = gameManager.GetPlayerStats();
        PlayerData maxStats = gameManager.GetPlayerStats();
        for (int i = 0; i < 100; i += 10)
        {
            if(i < stats.speedModifier/maxStats.speedModifier*100)
            {
                speedPips.pips[i/10].sprite = speedPips.obtained;
            } else
            {
                speedPips.pips[i/10].sprite = speedPips.unObtained;
            }
        }

        for (int i = 0; i < 100; i += 10)
        {
            if (i < stats.attackSpeedModifier / maxStats.attackSpeedModifier * 100)
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
            if (i < stats.damageModifier / maxStats.damageModifier * 100)
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
            if (i < stats.maxEnergy / maxStats.maxEnergy * 100)
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
            if (i < stats.maxHealth / maxStats.maxHealth * 100)
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
