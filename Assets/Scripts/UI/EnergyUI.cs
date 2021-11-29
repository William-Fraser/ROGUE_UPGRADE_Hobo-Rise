using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    public Image energyImage;
    public Text energy;
    private void Update()
    {
        energyImage.rectTransform.localScale = new Vector3(GameManager.gameManager.player.GetComponent<PlayerStats>().GetEnergy() / GameManager.gameManager.player.GetComponent<PlayerStats>().GetMaxEnergy(), GameManager.gameManager.player.GetComponent<PlayerStats>().GetEnergy() / GameManager.gameManager.player.GetComponent<PlayerStats>().GetMaxEnergy(), 1);
        energy.text = (int)((GameManager.gameManager.player.GetComponent<PlayerStats>().GetEnergy() / GameManager.gameManager.player.GetComponent<PlayerStats>().GetMaxEnergy()) *100)+"%";
    }
}
