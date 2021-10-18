using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthUI : MonoBehaviour
{
    public Text healthText;
    private void Update()
    {
        healthText.text = "Health: " + GameManager.gameManager.player.GetComponent<Stats>().health;
    }
}
