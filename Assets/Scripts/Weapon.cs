using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool playerWeapon = false;
    public bool attacking = false;
    private float damage = 10;

    public void SetDamage(float value)
    {
        if (value <= 0)
        {
            Debug.LogWarning("WARNING: Damage is below zero!!");
            if (playerWeapon)
                value = 10;
        }
            damage = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(playerWeapon == true)
         Debug.LogWarning(collision.gameObject +", Damage: " + damage);
        if (GameManager.gameManager.currentScene != GameManager.GameScenes.InGame)
            return;
        if (playerWeapon && !attacking)
            return;

        Debug.Log("attack hit");
        if (collision.gameObject.GetComponent<Stats>() != null)
        collision.gameObject.GetComponent<Stats>().health -= damage;

        if (playerWeapon == true && this.gameObject == GameManager.gameManager.player.GetComponent<PlayerController>().weapon)
            GameManager.gameManager.DamageAdded(damage, playerWeapon);
        
    }
}
