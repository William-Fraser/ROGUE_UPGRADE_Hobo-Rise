using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private int damage = 10;

    public void SetDamage(int value)
    {
        if (value < 0)
            Debug.LogWarning("WARNING: Damage is below zero!!");
        damage = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.gameManager.currentScene != GameManager.GameScenes.InGame)
            return;
        Debug.Log("attack hit");
        if (collision.gameObject.GetComponent<Stats>() != null)
        collision.gameObject.GetComponent<Stats>().health -= damage;
        GameManager.gameManager.DamageAdded(damage);
    }
}
