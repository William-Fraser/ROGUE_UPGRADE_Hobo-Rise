using UnityEngine;
using UnityEngine.UI;

public class ResultsManager : MonoBehaviour
{
    public Text coinsCollected;
    public Text healthLeft;
    public Text energyLeft;

    private void Update()
    { 
        coinsCollected.text = "Coins Collected: $" + MoneyManager.moneyManager.GetCollectedMoney();
        healthLeft.text = "Health Remaining: " + GameManager.gameManager.player.GetComponent<PlayerStats>().GetHealth();
        energyLeft.text = "Energy Remaining: " + GameManager.gameManager.player.GetComponent<PlayerStats>().GetDisplayedEnergy();
    }
}
