using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public bool alive = true;
    public int maxHealth;
    public int health;
    public float speedModifier;
    public int attackSpeed;
    public int maxEnergy;
    public float energy;
    public int displayedEnergy; // used to display in place of energy to show better looking scale
    public float collectedMoney; // money collected in a single run
    public float totalMoney; // total collected money in game

    private void Update()
    {
        if (health <= 0)
            Death();
    }

    #region Set, Obtain, and Lose resources
    public float CollectedMoney { set { collectedMoney = value; } }
    public void ObtainCoins(float value)
    {
        collectedMoney += value;
    }
    public void LoseCoins(float value)
    {
        collectedMoney -= value;
        if (collectedMoney < 0)
            collectedMoney = 0;
    }
    public int EnergyAmount { set { energy = value; } }
    public void ObtainEnergy(int value)
    {
        energy += value;
    }
    public void LoseEnergy(int value)
    {
        energy -= value;
        if (energy < 0)
            energy = 0;
    } 
    public int Health { set { health = value; } }
    public void ObtainHealth(int value)
    {
        health += value;
    }
    public void LoseHealth(int value)
    {
        health -= value;
        if (health < 0)
            health = 0;
    }
    #endregion

    private void Death()
    {
        alive = false;
        Color colour = GetComponent<SpriteRenderer>().color;
        colour.a = 0; 
        GetComponent<SpriteRenderer>().color = colour;
        if (GetComponent<Rigidbody2D>() != null)
        { transform.gameObject.SetActive(false); }
        else if (GetComponentInParent<Rigidbody2D>())
        { transform.parent.gameObject.SetActive(false); }
        
    }
}
