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
        energyImage.rectTransform.localScale = new Vector3(GameManager.gameManager.player.GetComponent<Stats>().energy / GameManager.gameManager.player.GetComponent<Stats>().maxEnergy, GameManager.gameManager.player.GetComponent<Stats>().energy / GameManager.gameManager.player.GetComponent<Stats>().maxEnergy, 1);
        energy.text = (int)((GameManager.gameManager.player.GetComponent<Stats>().energy/ GameManager.gameManager.player.GetComponent<Stats>().maxEnergy)*100)+"%";
    }
}
