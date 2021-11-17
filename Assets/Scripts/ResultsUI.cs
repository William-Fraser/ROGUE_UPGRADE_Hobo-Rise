using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsUI : MonoBehaviour
{
    public Text coinsCollected;
    public Text healthLeft;
    public Text energyLeft;

    private void Update()
    { 
        coinsCollected.text = "Coins Collected: $" +GameManager.gameManager.collectedMoney;
        healthLeft.text = "Health Remaining: " + GameManager.gameManager.player.GetComponent<Stats>().health;
        energyLeft.text = "Energy Remaining: " + GameManager.gameManager.player.GetComponent<Stats>().displayedEnergy;
    }
}
