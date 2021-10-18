using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultsUI : MonoBehaviour
{
    public TextMeshProUGUI coinsCollected;

    private void Update()
    { 
        coinsCollected.text = "" +GameManager.gameManager.collectedMoney;
    }
}
