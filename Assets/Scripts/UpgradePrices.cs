using System;

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
        float percentStat = playerStat / maxStat;
        percentStat *= 100;
        if (percentStat > 100) percentStat = 100;
        int i = (int)(percentStat / 10);
        if (i == 10) i = 9;
        return health[i - 1];
    }
    public int GetSpeedPrice(float playerStat, float maxStat)
    {
        float percentStat = playerStat / maxStat;
        percentStat *= 100;
        if (percentStat > 100) percentStat = 100;
        int i = (int)(percentStat / 10);
        if (i == 10) i = 9;
        return speed[i - 1];
    }
    public int GetDamagePrice(float playerStat, float maxStat)
    {
        float percentStat = playerStat / maxStat;
        percentStat *= 100;
        if (percentStat > 100) percentStat = 100;
        int i = (int)(percentStat / 10);
        if (i == 10) i = 9;
        return damage[i - 1];
    }
    public int GetAttackSpeedPrice(float playerStat, float maxStat)
    {
        float percentStat = playerStat / maxStat;
        percentStat *= 100;
        if (percentStat > 100) percentStat = 100;
        int i = (int)(percentStat / 10);
        if (i == 10) i = 9;
        return attackSpeed[i - 1];
    }
    public int GetEnergyPrice(float playerStat, float maxStat)
    {
        float percentStat = playerStat / maxStat;
        percentStat *= 100;
        if (percentStat > 100) percentStat = 100;
        int i = (int)(percentStat / 10);
        if (i == 10) i = 9;
        return energy[i - 1];
    }
}