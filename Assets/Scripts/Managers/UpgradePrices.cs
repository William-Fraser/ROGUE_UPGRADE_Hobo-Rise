using System;
using UnityEngine;

[Serializable]
public class UpgradePrices
{
    public int[] health;
    public int[] speed;
    public int[] damage;
    public int[] attackSpeed;
    public int[] energy;

    public enum Stat
    {
        health,
        speed,
        damage,
        attackSpeed,
        energy
    }

    public int GetPriceFromStat(float playerStat, float maxStat, Stat stat)
    {
        switch (stat)
        {
            case Stat.health:
                return GetHealthPrice(playerStat, maxStat);
            case Stat.speed:
                return GetSpeedPrice(playerStat, maxStat);
            case Stat.damage:
                return GetDamagePrice(playerStat, maxStat);
            case Stat.attackSpeed:
                return GetAttackSpeedPrice(playerStat, maxStat);
            case Stat.energy:
                return GetEnergyPrice(playerStat, maxStat);
        }
        return 0;
    }
    public int GetHealthPrice(float playerStat, float maxStat)
    {
        int i = (int)(playerStat/maxStat*10);
        if (i >= health.Length) i = health.Length;
        return health[i-1];
    }
    public int GetSpeedPrice(float playerStat, float maxStat)
    {
        int i = (int)(playerStat / maxStat * 10);
        if (i >= speed.Length) i = speed.Length;
        return speed[i-1];
    }
    public int GetDamagePrice(float playerStat, float maxStat)
    {
        int i = (int)(playerStat / maxStat * 10);
        if (i >= damage.Length) i = damage.Length;
        return damage[i-1];
    }
    public int GetAttackSpeedPrice(float playerStat, float maxStat)
    {
        int i = (int)(playerStat / maxStat * 10);
        if (i >= attackSpeed.Length) i = attackSpeed.Length;
        return attackSpeed[i-1];
    }
    public int GetEnergyPrice(float playerStat, float maxStat)
    {
        int i = (int)(playerStat / maxStat * 10);
        if (i >= energy.Length) i = energy.Length;
        return energy[i-1];
    }
}