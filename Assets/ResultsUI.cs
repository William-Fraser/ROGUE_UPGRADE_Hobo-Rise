using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultsUI : MonoBehaviour
{
    public TextMeshProUGUI coinsCollected;
    public TextMeshProUGUI healthLeft;
    public TextMeshProUGUI enemiesKilled;
    public TextMeshProUGUI distanceTraveled;
    public TextMeshProUGUI damageDealt;

    private void Update()
    { 
        coinsCollected.text = "" +GameManager.gameManager.collectedMoney;
        healthLeft.text = "" + GameManager.gameManager.player.GetComponent<Stats>().health;
        enemiesKilled.text = "" + GameManager.gameManager.enemiesKilled;
        distanceTraveled.text = "" + GameManager.gameManager.collectedMoney;
        damageDealt.text = "" + GameManager.gameManager.damageDealt;
    }
}
