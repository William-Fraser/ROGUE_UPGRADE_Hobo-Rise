using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private bool attacking = false;
    private float damage = 10;
    private SpriteRenderer sprite;
    private GameObject attacker;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void SetDamage(float value)
    {
        if (value <= 0)
        {
            Debug.LogWarning("WARNING: Damage is below zero!!");
            value = 0;
        }
        
        damage = value;
    }

    public void StartAttack(GameObject attacker)
    {
        this.attacker = attacker;
        attacking = true;
        sprite.sortingOrder = 10;
    }

    public void StopAttack()
    {
        attacking = false;
        sprite.sortingOrder = -1;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.gameManager.currentScene != GameManager.GameScenes.InGame)
            return;

        if (collision.gameObject.GetComponent<Stats>() != null && collision.gameObject != attacker)
        {
            PlayerData stats = GameManager.gameManager.GetPlayerStats();
            collision.gameObject.GetComponent<Stats>().LoseHealth((int)stats.damageModifier, true);
        }        
    }
}
