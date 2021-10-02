using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string enemyTag = "Enemy";
    private int damage;

    public void SetDamage(int value)
    {
        if (value < 0)
            Debug.LogWarning("Player Damage is below zero!!");
        damage = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == enemyTag) ; // REMOVE SEMICOLON WHEN ENEMIES ARE DONE!
            //TAKE DAMAGE METHOD HERE
    }
}
