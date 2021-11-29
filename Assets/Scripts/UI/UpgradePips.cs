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
        PlayerData maxStats = gameManager.GetMaxStats();

        PopulatePips(healthPips, stats.maxHealth, maxStats.maxHealth);
        PopulatePips(energyPips, stats.maxEnergy, maxStats.maxEnergy);
        PopulatePips(damagePips, stats.damageModifier, maxStats.damageModifier);
        PopulatePips(attackPips, stats.attackSpeedModifier, maxStats.attackSpeedModifier);
        PopulatePips(speedPips, stats.speedModifier, maxStats.speedModifier);
    }
    private void PopulatePips(Pips targetPips, float playerStat, float maxStat)
    {
        for (int i = 0; i < 100; i += 10)
        {
            if (i < playerStat / maxStat * 100)
            {
                targetPips.pips[i / 10].sprite = targetPips.obtained;
            }
            else
            {
                targetPips.pips[i / 10].sprite = targetPips.unObtained;
            }
        }
    }
}
