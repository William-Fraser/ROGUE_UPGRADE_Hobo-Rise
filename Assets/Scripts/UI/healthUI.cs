using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class healthUI : MonoBehaviour
{
    public Image heart;
    public Text health;
    private void Update()
    {
        heart.rectTransform.localScale = new Vector3(GameManager.gameManager.player.GetComponent<Stats>().GetHealth() / GameManager.gameManager.player.GetComponent<Stats>().GetMaxHealth(), GameManager.gameManager.player.GetComponent<Stats>().GetHealth() / GameManager.gameManager.player.GetComponent<Stats>().GetMaxHealth(), 1);
        health.text = ""+GameManager.gameManager.player.GetComponent<Stats>().GetHealth();
    }
}
