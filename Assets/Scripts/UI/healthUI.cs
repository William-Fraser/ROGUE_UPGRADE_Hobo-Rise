using UnityEngine;
using UnityEngine.UI;

public class healthUI : MonoBehaviour
{
    public Image heart;
    public Text health;
    private void Update()
    {
        heart.rectTransform.localScale = new Vector3(GameManager.gameManager.player.GetComponent<PlayerStats>().GetHealth() / GameManager.gameManager.player.GetComponent<PlayerStats>().GetMaxHealth(), GameManager.gameManager.player.GetComponent<PlayerStats>().GetHealth() / GameManager.gameManager.player.GetComponent<PlayerStats>().GetMaxHealth(), 1);
        health.text = ""+GameManager.gameManager.player.GetComponent<PlayerStats>().GetHealth();
    }
}
