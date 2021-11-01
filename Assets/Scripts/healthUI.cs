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
        heart.rectTransform.localScale = new Vector3(GameManager.gameManager.player.GetComponent<Stats>().health / GameManager.gameManager.player.GetComponent<Stats>().maxHealth, GameManager.gameManager.player.GetComponent<Stats>().health / GameManager.gameManager.player.GetComponent<Stats>().maxHealth, 1);
        health.text = ""+GameManager.gameManager.player.GetComponent<Stats>().health;
    }
}
