using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool playerWeapon = false;
    public bool attacking = false;
    public float damage = 10;
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void SetDamage(float value)
    {
        if (value <= 0)
        {
            Debug.LogWarning("WARNING: Damage is below zero!!");
            if (playerWeapon)
                value = 10;
        }
        
        damage = value;
        Debug.LogWarning($"Weapon Damage is {damage}");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(playerWeapon == true)
         Debug.LogWarning(collision.gameObject +", Damage: " + damage);
        if (GameManager.gameManager.currentScene != GameManager.GameScenes.InGame)
            return;

        //SetDamage(GameManager.gameManager.player.GetComponent<Stats>().damageModifier);

        if (!attacking)
        {
            sprite.sortingOrder = -1;
            return;
        }

        if (collision.gameObject.GetComponent<Stats>() != null)
        {
            sprite.sortingOrder = 10;
            Debug.LogWarning(collision.gameObject + " hit with damage of " + damage);
            collision.gameObject.GetComponent<Stats>().LoseHealth((int)damage, true);
        }

        if (playerWeapon == true && this.gameObject == GameManager.gameManager.player.GetComponent<PlayerController>().weapon)
            GameManager.gameManager.DamageAdded(damage, playerWeapon);

        
    }
}
