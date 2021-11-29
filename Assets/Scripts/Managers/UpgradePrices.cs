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
        if (i >= 10) i = 9;
        Debug.Log(i);
        return health[i];
    }
    public int GetSpeedPrice(float playerStat, float maxStat)
    {
        int i = (int)(playerStat / maxStat);
        if (i >= 10) i = 9;
        return speed[i];
    }
    public int GetDamagePrice(float playerStat, float maxStat)
    {
        int i = (int)(playerStat / maxStat);
        if (i >= 10) i = 9;
        return damage[i];
    }
    public int GetAttackSpeedPrice(float playerStat, float maxStat)
    {
        int i = (int)(playerStat / maxStat);
        if (i >= 10) i = 9;
        return attackSpeed[i];
    }
    public int GetEnergyPrice(float playerStat, float maxStat)
    {
        int i = (int)(playerStat / maxStat);
        if (i >= 10) i = 9;
        return energy[i];
    }
}