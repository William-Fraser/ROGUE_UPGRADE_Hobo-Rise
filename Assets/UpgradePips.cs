using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePips : MonoBehaviour
{

    public Pips speedPips;
    public Pips attackPips;
    public Pips attackSpeedPips;
    public Pips healthPips;
    public Pips energyPips;
    public float speed;
    [Serializable]
    public struct Pips
    {
        public Image[] pips;
        public Sprite unObtained;
        public Sprite obtained;
    }
    void Update()
    {
        speed = GameManager.gameManager.GetSpeedUpgrades();
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
    }
}
