using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    public Text energyText;
    private void Update()
    {
        energyText.text = "Energy: " + GameManager.gameManager.player.GetComponent<Stats>().displayedEnergy;
    }
}
