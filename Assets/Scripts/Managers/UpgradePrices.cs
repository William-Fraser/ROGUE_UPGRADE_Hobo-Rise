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

    public int GetHealthPrice(float playerStat, float maxStat)
    {
        int i = (int)(playerStat/maxStat);
        if (i >= health.Length) i = health.Length - 1;
        return health[i];
    }
    public int GetSpeedPrice(float playerStat, float maxStat)
    {
        int i = (int)(playerStat / maxStat);
        if (i >= speed.Length) i = speed.Length-1;
        return speed[i];
    }
    public int GetDamagePrice(float playerStat, float maxStat)
    {
        int i = (int)(playerStat / maxStat);
        if (i >= damage.Length) i = damage.Length - 1;
        return damage[i];
    }
    public int GetAttackSpeedPrice(float playerStat, float maxStat)
    {
        int i = (int)(playerStat / maxStat);
        if (i >= attackSpeed.Length) i = attackSpeed.Length - 1;
        return attackSpeed[i];
    }
    public int GetEnergyPrice(float playerStat, float maxStat)
    {
        int i = (int)(playerStat / maxStat);
        if (i >= energy.Length) i = energy.Length - 1;
        return energy[i];
    }
}